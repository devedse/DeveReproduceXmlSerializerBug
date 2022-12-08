using DeveReproduceXmlSerializerBug.TextFormatting;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DeveReproduceXmlSerializerBug.Tests.TextFormatting
{
    public class TableToTextPrinterFacts
    {
        [Fact]
        public void ReturnsTheRightString()
        {
            //Arrange
            var data = new List<List<string>>();
            data.Add(new List<string>() { "", "First Name", "Last Name" });
            data.Add(null);
            data.Add(new List<string>() { "1", "Heinz", "Dovenschmirtz" });
            data.Add(new List<string>() { "2", "Mickey", "Mouse" });

            //Act
            var result = TableToTextPrinter.TableToText(data);

            //Assert
            var expected = $@"|   | First Name |   Last Name   |{Environment.NewLine}----------------------------------{Environment.NewLine}| 1 |   Heinz    | Dovenschmirtz |{Environment.NewLine}| 2 |   Mickey   |     Mouse     |{Environment.NewLine}";
            Assert.Equal(expected, result);
        }

        [Fact]
        public void ReturnsTheRightStringWhenWorkingWithTemptyColumnsInHeader()
        {
            //Arrange
            var data = new List<List<string>>();
            data.Add(new List<string>() { "", "", "The Name" });
            data.Add(new List<string>() { "1", "First of his Name", "Heinz Dovenschmirtz" });
            data.Add(new List<string>() { "2", "Mother of dragons", "Mickey Mouse" });

            //Act
            var result = TableToTextPrinter.TableToText(data);

            //Assert
            var expected = $@"|                       |      The Name       |{Environment.NewLine}| 1 | First of his Name | Heinz Dovenschmirtz |{Environment.NewLine}| 2 | Mother of dragons |    Mickey Mouse     |{Environment.NewLine}";
            Assert.Equal(expected, result);
        }

        [Fact]
        public void WorksWithMultiNull()
        {
            //Arrange
            var data = new List<List<string>>();
            data.Add(new List<string>() { "", "First Name", "Last Name" });
            data.Add(null);
            data.Add(new List<string>() { "1", "Heinz", "Dovenschmirtz" });
            data.Add(null);

            //Act
            var result = TableToTextPrinter.TableToText(data);

            //Assert
            var expected = $@"|   | First Name |   Last Name   |{Environment.NewLine}----------------------------------{Environment.NewLine}| 1 |   Heinz    | Dovenschmirtz |{Environment.NewLine}----------------------------------{Environment.NewLine}";
            Assert.Equal(expected, result);
        }

        [Fact]
        public void WorksWithEmptyList()
        {
            //Arrange
            var data = new List<List<string>>();
            data.Add(new List<string>() { "", "First Name", "Last Name" });
            data.Add(new List<string>());
            data.Add(new List<string>() { "1", "Heinz", "Dovenschmirtz" });
            data.Add(null);

            //Act
            var result = TableToTextPrinter.TableToText(data);

            //Assert
            var expected = $@"|   | First Name |   Last Name   |{Environment.NewLine}|                                |{Environment.NewLine}| 1 |   Heinz    | Dovenschmirtz |{Environment.NewLine}----------------------------------{Environment.NewLine}";
            Assert.Equal(expected, result);
        }

        [Fact]
        public void WorksWithNullValues()
        {
            //Arrange
            var data = new List<List<string>>();
            data.Add(new List<string>() { "", "First Name", "Last Name" });
            data.Add(new List<string>() { "1", null, "Dovenschmirtz" });
            data.Add(null);

            //Act
            var result = TableToTextPrinter.TableToText(data);

            //Assert
            var expected = $@"|   | First Name |   Last Name   |{Environment.NewLine}| 1 |            | Dovenschmirtz |{Environment.NewLine}----------------------------------{Environment.NewLine}";
            Assert.Equal(expected, result);
        }
    }
}
