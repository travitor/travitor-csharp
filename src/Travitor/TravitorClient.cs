using Microsoft.IdentityModel.Tokens.JWT;
using Microsoft.WindowsAzure.ActiveDirectory.Authentication;
using System;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Travitor.Configuration;

namespace Travitor {
    public class TravitorClient : IDisposable {
        static TravitorClient() {
            ServicePointManager.ServerCertificateValidationCallback = ((sender, certificate, chain, sslPolicyErrors) => true);
        }

        private CookieContainer _cookies = new CookieContainer();
        private HttpClient _client;
        private HttpMessageHandler _handler;
        private bool _disposed;
        private string _username;
        private string _password;
        private Uri _address;
        private Uri _tenant;
        private Uri _realm;
        private string _provider;
        private AuthenticationContext _context;
        private AssertionCredential _credential;

        public static TravitorClient New(Action<ITravitorClientConfigurator> configure = null) {
            var configurator = new TravitorClientConfigurator();

            if (configure != null) {
                configure(configurator);
            }

            return new TravitorClient(configurator.Settings);
        }

        public TravitorClient(ITravitorClientSettings settings)
            : this(settings.Address, settings.Tenant, settings.Realm, settings.Provider, settings.Username, settings.Password) {
        }

        public TravitorClient(Uri address, Uri tenant, Uri realm, string provider, string username, string password) {
            _address = address;
            _tenant = tenant;
            _realm = realm;
            _provider = provider;
            _username = username;
            _password = password;

            _handler = new HttpClientHandler {
                UseCookies = true,
                CookieContainer = _cookies,
                AllowAutoRedirect = true,
            };

            _client = new HttpClient(_handler) {
                BaseAddress = address
            };
            _client.DefaultRequestHeaders.Accept("application/vnd.siren+json");
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

        public Task LoginAsync() {
            return LoginAsync(_username, _password);
        }

        public async Task LoginAsync(string username, string password) {
            const string Scheme = "Bearer";

            await Task.Run(() => {
                _context = new Microsoft.WindowsAzure.ActiveDirectory.Authentication.AuthenticationContext(Tenant.ToString());

                var providers = _context.GetProviders(Realm);
                var identity = providers.First(x => x.Name.Equals(Provider, StringComparison.InvariantCultureIgnoreCase));

                var credential = new UsernamePasswordCredential(identity.Name, username, password);

                _credential = _context.AcquireToken(Realm, identity, credential);

                var token = _credential.AsSecurityToken();
                var header = _credential.CreateAuthorizationHeader();

                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Scheme, _credential.Assertion);

            }).ConfigureAwait(false);
        }

        public Task LogoutAsync() {
            return null;
        }

        public Task<string> GetApiAsync() {

        }
    }
}
