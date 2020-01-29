using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.ServiceBus;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SSApp;
using SSApp.Models;
using SSApp.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SSApp.Tests
{
    [TestClass()]
    public class AlterationsControllerTests
    {
        private AlterationsController _controller;
        private Mock<IAlterationService> _serviceMock = new Mock<IAlterationService>();
        private Mock<ITopicClient> _topicClient = new Mock<ITopicClient>();

        public AlterationsControllerTests()
        {
            _controller = new AlterationsController(_serviceMock.Object, _topicClient.Object);
        }

        [TestMethod()]
        public async Task IndexTest()
        {
            //Arrange
            var list = new List<Alteration>()
            {
                new Alteration
                {
                    Id = 1,
                    LeftLength = 1,
                    RightLength = 1,
                    Status = StatusEnum.Created,
                    Type = AlterationTypeEnum.Sleeve
                },
                new Alteration
                {
                    Id = 2,
                    LeftLength = 1,
                    RightLength = 1,
                    Status = StatusEnum.Created,
                    Type = AlterationTypeEnum.Trouser
                }
            };
            _serviceMock.Setup(x => x.ListAsync()).ReturnsAsync(list);
            //Act
            var actualResult = await _controller.Index();
            var viewResult = actualResult as ViewResult;
            //Assert
            Assert.IsInstanceOfType(actualResult, typeof(ViewResult));
            Assert.AreEqual(list.Count, (viewResult.Model as List<Alteration>).Count);
        }

        [TestMethod()]
        public async Task DetailsTest()
        {
            //Arrange
            var a = new Alteration
            {
                Id = 1,
                LeftLength = 1,
                RightLength = 1,
                Status = StatusEnum.Created,
                Type = AlterationTypeEnum.Sleeve
            };
            _serviceMock.Setup(x => x.DetailAsync(a.Id)).ReturnsAsync(a);
            //Act
            var actualResult = (ViewResult)await _controller.Details(a.Id);
            //Assert
            Assert.AreEqual(a.Id, (actualResult.Model as Alteration).Id);
            Assert.AreEqual(a.Type, (actualResult.Model as Alteration).Type);
        }

        [TestMethod()]
        public void CreateTest()
        {
            var actualResult = _controller.Create();
            Assert.IsInstanceOfType(actualResult, typeof(ViewResult));
        }

        [TestMethod()]
        public async Task CreateTest_WithAlterationModel_Sucessfully()
        {
            //Arrange
            var a = new Alteration
            {
                Id = 1,
                LeftLength = 1,
                RightLength = 1,
                Status = StatusEnum.Created,
                Type = AlterationTypeEnum.Sleeve
            };
            _serviceMock.Setup(x => x.CreateAsync(a)).Verifiable();
            //Act
            var actualResult = await _controller.Create(a);
            var actionNameResult = (RedirectToActionResult)actualResult;

            Assert.IsInstanceOfType(actualResult, typeof(RedirectToActionResult));
            Assert.AreEqual("Index", actionNameResult.ActionName);
            _serviceMock.Verify(x => x.CreateAsync(It.IsAny<Alteration>()), Times.Once);
        }

        [TestMethod()]
        public async Task EditTest()
        {
            var a = new Alteration
            {
                Id = 1,
                LeftLength = 1,
                RightLength = 1,
                Status = StatusEnum.Created,
                Type = AlterationTypeEnum.Sleeve
            };
            _serviceMock.Setup(x => x.DetailAsync(a.Id)).ReturnsAsync(a);

            var actualResult = await _controller.Edit(a.Id);
            var viewData = ((ViewResult) actualResult).ViewData;

            Assert.IsInstanceOfType(actualResult, typeof(ViewResult));
            Assert.AreEqual(((Alteration) viewData.Model).Id, a.Id);

        }

        [TestMethod()]

        public async Task EditTest_ValidModel_Successful()
        {
            var a = new Alteration
            {
                Id = 1,
                LeftLength = 3,
                RightLength = 3,
                Status = StatusEnum.Created,
                Type = AlterationTypeEnum.Sleeve
            };
            _serviceMock.Setup(x => x.UpdateAsync(a)).ReturnsAsync(1);

            var actualResult = await _controller.Edit(a.Id, a);
            var actionName = ((RedirectToActionResult)actualResult).ActionName;

            Assert.IsInstanceOfType(actualResult, typeof(RedirectToActionResult));
            Assert.AreEqual("Index", actionName);
        }

        [TestMethod()]
        public async Task DeleteTest()
        {
            var a = new Alteration
            {
                Id = 1,
                LeftLength = 1,
                RightLength = 1,
                Status = StatusEnum.Created,
                Type = AlterationTypeEnum.Sleeve
            };
            _serviceMock.Setup(x => x.DetailAsync(a.Id)).ReturnsAsync(a);

            var actualResult = await _controller.Delete(a.Id);
            var viewData = ((ViewResult)actualResult).ViewData;

            Assert.IsInstanceOfType(actualResult, typeof(ViewResult));
            Assert.AreEqual(((Alteration)viewData.Model).Id, a.Id);

        }

        [TestMethod()]
        public async Task DeleteConfirmedTest()
        {
            var a = new Alteration
            {
                Id = 1,
                LeftLength = 3,
                RightLength = 3,
                Status = StatusEnum.Created,
                Type = AlterationTypeEnum.Sleeve
            };
            _serviceMock.Setup(x => x.DeleteAsync(a.Id)).ReturnsAsync(1);

            var actualResult = await _controller.DeleteConfirmed(a.Id);
            var actionName = ((RedirectToActionResult)actualResult).ActionName;

            Assert.IsInstanceOfType(actualResult, typeof(RedirectToActionResult));
            Assert.AreEqual("Index", actionName);
        }
    }
}