using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FlexyBoxTest.Domain.Concrete;
using FlexyBoxTest.Domain.Core;
using FlexyBoxTest.Utility;
using FlexyBoxTest.Utility.Extensions;
using Newtonsoft.Json;
using NUnit.Framework;

namespace FlexyBoxTest.Test
{
    // Task 5
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TemplateTest()
        {
            // Arrange

            // Act

            // Assert
            Assert.Pass();
        }
        [Test]
        public void FindMissingNumbersUsingList_ReturnsThreeNumbers()
        {
            // Arrange
            var numbers = new List<int>() {4,6,9};
            var expected = new List<int>() {5,7,8};
            
            // Act
            var actual = numbers.MissingElements().ToList();

            // Assert
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void FindMissingNumbersUsingList_ReturnsSevenNumbers()
        {
            // Arrange
            var numbers = new List<int>() {1, 9};
            var expected = new List<int>() {2, 3, 4, 5, 6, 7, 8};
            
            // Act
            var actual = numbers.MissingElements().ToList();

            // Assert
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void FindMissingNumbersUsingListAndNegativeNumbers_ReturnsSevenNegativeNumbers()
        {
            // Arrange
            var numbers = new List<int>() { -1, -9 };
            var expected = new List<int>() {-8, -7, -6, -5, -4, -3, -2};

            // Act
            var actual = numbers.MissingElements().ToList();

            // Assert
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void FindMissingNumbersUsingArray_ReturnsZeroNumbers()
        {
            // Arrange
            var numbers = new [] { 2,3,4 };
            var expected = new int[0]; 

            // Act
            var actual = numbers.MissingElements().ToArray();

            // Assert
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void FindMissingNumbersUsingArray_ReturnsOneNumber()
        {
            // Arrange
            var numbers = new[] {1, 3, 4};
            var expected = new[] { 2 };

            // Act
            var actual = numbers.MissingElements().ToArray();

            // Assert
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void CheckIfPalindrome_ReturnsTrue()
        {
            // Arrange
            var str = "lol";
            var expected = true;

            // Act
            var actual = str.IsPalindrome();

            // Assert
            Assert.AreEqual(expected, actual);
        } 
        [Test]
        public void CheckIfPalindrome_ReturnsFalse()
        {
            // Arrange
            var str = "haha";
            var expected = false;

            // Act
            var actual = str.IsPalindrome();

            // Assert
            Assert.AreEqual(expected, actual);
        } 
        [Test]
        public void ReverseString_ReturnsStringInReverseOrder()
        {
            // Arrange
            var str = "haha";
            var expected = "ahah";

            // Act
            var actual = str.ToCharArray().ReverseEnumerable().GetString();
            
            // Assert
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void VerifyInstanceCountViaInterface_ReturnsFour()
        {
            // Arrange
            var service = new InstanceService();
            var expected = 4;

            // Act
            var vehicles = service.GetInstances<IVehicle>();
            var actual = vehicles.Count();

            // Assert
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void VerifyInstanceCountViaAbstract_ReturnsFour()
        {
            // Arrange
            var service = new InstanceService();
            var expected = 4;

            // Act
            var vehicles = service.GetInstances<Vehicle>();
            var actual = vehicles.Count();

            // Assert
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void VerifyInstanceCountViaClass_ReturnsOne()
        {
            // Arrange
            var service = new InstanceService();
            var expected = 1;

            // Act
            var vehicle = service.GetInstances<Bicycle>();
            var actual = vehicle.Count();

            // Assert
            Assert.AreEqual(expected, actual);
        }
        /// <summary>
        /// Had to get the "Expected" value the way i did, as if it simply create a Bicycle and put in a list, it is not the same reference.
        /// </summary>
        [Test]
        public void SearchForBi_ReturnsBicycle()
        {
            // Arrange
            var service = new InstanceService();
            var searchTerm = "Bi";

            // Act
            var vehicles = service.GetInstances<IVehicle>();
            var enumerable = vehicles.ToList();
            var expected = enumerable.Select(x => x).Where(x => x.GetType().Name == nameof(Bicycle)).ToList();
            var actual = enumerable.FindElements(inputs =>
            {
                var output = inputs.Where(x => x.GetType().Name.ToLowerInvariant().Contains(searchTerm.ToLowerInvariant()))
                    .ToList();
                return output;
            }).ToList();

            // Assert
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void SearchForCycle_ReturnsBicycleAndMotorcycle()
        {
            // Arrange
            var service = new InstanceService();
            var searchTerm = "Cycle";

            // Act
            var vehicles = service.GetInstances<IVehicle>();
            var enumerable = vehicles.ToList();
            var expected = enumerable.Select(x => x).Where(x => x.GetType().Name == nameof(Bicycle) || x.GetType().Name == nameof(Motorcycle)).ToList();
            var actual = enumerable.FindElements(inputs =>
            {
                var output = inputs.Where(x => x.GetType().Name.ToLowerInvariant().Contains(searchTerm.ToLowerInvariant()))
                    .ToList();
                return output;
            }).ToList();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void SaveToJsonFile_ReadJsonString()
        {
            // Arrange
            var persistance = new PersistanceService();
            var service = new InstanceService();
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"Data.json");

            // Act
            var vehicles = service.GetInstances<IVehicle>();
            persistance.SaveToJson(path, vehicles);
            var expected = JsonConvert.SerializeObject(vehicles);
            var actual = persistance.GetJsonStringFromFile(path);

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}