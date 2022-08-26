using DP.Web.Controllers;
using DP.Web.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace DP.xUnitTestProject
{
    public partial class ComplainersApiTests
    {
        [Fact]
        public void GetComplainerById_NotFoundResult()
        {
            //Arrange 
            var dbName = nameof(ComplainersApiTests.GetComplainerById_NotFoundResult);
            var logger = Mock.Of<ILogger<ComplainersController>>();
            using var dbContext = DbContextMocker.GetApplicationDbContext(dbName);

            var controller = new ComplainersController(dbContext, logger);
            int findComplainerID = 50;

            //ACT 
            IActionResult actionResultGet = controller.GetComplainer(findComplainerID).Result;

            //ASSERT - check if the IActionResult is NotFound
            Assert.IsType<NotFoundResult>(actionResultGet);
            
            //ASSERT - check if the status code is (HTTP 404) "NotFound"
            int expectedStatusCode = (int)System.Net.HttpStatusCode.NotFound;
            var actualStatusCode = (actionResultGet as NotFoundResult).StatusCode;
            Assert.Equal<int>(expectedStatusCode, actualStatusCode);
        }

        [Fact]
        public void GetComplainerById_BadRequestResult()
        {
            //ARRANGE
            var dbName = nameof(ComplainersApiTests.GetComplainerById_BadRequestResult);
            var logger = Mock.Of<ILogger<ComplainersController>>();
            using var dbContext = DbContextMocker.GetApplicationDbContext(dbName);

            var controller = new ComplainersController(dbContext, logger);
            int? findComplainerID = null;

            //ACT
            IActionResult actionResultGet = controller.GetComplainer(findComplainerID).Result;

            //ASSERT - check if the IAction is BadRequest
            Assert.IsType<BadRequestResult>(actionResultGet);

            //ASSERT - Check if the Status code is (HTTP 400) "BadRequest"
            int expectedStatusCode = (int)System.Net.HttpStatusCode.BadRequest;
            var actualStatusCode = (actionResultGet as BadRequestResult).StatusCode;

            Assert.Equal<int>(expectedStatusCode, actualStatusCode);

        }

        [Fact]
        public void GetComplainerById_OkResult()
        {
            //Arrange
            var dbName = nameof(ComplainersApiTests.GetComplainerById_OkResult);
            var logger = Mock.Of<ILogger<ComplainersController>>();
            using var dbContext = DbContextMocker.GetApplicationDbContext(dbName);  //Disposable!

            var controller = new ComplainersController(dbContext, logger);
            int findComplainerID = 2;

            //ACT
            IActionResult actionResultGet = controller.GetComplainer(findComplainerID).Result;

            //ASSERT - if IActionResult is Ok
            Assert.IsType<OkObjectResult>(actionResultGet);

            //ASSERT - If the status code is HTTP 200(Ok)
            int expectedStatusCode = (int)System.Net.HttpStatusCode.OK;
            var actualStatusCode = (actionResultGet as OkObjectResult).StatusCode.Value;
            Assert.Equal<int>(expectedStatusCode, actualStatusCode);
        }

        [Fact]
        public void GetComplainerById_CorrectResult()
        {
            //ARRANGE
            var dbName = nameof(ComplainersApiTests.GetComplainerById_CorrectResult);
            var logger = Mock.Of<ILogger<ComplainersController>>();
            using var dbContext = DbContextMocker.GetApplicationDbContext(dbName);
            var controller = new ComplainersController(dbContext, logger);

            int findComplainerID = 2;
            Complainer expectedComplainer = DbContextMocker.TestData_Complainers
                                            .SingleOrDefault(c => c.ComplainerId == findComplainerID);

            // ACT
            IActionResult actionResultGet = controller.GetComplainer(findComplainerID).Result;

            //ASSERT - if IActionResult is Ok
            Assert.IsType<OkObjectResult>(actionResultGet);

            //ASSERT - IF IActionResult (i.e. OkObjectResult ) contains an object of the type Complainer
            OkObjectResult okResult = actionResultGet.Should().BeOfType<OkObjectResult>().Subject;
            Assert.IsType<Complainer>(okResult.Value);

            //Extract the complainer object from the result.
            Complainer actualComplainer = okResult.Value.Should().BeAssignableTo<Complainer>().Subject;
            _testOutputHelper.WriteLine($"Found: CompalinerID == {actualComplainer.ComplainerId}");

            //ASSERT - IF complainer is NOT NULL 
            Assert.NotNull(actualComplainer);

            //Assert - if the ComplainerId is containing the expected data.
            Assert.Equal<int>(expected: expectedComplainer.ComplainerId,
                                actual: actualComplainer.ComplainerId);

            //Assert - if the ComplainerName is correct
            Assert.Equal(expectedComplainer.FirstName, actualComplainer.FirstName);
        }
    }
}
