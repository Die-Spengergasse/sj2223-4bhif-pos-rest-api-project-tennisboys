db.ClubNews.insertMany([
  {
    "Title": "News Title 1",
    "Info": "News Info 1",
    "ClubNavigationId": clubId,
    "Written": ISODate("2022-01-01T12:00:00Z")
  },
  {
    "Title": "News Title 2",
    "Info": "News Info 2",
    "ClubNavigationId": clubId,
    "Written": ISODate("2022-01-02T14:30:00Z")
  }
])