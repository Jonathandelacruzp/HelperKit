using Newtonsoft.Json;
using System;
using System.Collections.Specialized;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HelperKit.Mvc
{
    public class WebClientServiceHelper : IServiceHelper, IDisposable
    {
        protected string Url { get; set; }

        /// <summary>
        /// AppSettings SERVICE_URL
        /// </summary>
        public WebClientServiceHelper()
        {
            this.Url = System.Configuration.ConfigurationManager.AppSettings.Get("SERVICE_URL");
        }

        public WebClientServiceHelper(string url)
        {
            this.Url = url;
        }

        public virtual async Task<string> GetService(string serviceName, NameValueCollection reqParm, NameValueCollection headerParm = null)
        {
            var urlService = $"{this.Url}/{serviceName}/";
            var parameters = string.Empty;
            using (var client = new System.Net.WebClient())
            {
                if (headerParm != null)
                    client.Headers.Add(headerParm);
                var count = 0;
                foreach (var key in reqParm.AllKeys)
                {
                    if (reqParm[key] != null)
                    {
                        parameters += key + "=" + reqParm[key] + (count > 0 ? parameters += "&" : string.Empty);
                        count++;
                    }
                }
                var uri = new Uri(urlService + parameters);
                return await client.DownloadStringTaskAsync(uri);
            }
        }

        public async Task<string> PostService(string serviceName, NameValueCollection reqParm, NameValueCollection headerParm = null)
        {
            var urlServiceParameter = $"{this.Url}/{serviceName}/";

            using (var webClient = new System.Net.WebClient())
            {
                if (headerParm != null)
                    webClient.Headers.Add(headerParm);

                var responseBytes = await webClient.UploadValuesTaskAsync(urlServiceParameter, "POST", reqParm);

                return Encoding.UTF8.GetString(responseBytes);
            }
        }

        public virtual async Task<T> CreateObject<T>(FormMethod method, string serviceName, NameValueCollection reqParm, NameValueCollection headerParm = null) where T : class
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