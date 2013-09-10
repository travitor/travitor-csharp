using System.Collections.Generic;

namespace Travitor.Net.Http.Siren.Models {
    public class Entities : List<Entity> {
        public Entities() {
        }

        public Entities(IEnumerable<Entity> collection)
            : base(collection) {
        }
    }
}
