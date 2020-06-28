using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HelperKit.Mvc
{
    public class HttpClientServiceHelper : IServiceHelper, IDisposable
    {
        protected string Url { get; set; }

        /// <summary>
        /// AppSettings SERVICE_URL
        /// </summary>
        public HttpClientServiceHelper()
        {
            this.Url = System.Configuration.ConfigurationManager.AppSettings.Get("SERVICE_URL");
        }

        public HttpClientServiceHelper(string url)
        {
            this.Url = url;
        }

        public async Task<T> CreateObject<T>(FormMethod method, string serviceName, NameValueCollection reqParm, NameValueCollection headerParm = null) where T : class
        {
            switch (method)
            {
                case FormMethod.Get:
                    return JsonConvert.DeserializeObject<T>(await GetService(serviceName, reqParm, headerParm));

                case FormMethod.Post:
                    return JsonConvert.DeserializeObject<T>(await PostService(serviceName, reqParm, headerParm));

                default:
                    return (T)Activator.CreateInstance(typeof(T));
            }
        }

        public virtual async Task<string> GetService(string serviceName, NameValueCollection reqParm, NameValueCollection headerParm = null)
        {
            var parameters = serviceName;
            using (var client = new System.Net.Http.HttpClient { BaseAddress = new Uri(this.Url) })
            {
                if (headerParm != null)
                {
                    foreach (var item in headerParm.AllKeys)
                    {
                        client.DefaultRequestHeaders.Add(item, headerParm[item]);
                    }
                }

                var count = 0;
                foreach (var key in reqParm.AllKeys)
                {
                    if (reqParm[key] != null)
                    {
                        parameters += key + "=" + reqParm[key] + (count > 0 ? parameters += "&" : string.Empty);
                        count++;
                    }
                }

                var response = await client.GetAsync(serviceName);

                if (response.IsSuccessStatusCode)
                    return await response.Content.ReadAsStringAsync();
                else
                    throw new HttpRequestException(await response.RequestMessage.Content.ReadAsStringAsync());
            }
        }

        public async Task<string> PostService(string serviceName, NameValueCollection reqParm, NameValueCollection headerParm = null)
        {
            using (var client = new System.Net.Http.HttpClient { BaseAddress = new Uri(this.Url) })
            {
                if (headerParm != null)
                {
                    foreach (var item in headerParm.AllKeys)
                    {
                        client.DefaultRequestHeaders.Add(item, headerParm[item]);
                    }
                }

                var keyPair = new List<KeyValuePair<string, string>>();
                foreach (var item in reqParm.AllKeys)
                    keyPair.Add(new KeyValuePair<string, string>(item, reqParm[item]));

                var content = new FormUrlEncodedContent(keyPair);
                var response = await client.PostAsync(serviceName, content);

                if (response.IsSuccessStatusCode)
                    return await response.Content.ReadAsStringAsync();
                else
                    throw new HttpRequestException(await response.RequestMessage.Content.ReadAsStringAsync());
            }
        }

        #region IDisposable Support

        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}