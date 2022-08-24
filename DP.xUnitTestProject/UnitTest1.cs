using System;
using Xunit;
using Xunit.Abstractions;

namespace DP.xUnitTestProject
{
    public class UnitTest1
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public UnitTest1(
            ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }
        [Fact]
        public void Test1()
        {
            //Arrange

            int expectedResult = 1500;
            int actualResult;
            int a = 1000, b = 500;

            //Act

            actualResult = a + b;
            _testOutputHelper.WriteLine("Welcome to the Testing World Aditya!!!");
            _testOutputHelper.WriteLine($"input values are {a} and {b}");
            _testOutputHelper.WriteLine($"ExpectedResult = {expectedResult}");
            
            //Assert

            Assert.Equal(expectedResult, actualResult);

        }
    }
}
