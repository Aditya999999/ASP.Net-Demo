using DP.Web.Models;
using DP.Web.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DP.xUnitTestProject
{
    public partial class ComplainersApiTests
    {
        [Fact]
        public void GetComplainers_OkResult()
        {
            //1. Arrange
            var dbName = nameof(ComplainersApiTests.GetComplainers_OkResult);
            var logger = Mock.Of<ILogger<ComplainersController>>();
            using var dbContext = DbContextMocker.GetApplicationDbContext(dbName);   //Disposable!
            var controller = new ComplainersController(dbContext, logger);

            //2. ACT 
            IActionResult actionResultGet = controller.GetComplainers().Result;

            //3. Assert
            //---Check if the IActionResult is OK(HTTP 200)
            Assert.IsType<OkObjectResult>(actionResultGet);
            //--- if the status code is HTTP 200 "OK"
            int expectedStatusCode = (int)System.Net.HttpStatusCode.OK;
            var actualStatusCode = (actionResultGet as OkObjectResult).StatusCode.Value;
            Assert.Equal(expectedStatusCode, actualStatusCode);
        }

        [Fact]
        public void GetComplainers_CheckCorrectResult()
        {
            //ARRANGE
            var dbName = nameof(ComplainersApiTests.GetComplainers_CheckCorrectResult);
            var logger = Mock.Of<ILogger<ComplainersController>>();
            using var dbContext = DbContextMocker.GetApplicationDbContext(dbName);

            var controller =new ComplainersController(dbContext, logger);

            //ACT
            IActionResult actionResultGet = controller.GetComplainers().Result;



            //ASSERT if the IActionResult is OK (HTTP 200)
            Assert.IsType<OkObjectResult>(actionResultGet);

            //Extract the result from the IActionResult object
            var okResult = actionResultGet.Should().BeOfType<OkObjectResult>().Subject;

            //ASSERT if OkResult contains an object of the correct type
            Assert.IsAssignableFrom<List<Complainer>>(okResult.Value);

            //Extract the complainers from the result of the action.
            var complainers = okResult.Value.Should().BeAssignableTo<List<Complainer>>().Subject;

            //Assert if complainer is NOT NULL
            Assert.NotNull(complainers);

            //Assert if number of complainers matches with the TEST Data
            Assert.Equal(expected: DbContextMocker.TestData_Complainers.Length, actual: complainers.Count);

            //Assert if data is correct
            int ndx = 0;
            foreach (Complainer complainer in DbContextMocker.TestData_Complainers)
            {
                Assert.Equal<int>(expected: complainer.ComplainerId,
                                    actual: complainers[ndx].ComplainerId);
                Assert.Equal(expected: complainer.FirstName,
                                  actual: complainers[ndx].FirstName);

                _testOutputHelper.WriteLine($"ROW #{ndx} OKAY");
                ndx++;
            }
        
        }

    }
}
