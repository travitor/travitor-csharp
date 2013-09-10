using Microsoft.WindowsAzure.ActiveDirectory.Authentication;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.IdentityModel.Tokens;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Security;
using System.Web;
using Travitor.Configuration;
using System.Linq;

namespace Travitor {
    public static partial class Extensions {
        internal static bool IsNotNullOrEmpty(this string self) {
            return false == string.IsNullOrEmpty(self);
        }

        internal static IList<IdentityProviderDescriptor> GetProviders(this Microsoft.WindowsAzure.ActiveDirectory.Authentication.AuthenticationContext self, Uri targetService) {
            Contract.Requires<ArgumentNullException>(targetService != null);
            return self.GetProviders(targetService.ToString());
        }

        internal static AssertionCredential AcquireToken(this Microsoft.WindowsAzure.ActiveDirectory.Authentication.AuthenticationContext self, Uri targetService, IdentityProviderDescriptor identityProvider, Credential credential) {
            return self.AcquireToken(targetService.ToString(), identityProvider, credential);
        }

        internal static JwtSecurityToken AsSecurityToken(this AssertionCredential self) {
            return new JwtSecurityToken(self.Assertion);
        }

        internal static void Accept(this HttpRequestHeaders self, string mediaType) {
            self.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaType));
        }

        internal static void Version(this HttpRequestHeaders self, string version) {
            self.Add("TRAVITOR-API-VERSION", version);
        }

        public static void Address(this ITravitorClientConfigurator self, string value) {
            self.Address(new Uri(value));
        }

        internal static string AsQuery(this object values) {
            return values.ToString();
            //return (values == null) ? string.Empty : TypeDescriptor.GetProperties(values)
            //    .Where(x => x.GetValue(values) != null)
            //    .Select(x => string.Format("{0}={1}", HttpUtility.UrlEncode(x.Name), HttpUtility.UrlEncode(x.GetValue(values).ToString())))
            //    .Join();
        }

        internal static string FormatWith(this string format, params string[] args) {
            return string.Format(format, args);
        }

        internal static string FormatWith(this string format, params object[] args) {
            return string.Format(format, args);
        }

        //internal static IEnumerable<PropertyDescriptor> Where(this PropertyDescriptorCollection collection, Func<PropertyDescriptor, bool> predicate) {
        //    foreach (PropertyDescriptor item in collection) {
        //        if (predicate(item)) {
        //            yield return item;
        //        }
        //    }
        //}

        private static string Join(this IEnumerable<string> source, string seperator = "&") {
            return string.Join(seperator, source);
        }
    }
}
