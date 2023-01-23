CREATE TABLE nauczyciele( 
    id BIGSERIAL PRIMARY KEY,
    imie VARCHAR(50) NOT NULL,
    nazwisko VARCHAR(50) NOT NULL
);

CREATE TABLE przedmioty( 
    id BIGSERIAL PRIMARY KEY,
    nazwa_przedmiotu VARCHAR(50) NOT NULL,
    sala_id BIGINT NOT NULL
);

CREATE TABLE uczniowie( 
    id BIGSERIAL PRIMARY KEY,
    imie VARCHAR(50) NOT NULL,
    nazwisko VARCHAR(50) NOT NULL,
    klasa_id BIGINT NOT NULL
);

CREATE TABLE klasy( 
    id BIGSERIAL PRIMARY KEY,
    rok BIGINT NOT NULL,
    wychowawca_id BIGINT UNIQUE NOT NULL
);

CREATE TABLE obecnosci( 
    id BIGSERIAL PRIMARY KEY,
    uczen_id BIGINT NOT NULL,
    obecny BOOLEAN NOT NULL,
    data DATE NOT NULL
);

CREATE TABLE uczen_oceny( 
    id BIGSERIAL PRIMARY KEY,
    uczen_id BIGINT NOT NULL,
    przedmiot_oddzial_id BIGINT NOT NULL,
    nazwa_pracy VARCHAR(255) NOT NULL
);

CREATE TABLE oplaty( 
    id BIGSERIAL PRIMARY KEY,
    rodzaj_id BIGINT NOT NULL,
    uczen_id BIGINT NOT NULL,
    wartosc DECIMAL(8, 2) NOT NULL
);

CREATE TABLE plan_zajec( 
    id BIGSERIAL PRIMARY KEY,
    przedmiot_oddzial_id BIGINT NOT NULL,
    termin_od TIMESTAMP,
    termin_do TIMESTAMP
);

CREATE TABLE sale( 
    id BIGSERIAL PRIMARY KEY,
    numer_sali VARCHAR(255) NOT NULL
);

CREATE TABLE rodzaje_oplat( 
    id BIGSERIAL PRIMARY KEY,
    powod VARCHAR(255) NOT NULL
);

CREATE TABLE oceny( 
    id BIGSERIAL PRIMARY KEY,
    ocena INT NOT NULL,
    uczen_ocena_id BIGINT NOT NULL
);


CREATE TABLE przedmiot_oddzial( 
    id BIGSERIAL PRIMARY KEY,
    klasa_id BIGINT NOT NULL,
    nauczyciel_id BIGINT NOT NULL,
    przedmiot_id BIGINT NOT NULL
);

--tabela nieujeta w ERD - dotyczy uzytkownikow aplikacji
CREATE TABLE users(
    id BIGSERIAL PRIMARY KEY,
    email VARCHAR(50) NOT NULL,
    password TEXT NOT NULL
);

ALTER TABLE 
    users ADD CONSTRAINT ck_users_email CHECK(email LIKE '%@%.%');

--foreign key constraints
ALTER TABLE
    przedmiot_oddzial ADD CONSTRAINT fk_przedmiot_oddzial_przedmiot_id FOREIGN KEY(przedmiot_id) REFERENCES przedmioty(id);
ALTER TABLE
    obecnosci ADD CONSTRAINT fk_obecnosci_uczen_id FOREIGN KEY(uczen_id) REFERENCES uczniowie(id);
ALTER TABLE
    oplaty ADD CONSTRAINT fk_oplaty_uczen_id FOREIGN KEY(uczen_id) REFERENCES uczniowie(id);
ALTER TABLE
    uczen_oceny ADD CONSTRAINT fk_uczen_oceny_uczen_id FOREIGN KEY(uczen_id) REFERENCES uczniowie(id);
ALTER TABLE
    uczniowie ADD CONSTRAINT fk_uczniowie_klasa_id FOREIGN KEY(klasa_id) REFERENCES klasy(id);
ALTER TABLE
    klasy ADD CONSTRAINT fk_klasy_wychowawca_id FOREIGN KEY(wychowawca_id) REFERENCES nauczyciele(id);
ALTER TABLE
    przedmiot_oddzial ADD CONSTRAINT fk_przedmiot_oddzial_nauczyciel_id FOREIGN KEY(nauczyciel_id) REFERENCES nauczyciele(id);
ALTER TABLE
    plan_zajec ADD CONSTRAINT fk_plan_zajec_przedmiot_oddzial_id FOREIGN KEY(przedmiot_oddzial_id) REFERENCES przedmiot_oddzial(id);
ALTER TABLE
    przedmioty ADD CONSTRAINT fk_przedmioty_sala_id FOREIGN KEY(sala_id) REFERENCES sale(id);
ALTER TABLE
    oplaty ADD CONSTRAINT fk_oplaty_rodzaj_id FOREIGN KEY(rodzaj_id) REFERENCES rodzaje_oplat(id);
ALTER TABLE
    oceny ADD CONSTRAINT fk_oceny_uczen_ocena_id FOREIGN KEY(uczen_ocena_id) REFERENCES uczen_oceny(id);
ALTER TABLE
    uczen_oceny ADD CONSTRAINT fk_uczen_oceny_przedmiot_oddzial_id FOREIGN KEY(przedmiot_oddzial_id) REFERENCES przedmiot_oddzial(id);
ALTER TABLE
    przedmiot_oddzial ADD CONSTRAINT fk_przedmiot_oddzial_klasa_id FOREIGN KEY(klasa_id) REFERENCES klasy(id);

--check constraints
ALTER TABLE 
    oceny ADD CONSTRAINT ck_oceny_ocena CHECK(ocena >= 1 AND ocena <= 6);
ALTER TABLE
    klasy ADD CONSTRAINT ck_rok CHECK(rok >= 1 AND rok <= 8);

--unique constraints
ALTER TABLE 
    nauczyciele ADD CONSTRAINT uq_nauczyciele_imie_nazwisko UNIQUE(imie,nazwisko);

ALTER TABLE 
    przedmiot_oddzial ADD CONSTRAINT uq_przedmiot_oddzial_klasa_nauczyciel UNIQUE(klasa_id,nauczyciel_id);

ALTER TABLE 
    sale ADD CONSTRAINT uq_sale_numer_sali UNIQUE(numer_sali);

ALTER TABLE 
    rodzaje_oplat ADD CONSTRAINT uq_rodzaje_oplat_powod UNIQUE(powod);    

--default values
ALTER TABLE
    plan_zajec ALTER COLUMN termin_od SET DEFAULT NOW(); 

ALTER TABLE
    plan_zajec ALTER COLUMN termin_do SET DEFAULT NOW() + INTERVAL '45 minutes'; 

ALTER TABLE 
    obecnosci ALTER COLUMN data SET DEFAULT NOW();

--trigger który działa w następujący sposób, gdy okaże się, że osoba o podanym imieniu i nazwisku już istnieje w jakiejś klasie
--to jeśli jest jakaś kolejna klasa to wówczas tam ląduje
CREATE OR REPLACE FUNCTION fn_tr_dodaj_osobe_do_klasy() RETURNS TRIGGER AS $$
        DECLARE 
            cl_id INTEGER;
            max_cl_id INTEGER;
        BEGIN
                IF EXISTS(SELECT 1 FROM uczniowie WHERE imie = NEW.imie AND nazwisko = NEW.nazwisko AND klasa_id = NEW.klasa_id) THEN
                        cl_id:=NEW.klasa_id+1;
                        SELECT MAX(klasa_id) INTO max_cl_id FROM uczniowie;
                        IF (cl_id > max_cl_id) THEN
                            RAISE EXCEPTION 'Nie ma mozliwosci dodanie ucznia do klasy';
                        ELSE
                            INSERT INTO uczniowie(imie,nazwisko,klasa_id) VALUES(NEW.imie,NEW.nazwisko,cl_id);
                            RETURN NULL;
                        END IF;
                ELSE
                        RETURN NEW;
                END IF;
        END;
$$ LANGUAGE 'plpgsql';  

CREATE TRIGGER tr_add_person_to_class BEFORE INSERT ON uczniowie FOR EACH ROW 
EXECUTE PROCEDURE fn_tr_dodaj_osobe_do_klasy();

--trigger który "upewnia się", że wf nie moze byc w środę ani we wtorek
CREATE OR REPLACE FUNCTION fn_tr_dodaj_wf_do_planu() RETURNS TRIGGER AS $$
        DECLARE 
            nazwa_przedmiotu VARCHAR(50);
            dow_termin_od INTEGER;
            dow_termin_do INTEGER;
        BEGIN
                SELECT p.nazwa_przedmiotu INTO nazwa_przedmiotu 
                FROM przedmioty p
                INNER JOIN przedmiot_oddzial po
                ON p.id = po.przedmiot_id
                INNER JOIN plan_zajec pz
                ON po.id = pz.przedmiot_oddzial_id
                WHERE pz.przedmiot_oddzial_id = NEW.przedmiot_oddzial_id;

                IF (nazwa_przedmiotu = 'Wychowanie fizyczne') THEN
                        SELECT EXTRACT(isodow  FROM NEW.termin_od) INTO dow_termin_od;
                        SELECT EXTRACT(isodow  FROM NEW.termin_do) INTO dow_termin_do;

                        IF(dow_termin_od != dow_termin_do OR dow_termin_od IN (1,2)) THEN
                            RAISE EXCEPTION 'Nie ma mozliwosci terminu zajec WF';
                            RETURN NULL;
                        ELSE
                            RETURN NEW;
                        END IF;
                ELSE
                        RETURN NEW;
                END IF;
        END;
$$ LANGUAGE 'plpgsql';  

--trigger, który blokuje dodwanie przedmiotu w planie zajęć dla terminu wcześniejszego niż dzień dzisiejszy
CREATE OR REPLACE FUNCTION fn_tr_dodaj_do_planu_pozniej() RETURNS TRIGGER AS $$
        BEGIN
            IF(NEW.termin_od::date < NOW()::date) THEN
                RAISE EXCEPTION 'Nie ma mozliwosci dodania zajec w terminie wczesniejszym niz dzien dzisiejszy';
                RETURN NULL;
            ELSE
                RETURN NEW;
            END IF;
        END;
$$ LANGUAGE 'plpgsql';  

CREATE TRIGGER tr_add_later_to_schedule BEFORE INSERT ON plan_zajec FOR EACH ROW 
EXECUTE PROCEDURE fn_tr_dodaj_do_planu_pozniej();
CREATE TRIGGER tr_add_pe_to_schedule BEFORE INSERT ON plan_zajec FOR EACH ROW 
EXECUTE PROCEDURE fn_tr_dodaj_wf_do_planu();

--blokowanie dodawania frekwencji w miesiacach wakacyjnych
CREATE OR REPLACE FUNCTION fn_tr_obecnosc_w_wakacje() RETURNS TRIGGER AS $$
        DECLARE
            month_num INTEGER;
        BEGIN
            SELECT EXTRACT(MONTH FROM NEW.data::DATE) INTO month_num;
            IF(month_num IN (7,8)) THEN
                RAISE EXCEPTION 'Nie ma dodania obecnosci w miesiacach wakacyjnych';
                RETURN NULL;
            ELSE
                RETURN NEW;
            END IF;
        END;
$$ LANGUAGE 'plpgsql';  

CREATE TRIGGER tr_cannot_add_attendance_during_holidays BEFORE INSERT ON obecnosci FOR EACH ROW 
EXECUTE PROCEDURE fn_tr_obecnosc_w_wakacje();

--blokowanie dodawania popraw ocen w nieskończoność (możliwa tylko 1 poprawka)
CREATE OR REPLACE FUNCTION fn_tr_dwie_oceny_per_test() RETURNS TRIGGER AS $$
        DECLARE
            test_grade_cnt INTEGER;
        BEGIN
            SELECT COUNT(*) INTO test_grade_cnt FROM oceny WHERE uczen_ocena_id = NEW.uczen_ocena_id;
            IF(test_grade_cnt = 2) THEN
                RAISE EXCEPTION 'Nie ma mozliwosci dodania kolejnej oceny';
                RETURN NULL;
            ELSE
                RETURN NEW;
            END IF;
        END;
$$ LANGUAGE 'plpgsql';  

CREATE TRIGGER tr_allow_only_two_grades_per_test BEFORE INSERT ON oceny FOR EACH ROW 
EXECUTE PROCEDURE fn_tr_dwie_oceny_per_test();