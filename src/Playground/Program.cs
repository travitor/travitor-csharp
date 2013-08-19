using Migrap.Json.Serialization;
using Migrap.Net.Http.Formatting.Converters;
using Migrap.Net.Http.Siren.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Travitor;

namespace Playground {
    class Program {
        static void Main(string[] args) {
            var travitor = TravitorClient.New(x => {
                x.Address("http://localhost:4686/api");
            });
            travitor.LoginAsync("michael.park", "!!urDead").Wait();

            var document = travitor.GetCoursesAsync().Result;



            var courses = document.Entities.Where(x => x.Class.Contains("course"))
                .Select(x => (x.Properties as JObject).ToObject<Course>())
                .ToArray();


            (new AutoResetEvent(false)).WaitOne();
        }
    }
}
