using CarAPI.Entities;
using CarFactoryAPI.Entities;
using CarFactoryAPI.Repositories_DAL;
using Moq;
using Moq.EntityFrameworkCore;

namespace CarFactoryAPI_Lab_test
{
    public class OwnerRepo_Lab_tests
    {

        private Mock<FactoryContext> factoryContextMock;
        private OwnerRepository ownerRepository;
        public OwnerRepo_Lab_tests()
        {
            factoryContextMock = new Mock<FactoryContext>();

            ownerRepository = new OwnerRepository(factoryContextMock.Object);
        }

        [Fact]
        [Trait("Author", "Moudy Rasmy")]
        public void GetOwnerById_AskForOwner50_ReturnOwner()
        {
            // Arrange

            // Build the mock Data
            List<Owner> owners = new List<Owner>() { new Owner() { Id = 50 } };


            // setup called DbSets

            factoryContextMock.Setup(fcm => fcm.Owners).ReturnsDbSet(owners);

            //Act
            Owner owner = ownerRepository.GetOwnerById(50);
            //Assert

            Assert.NotNull(owner);
        }

        [Fact]
        [Trait("Author", "Moudy Rasmy")]
        public void GetAllOwner_ReturnListOfOwner()
        {

            List<Owner> owners = new List<Owner>() { new Owner() { Id = 10 }, new Owner { Id = 20 }, new Owner { Id = 30 } };

            factoryContextMock.Setup(fcm => fcm.Owners).ReturnsDbSet(owners);


            List<Owner> owner = ownerRepository.GetAllOwners();


            Assert.NotNull(owner);
        }
    }
}
