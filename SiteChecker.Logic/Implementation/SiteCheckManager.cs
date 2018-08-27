using NLog;
using SiteChecker.Core;
using SiteChecker.Logic.Interfaces;
using SiteChecker.Logic.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace SiteChecker.Logic.Implementation
{
    public class SiteCheckManager : ISiteCheckManager
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        private readonly ConcurrentDictionary<string, SiteCheckInfo> _info;
        private readonly ISiteCheckProvider _provider;
        private readonly int _checkTimeoutMs;
        private readonly System.Timers.Timer _timer;

        public SiteCheckManager(Config config, ISiteCheckProvider provider)
        {
            _provider = provider;
            _checkTimeoutMs = config.CheckTimeoutInMs;
            _info = new ConcurrentDictionary<string, SiteCheckInfo>(config.Urls.Select(t => new KeyValuePair<string, SiteCheckInfo>(t, GetDefaultSiteCheckInfo(t))));

            _timer = new System.Timers.Timer(config.CheckIntervalInMs);
            _timer.Elapsed += Check;
        }

        public void AddSite(string url)
        {
            _info.TryAdd(url, GetDefaultSiteCheckInfo(url));
            Task.Factory.StartNew(ReCheck);
        }

        public void DeleteSite(string url)
        {
            SiteCheckInfo t;
            _info.TryRemove(url, out t);
        }

        public SiteCheckInfo[] GetInfo()
        {
            return _info.Values.ToArray();
        }

        public void ReCheck()
        {
            Check(null, null);
        }

        private void Check(object sender, ElapsedEventArgs e)
        {
            var urls = _info.Keys.ToArray();
            var threads = GetThreadCount(urls.Length);
            _logger.Info($"Found {urls.Length} urls to check");
            Parallel.ForEach(urls, new ParallelOptions { MaxDegreeOfParallelism = threads }, (s) =>
            {
                _info[s] = Check(s).Result;
            });
        }

        private async Task<SiteCheckInfo> Check(string url)
        {
            HttpStatusCode? statusCode = null;
            int? time = null;

            var cts = new CancellationTokenSource();
            var task = Task.Run(() =>
            {
                var stopwatch = new Stopwatch();
                stopwatch.Start();
                statusCode = GetStatusCode(url);
                stopwatch.Stop();

                time = (int)stopwatch.ElapsedMilliseconds;
            }, cts.Token);

            if (await Task.WhenAny(task, Task.Delay(_checkTimeoutMs)) != task)
            {
                cts.Cancel();
            }

            var res = _provider.GetInfo(statusCode, time);
            res.Url = url;
            _logger.Debug($"Url [{url}] return {statusCode} in {time}ms");

            return res;
        }

        private SiteCheckInfo GetDefaultSiteCheckInfo(string url)
        {
            return new SiteCheckInfo
            {
                Url = url
            };
        }

        private HttpStatusCode? GetStatusCode(string url)
        {
            HttpStatusCode? result = null;

            var request = WebRequest.Create(url);
            request.Method = "HEAD";
            using (var response = request.GetResponse() as HttpWebResponse)
            {
                if (response != null)
                {
                    result = response.StatusCode;
                    response.Close();
                }
            }

            return result;
        }

        private static int GetThreadCount(int count)
        {
            return count > 100 ? count / 10 : Math.Max(1, (int)(Math.Log(count) / Math.Log(1.5)));
        }
    }
}
