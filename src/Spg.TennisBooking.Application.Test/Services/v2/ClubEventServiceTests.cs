using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Spg.TennisBooking.Domain.Dtos.ClubEventDtos;
using Spg.TennisBooking.Domain.Interfaces;
using Xunit;

namespace Spg.TennisBooking.Application.Test.Services.v2
{
    public class ClubEventServiceTests
    {
        [Fact]
        public async void GetClubEventTest()
        {
            //Arrange
            GetClubEventDto getClubEventDto = new GetClubEventDto(){
                //Füllt das Objekt mit Daten
                Id = 1,               
                Name = "TestEvent",

                Time = DateTime.UtcNow,

                Info = "TestInfo",

                ClubLink = "TestLink"
            };
            

            IActionResult Action = new OkObjectResult(getClubEventDto);
            
            //Mock
            var mockClubEventService = new Mock<IClubEventService>();
            mockClubEventService.Setup(x => x.Get(1)).Returns(Task.FromResult(Action));

            //Act
            var response = await mockClubEventService.Object.Get(1);

            //Assert
            Assert.Equal(Action, response);
        }

        [Fact]
        public async void GetAllClubEventTest()
        {
           //Arrange
           List<GetAllClubEventDto> getAllClubEventDto = new List<GetAllClubEventDto>(){
            new GetAllClubEventDto(){
                Id = 1,
                Name = "TestEvent",
                Time = DateTime.UtcNow
            },
            new GetAllClubEventDto(){
                Id = 2,
                Name = "TestEvent",
                Time = DateTime.UtcNow
            },
            new GetAllClubEventDto(){
                Id = 3,
                Name = "TestEvent",
                Time = DateTime.UtcNow
            }
           };

            IActionResult Action = new OkObjectResult(getAllClubEventDto);
            
            //Mock
            var mockClubEventService = new Mock<IClubEventService>();
            mockClubEventService.Setup(x => x.GetAll("EinClub")).Returns(Task.FromResult(Action));

            //Act
            var response = await mockClubEventService.Object.GetAll("EinClub");

            //Assert
            Assert.Equal(Action, response);
        }

        [Fact]
        public async void PostClubEventTest()
        {
            //Arrange
            
            //Mock

            //Act

            //Assert 
        }

        [Fact]
        public async void PutClubEventTest()
        {
            //Arrange
            
            //Mock

            //Act

            //Assert
        }

        [Fact]
        public async void DeleteClubEventTest()
        {
            
        }
    }
}