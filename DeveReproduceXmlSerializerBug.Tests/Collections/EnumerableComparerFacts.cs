using DeveReproduceXmlSerializerBug.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace DeveReproduceXmlSerializerBug.Tests.Collections
{
    public class EnumerableComparerFacts
    {
        public class SequenceEqualEqualityComparer
        {
            [Fact]
            public void DetectsEqualSimple()
            {
                //Arrange
                var first = new List<ComparableObject>()
                {
                    new ComparableObject() { FirstName = "Test", LastName = "User"}
                };
                var second = new List<ComparableObject>()
                {
                    new ComparableObject() { FirstName = "Test", LastName = "User"}
                };

                //Act
                var result = EnumerableComparer.SequenceEqual(first, second, (first, second) => first.FirstName == second.FirstName && first.LastName == second.LastName);

                //Assert
                Assert.True(result);
            }

            [Fact]
            public void DetectsEqualSimpleWithCaseDifference()
            {
                //Arrange
                var first = new List<ComparableObject>()
                {
                    new ComparableObject() { FirstName = "Test", LastName = "user"}
                };
                var second = new List<ComparableObject>()
                {
                    new ComparableObject() { FirstName = "test", LastName = "User"}
                };

                //Act
                var result = EnumerableComparer.SequenceEqual(first, second, (first, second) => first.FirstName.Equals(second.FirstName, StringComparison.OrdinalIgnoreCase) && first.LastName.Equals(second.LastName, StringComparison.OrdinalIgnoreCase));

                //Assert
                Assert.True(result);
            }

            [Fact]
            public void DetectsNotEqualSimple()
            {
                //Arrange
                var first = new List<ComparableObject>()
                {
                    new ComparableObject() { FirstName = "Test", LastName = "user"}
                };
                var second = new List<ComparableObject>()
                {
                    new ComparableObject() { FirstName = "test", LastName = "UserZ"}
                };

                //Act
                var result = EnumerableComparer.SequenceEqual(first, second, (first, second) => first.FirstName.Equals(second.FirstName, StringComparison.OrdinalIgnoreCase) && first.LastName.Equals(second.LastName, StringComparison.OrdinalIgnoreCase));

                //Assert
                Assert.False(result);
            }

            [Fact]
            public void DetectsEqualMultipleInFirst()
            {
                //Arrange
                var first = new List<ComparableObject>()
                {
                    new ComparableObject() { FirstName = "Test", LastName = "User"},
                    new ComparableObject() { FirstName = "Test", LastName = "User"}
                };
                var second = new List<ComparableObject>()
                {
                    new ComparableObject() { FirstName = "Test", LastName = "User"}
                };

                //Act
                var result = EnumerableComparer.SequenceEqual(first, second, (first, second) => first.FirstName == second.FirstName && first.LastName == second.LastName);

                //Assert
                Assert.False(result);
            }

            [Fact]
            public void DetectsEqualMultipleInSecond()
            {
                //Arrange
                var first = new List<ComparableObject>()
                {
                    new ComparableObject() { FirstName = "Test", LastName = "User"}
                };
                var second = new List<ComparableObject>()
                {
                    new ComparableObject() { FirstName = "Test", LastName = "User"},
                    new ComparableObject() { FirstName = "Test", LastName = "User"}
                };

                //Act
                var result = EnumerableComparer.SequenceEqual(first, second, (first, second) => first.FirstName == second.FirstName && first.LastName == second.LastName);

                //Assert
                Assert.False(result);
            }

            [Fact]
            public void DetectsEqualMultipleInBoth()
            {
                //Arrange
                var first = new List<ComparableObject>()
                {
                    new ComparableObject() { FirstName = "Test", LastName = "User"},
                    new ComparableObject() { FirstName = "Test", LastName = "User"}
                };
                var second = new List<ComparableObject>()
                {
                    new ComparableObject() { FirstName = "Test", LastName = "User"},
                    new ComparableObject() { FirstName = "Test", LastName = "User"}
                };

                //Act
                var result = EnumerableComparer.SequenceEqual(first, second, (first, second) => first.FirstName == second.FirstName && first.LastName == second.LastName);

                //Assert
                Assert.True(result);
            }

            [Fact]
            public void DetectsNotEqualMultipleIfOrderedIncorrectly()
            {
                //Arrange
                var first = new List<ComparableObject>()
                {
                    new ComparableObject() { FirstName = "Test1", LastName = "User1"},
                    new ComparableObject() { FirstName = "Test", LastName = "User"}
                };
                var second = new List<ComparableObject>()
                {
                    new ComparableObject() { FirstName = "Test", LastName = "User"},
                    new ComparableObject() { FirstName = "Test1", LastName = "User1"}
                };

                //Act
                var result = EnumerableComparer.SequenceEqual(first, second, (first, second) => first.FirstName == second.FirstName && first.LastName == second.LastName);

                //Assert
                Assert.False(result);
            }
        }


        public class SequenceEqualProperty
        {
            [Fact]
            public void DetectsEqualSimple()
            {
                //Arrange
                var first = new List<ComparableObject>()
                {
                    new ComparableObject() { FirstName = "Test", LastName = "User"}
                };
                var second = new List<ComparableObject>()
                {
                    new ComparableObject() { FirstName = "Test", LastName = "User"}
                };

                //Act
                var result = EnumerableComparer.SequenceEqual(first, second, t => t.FirstName, t => t.LastName);

                //Assert
                Assert.True(result);
            }

            [Fact]
            public void DetectsNotEqualSimpleWithCaseDifference()
            {
                //Arrange
                var first = new List<ComparableObject>()
                {
                    new ComparableObject() { FirstName = "Test", LastName = "user"}
                };
                var second = new List<ComparableObject>()
                {
                    new ComparableObject() { FirstName = "test", LastName = "User"}
                };

                //Act
                var result = EnumerableComparer.SequenceEqual(first, second, t => t.FirstName, t => t.LastName);

                //Assert
                Assert.False(result);
            }

            [Fact]
            public void DetectsNotEqualSimple()
            {
                //Arrange
                var first = new List<ComparableObject>()
                {
                    new ComparableObject() { FirstName = "Test", LastName = "user"}
                };
                var second = new List<ComparableObject>()
                {
                    new ComparableObject() { FirstName = "test", LastName = "UserZ"}
                };

                //Act
                var result = EnumerableComparer.SequenceEqual(first, second, t => t.FirstName, t => t.LastName);

                //Assert
                Assert.False(result);
            }

            [Fact]
            public void DetectsEqualMultipleInFirst()
            {
                //Arrange
                var first = new List<ComparableObject>()
                {
                    new ComparableObject() { FirstName = "Test", LastName = "User"},
                    new ComparableObject() { FirstName = "Test", LastName = "User"}
                };
                var second = new List<ComparableObject>()
                {
                    new ComparableObject() { FirstName = "Test", LastName = "User"}
                };

                //Act
                var result = EnumerableComparer.SequenceEqual(first, second, t => t.FirstName, t => t.LastName);

                //Assert
                Assert.False(result);
            }

            [Fact]
            public void DetectsEqualMultipleInSecond()
            {
                //Arrange
                var first = new List<ComparableObject>()
                {
                    new ComparableObject() { FirstName = "Test", LastName = "User"}
                };
                var second = new List<ComparableObject>()
                {
                    new ComparableObject() { FirstName = "Test", LastName = "User"},
                    new ComparableObject() { FirstName = "Test", LastName = "User"}
                };

                //Act
                var result = EnumerableComparer.SequenceEqual(first, second, t => t.FirstName, t => t.LastName);

                //Assert
                Assert.False(result);
            }

            [Fact]
            public void DetectsEqualMultipleInBoth()
            {
                //Arrange
                var first = new List<ComparableObject>()
                {
                    new ComparableObject() { FirstName = "Test", LastName = "User"},
                    new ComparableObject() { FirstName = "Test", LastName = "User"}
                };
                var second = new List<ComparableObject>()
                {
                    new ComparableObject() { FirstName = "Test", LastName = "User"},
                    new ComparableObject() { FirstName = "Test", LastName = "User"}
                };

                //Act
                var result = EnumerableComparer.SequenceEqual(first, second, t => t.FirstName, t => t.LastName);

                //Assert
                Assert.True(result);
            }

            [Fact]
            public void DetectsNotEqualMultipleIfOrderedIncorrectly()
            {
                //Arrange
                var first = new List<ComparableObject>()
                {
                    new ComparableObject() { FirstName = "Test1", LastName = "User1"},
                    new ComparableObject() { FirstName = "Test", LastName = "User"}
                };
                var second = new List<ComparableObject>()
                {
                    new ComparableObject() { FirstName = "Test", LastName = "User"},
                    new ComparableObject() { FirstName = "Test1", LastName = "User1"}
                };

                //Act
                var result = EnumerableComparer.SequenceEqual(first, second, t => t.FirstName, t => t.LastName);

                //Assert
                Assert.False(result);
            }
        }

        public class CompareEnumerables
        {
            [Fact]
            public void CorrectlyDetectsChangesForAddedAndRemoved()
            {
                //Arrange
                var oldList = new List<SampleTestClass>()
                {
                    new SampleTestClass() { Id = 1, Content = "old1" }
                };

                var newList = new List<SampleTestClass>()
                {
                    new SampleTestClass() { Id = 2, Content = "new2" }
                };

                //Act
                var result = EnumerableComparer.CompareEnumerables(oldList, newList, (item) => item.Id);

                //Assert
                Assert.Single(result.Added);
                Assert.Single(result.Removed);
                Assert.Empty(result.Updated);

                Assert.Equal("old1", result.Removed.First().Content);
                Assert.Equal("new2", result.Added.First().Content);
            }

            [Fact]
            public void CorrectlyDetectsChangesForUpdates()
            {
                //Arrange
                var oldList = new List<SampleTestClass>()
                {
                    new SampleTestClass() { Id = 1, Content = "old1" }
                };

                var newList = new List<SampleTestClass>()
                {
                    new SampleTestClass() { Id = 1, Content = "new2" }
                };

                //Act
                var result = EnumerableComparer.CompareEnumerables(oldList, newList, (item) => item.Id);

                //Assert
                Assert.Empty(result.Added);
                Assert.Empty(result.Removed);
                Assert.Single(result.Updated);

                var updatedItem = result.Updated.Single();
                Assert.Equal("old1", updatedItem.Old.Content);
                Assert.Equal("new2", updatedItem.New.Content);
            }

            [Fact]
            public void CorrectlyDetectsChangesForMultipleThings()
            {
                //Arrange
                var oldList = new List<SampleTestClass>()
                {
                    new SampleTestClass() { Id = 1, Content = "old1" },
                    new SampleTestClass() { Id = 2, Content = "old2" },
                    new SampleTestClass() { Id = 3, Content = "old3" }
                };

                var newList = new List<SampleTestClass>()
                {
                    new SampleTestClass() { Id = 2, Content = "new2" },
                    new SampleTestClass() { Id = 3, Content = "new3" },
                    new SampleTestClass() { Id = 4, Content = "new4" },
                    new SampleTestClass() { Id = 5, Content = "new5" }
                };

                //Act
                var result = EnumerableComparer.CompareEnumerables(oldList, newList, (item) => item.Id);

                //Assert
                Assert.Equal(2, result.Added.Count);
                Assert.Equal("new4", result.Added[0].Content);
                Assert.Equal("new5", result.Added[1].Content);

                Assert.Single(result.Removed);
                Assert.Equal("old1", result.Removed[0].Content);

                Assert.Equal(2, result.Updated.Count);
                var updated1 = result.Updated[0];
                var updated2 = result.Updated[1];
                Assert.Equal("old2", updated1.Old.Content);
                Assert.Equal("new2", updated1.New.Content);
                Assert.Equal("old3", updated2.Old.Content);
                Assert.Equal("new3", updated2.New.Content);
            }
        }
    }
}
