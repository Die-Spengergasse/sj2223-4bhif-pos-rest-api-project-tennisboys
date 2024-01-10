# DBI

## Team Members

- Adrian Schauer
- Fabian Lasser

## Aufgabenstellung

Schritt 1:
Auswahl eines bestehenden rel. Projektes mit skalierbarer seed-Integration Teamarbeit: jeder sucht ein vorhandenes Projekt raus, ich erhalte einen Screenshot vom Datenbankmodell und eine ungefähre Beschreibung des Seedings sowie ein Screenshot des Frontends. Wichtig ist, dass man dieses quasi beliebig skalieren kann. Also zwischen 10 und 100.000 Testfälle für writing-Operationen durchführen kann.

- Tennisplatzverwaltung

Schritt 2:
Nach Auswahl des relationalen Referenzprojektes Implementierung mit einer MongoDB-Schnittstelle der Wahl in der Variante "Optimiert auf Frontend" Musterprojekte für C# und Java sind zu finden unter <https://github.com/schletz/Dbi3Sem/tree/master/13_NoSQL/Uebungen/SalesDb/SalesDbGenerator> *1 Punkt für Modell* 1 Punkt für lauffähige Implementierung

- Model ist im Ordner "src\Spg.TennisBooking.Domain\ModelMongo" zu finden und lauffähig

Schritt 3:
Testen der CRUD-Operations sowohl auf json-DB als auch auf relationale DB mit Laufzeiten. Also zuerst *Writings in verschiedenen Skalierungen (100 - 1000 - 100000) ,* 4 Finds (ohne Filter, mit Filter, mit Filter und Projektion, mit Filter, Projektion und Sortierung) *1 Update* 1 Delete alles inkl. Vergleich auf die Relationale DB, dh das Programm kann sowohl relational als auch json-based speichern und Tests hintereinander ausführen. 2 Punkte für gesamten Schritt 3

## Screenshot einiger Datensätze von der MongoDB

![Data](data.png)

```json
[{"Id":{"Timestamp":1704922217,"Machine":12650868,"Pid":21300,"Increment":1615421,"CreationTime":"2024-01-10T21:30:17Z"},"Link":"73c76140-b5bc-4ae3-adf4-c3696f0e8957","Admin":{"Id":{"Timestamp":1704922217,"Machine":12650868,"Pid":21300,"Increment":1615420,"CreationTime":"2024-01-10T21:30:17Z"},"UUID":"cf13fb24-d644-4772-ae56-7a4f89f1cfc2","Email":"adrian.schauer@aon.at","Password":"AdminPswd","VerificationCode":"012345","Verified":false,"ResetCode":"","ResetCodeExpires":"2024-01-10T21:30:17.02Z","FirstName":"","LastName":"","Gender":3,"PhoneNumber":null,"BirthDate":null,"RegistrationDate":"2024-01-10T21:30:17.02Z","Welcomed":false,"Reservations":[]},"IBAN":"","PaidTill":null,"FreeTrialTill":"2024-02-09T21:30:17.02Z","Name":"TC Eichgraben","Info":"","Address":"","ZipCode":"","ImagePath":"","SocialHub":{"Id":{"Timestamp":1704922217,"Machine":12650868,"Pid":21300,"Increment":1615422,"CreationTime":"2024-01-10T21:30:17Z"},"Facebook":"","Instagram":"","Twitter":"","Youtube":"","LinkedIn":"","Telephone":"","Email":"","Website":""},"ClubNews":[],"ClubEvents":[],"Courts":[],"Trainers":[]},{"Id":{"Timestamp":1704922217,"Machine":12650868,"Pid":21300,"Increment":1615424,"CreationTime":"2024-01-10T21:30:17Z"},"Link":"a23da680-5a57-4fd4-be86-dd164336f9bc","Admin":{"Id":{"Timestamp":1704922217,"Machine":12650868,"Pid":21300,"Increment":1615423,"CreationTime":"2024-01-10T21:30:17Z"},"UUID":"deeef760-7d90-4117-b973-63fcfedce633","Email":"adrian.schauer@aon.at","Password":"AdminPswd","VerificationCode":"012345","Verified":false,"ResetCode":"","ResetCodeExpires":"2024-01-10T21:30:17.022Z","FirstName":"","LastName":"","Gender":3,"PhoneNumber":null,"BirthDate":null,"RegistrationDate":"2024-01-10T21:30:17.022Z","Welcomed":false,"Reservations":[]},"IBAN":"","PaidTill":null,"FreeTrialTill":"2024-02-09T21:30:17.022Z","Name":"TC Eichgraben","Info":"","Address":"","ZipCode":"","ImagePath":"","SocialHub":{"Id":{"Timestamp":1704922217,"Machine":12650868,"Pid":21300,"Increment":1615425,"CreationTime":"2024-01-10T21:30:17Z"},"Facebook":"","Instagram":"","Twitter":"","Youtube":"","LinkedIn":"","Telephone":"","Email":"","Website":""},"ClubNews":[],"ClubEvents":[],"Courts":[],"Trainers":[]},{"Id":{"Timestamp":1704922217,"Machine":12650868,"Pid":21300,"Increment":1615427,"CreationTime":"2024-01-10T21:30:17Z"},"Link":"ca79f431-bb58-47cd-b98a-1ded01fa37d6","Admin":{"Id":{"Timestamp":1704922217,"Machine":12650868,"Pid":21300,"Increment":1615426,"CreationTime":"2024-01-10T21:30:17Z"},"UUID":"cc484f98-97b4-4187-b249-38553298200f","Email":"adrian.schauer@aon.at","Password":"AdminPswd","VerificationCode":"012345","Verified":false,"ResetCode":"","ResetCodeExpires":"2024-01-10T21:30:17.024Z","FirstName":"","LastName":"","Gender":3,"PhoneNumber":null,"BirthDate":null,"RegistrationDate":"2024-01-10T21:30:17.024Z","Welcomed":false,"Reservations":[]},"IBAN":"","PaidTill":null,"FreeTrialTill":"2024-02-09T21:30:17.024Z","Name":"TC Eichgraben","Info":"","Address":"","ZipCode":"","ImagePath":"","SocialHub":{"Id":{"Timestamp":1704922217,"Machine":12650868,"Pid":21300,"Increment":1615428,"CreationTime":"2024-01-10T21:30:17Z"},"Facebook":"","Instagram":"","Twitter":"","Youtube":"","LinkedIn":"","Telephone":"","Email":"","Website":""},"ClubNews":[],"ClubEvents":[],"Courts":[],"Trainers":[]},{"Id":{"Timestamp":1704922217,"Machine":12650868,"Pid":21300,"Increment":1615430,"CreationTime":"2024-01-10T21:30:17Z"},"Link":"e2771def-1e8b-4714-bbc2-c74e262d18b1","Admin":{"Id":{"Timestamp":1704922217,"Machine":12650868,"Pid":21300,"Increment":1615429,"CreationTime":"2024-01-10T21:30:17Z"},"UUID":"aefcd4ec-597f-4305-8a67-a595003c1f0d","Email":"adrian.schauer@aon.at","Password":"AdminPswd","VerificationCode":"012345","Verified":false,"ResetCode":"","ResetCodeExpires":"2024-01-10T21:30:17.026Z","FirstName":"","LastName":"","Gender":3,"PhoneNumber":null,"BirthDate":null,"RegistrationDate":"2024-01-10T21:30:17.026Z","Welcomed":false,"Reservations":[]},"IBAN":"","PaidTill":null,"FreeTrialTill":"2024-02-09T21:30:17.026Z","Name":"TC Eichgraben","Info":"","Address":"","ZipCode":"","ImagePath":"","SocialHub":{"Id":{"Timestamp":1704922217,"Machine":12650868,"Pid":21300,"Increment":1615431,"CreationTime":"2024-01-10T21:30:17Z"},"Facebook":"","Instagram":"","Twitter":"","Youtube":"","LinkedIn":"","Telephone":"","Email":"","Website":""},"ClubNews":[],"ClubEvents":[],"Courts":[],"Trainers":[]},{"Id":{"Timestamp":1704922217,"Machine":12650868,"Pid":21300,"Increment":1615433,"CreationTime":"2024-01-10T21:30:17Z"},"Link":"d4a095b0-9dd1-4d35-98fc-f3f59c03c7e7","Admin":{"Id":{"Timestamp":1704922217,"Machine":12650868,"Pid":21300,"Increment":1615432,"CreationTime":"2024-01-10T21:30:17Z"},"UUID":"c86fc2ed-ee85-4ab4-b35f-be78c8af92c0","Email":"adrian.schauer@aon.at","Password":"AdminPswd","VerificationCode":"012345","Verified":false,"ResetCode":"","ResetCodeExpires":"2024-01-10T21:30:17.028Z","FirstName":"","LastName":"","Gender":3,"PhoneNumber":null,"BirthDate":null,"RegistrationDate":"2024-01-10T21:30:17.028Z","Welcomed":false,"Reservations":[]},"IBAN":"","PaidTill":null,"FreeTrialTill":"2024-02-09T21:30:17.028Z","Name":"TC Eichgraben","Info":"","Address":"","ZipCode":"","ImagePath":"","SocialHub":{"Id":{"Timestamp":1704922217,"Machine":12650868,"Pid":21300,"Increment":1615434,"CreationTime":"2024-01-10T21:30:17Z"},"Facebook":"","Instagram":"","Twitter":"","Youtube":"","LinkedIn":"","Telephone":"","Email":"","Website":""},"ClubNews":[],"ClubEvents":[],"Courts":[],"Trainers":[]}]
```

## Screenshot vom Laufzeitvergleich (Consolen-print)

### SQL

![sql](sql.png)

### MongoDB

![mongodb](mongodb.png)

### Notiz

SQLite wurde verwendet, daher ist es hier um einiges schneller als bei der MongoDB, da diese keine "echte" Datenbank ist, sondern nur eine Datei und auch somit keine Abfrage auf den localhost gemacht werden muss.

## Bonus

Änderung der Abfrage, sodass eine Aggregation notwendig wird --> Vergleich der Read-Laufzeiten zum selben Query auf der Relationalen. 0.5 Punkte

veränderte Version des Modells, bei dem mit referencing gearbeitet wird und Vergleich der Laufzeiten 0,5 Punkte

- Von sich aus schon Referenzierung verwendet

Umsetzung auf Atlas-Cloud (inkl. Laufzeitvergleiche, also beide DBs auf einer Cloud laufen lassen) 1Punkt

- **Kein Geld dafür**

funktionales Frontend mit Auswahlmöglichkeit der Anzeige (=Filter auf Abfrage) 1.5 Punkte

- Extrem schönes Frontend aber ohne Filter die sind hardcoded bereits mitgegeben und können nur im code geändert werden, da es bereits ein eigenes Projekt war und der MongoDB Teil nur zusätzlich implementiert wurde und im nachhinein deppert das alles mit schönen Filtern zu versehen.
- Frontend ist im Ordner "src\Spg.TennisBooking.MvcFrontend" zu finden unter link /benchmark

Vergleich der Laufzeiten beim Setzen eines Index auf die Mongo-Struktur 1.0 Punkte

- Index wurde gesetzt auf Attribut "Link"

Irgendwas cooles, das ich hier nicht aufzähle 1 Punkt

- Streams, usefull when working on sockets to detect changes on the mongoDB
