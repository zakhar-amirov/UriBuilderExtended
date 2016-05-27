﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using UriBuilderExtended;

namespace UriBuilderExtended.Test
{
    [TestClass]
    public class UriBuilderExtensionQueryTests
    {
        [TestMethod]
        public void HasQuery()
        {
            string urlString = "http://www.test.com/?key=value";

            UriBuilder uriBuilder = new UriBuilder(urlString);

            Assert.IsTrue(uriBuilder.HasQuery("key"), "Did not detect existing key");
            Assert.IsTrue(uriBuilder.HasQuery("key", "value"), "Did not detect existing key and value");

            Assert.IsFalse(uriBuilder.HasQuery("nokey"), "Wrongfully detected non-existing key");
            Assert.IsFalse(uriBuilder.HasQuery("nokey", "value"), "Wrongfully detected non-existing key with value");
        }

        [TestMethod]
        public void HasQueryMultiKey()
        {
            string urlString = "http://www.test.com/?key1=value1&key1=value2&key2=value1";

            UriBuilder uriBuilder = new UriBuilder(urlString);

            Assert.IsTrue(uriBuilder.HasQuery("key1"), "Did not detect existing key");
            Assert.IsTrue(uriBuilder.HasQuery("key2"), "Did not detect existing key");

            Assert.IsFalse(uriBuilder.HasQuery("nokey"), "Wrongfully detected non-existing key");
        }
        [TestMethod]
        public void HasQueryMultiValue()
        {
            string urlString = "http://www.test.com/?key=value1&key=value2";

            UriBuilder uriBuilder = new UriBuilder(urlString);

            Assert.IsTrue(uriBuilder.HasQuery("key"), "Did not detect existing key");
            Assert.IsTrue(uriBuilder.HasQuery("key", "value1"), "Did not detect existing key and value");
            Assert.IsTrue(uriBuilder.HasQuery("key", "value2"), "Did not detect existing key and value");
            Assert.IsTrue(uriBuilder.HasQuery("key", "value1", "value2"), "Did not detect existing key and values");

            Assert.IsFalse(uriBuilder.HasQuery("nokey", "value1", "value2"), "Wrongfully detected non-existing key with values");
        }

        [TestMethod]
        public void AddQuery()
        {
            string urlString = "http://www.test.com/";

            UriBuilder uriBuilder = new UriBuilder(urlString);

            uriBuilder.AddQuery("key", "value1");
            Assert.IsTrue(uriBuilder.HasQuery("key", "value1"), "Adding from blank failed");
            Assert.IsFalse(uriBuilder.HasQuery("key", "value2"), "Unexpected value found");

            uriBuilder.AddQuery("key", "value1");
            Assert.IsTrue(uriBuilder.HasQuery("key", "value1"), "Adding existing failed");
            Assert.IsFalse(uriBuilder.HasQuery("key", "value2"), "Unexpected value found");
        }

        [TestMethod]
        public void AddQueryMultiValue()
        {
            string urlString = "http://www.test.com/";

            UriBuilder uriBuilder = new UriBuilder(urlString);

            uriBuilder.AddQuery("key", "value1", "value2");
            Assert.IsTrue(uriBuilder.HasQuery("key", "value1"), "Adding multiple from blank failed");
            Assert.IsTrue(uriBuilder.HasQuery("key", "value2"), "Adding multiple from blank failed");
            Assert.IsTrue(uriBuilder.HasQuery("key", "value1", "value2"), "Adding multiple from blank failed");

            uriBuilder.AddQuery("key", "value1", "value2");
            Assert.IsTrue(uriBuilder.HasQuery("key", "value1"), "Adding multiple from blank failed");
            Assert.IsTrue(uriBuilder.HasQuery("key", "value2"), "Adding multiple from blank failed");
            Assert.IsTrue(uriBuilder.HasQuery("key", "value1", "value2"), "Adding multiple from blank failed");
        }

        [TestMethod]
        public void AddQueryMultiKey()
        {
            string urlString = "http://www.test.com/";

            UriBuilder uriBuilder = new UriBuilder(urlString);

            uriBuilder.AddQuery("key1", "value1", "value2");
            uriBuilder.AddQuery("key2", "value3", "value4");
            Assert.IsTrue(uriBuilder.HasQuery("key1", "value1", "value2"), "Adding key1 failed");
            Assert.IsTrue(uriBuilder.HasQuery("key2", "value3", "value4"), "Adding key2 failed");
        }

        [TestMethod]
        public void RemoveQuery()
        {
            string urlString = "http://www.test.com/";

            UriBuilder uriBuilder = new UriBuilder(urlString);

            uriBuilder.AddQuery("key1", "value1", "value2");
            uriBuilder.RemoveQuery("key1");

            Assert.AreEqual<string>(urlString, uriBuilder.Uri.ToString(), "Removing queries failed");

            uriBuilder.AddQuery("key1", "value1", "value2");
            uriBuilder.AddQuery("key2", "value1", "value2");
            uriBuilder.RemoveQuery("key1");

            Assert.IsFalse(uriBuilder.HasQuery("key1"), "Removing query failed");
            Assert.IsTrue(uriBuilder.HasQuery("key2"), "Query unexpectedly removed");
        }

        [TestMethod]
        public void SetQuery()
        {
            string urlString = "http://www.test.com/";

            UriBuilder uriBuilder = new UriBuilder(urlString);

            uriBuilder.SetQuery("key", "value1");
            Assert.IsTrue(uriBuilder.HasQuery("key", "value1"), "Setting from blank failed");
            Assert.IsFalse(uriBuilder.HasQuery("key", "value2"), "Unexpected value found");

            uriBuilder.SetQuery("key", "value2");
            Assert.IsTrue(uriBuilder.HasQuery("key", "value2"), "Setting existing failed");
            Assert.IsFalse(uriBuilder.HasQuery("key", "value1"), "Unexpected value found");

            uriBuilder.SetQuery("key", "value1", "value2");
            Assert.IsTrue(uriBuilder.HasQuery("key", "value1", "value2"), "Setting existing failed");
            Assert.IsFalse(uriBuilder.HasQuery("key", "value3"), "Unexpected value found");
        }

        [TestMethod]
        public void SpecialCharacterStrings()
        {
            // TODO: Test special characters
            Assert.Fail("Test not written.");
        }
    }
}