db.clubs.insertOne({
    Link: "your_generated_link",
    AdminId: "admin_id_value",
    IBAN: "iban_value",
    PaidTill: ISODate("2023-12-01T00:00:00Z"), // Set the date value accordingly
    FreeTrialTill: ISODate("2023-12-31T00:00:00Z"), // Set the date value accordingly
    Name: "club_name",
    Info: "club_info",
    Address: "club_address",
    ZipCode: "zip_code",
    ImagePath: "image_path",
    SocialHub: {
        // SocialHub properties, if any
    },
    ClubNews: [
        {
            Title: "news_title_1",
            Content: "news_content_1",
            Date: ISODate("2023-12-01T12:00:00Z") // Set the date value accordingly
        },
        {
            Title: "news_title_2",
            Content: "news_content_2",
            Date: ISODate("2023-12-02T12:00:00Z") // Set the date value accordingly
        }
        // Add more ClubNews items as needed
    ]
});
