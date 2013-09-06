using JustAgile.Html.Linq;
using Migrap.Json.Serialization;
using Migrap.Net.Http.Formatting.Converters;
using Migrap.Net.Http.Siren.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Travitor;

namespace Playground {
    class Program {
        static void Main(string[] args) {
            var html = HDocument.Load(@"c:\courses.html");
            var assignments = html.Descendants("a")
               .Where(a => a.Attributes().Count() == 1 && a.Attributes().Any(x => x.Value.StartsWith("javascript:openItemBrowser")))
               .Distinct(x => x.Attributes().First().Value)
               .Select(x => AsAssignment(x))
               .ToArray();

            
            var travitor = TravitorClient.New(x => {
                x.Address("http://localhost:4686/api");
            });
            travitor.LoginAsync("michael.park", "!!urDead").Wait();
            var text = travitor.GetStringAsync("courses").Result;
            var json = travitor.GetStringAsync("courses", new { index = 0, count = 50 }).Result;
            Console.WriteLine(json);
            Console.ReadLine();
            var document = travitor.GetCoursesAsync(new { index = 0, count = 50 }).Result;

            var courses = document.Entities.AsCourses();

            foreach (var course in courses) {
                Console.WriteLine("{0,-10}{1,-100}{2}", course.Id, course.Name, course.Description.Replace("\n", string.Empty));
            }

            (new AutoResetEvent(false)).WaitOne();
        }

        public static Assignment AsAssignment(HElement element) {
            var name = element.Descendants("b").First().Value;
            var parts = element.Attribute("href").Value.Split("(),".ToCharArray());

            var assignment = new Assignment {
                Name = name,
                CourseId = parts[1],
                AssignmentId = parts[3]
            };

            return assignment;
        }
    }

    public class Assignment {
        public string AssignmentId { get; set; }
        public string CourseId { get; set; }
        public string Name { get; set; }
    }

    public static partial class Extensions {
        public static Courses AsCourses(this Entities entities, string @class = "course") {
            var courses = entities.Select<Course>(@class);
            return new Courses(courses);
        }

        public static IEnumerable<TResult> Select<TResult>(this Entities entities, string @class) {
            return entities.Where(x => x.Class.Contains(@class))
                .Select(x => x.Properties)
                .Cast<JObject>()
                .Select(x => x.ToObject<TResult>())
                .AsEnumerable();
        }

        public static IEnumerable<T> Distinct<T, TIdentity>(this IEnumerable<T> source, Expression<Func<T, TIdentity>> identitySelector) {
            return source.Distinct(By(identitySelector.Compile()));
        }

        private static IEqualityComparer<TSource> By<TSource, TIdentity>(Func<TSource, TIdentity> identitySelector) {
            return new DelegateComparer<TSource, TIdentity>(identitySelector);
        }

        private class DelegateComparer<T, TIdentity> : IEqualityComparer<T> {
            private readonly Func<T, TIdentity> identitySelector;

            public DelegateComparer(Func<T, TIdentity> identitySelector) {
                this.identitySelector = identitySelector;
            }

            public bool Equals(T x, T y) {
                return Equals(identitySelector(x), identitySelector(y));
            }

            public int GetHashCode(T obj) {
                return identitySelector(obj).GetHashCode();
            }
        }
    }
}