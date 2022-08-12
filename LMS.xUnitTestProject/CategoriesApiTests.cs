using System;
using System.Collections.Generic;
using System.Text;

using Xunit;
using Xunit.Abstractions;

// NOTE:
// Add the following NuGet PACKAGES:
//      FluentAssertions                            (latest version)
//      Moq                                         (latest version)
//      Microsoft.EntityFrameworkCore.InMemory      (same version as EFCore in the LMS.Web project
// Also add REFERENCE to the LMS.Web Project

namespace LMS.xUnitTestProject
{
    public class CategoriesApiTests
    {
        private readonly ITestOutputHelper testOutputHelper;

        public CategoriesApiTests(
            ITestOutputHelper testOutputHelper)
        {
            this.testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void Test1()
        {
            // 1. ARRANGE
            // 2. ACT
            // 3. ASSERT
        }
    }
}