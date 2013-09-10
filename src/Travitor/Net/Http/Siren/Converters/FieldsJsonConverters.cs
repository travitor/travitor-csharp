using Newtonsoft.Json;
using System;
using System.Linq;
using Travitor.Net.Http.Siren.Models;

namespace Travitor.Net.Http.Siren.Converters {
    public class FieldsJsonConverters : JsonConverter {
        public override bool CanConvert(Type objectType) {
            return object.Equals(objectType, typeof(Fields));
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
            if (null == value && serializer.NullValueHandling == NullValueHandling.Include) {
                writer.WriteNull();
                return;
            }

            var fields = value as Fields;
            if (fields.Any()) {
                writer.WriteStartArray();
                fields.ForEach(field => serializer.Serialize(writer, field));
                writer.WriteEndArray();
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
            throw new NotImplementedException();
        }
    }
}
