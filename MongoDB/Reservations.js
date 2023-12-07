use yourDatabaseName  // Datenbanknamen anpassen

// Erstellen Sie eine Collection für Reservations
db.createCollection("Reservations")

// Fügen Sie Reservation-Dokumente hinzu und verweisen auf Court, User und Club
var clubId = db.Clubs.findOne({"ClubName": "Sample Club"})._id;  // Ersetzen Sie "Sample Club" durch den tatsächlichen Clubnamen
var courtId = db.Courts.findOne({"Name": "Court 1"})._id;  // Ersetzen Sie "Court 1" durch den tatsächlichen Gerichtsnamen
var userId = db.Users.findOne({/* Geben Sie die Kriterien für den Benutzer an */})._id;  // Geben Sie die Kriterien für den Benutzer an

db.Reservations.insertMany([
  {
    "StartTime": ISODate("2022-01-01T10:00:00Z"),
    "EndTime": ISODate("2022-01-01T12:00:00Z"),
    "Comment": "Reservation Comment 1",
    "CourtNavigationId": courtId,
    "UserNavigationId": userId,
    "ClubNavigationId": clubId
  },
  {
    "StartTime": ISODate("2022-01-02T14:00:00Z"),
    "EndTime": ISODate("2022-01-02T16:00:00Z"),
    "Comment": "Reservation Comment 2",
    "CourtNavigationId": courtId,
    "UserNavigationId": userId,
    "ClubNavigationId": clubId
  }
])
