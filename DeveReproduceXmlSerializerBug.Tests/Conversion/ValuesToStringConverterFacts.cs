using DeveReproduceXmlSerializerBug.Conversion;
using System.Globalization;
using Xunit;

namespace DeveReproduceXmlSerializerBug.Tests.Conversion
{
    public class ValuesToStringConverterFacts
    {
        [Fact]
        public void ConvertsBytesToTb()
        {
            //Arrange

            //Act
            var result = ValuesToStringHelper.BytesToString(12345678000000L).Replace('.', ',');

            //Assert
            Assert.Equal("11,2TB", result);
        }

        [Fact]
        public void ConvertsALotOfBytesToEB()
        {
            //Arrange

            //Act
            var result = ValuesToStringHelper.BytesToString(long.MaxValue - 1).Replace('.', ',');

            //Assert
            Assert.Equal("8EB", result);
        }

        [Fact]
        public void ConvertsBytesToMb()
        {
            //Arrange

            //Act
            var result = ValuesToStringHelper.BytesToString(12345678L).Replace('.', ',');

            //Assert
            Assert.Equal("11,8MB", result);
        }

        [Fact]
        public void ConvertsBytesToKb()
        {
            //Arrange

            //Act
            var result = ValuesToStringHelper.BytesToString(1024L).Replace('.', ',');

            //Assert
            Assert.Equal("1KB", result);
        }

        [Fact]
        public void ConvertsBytesToKbWithInvariantCulture()
        {
            //Arrange

            //Act
            var result = ValuesToStringHelper.BytesToString(1155L, CultureInfo.InvariantCulture);

            //Assert
            Assert.Equal("1.1KB", result);
        }

        [Fact]
        public void ConvertsBytesToKbWithNLCulture()
        {
            //Arrange

            //Act
            var result = ValuesToStringHelper.BytesToString(1155L, CultureInfo.GetCultureInfo("nl-nl"));

            //Assert
            Assert.Equal("1,1KB", result);
        }

        [Fact]
        public void Converts0BytesToB()
        {
            //Arrange

            //Act
            var result = ValuesToStringHelper.BytesToString(0L).Replace('.', ',');

            //Assert
            Assert.Equal("0B", result);
        }


        [Fact]
        public void ConvertsSecondsToHours()
        {
            //Arrange

            //Act
            var result = ValuesToStringHelper.SecondsToString(3800L).Replace('.', ',');

            //Assert
            Assert.Equal("1,1 Hours", result);
        }

        [Fact]
        public void ConvertsSecondsToHoursInvariantCulture()
        {
            //Arrange

            //Act
            var result = ValuesToStringHelper.SecondsToString(3800L, CultureInfo.InvariantCulture);

            //Assert
            Assert.Equal("1.1 Hours", result);
        }

        [Fact]
        public void ConvertsSecondsToHoursNLCulture()
        {
            //Arrange

            //Act
            var result = ValuesToStringHelper.SecondsToString(3800L, CultureInfo.GetCultureInfo("nl-nl"));

            //Assert
            Assert.Equal("1,1 Hours", result);
        }

        [Fact]
        public void ConvertsSecondsToHours2()
        {
            //Arrange

            //Act
            var result = ValuesToStringHelper.SecondsToString(3600L).Replace('.', ',');

            //Assert
            Assert.Equal("1 Hour", result);
        }

        [Fact]
        public void ConvertsSecondsToMinutes()
        {
            //Arrange

            //Act
            var result = ValuesToStringHelper.SecondsToString(120L).Replace('.', ',');

            //Assert
            Assert.Equal("2 Minutes", result);
        }

        [Fact]
        public void Converts0SecondsToSeconds()
        {
            //Arrange

            //Act
            var result = ValuesToStringHelper.SecondsToString(0L).Replace('.', ',');

            //Assert
            Assert.Equal("0 Seconds", result);
        }

        [Fact]
        public void ConvertALotOfSeconds()
        {
            //Arrange

            //Act
            var result = ValuesToStringHelper.SecondsToString(1234567890123L).Replace('.', ',');

            //Assert
            Assert.Equal("342935525 Hours", result);
        }

        [Fact]
        public void ConvertMaxOfSeconds()
        {
            //Arrange

            //Act
            var result = ValuesToStringHelper.SecondsToString(long.MaxValue).Replace('.', ',');

            //Assert
            Assert.Equal("2562047788015215,5 Hours", result);
        }
    }
}
