using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Dynamic;
using System.Net.Http.Headers;

using Newtonsoft.Json;

namespace AcmeFood.Handler {
    public class CookieHandler : DelegatingHandler {
        public static string CookieName = "Acme.Cookie";

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken) {
            string UniqId;

            // Try to get the unique ID from the request; otherwise create a new ID.
            var cookie = request.Headers.GetCookies(CookieName).FirstOrDefault();
            if (cookie == null) {
                UniqId = Guid.NewGuid().ToString();
            } else {
                UniqId = cookie[CookieName].Value;
                try {
                    Guid guid = Guid.Parse(UniqId);
                } catch (FormatException) {
                    // Bad unique ID. Create a new one.
                    UniqId = Guid.NewGuid().ToString();
                }
            }

            // Store the unique ID in the request property bag.
            request.Properties[CookieName] = UniqId;

            // Continue processing the HTTP request.
            HttpResponseMessage response = await base.SendAsync(request, cancellationToken);

            // Set the unique ID as a cookie in the response message.
            response.Headers.AddCookies(new CookieHeaderValue[] {
                new CookieHeaderValue(CookieName, UniqId)
            });

            return response;
        }
    }
}