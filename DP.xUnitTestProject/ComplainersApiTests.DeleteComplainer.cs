﻿using DP.Web.Areas.Citizens.Controllers;
using DP.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using ComplainersController = DP.Web.Controllers.ComplainersController;

namespace DP.xUnitTestProject
{
    public partial class ComplainersApiTests
    {
        [Fact]
        public void DeleteComplainer_NotFoundResult()
        {
            // ARRANGE
            var dbName = nameof(ComplainersApiTests.DeleteComplainer_NotFoundResult);
            var logger = Mock.Of<ILogger<ComplainersController>>();
            using var dbContext = DbContextMocker.GetApplicationDbContext(dbName);      // Disposable!

            var controller = new ComplainersController(dbContext, logger);
            int findComplainerID = 900;

            // ACT
            IActionResult actionResultDelete = controller.DeleteComplainer(findComplainerID).Result;

            // ASSERT - check if the IActionResult is NotFound 
            Assert.IsType<NotFoundResult>(actionResultDelete);

            // ASSERT - check if the Status Code is (HTTP 404) "NotFound"
            int expectedStatusCode = (int)System.Net.HttpStatusCode.NotFound;
            var actualStatusCode = (actionResultDelete as NotFoundResult).StatusCode;
            Assert.Equal<int>(expectedStatusCode, actualStatusCode);
        }

        [Fact]
        public void DeleteComplainer_BadRequestResult()
        {
            // ARRANGE
            var dbName = nameof(ComplainersApiTests.DeleteComplainer_BadRequestResult);
            var logger = Mock.Of<ILogger<ComplainersController>>();
            using var dbContext = DbContextMocker.GetApplicationDbContext(dbName);      // Disposable!

            var controller = new ComplainersController(dbContext, logger);
            int? findComplainerID = null;

            // ACT
            IActionResult actionResultDelete = controller.DeleteComplainer(findComplainerID).Result;

            // ASSERT - check if the IActionResult is BadRequest 
            Assert.IsType<BadRequestResult>(actionResultDelete);

            // ASSERT - check if the Status Code is (HTTP 400) "BadRequest"
            int expectedStatusCode = (int)System.Net.HttpStatusCode.BadRequest;
            var actualStatusCode = (actionResultDelete as BadRequestResult).StatusCode;
            Assert.Equal<int>(expectedStatusCode, actualStatusCode);
        }

        [Fact]
        public void DeleteComplainer_OkResult()
        {
            // ARRANGE
            var dbName = nameof(ComplainersApiTests.DeleteComplainer_BadRequestResult);
            var logger = Mock.Of<ILogger<ComplainersController>>();
            using var dbContext = DbContextMocker.GetApplicationDbContext(dbName);      // Disposable!

            var controller = new ComplainersController(dbContext, logger);
            int findComplainerID = 1;

            // ACT
            IActionResult actionResultDelete = controller.DeleteComplainer(findComplainerID).Result;

            // ASSERT - if IActionResult is Ok
            Assert.IsType<OkObjectResult>(actionResultDelete);

            // ASSERT - if Status Code is HTTP 200 (Ok)
            int expectedStatusCode = (int)System.Net.HttpStatusCode.OK;
            var actualStatusCode = (actionResultDelete as OkObjectResult).StatusCode.Value;
            Assert.Equal<int>(expectedStatusCode, actualStatusCode);
        }
    }
}
