using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSTestHacks;
using SiteChecker.Logic.Models;
using SiteChecker.Logic.Implementation;
using System.Linq;
using SiteChecker.Core;
using System.Collections.Generic;
using System.Net;

namespace SiteChecker.Tests
{
    [TestClass]
    public class SiteCheckManagerIntegrationTests : TestBase
    {
        private IEnumerable<SiteCheckInfo> TestCases
        {
            get
            {
                return new[]
                {
                    new SiteCheckInfo
                    {
                        Url = "https://ya.ru",
                        IsAvaliable = true,
                        StatusCode = HttpStatusCode.OK,
                        ResponseTime = 1
                    },
                    new SiteCheckInfo
                    {
                        Url = "https://ya.ru/404",
                        IsAvaliable = false,
                        StatusCode = HttpStatusCode.NotFound,
                        ResponseTime = 1
                    },
                    new SiteCheckInfo
                    {
                        Url = "http://ya321.ru/",
                        IsAvaliable = false,
                        StatusCode = null,
                        ResponseTime = 1
                    },
                    new SiteCheckInfo
                    {
                        Url = "http://www.fakeresponse.com/api/?sleep=2",
                        IsAvaliable = false,
                        StatusCode = null,
                        ResponseTime = null
                    }
                };
            }
        }

        [TestMethod]
        [DataSource("SiteChecker.Tests.SiteCheckManagerIntegrationTests.TestCases")]
        public void ManagerTests()
        {
            var data = TestContext.GetRuntimeDataSourceObject<SiteCheckInfo>();

            var manager = new SiteCheckManager(
                new Config
                {
                    Urls = new List<string>() { data.Url },
                    CheckTimeoutInMs = 1000,
                    CheckIntervalInMs = 1000000
                }, 
                new SiteCheckProvider());
            manager.ReCheck();
            var res = manager.GetInfo().First();

            Assert.AreEqual(res.IsAvaliable, data.IsAvaliable);
            Assert.AreEqual(res.ResponseTime.HasValue, data.ResponseTime.HasValue);
            Assert.AreEqual(res.StatusCode, data.StatusCode);
        }
    }
}
