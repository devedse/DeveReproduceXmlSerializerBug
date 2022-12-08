using DeveReproduceXmlSerializerBug.OtherExtensions;
using Xunit;

namespace DeveReproduceXmlSerializerBug.Tests.OtherExtensions
{
    public class StringExtensionsFacts
    {
        [Fact]
        public void TrimStartOnceWorks()
        {
            //Arrange
            var input = @"hello:hello:C:\testhello:hello:";

            //Act
            var trimmed = input.TrimStartOnce("hello:");

            //Assert
            Assert.Equal(@"hello:C:\testhello:hello:", trimmed);
        }

        [Fact]
        public void TrimStartWorks()
        {
            //Arrange
            var input = @"hello:hello:C:\testhello:hello:";

            //Act
            var trimmed = input.TrimStart("hello:");

            //Assert
            Assert.Equal(@"C:\testhello:hello:", trimmed);
        }

        [Fact]
        public void TrimEndOnceWorks()
        {
            //Arrange
            var input = @"hello:hello:C:\testhello:hello:";

            //Act
            var trimmed = input.TrimEndOnce("hello:");

            //Assert
            Assert.Equal(@"hello:hello:C:\testhello:", trimmed);
        }

        [Fact]
        public void TrimEndWorks()
        {
            //Arrange
            var input = @"hello:hello:C:\testhello:hello:";

            //Act
            var trimmed = input.TrimEnd("hello:");

            //Assert
            Assert.Equal(@"hello:hello:C:\test", trimmed);
        }
    }
}
