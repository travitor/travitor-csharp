using Microsoft.WindowsAzure.ActiveDirectory.Authentication;
using System;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security;
using System.Threading.Tasks;
using Travitor.Configuration;
using Travitor.Configuration.Configurators;
using Travitor.Configuration.Settings;

namespace Travitor {
    public class TravitorClient : IDisposable {
        private CookieContainer _cookies = new CookieContainer();
        private HttpClient _client;
        private HttpMessageHandler _handler;
        private bool _disposed;
        private string _username;
        private SecureString _password;
        private Uri _address;
        private Uri _tenant;
        private Uri _realm;
        private string _provider;
        private AuthenticationContext _context;
        private AssertionCredential _credential;

        public TravitorClient(ITravitorClientSettings settings)
            : this(settings.Address, settings.Tenant, settings.Realm, settings.Provider, settings.Username, settings.Password) {
        }

        public TravitorClient(Uri address, Uri tenant, Uri realm, string provider, string username, string password) {
            _address = address;
            _tenant = tenant;
            _realm = realm;
            _provider = provider;
            _username = username;
            _password = password.Secure();

            _handler = new HttpClientHandler {
                UseCookies = true,
                CookieContainer = _cookies,
                AllowAutoRedirect = true,
            };

            _client = new HttpClient(_handler) {
                BaseAddress = address
            };
        }

        ~TravitorClient() {
            Dispose(false);
        }

        public Uri Tenant {
            get { return _tenant; }
        }

        public Uri Realm {
            get { return _realm; }
        }

        private string Provider {
            get { return _provider; }
        }

        public void Dispose() {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing) {
            if (_disposed) {
                return;
            }

            if (disposing) {
                LogoutAsync().Wait();
            }

            _disposed = true;
        }

        public Task<HttpResponseMessage> LoginAsync() {
            return LoginAsync(_username, _password.Unsecure());
        }

        public Task<HttpResponseMessage> LoginAsync(string username, string password) {
            Contract.Requires<ArgumentException>(username.IsNotNullOrEmpty());
            Contract.Requires<ArgumentException>(password.IsNotNullOrEmpty());

            _context = new AuthenticationContext(Tenant.ToString());

            var providers = _context.GetProviders(Realm);
            var provider = providers.First(x => x.Name.Equals(Provider, StringComparison.InvariantCultureIgnoreCase));

            var credential = new UsernamePasswordCredential(provider.Name, _username, _password.Unsecure());

            _credential = _context.AcquireToken(Realm, provider, credential);

            var token = _credential.AsSecurityToken();
            var header = _credential.CreateAuthorizationHeader();

            return null;
        }

        public Task<HttpResponseMessage> LogoutAsync() {
            return null;
        }

        public static TravitorClient New(Action<ITravitorClientConfigurator> configure) {
            var configurator = new TravitorClientConfigurator();
            configure(configurator);

            return new TravitorClient(configurator.Settings);
        }
    }    
}
