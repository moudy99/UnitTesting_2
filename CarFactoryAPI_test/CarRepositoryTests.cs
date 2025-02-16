﻿using CarAPI.Entities;
using CarFactoryAPI.Entities;
using CarFactoryAPI.Repositories_DAL;
using Moq;
using Moq.EntityFrameworkCore;

namespace CarFactoryAPI_test
{
    public class CarRepositoryTests
    {
        private Mock<FactoryContext> factoryContextMock;
        private CarRepository carRepository;
        public CarRepositoryTests()
        {
            // Create Mock of dependencies
            factoryContextMock = new Mock<FactoryContext>();

            // use fake object as dependency
            carRepository = new CarRepository(factoryContextMock.Object);
        }
        [Fact]
        [Trait("Author", "MOD")]
        [Trait("Priority", "9")]

        public void GetCarById_AskForCar10_ReturnCar()
        {
            // Arrange

            // Build the mock Data
            List<Car> cars = new List<Car>() { new Car() { Id = 10 } };

            // setup called DbSets
            factoryContextMock.Setup(fcm => fcm.Cars).ReturnsDbSet(cars);

            // Act 
            Car car = carRepository.GetCarById(10);

            // Aassert

            Assert.NotNull(car);
        }
    }
}
