using System;

namespace Travitor.Configuration {
    public interface ITravitorClientConfigurator {
        void Address(Uri value);
        void Tenant(Uri value);
        void Realm(Uri value);
        void Provider(string value);
        void Username(string value);
        void Password(string value);
    }
}
