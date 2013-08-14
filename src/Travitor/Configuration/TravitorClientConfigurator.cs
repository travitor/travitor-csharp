using System;
using System.Diagnostics.Contracts;

namespace Travitor.Configuration {
    internal class TravitorClientConfigurator : ITravitorClientConfigurator {
        private TravitorClientSettings _settings = new TravitorClientSettings();

        public TravitorClientConfigurator()
            : this(TravitorClientSettings.Default) {
        }

        private TravitorClientConfigurator(ITravitorClientSettings settings) {
            _settings.Address = settings.Address;
            _settings.Password = settings.Password;
            _settings.Provider = settings.Provider;
            _settings.Realm = settings.Realm;
            _settings.Tenant = settings.Tenant;
            _settings.Username = settings.Username;
        }

        public void Address(Uri value) {
            Contract.Requires<ArgumentNullException>(value != null);
            _settings.Address = value;
        }

        public void Tenant(Uri value) {
            Contract.Requires<ArgumentNullException>(value != null);
            _settings.Tenant = value;
        }

        public void Realm(Uri value) {
            Contract.Requires<ArgumentNullException>(value != null);
            _settings.Realm = value;
        }

        public void Provider(string value) {
            Contract.Requires<ArgumentException>(value.IsNotNullOrEmpty());
            _settings.Provider = value;
        }

        public void Username(string value) {
            Contract.Requires<ArgumentException>(value.IsNotNullOrEmpty());
            _settings.Username = value;
        }

        public void Password(string value) {
            Contract.Requires<ArgumentException>(value.IsNotNullOrEmpty());
            _settings.Password = value;
        }

        public ITravitorClientSettings Settings {
            get { return _settings; }
        }
    }
}
