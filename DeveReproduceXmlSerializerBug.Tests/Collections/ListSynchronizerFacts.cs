using DeveReproduceXmlSerializerBug.Collections;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace DeveReproduceXmlSerializerBug.Tests.Collections
{
    public class ListSynchronizerFacts
    {
        private readonly ITestOutputHelper _output;
        private readonly string _currentTestName;

        public ListSynchronizerFacts(ITestOutputHelper output)
        {
            _output = output;
            var type = output.GetType();
            var testMember = type.GetField("test", BindingFlags.Instance | BindingFlags.NonPublic);
            var test = (ITest)testMember.GetValue(output);
            _currentTestName = test.DisplayName;
        }

        [Fact]
        public void AddsItems()
        {
            //Arrange
            var source = new List<string>()
            {
                "Item 1",
                "Item 2",
                "Item 3"
            };

            var dest = new ObservableCollection<string>()
            {
                "Item 1",
                "Item 2"
            };

            dest.CollectionChanged += (sender, e) => _output.WriteLine($"{_currentTestName}: {e.Action}");

            //Act
            ListSynchronizer.SynchronizeLists(source, dest);

            //Assert
            Assert.Equal(source.Count, dest.Count);
            for (int i = 0; i < source.Count; i++)
            {
                Assert.Equal(source[i], dest[i]);
            }
        }

        [Fact]
        public void RemovesItems()
        {
            //Arrange
            var source = new List<string>()
            {
                "Item 1",
                "Item 2"
            };

            var dest = new ObservableCollection<string>()
            {
                "Item 1",
                "Item 2",
                "Item 3"
            };

            dest.CollectionChanged += (sender, e) => _output.WriteLine($"{_currentTestName}: {e.Action}");

            //Act
            ListSynchronizer.SynchronizeLists(source, dest);

            //Assert
            Assert.Equal(source.Count, dest.Count);
            for (int i = 0; i < source.Count; i++)
            {
                Assert.Equal(source[i], dest[i]);
            }
        }

        [Fact]
        public void CorrectlySortsItems()
        {
            //Arrange
            var source = new List<string>()
            {
                "Item 1",
                "Item 2",
                "Item 3"
            };

            var dest = new ObservableCollection<string>()
            {
                "Item 3",
                "Item 2",
                "Item 1"
            };

            dest.CollectionChanged += (sender, e) => _output.WriteLine($"{_currentTestName}: {e.Action}");

            //Act
            ListSynchronizer.SynchronizeLists(source, dest);

            //Assert
            Assert.Equal(source.Count, dest.Count);
            for (int i = 0; i < source.Count; i++)
            {
                Assert.Equal(source[i], dest[i]);
            }
        }

        [Fact]
        public void AddsAndSortsItems()
        {
            //Arrange
            var source = new List<string>()
            {
                "Item 1",
                "Item 2",
                "Item 3"
            };

            var dest = new ObservableCollection<string>()
            {
                "Item 2",
                "Item 3"
            };

            dest.CollectionChanged += (sender, e) => _output.WriteLine($"{_currentTestName}: {e.Action}");

            //Act
            ListSynchronizer.SynchronizeLists(source, dest);

            //Assert
            Assert.Equal(source.Count, dest.Count);
            for (int i = 0; i < source.Count; i++)
            {
                Assert.Equal(source[i], dest[i]);
            }
        }

        [Fact]
        public void RemovesAndSortsItems()
        {
            //Arrange
            var source = new List<string>()
            {
                "Item 1",
                "Item 2"
            };

            var dest = new ObservableCollection<string>()
            {
                "Item 3",
                "Item 1",
                "Item 2"
            };

            dest.CollectionChanged += (sender, e) => _output.WriteLine($"{_currentTestName}: {e.Action}");

            //Act
            ListSynchronizer.SynchronizeLists(source, dest);

            //Assert
            Assert.Equal(source.Count, dest.Count);
            for (int i = 0; i < source.Count; i++)
            {
                Assert.Equal(source[i], dest[i]);
            }
        }

        [Fact]
        public void AddsAndRemovesItems()
        {
            //Arrange
            var source = new List<string>()
            {
                "Item 2",
                "Item 3",
                "Item 4"
            };

            var dest = new ObservableCollection<string>()
            {
                "Item 1",
                "Item 2",
                "Item 3"
            };

            dest.CollectionChanged += (sender, e) => _output.WriteLine($"{_currentTestName}: {e.Action}");

            //Act
            ListSynchronizer.SynchronizeLists(source, dest);

            //Assert
            Assert.Equal(source.Count, dest.Count);
            for (int i = 0; i < source.Count; i++)
            {
                Assert.Equal(source[i], dest[i]);
            }
        }

        [Fact]
        public void RemovesDuplicateItemsAlso()
        {
            //Arrange
            var source = new List<string>()
            {
                "Item 1",
                "Item 2",
                "Item 3"
            };

            var dest = new ObservableCollection<string>()
            {
                "Item 1",
                "Item 1",
                "Item 1",
                "Item 2",
                "Item 3"
            };

            dest.CollectionChanged += (sender, e) => _output.WriteLine($"{_currentTestName}: {e.Action}");

            //Act
            ListSynchronizer.SynchronizeLists(source, dest);

            //Assert
            Assert.Equal(source.Count, dest.Count);
            for (int i = 0; i < source.Count; i++)
            {
                Assert.Equal(source[i], dest[i]);
            }
        }

        [Fact]
        public void RemovesDuplicateItemsThatAreNotInSource()
        {
            //Arrange
            var source = new List<string>()
            {
                "Item 2",
                "Item 3"
            };

            var dest = new ObservableCollection<string>()
            {
                "Item 1",
                "Item 1",
                "Item 1",
                "Item 2",
                "Item 3"
            };

            dest.CollectionChanged += (sender, e) => _output.WriteLine($"{_currentTestName}: {e.Action}");

            //Act
            ListSynchronizer.SynchronizeLists(source, dest);

            //Assert
            Assert.Equal(source.Count, dest.Count);
            for (int i = 0; i < source.Count; i++)
            {
                Assert.Equal(source[i], dest[i]);
            }
        }

        [Fact]
        public void AddsDuplicateItems()
        {
            //Arrange
            var source = new List<string>()
            {
                "Item 1",
                "Item 1",
                "Item 1",
                "Item 2",
                "Item 3"
            };

            var dest = new ObservableCollection<string>()
            {
                "Item 1",
                "Item 2",
                "Item 3"
            };

            dest.CollectionChanged += (sender, e) => _output.WriteLine($"{_currentTestName}: {e.Action}");

            //Act
            ListSynchronizer.SynchronizeLists(source, dest);

            //Assert
            Assert.Equal(source.Count, dest.Count);
            for (int i = 0; i < source.Count; i++)
            {
                Assert.Equal(source[i], dest[i]);
            }
        }

        [Fact]
        public void DoesAllTheComplexStuffAtOnce()
        {
            //Arrange
            var source = new List<string>()
            {
                "Item 6",
                "Item 1",
                "Item 3",
                "Item 1",
                "Item 2",
                "Item 1"
            };

            var dest = new ObservableCollection<string>()
            {
                "Item 1",
                "Item 2",
                "Item 3",
                "Item 3",
                "Item 3",
                "Item 4",
                "Item 4",
                "Item 5"
            };

            dest.CollectionChanged += (sender, e) => _output.WriteLine($"{_currentTestName}: {e.Action}");

            //Act
            ListSynchronizer.SynchronizeLists(source, dest);

            //Assert
            Assert.Equal(source.Count, dest.Count);
            for (int i = 0; i < source.Count; i++)
            {
                Assert.Equal(source[i], dest[i]);
            }
        }
    }
}
