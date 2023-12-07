use yourDatabaseName  // Datenbanknamen anpassen

// Erstellen Sie eine Collection für Users
db.createCollection("Users")

// Fügen Sie User-Dokumente hinzu
db.Users.insertMany([
  {
    "Email": "john.doe@example.com",
    "Password": "hashedPassword1",  // Stellen Sie sicher, dass Sie das Passwort ordnungsgemäß hashen und speichern
    "VerificationCode": "verificationCode1",
    "Verified": true,
    "FirstName": "John",
    "LastName": "Doe",
    "Gender": "Male",
    "BirthDate": ISODate("1990-01-01"),
    "RegistrationDate": ISODate("2022-01-01"),
    "Welcomed": false
  },
  {
    "Email": "jane.smith@example.com",
    "Password": "hashedPassword2",  // Stellen Sie sicher, dass Sie das Passwort ordnungsgemäß hashen und speichern
    "VerificationCode": "verificationCode2",
    "Verified": true,
    "FirstName": "Jane",
    "LastName": "Smith",
    "Gender": "Female",
    "BirthDate": ISODate("1985-02-15"),
    "RegistrationDate": ISODate("2022-01-02"),
    "Welcomed": true
  }
])
