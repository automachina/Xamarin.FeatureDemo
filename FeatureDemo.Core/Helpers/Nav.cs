using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Linq;

namespace FeatureDemo.Core.Helpers
{
    public class Nav : INav
    {

        public Nav()
        {
            _to = new NavFluentInterface(this);
            Paths = new List<string>();
        }

        internal List<string> Paths;
        private readonly NavFluentInterface _to;
        public NavFluentInterface To 
        { 
            get 
            { 
                Paths.Clear(); 
                return _to; 
            } 
        }
    }

    public class NavFluentInterface
    {

        private static Nav nav;

        public NavFluentInterface(Nav _nav)
        {
            nav = _nav;
        }

        public NavFluentInterface GetPropName([CallerMemberName] string prop = "")
        {
            if (prop != "")
            {
                nav.Paths.Add(prop);
            }
            return this;
        }

        public NavFluentInterface Root => GetPropName();
        public NavFluentInterface Home => GetPropName();
        public NavFluentInterface ItemDetail => GetPropName();
        public NavFluentInterface Items => GetPropName();
        public NavFluentInterface Menu => GetPropName();
        public NavFluentInterface Navigation => GetPropName();
        public NavFluentInterface Tabbed => GetPropName();
        public NavFluentInterface About => GetPropName();
        public NavFluentInterface NewItem => GetPropName();
        public NavFluentInterface MasterDetail => GetPropName();
        public NavFluentInterface Login => GetPropName();

        public string Go => nav.Paths.ToArray().Aggregate((acc, item) => acc += $"{item}/").TrimEnd('/');
    }

}
