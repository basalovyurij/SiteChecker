using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using SiteChecker.Logic.Models;
using SiteChecker.Logic.Implementation;
using MSTestHacks;
using System.Net;

namespace SiteChecker.Tests
{
    [TestClass]
    public class SiteCheckProviderUnitTests : TestBase
    {
        private IEnumerable<SiteCheckInfo> TestCases
        {
            get
            {
                return new[]
                {
                    new SiteCheckInfo
                    {
                        IsAvaliable = true,
                        StatusCode = HttpStatusCode.OK,
                        ResponseTime = 1
                    },
                    new SiteCheckInfo
                    {
                        IsAvaliable = false,
                        StatusCode = HttpStatusCode.NotFound,
                        ResponseTime = 1
                    },
                    new SiteCheckInfo
                    {
                        IsAvaliable = false,
                        StatusCode = null,
                        ResponseTime = 1
                    },
                    new SiteCheckInfo
                    {
                        IsAvaliable = false,
                        StatusCode = null,
                        ResponseTime = null
                    }
                };
            }
        }

        [TestMethod]
        [DataSource("SiteChecker.Tests.SiteCheckProviderUnitTests.TestCases")]
        public void ProviderTests()
        {
            var data = TestContext.GetRuntimeDataSourceObject<SiteCheckInfo>();

            var res = new SiteCheckProvider().GetInfo(data.StatusCode, data.ResponseTime);

            Assert.AreEqual(res.IsAvaliable, data.IsAvaliable);
            Assert.AreEqual(res.ResponseTime, data.ResponseTime);
            Assert.AreEqual(res.StatusCode, data.StatusCode);
        }
    }
}
