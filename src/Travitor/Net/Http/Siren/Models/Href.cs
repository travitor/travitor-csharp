using System;

namespace Travitor.Net.Http.Siren.Models {
    public class Href : Uri {
        public Href(string uriString)
            : base(uriString, UriKind.RelativeOrAbsolute) {
        }

        public Href(string uriString, UriKind uriKind)
            : base(uriString, uriKind) {
        }

        public Href(Uri baseUri, string relativeUri)
            : base(baseUri, relativeUri) {
        }

        public Href(Uri baseUri, Uri relativeUri)
            : base(baseUri, relativeUri) {
        }

        public static implicit operator string(Href value) {
            return value.ToString();
        }

        public static implicit operator Href(string value) {
            return new Href(value);
        }

        public override string ToString() {
            return base.ToString();
        }
    }
}
