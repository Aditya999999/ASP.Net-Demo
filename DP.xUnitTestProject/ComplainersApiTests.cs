using System;
using System.Collections.Generic;
using System.Text;
using Xunit.Abstractions;

namespace DP.xUnitTestProject
{
    public partial class ComplainersApiTests
    {
        private readonly ITestOutputHelper _testOutputHelper;
        public ComplainersApiTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }
    }
}
