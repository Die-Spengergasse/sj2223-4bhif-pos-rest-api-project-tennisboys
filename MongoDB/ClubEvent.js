db.ClubEvents.insertOne({
  "Name": "Event Name",
  "Time": ISODate("2022-01-01T00:00:00Z"),  // Passen Sie das Datum an
  "Info": "Event Info",
  "ClubNavigationId": ObjectId("yourClubObjectId")  // Ersetzen Sie durch die tatsächliche ObjectId des Clubs
})