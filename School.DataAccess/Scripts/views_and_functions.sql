-- widok prezentujący wszystkie klasy
CREATE VIEW v_all_class
AS
    SELECT k.id,k.rok,CONCAT(n.imie,' ',n.nazwisko) AS imienazwiskonauczyciela
    FROM klasy k
    INNER JOIN nauczyciele n
    ON k.wychowawca_id = n.id;


-- widok prezentujący wszystkie przedmioty
CREATE VIEW v_all_subjects
AS
    SELECT p.id,p.nazwa_przedmiotu AS nazwaprzedmiotu,s.numer_sali AS numersali 
    FROM przedmioty p
    INNER JOIN sale s
    ON p.sala_id = s.id;

-- widok prezentujący bardziej szczegółowe informacje o przedmiotach
CREATE VIEW v_all_subjects_detailed
AS
    SELECT p.id,k.rok,CONCAT(n.imie,' ',n.nazwisko) AS imienazwiskonauczyciela,n.id AS nauczycielid,pp.nazwa_przedmiotu AS nazwaprzedmiotu
    FROM przedmiot_oddzial p
    INNER JOIN klasy k
    ON p.klasa_id = k.id
    INNER JOIN nauczyciele n
    ON p.nauczyciel_id = n.id
    INNER JOIN przedmioty pp
    ON p.przedmiot_id = pp.id;

-- widok prezentujący informacje o wszystkich uczniach
CREATE VIEW v_all_students
AS
    SELECT u.id,u.imie,u.nazwisko,k.rok FROM uczniowie u
    INNER JOIN klasy k
    ON u.klasa_id = k.id;

-- widok prezentujący obecności uczniów
CREATE VIEW v_student_attendance
AS
    SELECT u.imie,u.nazwisko,u.klasa_id,o.obecny,o.data FROM obecnosci o
    INNER JOIN uczniowie u
    ON o.uczen_id = u.id;

-- widok pobierajacy wszystkie srednie grupujac po id klasy, roku i nazwie pracy
CREATE VIEW v_class_avg
AS
    SELECT k.id,k.rok,uo.nazwa_pracy AS nazwapracy,AVG(o.ocena) AS sredniaocena 
    FROM klasy k
    INNER JOIN przedmiot_oddzial po
    ON k.id = po.klasa_id
    INNER JOIN uczen_oceny uo
    ON po.id = uo.przedmiot_oddzial_id
    INNER JOIN oceny o
    ON uo.id = o.uczen_ocena_id
    GROUP BY k.id,k.rok,uo.nazwa_pracy;

-- widok prezentujacy wszystkie oplaty
CREATE VIEW v_all_payments
AS
    SELECT o.id,ro.powod,u.imie,u.nazwisko,o.wartosc 
    FROM oplaty o
    INNER JOIN rodzaje_oplat ro
    ON o.rodzaj_id = ro.id
    INNER JOIN uczniowie u
    ON o.uczen_id = u.id;

-- widok prezentujacy plan zajec
CREATE VIEW v_schedule
AS
    SELECT p.id,p.termin_od AS terminod,p.termin_do AS termindo,pp.nazwa_przedmiotu AS przedmiot,k.rok AS rok
    FROM plan_zajec p
    INNER JOIN przedmiot_oddzial po
    ON p.przedmiot_oddzial_id = po.id
    INNER JOIN przedmioty pp
    ON po.przedmiot_id = pp.id
    INNER JOIN klasy k
    ON po.klasa_id = k.id;

-- widok dla frekwencji
CREATE VIEW v_Attendance 
AS 
    SELECT o.id,u.imie,u.nazwisko,o.data,o.obecny FROM obecnosci o
    INNER JOIN uczniowie u
    ON o.uczen_id = u.id;

-- widok dla prac ucznia
CREATE VIEW v_user_grade AS 
    SELECT uo.id, CONCAT(u.imie,' ',u.nazwisko) as ImieNazwiskoUcznia,CONCAT(p.nazwa_przedmiotu,'- ',po.klasa_id) as NazwaPrzedmiotu,uo.nazwa_pracy FROM uczen_oceny uo
    INNER JOIN uczniowie u
    ON uo.uczen_id = u.id
    INNER JOIN przedmiot_oddzial po
    ON uo.przedmiot_oddzial_id = po.id
    INNER JOIN przedmioty p 
    ON po.przedmiot_id = p.id;

-- widok prezentujacy wszyskie przedmioty w oddzialach
CREATE VIEW v_subject_class AS
    SELECT k.rok,CONCAT(n.imie, ' ',n.nazwisko) AS nauczyciel,p.nazwa_przedmiotu AS nazwaprzedmiotu FROM przedmiot_oddzial po
    INNER JOIN klasy k
    ON po.klasa_id = k.id
    INNER JOIN nauczyciele n
    ON po.nauczyciel_id = n.id
    INNER JOIN przedmioty p
    ON po.przedmiot_id = p.id;


-- funkcja zwracająca obecności per klasa
CREATE OR REPLACE FUNCTION fn_get_attendance_per_class (classId INT)
RETURNS TABLE(imie VARCHAR(50),nazwisko VARCHAR(50),rok BIGINT,obecny BOOLEAN,data DATE)
AS $$
BEGIN
    RETURN QUERY SELECT u.imie,u.nazwisko,k.rok,o.obecny,o.data FROM klasy k
    INNER JOIN uczniowie u
    ON k.id = u.klasa_id
    INNER JOIN obecnosci o
    ON u.id = o.uczen_id
    WHERE k.id = classId;
END; $$ LANGUAGE 'plpgsql';

-- funkcja zwracajac oceny, pobieramy nazwe pracy oraz klase
CREATE OR REPLACE FUNCTION fn_get_grades (classId INT,np TEXT)
RETURNS TABLE(ocena INT,uoid BIGINT,nazwapracy VARCHAR(255),uczenid BIGINT,klasaid BIGINT)
AS $$
BEGIN
    RETURN QUERY SELECT MAX(o.ocena) AS ocena, o.uczen_ocena_id AS uoid,uo.nazwa_pracy AS nazwapracy,uo.uczen_id AS uczenid,k.id AS klasaid
    FROM oceny o
    INNER JOIN uczen_oceny uo 
    ON o.uczen_ocena_id = uo.id
    INNER JOIN uczniowie u
    ON uo.uczen_id = u.id
    INNER JOIN klasy k
    ON u.klasa_id = k.id
    GROUP BY o.uczen_ocena_id,uo.nazwa_pracy,uo.uczen_id,k.id
    HAVING (classId != -1 AND k.id = classId) OR (uo.nazwa_pracy != '-1' AND uo.nazwa_pracy LIKE np);
END; $$ LANGUAGE 'plpgsql';

-- funkcja pobierajaca wszystkie oceny ucznia
CREATE OR REPLACE FUNCTION fn_get_student_grades(studentID BIGINT)
RETURNS TABLE(ocena INT,nazwapracy VARCHAR(255),uczenid BIGINT,przedmiotoddzialid BIGINT)
AS $$
BEGIN
    RETURN QUERY (SELECT o.ocena,uo.nazwa_pracy AS nazwapracy,uo.uczen_id AS uczenid,uo.przedmiot_oddzial_id AS przedmiotoddzialid 
    FROM oceny o
    INNER JOIN uczen_oceny uo
    ON o.uczen_ocena_id = uo.id
    WHERE uo.uczen_id = studentID);
END; $$ LANGUAGE 'plpgsql';

--funkcja wstawiająca przedmiot do tabeli przedmioty
CREATE OR REPLACE FUNCTION fn_insert_subject(np VARCHAR(50),ns VARCHAR(50)) RETURNS INTEGER AS $$
        DECLARE
            id_sali INTEGER;
        BEGIN
            SELECT s.id INTO id_sali FROM sale s WHERE s.numer_sali = ns;

            INSERT INTO przedmioty(nazwa_przedmiotu,sala_id) VALUES(np,id_sali);
            RETURN -1;
        END;
$$ LANGUAGE 'plpgsql';  