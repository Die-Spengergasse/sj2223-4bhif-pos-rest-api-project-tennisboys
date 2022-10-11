# Tennis Buchungssystem
sj2223-4bhif-pos-rest-api-project-tennisboys created by GitHub Classroom

## Pages
```mermaid
graph TD
A[Website] --> B[LandingPage]
A --> C[LoginPage]
C --> D[RegisterPage]
A --> F[TermsPage]
A --> G[ContactPage]
B --> L[CreateClubPage]
A --> E[ClubPage /c/tceichgraben]
E --> H[BookingPage]
H --> I[BookingOverviewPage]
E --> J[DashboardPage]
A --> K[UserPage]
```

## ClubPage
```mermaid
graph TD
A[ClubPage] --> B[ClubInfo]
A --> C[ClubPictures]
A --> D[Courts]
A --> E[Trainers]
A --> F[ClubEvents]
A --> G[ClubNews]
A --> H[SocialHub]
H --> I[Facebook, Instagram, Twitter, Youtube, LinkedIn, Telephone, Email, Website]
```

## Courts
```mermaid
graph TD
A[Court] --> B[Name]
A --> C[Type]
A --> D[Price]
```

##Database
