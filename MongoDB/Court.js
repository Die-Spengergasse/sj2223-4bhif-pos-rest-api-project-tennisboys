use yourDatabaseName  // Datenbanknamen anpassen

// Erstellen Sie eine Collection für Courts
db.createCollection("Courts")

// Fügen Sie Court-Dokumente hinzu und verweisen auf den Club
var clubId = db.Clubs.findOne({"ClubName": "Sample Club"})._id;  // Ersetzen Sie "Sample Club" durch den tatsächlichen Clubnamen
db.Courts.insertMany([
  {
    "Name": "Court 1",
    "Bookable": true,
    "Type": "Sand",
    "APrice": 10.0,
    "BPrice": null,
    "ATimeFrom": 8,
    "ATimeTill": 18,
    "AWeekendTimeTill": 16,
    "ClubNavigationId": clubId
  },
  {
    "Name": "Court 2",
    "Bookable": false,
    "Type": "Grass",
    "APrice": 15.0,
    "BPrice": 20.0,
    "ATimeFrom": 10,
    "ATimeTill": 20,
    "AWeekendTimeTill": 18,
    "ClubNavigationId": clubId
  }
])
