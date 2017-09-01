using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Linq;
using System.Text;
using System.Net;

namespace FeatureDemo.Core.Helpers
{
public class Nav
	{
		public Nav()
		{
            Pages = new MultiValueDictionary<string, string>();
		}
        private MultiValueDictionary<string, string> Pages { get; set; }

		public NavFluentInterface To
		{
			get
			{
                Pages?.Clear();
				return new NavFluentInterface(this);
			}
		}

	    static string GetPropertyName([CallerMemberName] string name = "") => name;

		public static string Root => GetPropertyName();
		public static string Home => GetPropertyName();
		public static string ItemDetail => GetPropertyName();
		public static string Items => GetPropertyName();
		public static string Menu => GetPropertyName();
		public static string Navigation => GetPropertyName();
		public static string Tabbed => GetPropertyName();
		public static string About => GetPropertyName();
		public static string NewItem => GetPropertyName();
		public static string MasterDetail => GetPropertyName();
		public static string Login => GetPropertyName();
        public static string WebView => GetPropertyName();
        public static string RepoSearch => GetPropertyName();
        public static string Map => GetPropertyName();
        public static string HyperWebView => GetPropertyName();

		public class NavFluentInterface
		{
			private Nav _nav { get; set; }
			public NavFluentInterface(Nav nav)
			{
				_nav = nav;
			}

			public NavFluentInterface Root(string parameters = null)
			{
				_nav.Pages.Add(Nav.Root, parameters);
				return this;
			}

			public NavFluentInterface Home(string parameters = null)
			{
				_nav.Pages.Add(Nav.Home, parameters);
				return this;
			}

			public NavFluentInterface ItemDetail(string parameters = null)
			{
				_nav.Pages.Add(Nav.ItemDetail, parameters);
				return this;
			}

			public NavFluentInterface Items(string parameters = null)
			{
				_nav.Pages.Add(Nav.Items, parameters);
				return this;
			}

			public NavFluentInterface Menu(string parameters = null)
			{
				_nav.Pages.Add(Nav.Menu, parameters);
				return this;
			}

			public NavFluentInterface Navigation(string parameters = null)
			{
				_nav.Pages.Add(Nav.Navigation, parameters);
				return this;
			}

			public NavFluentInterface Tabbed(string parameters = null)
			{
				_nav.Pages.Add(Nav.Tabbed, parameters);
				return this;
			}

			public NavFluentInterface About(string parameters = null)
			{
				_nav.Pages.Add(Nav.About, parameters);
				return this;
			}

			public NavFluentInterface NewItem(string parameters = null)
			{
				_nav.Pages.Add(Nav.NewItem, parameters);
				return this;
			}

			public NavFluentInterface MasterDetail(string parameters = null)
			{
				_nav.Pages.Add(Nav.MasterDetail, parameters);
				return this;
			}

			public NavFluentInterface Login(string parameters = null)
			{
				_nav.Pages.Add(Nav.Login, parameters);
				return this;
			}

            public NavFluentInterface WebView(string parameters = null)
            {
                _nav.Pages.Add(Nav.WebView, parameters);
                return this;
            }

            public NavFluentInterface RepoSearch(string parameters = null)
            {
                _nav.Pages.Add(Nav.RepoSearch, parameters);
                return this;
            }

			public NavFluentInterface Map(string parameters = null)
			{
                _nav.Pages.Add(Nav.Map, parameters);
				return this;
			}

            public NavFluentInterface HyperWebView(string parameters = null)
            {
                _nav.Pages.Add(Nav.HyperWebView, parameters);
                return this;
            }

			public string Go
			{
				get
				{
					if (_nav.Pages.Count == 0)
						return null;

					var path = new StringBuilder();
					foreach (var page in _nav.Pages)
					{
                        var parm = page.Value != null ? Uri.EscapeDataString(page.Value) : null;
						path.Append(parm != null ? $"{page.Key}?{parm}/" : $"{page.Key}/");
					}

					return path.ToString().Trim('/');
				}
			}
		}
	}

}
