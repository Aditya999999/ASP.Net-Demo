using DP.Web.Controllers;
using DP.Web.Models;
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
        public void InsertComplainer_OkResult()
        {
            // ARRANGE
            var dbName = nameof(ComplainersApiTests.InsertComplainer_OkResult);
            var logger = Mock.Of<ILogger<ComplainersController>>();
            using var dbContext = DbContextMocker.GetApplicationDbContext(dbName);      // Disposable!

            var controller = new ComplainersController(dbContext, logger);
            Complainer complainerToAdd = new Complainer
            {
                ComplainerId = 5,
                FirstName = null             // INVALID!  FirstName is REQUIRED
            };

            // ACT
            IActionResult actionResultPost = controller.PostComplainer(complainerToAdd).Result;

            // ASSERT - check if the IActionResult is Ok
            Assert.IsType<OkObjectResult>(actionResultPost);

            // ASSERT - check if the Status Code is (HTTP 200) "Ok", (HTTP 201 "Created")
            int expectedStatusCode = (int)System.Net.HttpStatusCode.OK;
            var actualStatusCode = (actionResultPost as OkObjectResult).StatusCode.Value;
            Assert.Equal<int>(expectedStatusCode, actualStatusCode);

            // Extract the result from the IActionResult object.
            var postResult = actionResultPost.Should().BeOfType<OkObjectResult>().Subject;

            // ASSERT - if the result is a CreatedAtActionResult
            Assert.IsType<CreatedAtActionResult>(postResult.Value);

            // Extract the inserted Category object
            Complainer actualComplainer = (postResult.Value as CreatedAtActionResult).Value
                                      .Should().BeAssignableTo<Complainer>().Subject;

            // ASSERT - if the inserted Category object is NOT NULL
            Assert.NotNull(actualComplainer);

            Assert.Equal(complainerToAdd.ComplainerId, actualComplainer.ComplainerId);
            Assert.Equal(complainerToAdd.FirstName, actualComplainer.FirstName);
        }
    }
}
