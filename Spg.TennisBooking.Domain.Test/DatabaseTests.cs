﻿using Microsoft.EntityFrameworkCore;
using Spg.TennisBooking.Domain.Model;
using Spg.TennisBooking.Infrastructure;
using System;
using System.Linq;
using Xunit;

namespace Spg.TennisBooking.Domain.Test
{
    public class DatabaseTests
    {
        private TennisBookingContext GetContext()
        {
            DbContextOptions options = new DbContextOptionsBuilder()
            .UseSqlite("Data Source=TennisBookingTest.db")
            .Options;

            TennisBookingContext db = new TennisBookingContext(options);
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            return db;
        }

        [Fact]
        public void DomainModel_Create_Product_Success_Test()
        {
            TennisBookingContext db = GetContext();
            
            Product newProduct = new Product()
            {
                DeliveryDate = DateTime.Now,
                Ean13 = "132456789123",
                ExpiaryDate = DateTime.Now,
                Guid = Guid.NewGuid(),
                Name = "Test Product",
                Price = 20.90M,
                Stock = 10
            };

            db.Products.Add(newProduct);
            db.SaveChanges();

            Assert.Equal(1, db.Products.Count());
        }

        [Fact]
        public void DomainModel_Create_Customer_Success_Test()
        {
            TennisBookingContext db = GetContext();

            Customer newCustomer = new Customer()
            {
                EMail = "xy@gmail.at",
                FirstName="TestFirstName",
                LastName="TestLastName",
                Gender=GenderTypes.MALE,
                Guid=Guid.NewGuid(),
                RegistrationDateTime=DateTime.Now,
            };

            db.Customers.Add(newCustomer);
            db.SaveChanges();

            Assert.Equal(1, db.Customers.Count());
        }

        [Fact]
        public void DomainModel_AddShoppingCartToCustomer_Success_Test()
        {
            TennisBookingContext db = GetContext();

            Customer newCustomer = new Customer()
            {
                EMail = "xy@gmail.at",
                FirstName = "TestFirstName",
                LastName = "TestLastName",
                Gender = GenderTypes.MALE,
                Guid = Guid.NewGuid(),
                RegistrationDateTime = DateTime.Now,
            };
            db.Customers.Add(newCustomer);

            ShoppingCart newShoppingCart = new ShoppingCart(States.ACTIVE, Guid.NewGuid())
            {
            };

            newCustomer.AddShoppingCart(newShoppingCart);

            db.SaveChanges();

            Assert.Equal(1, db.Customers.Count());
            Assert.Equal(1, db.ShoppingCarts.Count());
        }

        [Fact]
        public void DomainModel_AddShoppingCartToCustomer_Navigation_Success_Test()
        {
            TennisBookingContext db = GetContext();

            Customer newCustomer = new Customer()
            {
                EMail = "xy@gmail.at",
                FirstName = "TestFirstName",
                LastName = "TestLastName",
                Gender = GenderTypes.MALE,
                Guid = Guid.NewGuid(),
                RegistrationDateTime = DateTime.Now,
            };
            db.Customers.Add(newCustomer);

            ShoppingCart newShoppingCart = new ShoppingCart(States.ACTIVE, Guid.NewGuid())
            {
                CustomerNavigation = newCustomer,
            };

            db.SaveChanges();

            Assert.Equal(1, db.Customers.Count());
            Assert.Equal(1, db.ShoppingCarts.Count());
        }
    }
}