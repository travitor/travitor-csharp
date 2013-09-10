﻿
namespace Travitor.Net.Http.Siren.Models {
    public class Action {
        public string Name { get; set; }
        public string Title { get; set; }
        public string Method { get; set; }
        public Href Href { get; set; }
        public string Type { get; set; }
        public Fields Fields { get; set; }

        public Action() {
            Fields = new Fields();
        }
    }
}
