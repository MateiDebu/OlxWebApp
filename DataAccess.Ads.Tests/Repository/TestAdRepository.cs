using DataAccess.Announcement.Repository;
using DataAccess.Database.Context;
using DataAccess.Database.Models;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace DataAccess.Ads.Tests.Repository
{
    [TestFixture]
    public class TestAdRepository
    {
        
        private AdsRepository _adsRepository;
        private OlxContext _olxContext;

        [SetUp]
        public async Task SetUp()
        {
            var builder = new DbContextOptionsBuilder<OlxContext>().UseInMemoryDatabase("TestUserRepository" +
                TestContext.CurrentContext.Test.Name);
            _olxContext = new OlxContext(builder.Options);
            await _olxContext.Database.EnsureCreatedAsync();

            _adsRepository = new AdsRepository(_olxContext);
        }

        [Test]

        public async Task AdsRepository_CreateAd_ValidParameters_ReturnAd()
        {
            //Arrange
            var appUser = new ApplicationUser() { Name = "name", UserName = "username" };
            _olxContext.Users.Add(appUser);
            await _olxContext.SaveChangesAsync();

            //Act
            var result = await _adsRepository.CreateAd(appUser.Id, "name", "description");

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Not.Zero);
            Assert.That(result.Name, Is.EqualTo("name"));
            Assert.That(result.Description, Is.EqualTo("description"));
        }

        [Test]
        public async Task AdsRepository_GetAllAdsForUser_ValidParameters_RetrunsAds()
        {
            //Arrange
            var appUser = new ApplicationUser() { Name = "name", UserName = "username" };
            _olxContext.Users.Add(appUser);

            var ad1=new Ad() { UserId=appUser.Id,Name="name1",Description="description1"};
            var ad2 = new Ad() { UserId = appUser.Id, Name = "name2", Description = "description2" };
            _olxContext.Announcements.Add(ad1);  
            _olxContext.Announcements.Add(ad2);  

            await _olxContext.SaveChangesAsync();

            //Act
            var result = _adsRepository.GetAllAdsForUser(appUser.Id, 0, 10);
            var resultList = result.ToList();

            //Assert 
            Assert.That(result, Is.Not.Null);
            Assert.That(resultList , Has.Count.EqualTo(2));
            Assert.That(resultList[0].Name, Is.EqualTo("name1"));
            Assert.That(resultList[0].Description, Is.EqualTo("description1"));
            Assert.That(resultList[1].Name, Is.EqualTo("name2"));
            Assert.That(resultList[1].Description, Is.EqualTo("description2"));
        }

        [Test]
        public async Task AdsRepository_GetAllAds_ValidParameters_RetrunsAds()
        {
            //Arrange
            var appUser = new ApplicationUser() { Name = "name", UserName = "username" };
            _olxContext.Users.Add(appUser);

            var ad1 = new Ad() { UserId = appUser.Id, Name = "name1", Description = "description1" };
            var ad2 = new Ad() { UserId = appUser.Id, Name = "name2", Description = "description2" };
            _olxContext.Announcements.Add(ad1);
            _olxContext.Announcements.Add(ad2);

            await _olxContext.SaveChangesAsync();

            //Act
            var result = _adsRepository.GetAllAds(0, 10);
            var resultList = result.ToList();

            //Assert 
            Assert.That(result, Is.Not.Null);
            Assert.That(resultList, Has.Count.EqualTo(2));
            Assert.That(resultList[0].Name, Is.EqualTo("name1"));
            Assert.That(resultList[0].Description, Is.EqualTo("description1"));
            Assert.That(resultList[1].Name, Is.EqualTo("name2"));
            Assert.That(resultList[1].Description, Is.EqualTo("description2"));
        }

        [Test]
        public async Task AdsRepository_DeleteAd_AdFound_AdIsDeleted()
        {
            //Arrange
            var appUser = new ApplicationUser() { Name = "name", UserName = "username" };
            _olxContext.Users.Add(appUser);

            var ad = new Ad() { UserId = appUser.Id, Name = "name", Description = "description" };
            _olxContext.Announcements.Add(ad);

            await _olxContext.SaveChangesAsync();

            //Act
            var result = _adsRepository.DeleteAd(ad.Id);

            //Assert
            Assert.That(_olxContext.Announcements,Is.Empty);
        }

        [Test]
        public void UserRepository_DeleteAd_AdNotFound_DoesNothing()
        {
            //Act & assert
            Assert.That(() => _adsRepository.DeleteAd(101), Throws.Nothing);
        }

        [Test]
        public async Task UserRepository_GetById_AdFound_ReturnAd()
        {
            //Arrange
            var appUser = new ApplicationUser() { Name = "name", UserName = "username" };
            _olxContext.Users.Add(appUser);

            var ad = new Ad() { UserId = appUser.Id, Name = "name", Description = "description" };
            _olxContext.Announcements.Add(ad);

            await _olxContext.SaveChangesAsync();

            //Act
            var result = _adsRepository.GetById(ad.Id);
            
            //Assert
            Assert.That(result,Is.Not.Null);    
            Assert.That(result.Result.Name,Is.EqualTo(ad.Name));
            Assert.That(result.Result.Description,Is.EqualTo(ad.Description));
        }

        [Test]
        public async Task UserRepository_GetById_AdNotFound_ReturnNull()
        {
            //Act
            var result = await _adsRepository.GetById(101);

            //Assert
            Assert.That(result, Is.Null);
        }

        [Test]

        public async Task UserRepository_UpdateAd_AdFound_AdIsModified()
        {
            //Assert
            var appUser = new ApplicationUser() { Name = "name", UserName = "username" };
            _olxContext.Users.Add(appUser);

            var ad = new Ad() { UserId = appUser.Id, Name = "name", Description = "description" };
            _olxContext.Announcements.Add(ad);

            await _olxContext.SaveChangesAsync();

            //Act
            var result =await _adsRepository.UpdateAd(ad);

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Name, Is.EqualTo(ad.Name));
            Assert.That(result.Description, Is.EqualTo(ad.Description));
        }
    }
}
