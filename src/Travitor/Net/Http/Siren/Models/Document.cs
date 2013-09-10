using System;

namespace Travitor.Net.Http.Siren.Models {
    public class Document {
        private Class _class = new Class();
        private Object _properties = new Object();
        private Entities _entities = new Entities();
        private Links _links = new Links();
        private Actions _actions = new Actions();
        private Rel _rel = new Rel();
        private Href _href;

        public Class Class {
            get { return _class; }
            set { _class = value; }
        }

        public Object Properties {
            get { return _properties; }
            set { _properties = value; }
        }

        public Entities Entities {
            get { return _entities; }
            set { _entities = value; }
        }

        public Links Links {
            get { return _links; }
            set { _links = value; }
        }

        public Actions Actions {
            get { return _actions; }
            set { _actions = value; }
        }

        public Rel Rel {
            get { return _rel; }
            set { _rel = value; }
        }

        public Href Href {
            get { return _href; }
            set { _href = value; }
        }
    }
}