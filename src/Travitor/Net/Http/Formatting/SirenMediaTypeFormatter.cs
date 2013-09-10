using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using Travitor.Net.Http.Siren.Converters;
using Travitor.Net.Http.Siren.Serialization;

namespace Travitor.Net.Http.Formatting {
    /// <summary>
    /// 
    /// </summary>
    public class SirenMediaTypeFormatter : JsonMediaTypeFormatter {
        public const string MediaType = "application/vnd.siren+json";
        private MediaTypeHeaderValue _mediaType;

        public SirenMediaTypeFormatter(string mediaType = "application/vnd.siren+json") {
            SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
            SerializerSettings.ContractResolver = new SirenContractResolver();
            SerializerSettings.Converters.Add(new HrefJsonConverter());
            //SerializerSettings.Converters.Add(new FieldsJsonConverters());

            SupportedMediaTypes.Clear();
            SupportedMediaTypes.Add(new MediaTypeHeaderValue(mediaType));
        }

        public override bool CanReadType(Type type) {
            return true;
        }

        public override bool CanWriteType(Type type) {
            return true;
        }
    }
}