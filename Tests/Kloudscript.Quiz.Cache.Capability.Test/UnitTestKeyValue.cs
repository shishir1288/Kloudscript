using Kloudscript.Quiz.Cache.Capability.Controllers;
using Kloudscript.Quiz.Cache.Capability.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Kloudscript.Quiz.Cache.Capability.Test
{
    [TestFixture]
    public class KeyValueTests
    {
        #region private members
        private static KeyValueControllerTestData _keyValueControllerTestData;
        private static KeyValueController _keyValueController;
        private static IMemoryCache _memoryCache;
        private static List<KeyValue> _keyValueList; 
        #endregion

        #region setup

        /// <summary>
        /// Setting up mock data and memory cache
        /// </summary>
        [SetUp]
        public void Setup()
        {
            _keyValueControllerTestData = new KeyValueControllerTestData();
            _keyValueList = _keyValueControllerTestData.GetCachedKeyValues(); 
            _memoryCache = new MemoryCache(new MemoryCacheOptions());

        }
        #endregion

        #region public members

        /// <summary>
        /// Return status code 404
        /// if passing key was not found
        /// </summary>
        /// <param name="key"></param>
        [TestCase("A")]
        [TestCase("B")]
        [TestCase("C")]
        public void KeyNotExists_Return_StatusCode_404(string key)
        {
            _memoryCache.Set(_keyValueList[0].Key, _keyValueList[0]);
            _keyValueController = new KeyValueController(_memoryCache);

            var result = _keyValueController.GetAsync(key);

            var notFound = (result.Result as NotFoundObjectResult);

            // Assert 
            Assert.IsNotNull(notFound);
            Assert.That(notFound.StatusCode, Is.EqualTo(404));
        }

        /// <summary>
        /// Return status code 400(BadRequest)
        /// if passing no key has been sent
        /// </summary>
        /// <param name="key"></param>
        [TestCase("")]
        [TestCase(null)]
        public void If_NoKeyHasBeenPassed_Return_StatusCode_400(string key)
        {
            _keyValueController = new KeyValueController(_memoryCache);

            var result = _keyValueController.GetAsync(key);

            var badRequest = (result.Result as BadRequestObjectResult);

            // Assert 
            Assert.IsNotNull(badRequest);
            Assert.That(badRequest.StatusCode, Is.EqualTo(400));
        }

        /// <summary>
        /// Return status code 200(OK)
        /// if passing key and value has been found
        /// </summary>
        /// <param name="key"></param>
        [TestCase("Key2")] 
        public void FoundKey_Return_StatusCode_200(string key)
        {
            _memoryCache.Set(_keyValueList[1].Key, _keyValueList[1]);
            _keyValueController = new KeyValueController(_memoryCache);
            var result = _keyValueController.GetAsync(key);

            var okResult = (result.Result as OkObjectResult);

            // Assert 
            Assert.That(okResult, Is.Not.Null);
            Assert.That(okResult.StatusCode, Is.EqualTo(200)); 
        }

        /// <summary>  
        /// Return status code 201(Created)
        /// Setting up cache for Key1
        /// </summary>
        [Test]
        public void SettingValueByKey1_Return_StatusCode_201()
        {
            _keyValueController = new KeyValueController(_memoryCache);

            var result = _keyValueController.Post(_keyValueList[0]);

            var createdResult = (result as ObjectResult)?.StatusCode;

            // Assert 
            Assert.That(createdResult, Is.Not.Null);
            Assert.That(createdResult, Is.EqualTo(201));
        }

        /// <summary>
        ///Return status code 201(Created)
        /// Setting up cache for Key2
        /// </summary>
        [Test]
        public void SettingValueByKey2_Return_StatusCode_201()
        {
            _keyValueController = new KeyValueController(_memoryCache);

            var result = _keyValueController.Post(_keyValueList[1]);
            var createdResult = (result as ObjectResult)?.StatusCode;

            // Assert 
            Assert.That(createdResult, Is.Not.Null);
            Assert.That(createdResult, Is.EqualTo(201));
        }


        /// <summary>
        /// Return status code 400(BadRequest)
        /// If no key Value pair has been sent to POST method
        /// </summary>
        [TestCase(null)]
        public void If_NoKeyValuePassed_Return_StatusCode_400(KeyValue keyValue)
        {
            _keyValueController = new KeyValueController(_memoryCache);

            var result = _keyValueController.Post(keyValue);

            var badRequest = result as BadRequestResult;

            // Assert 
            Assert.That(badRequest, Is.Not.Null);
            Assert.That(badRequest.StatusCode, Is.EqualTo(400));
        }
        #endregion
    }
}