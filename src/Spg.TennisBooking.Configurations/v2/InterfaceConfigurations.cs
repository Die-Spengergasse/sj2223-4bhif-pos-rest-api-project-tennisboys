﻿using Microsoft.Extensions.DependencyInjection;
using Spg.TennisBooking.Application.Services.v2;
using Spg.TennisBooking.Domain.Interfaces;
using Spg.TennisBooking.Repository.Repositories.v2;

namespace Spg.TennisBooking.Configurations.v2
{
    public static class InterfaceConfigurations
    {
        public static void ConfigureInterfaces(this IServiceCollection services)
        {
            //Auth
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IAuthRepository, AuthRepository>();
            //Club
            services.AddTransient<IClubService, ClubService>();
            services.AddTransient<IClubRepository, ClubRepository>();
            //ClubEvent
            services.AddTransient<IClubEventService, ClubEventService>();
            services.AddTransient<IClubEventRepository, ClubEventRepository>();
            //ClubNews
            services.AddTransient<IClubNewsService, ClubNewsService>();
            services.AddTransient<IClubNewsRepository, ClubNewsRepository>();
            //Court
            services.AddTransient<ICourtService, CourtService>();
            services.AddTransient<ICourtRepository, CourtRepository>();
            //Reservation
            services.AddTransient<IReservationService, ReservationService>();
            services.AddTransient<IReservationRepository, ReservationRepository>();
            //User
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IUserRepository, UserRepository>();
            //SocialHub
            services.AddTransient<ISocialHubRepository, SocialHubRepository>();
        }
    }
}