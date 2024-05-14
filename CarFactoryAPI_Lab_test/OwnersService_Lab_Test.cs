using CarAPI.Entities;
using CarAPI.Models;
using CarAPI.Payment;
using CarAPI.Repositories_DAL;
using CarAPI.Services_BLL;
using Moq;
using Xunit;
using Xunit.Abstractions;

namespace CarFactoryAPI_Lab_test
{
    public class OwnersService_Lab_Test : IDisposable
    {
        private readonly ITestOutputHelper outputHelper;
        // Create Mock of the dependencies
        Mock<ICarsRepository> carRepoMock;
        Mock<IOwnersRepository> ownersRepoMock;
        Mock<ICashService> cashServiceMock;

        // use the fake object as dependency
        OwnersService ownersService;

        public OwnersService_Lab_Test(ITestOutputHelper outputHelper)
        {
            this.outputHelper = outputHelper;
            // Test Start up
            outputHelper.WriteLine("Test start up");

            // Create Mock of the dependencies
            carRepoMock = new();
            ownersRepoMock = new();
            cashServiceMock = new();

            // use the fake object as dependency
            ownersService = new OwnersService(carRepoMock.Object, ownersRepoMock.Object, cashServiceMock.Object);
        }

        public void Dispose()
        {
            outputHelper.WriteLine("Test clean up");
        }

        [Fact]
        public void BuyCar_HaveCar_AlreadyHaveCar()
        {
            outputHelper.WriteLine("Test 1 : ");

            // Arrange
            Car car = new Car() { Id = 2 };
            Owner owner = new Owner() {Id=200, Car = new Car() };
        

            // Mock the behavior of GetOwnerById to return the existing owner
            ownersRepoMock.Setup(om => om.GetOwnerById(It.IsAny<int>())).Returns(owner);

            // Mock the behavior of GetCarById to return the existing car
            carRepoMock.Setup(cm => cm.GetCarById(It.IsAny<int>())).Returns(car);

            BuyCarInput input   = new() { CarId = 2, OwnerId = 200, Amount = 5000 };

            // Act
            string result = ownersService.BuyCar(input);

            // Assert
            Assert.Contains("Already have car", result);
        }

        [Fact]
        public void BuyCar_LowCarPtice_Insufficient()
        {
            outputHelper.WriteLine("Test 2 : Insufficient fund");

            // Arrange
            Car car = new Car() { Id = 3 ,Price=10000 };
            Owner owner = new Owner() { Id = 300};


            // Mock the behavior of GetOwnerById to return the existing owner
            ownersRepoMock.Setup(om => om.GetOwnerById(It.IsAny<int>())).Returns(owner);

            // Mock the behavior of GetCarById to return the existing car
            carRepoMock.Setup(cm => cm.GetCarById(It.IsAny<int>())).Returns(car);

            BuyCarInput input = new() { CarId = 3, OwnerId = 300, Amount = 5000 };

            // Act
            string result = ownersService.BuyCar(input);

            // Assert
            Assert.Contains("Insufficient", result);
        }

        [Fact]
        public void AssignToOwner_Successfull_Successfull()
        {
            outputHelper.WriteLine("Test 3 : Successfull Assigned Car to owner");

            // Arrange
            Car car = new Car() { Id = 3, Price = 10000 };
            Owner owner = new Owner() { Id = 300 };


            // Mock the behavior of GetOwnerById to return the existing owner
            ownersRepoMock.Setup(om => om.GetOwnerById(It.IsAny<int>())).Returns(owner);

            // Mock the behavior of GetCarById to return the existing car
            carRepoMock.Setup(cm => cm.GetCarById(It.IsAny<int>())).Returns(car);

            //Mock
            carRepoMock.Setup(cm=>cm.AssignToOwner(It.IsAny<int>(),It.IsAny<int>())).Returns(true);
            BuyCarInput input = new() { CarId = 3, OwnerId = 300, Amount = 10000 };

            // Act
            string result = ownersService.BuyCar(input);

            // Assert
            Assert.Contains("Successfull", result);
        }

        // can't test somthing went wrong 
        //[Fact]
        //public void AssignToOwner_wentwrong_wrong()
        //{
        //    outputHelper.WriteLine("Test 3 : went wrong Assigned Car to owner");

        //    // Arrange
        //    Car car = new Car() { Id = 5, Price = 10000 };
        //    Owner owner = new Owner();


        //    // Mock the behavior of GetOwnerById to return the existing owner
        //    ownersRepoMock.Setup(om => om.GetOwnerById(It.IsAny<int>())).Returns(owner);

        //    // Mock the behavior of GetCarById to return the existing car
        //    carRepoMock.Setup(cm => cm.GetCarById(It.IsAny<int>())).Returns(car);

        //    //Mock
        //    carRepoMock.Setup(cm => cm.AssignToOwner(It.IsAny<int>(), It.IsAny<int>())).Returns(true);
        //    BuyCarInput input = new() { CarId = 5,OwnerId=300, Amount = 10000 };

        //    // Act
        //    string result = ownersService.BuyCar(input);

        //    // Assert
        //    Assert.Contains("went wrong", result);
        //}
    }
}
