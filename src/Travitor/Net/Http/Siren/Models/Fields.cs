using System.Collections.Generic;

namespace Travitor.Net.Http.Siren.Models {
    public class Fields : List<Field> {
        public Fields() {
        }

        public Fields(IEnumerable<Field> collection)
            : base(collection) {
        }
    }
}
