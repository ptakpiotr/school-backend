-- demonstracja funkcji, check i unique constraints oraz triggerow:

-- unique

INSERT INTO nauczyciele(imie,nazwisko) VALUES('Berny','Leaver');
-- poprawne wstawienie
INSERT INTO nauczyciele(imie,nazwisko) VALUES('Marta','Nowak');

INSERT INTO przedmiot_oddzial(klasa_id,nauczyciel_id,przedmiot_id) VALUES(1,1,7);
-- poprawne wstawienie
INSERT INTO przedmiot_oddzial(klasa_id,nauczyciel_id,przedmiot_id) VALUES(1,7,7);

INSERT INTO sale(numer_sali) VALUES('04');
-- poprawne wstawienie
INSERT INTO sale(numer_sali) VALUES('05');

INSERT INTO rodzaje_oplat(powod) VALUES('basen');
--poprawne wstawienie
INSERT INTO rodzaje_oplat(powod) VALUES('testowa wplata');

-- check

INSERT INTO oceny(ocena,uczen_ocena_id) VALUES(7,1);
--poprawne wstawienie
INSERT INTO oceny(ocena,uczen_ocena_id) VALUES(2,1);

-- funkcje
SELECT * FROM fn_frekwencja_per_klasa(2);

SELECT * FROM fn_pobierz_oceny(3,'-1');
SELECT * FROM fn_pobierz_oceny(-1,'Hasteola suaveolens (L.) Pojark.');

SELECT * FROM fn_pobierz_oceny_ucznia(2);

--niepoprawne wywolanie (sale nie istnieje)
SELECT * FROM fn_wstaw_przedmiot('Przedmiot testowy','22');
--poprawne wywolanie
SELECT * FROM fn_wstaw_przedmiot('Przedmiot testowy','04');

-- triggery

-- trigger przenoszacy osobe do klasy wyzej jesli osoba o tym samym imieniu i nazwisku juz w danej klasie istnieje
INSERT INTO uczniowie(imie,nazwisko,klasa_id) VALUES('Jenna','Halvosen',6);
SELECT * FROM uczniowie WHERE imie = 'Jenna' AND nazwisko = 'Halvosen';

-- trigger upewniajacy sie ze WF nie moze byc ani we wtorek ani w srode
INSERT INTO plan_zajec(przedmiot_oddzial_id,termin_od,termin_do) VALUES(9,'2023-01-24','2023-01-24');
--poprawne
INSERT INTO plan_zajec(przedmiot_oddzial_id,termin_od,termin_do) VALUES(9,'2023-01-25','2023-01-25');

--trigger blokujacy dodawanie przedmiotu w terminie wczesniejszym niz dzis do planu
INSERT INTO plan_zajec(przedmiot_oddzial_id,termin_od,termin_do) VALUES(9,'2023-01-21','2023-01-22');

--trigger blokujacy mozliwosc dodania frekwencji w wakacje
INSERT INTO obecnosci(uczen_id,obecny,data) VALUES(33,'F','2022-08-11');
--ale dla marca juz tak
INSERT INTO obecnosci(uczen_id,obecny,data) VALUES(33,'F','2022-03-11');

--trigger blokujacy poprawianie ocen w nieskonczonosc (2 per praca dopuszczalne)
INSERT INTO oceny(ocena,uczen_ocena_id) VALUES(5,1);
--tutaj zostanie wyrzucony blad
INSERT INTO oceny(ocena,uczen_ocena_id) VALUES(4,1);
--dla innej pracy dziala
INSERT INTO oceny(ocena,uczen_ocena_id) VALUES(4,2);