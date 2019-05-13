using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using RotaGeek.Controllers;
using RotaGeek.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RotaGeek.Tests.ControllerTests
{
    [TestFixture]
    public class ContactUsControllerTests
    {
        private ContactUsController _controller;
        private Mock<IRepository<ContactMessage>> _contactMessageRepo;

        [SetUp]
        public void Setup()
        {
            _contactMessageRepo = new Mock<IRepository<ContactMessage>>();
            _controller = new ContactUsController(_contactMessageRepo.Object);
        }

        [Test]
        public void GetMessage_ReturnsContactMessage_IfExistsInDb()
        {
            ContactMessage message = new ContactMessage();
            _contactMessageRepo.Setup(x => x.FindAsync(It.IsAny<int>())).ReturnsAsync(message);

            int id = 1;
            var result = _controller.GetMessage(id).Result;

            Assert.AreEqual(typeof(OkObjectResult), result.GetType());
            var resultValue = ((OkObjectResult)result).Value;
            Assert.IsNotNull(resultValue);
            Assert.AreEqual(message, resultValue);
        }

        [Test]
        public void GetMessage_ReturnsNotFound_IfDoesntExistsInDb()
        {
            ContactMessage message = null;
            _contactMessageRepo.Setup(x => x.FindAsync(It.IsAny<int>())).ReturnsAsync(message);

            int id = 1;
            var result = _controller.GetMessage(id).Result;

            Assert.AreEqual(typeof(NotFoundResult), result.GetType());
        }

        [Test]
        public void GetContactMessages_ReturnsListOfContactMessages()
        {
            _contactMessageRepo.Setup(x => x.GetAllAsync()).ReturnsAsync(new List<ContactMessage>());

            var result = _controller.ContactMessages().Result;

            Assert.IsNotNull(result);
        }

        [Test]
        public void Save_ShouldCreateNewContactMessage_IfStateIsValid()
        {
            ContactMessage message = new ContactMessage();

            var result = _controller.Save(message).Result;

            Assert.AreEqual(typeof(CreatedAtActionResult), result.GetType());
            _contactMessageRepo.Verify(x => x.AddAsync(message), Times.Once);
            _contactMessageRepo.Verify(x => x.SaveAsync(), Times.Once);
        }

        [Test]
        public void Save_ShouldReturnBadRequest_IfStateIsNotValid()
        {
            ContactMessage message = new ContactMessage();

            _controller.ModelState.AddModelError("Test", "TestError");
            var result = _controller.Save(message).Result;

            Assert.AreEqual(typeof(BadRequestObjectResult), result.GetType());
            _contactMessageRepo.Verify(x => x.AddAsync(message), Times.Never);
            _contactMessageRepo.Verify(x => x.SaveAsync(), Times.Never);
        }

        [Test]
        public void Delete_DeletesMessageAndReturnsContactMessage_IfExistsInDb()
        {
            ContactMessage message = new ContactMessage();
            _contactMessageRepo.Setup(x => x.FindAsync(It.IsAny<int>())).ReturnsAsync(message);

            int id = 1;
            var result = _controller.Delete(id).Result as OkObjectResult;

            Assert.AreEqual(typeof(OkObjectResult), result.GetType());
            Assert.IsNotNull(result);
            Assert.AreEqual(message, result.Value);
            _contactMessageRepo.Verify(x => x.Delete(message), Times.Once);
            _contactMessageRepo.Verify(x => x.SaveAsync(), Times.Once);
        }

        [Test]
        public void Delete_ReturnsNotFound_IfDoesntExistsInDb()
        {
            ContactMessage message = null;
            _contactMessageRepo.Setup(x => x.FindAsync(It.IsAny<int>())).ReturnsAsync(message);

            int id = 1;
            var result = _controller.Delete(id).Result;

            Assert.AreEqual(typeof(NotFoundResult), result.GetType());
        }
    }
}
