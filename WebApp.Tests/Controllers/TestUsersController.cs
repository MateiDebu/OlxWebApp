using Core.Contracts;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using NUnit.Framework;
using WebApp.Controllers;

namespace WebApp.Tests.Controllers
{
    [TestFixture]
    public class TestUsersController
    {
        private IUserService _userServiceMock;
        private UserController _userController;
        [SetUp]
        public void SetUp()
        {
            _userServiceMock = Substitute.For<IUserService>();
            _userController = new UserController(_userServiceMock);
        }

        [Test]
        public void UserController_GetUsers_ValidPrameters_ReturnOk()
        {
            //Arrange-pregatesc testul
            
            Random r = new Random();
            int offset = r.Next(0, 100);
            int limit = r.Next(0, 100);

            var mockedUsers = new List<UserForList>();
            _userServiceMock.GetAll(offset, limit).Returns(mockedUsers);

            //Act-fac actiunea pe care o testez
            var result= _userController.GetUsers(offset, limit);

            //Assert-ceea ce am facut s-a intamplat cum ma astept
            Assert.That(result, Is.Not.Null);
            object resultValue=((ObjectResult)result.Result).Value;
            Assert.That(resultValue, Is.SameAs(mockedUsers));   
        }

        [Test]
        public async Task UserController_GetUserById_UserFound_ReturnsOk()
        {
            //Arrange
            UserForList mockedUser=new UserForList() { Id=1,Name="name"};
            _userServiceMock.GetById(1).Returns(mockedUser);

            //Act
            var result=await _userController.GetUserById(1);

            //Assert
            Assert.That(result, Is.Not.Null);
            object resultValue=((ObjectResult)result.Result).Value;
            Assert.That(resultValue, Is.SameAs(mockedUser));
        }

        [Test]
        public async Task UserController_GetUserById_UserNotFound_ReturnsNotFound()
        {
            //Arrange
            _userServiceMock.GetById(1).ReturnsNull();

            //Act
            var result = await _userController.GetUserById(1);

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Result, Is.TypeOf<NotFoundResult>());   
        }

        [Test]
        public async Task UserController_CreateUser_ValidParameters_ReturnsCreatedUser()
        {
            //Arrange
            string name = "name";
            string username = "username";
            string password="password";
            UserForList userForList = new UserForList() { Id=1,Name=name};
            _userServiceMock.CreateUser(name,username,password).Returns(userForList);

            //Act
            var result=await _userController.CreateUser(name,username, password);

            //Assert
            Assert.That(result, Is.Not.Null);

            //UserForList resultValue = (UserForList)((ObjectResult)result.Result).Value;
            //Assert.That(resultValue.Name,Is.EqualTo(name));

            object resultValue = ((ObjectResult)result.Result).Value;
            Assert.That(resultValue, Is.SameAs(userForList));
            await _userServiceMock.Received().CreateUser(name, username, password);
        }
    }
}
