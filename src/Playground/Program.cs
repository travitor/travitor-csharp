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
            var travitor = TravitorClient.New();
            travitor.LoginAsync("michael.park", "!!urDead").Wait();

            travitor.GetApiAsync();

            (new AutoResetEvent(false)).WaitOne();
        }
    }
}
