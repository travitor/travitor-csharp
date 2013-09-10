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
            Task.Run(() => Run());
            (new AutoResetEvent(false)).WaitOne();
        }

        static async Task Run() {
            var travitor = TravitorClient.New(x => {
                x.Address("http://localhost:4686/api");
            });

            await travitor.LoginAsync("michael.park", "!!urDead");

            var assignments = await travitor.GetAssignmentsAsync();
            for (int i = 0; i < assignments.Entities.Count; i++) {
                Print(assignments.Entities[i]);
            }
        }

        private static void Print(Entity value) {
            Console.WriteLine(value);
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

        //public static IEnumerable<Assignment> AsAssignments(this Entities entities, string @class = "assignment") {
        //    var assignments = entities.Select<Assignment>(@class);
        //    return new Assignments(assignments);
        //}

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