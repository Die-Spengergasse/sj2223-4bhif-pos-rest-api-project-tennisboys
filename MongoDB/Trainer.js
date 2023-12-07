db.Trainers.insertMany([
  {
    "FirstName": "John",
    "LastName": "Doe",
    "Gender": "Male",
    "Info": "Trainer Info 1",
    "TrainingTime": 10,
    "ImagePath": "/images/johndoe.jpg",
    "ClubNavigationId": clubId
  },
  {
    "FirstName": "Jane",
    "LastName": "Smith",
    "Gender": "Female",
    "Info": "Trainer Info 2",
    "TrainingTime": 15,
    "ImagePath": "/images/janesmith.jpg",
    "ClubNavigationId": clubId
  }
])