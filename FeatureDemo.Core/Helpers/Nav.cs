using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Linq;

namespace FeatureDemo.Core.Helpers
{
public class Nav
	{
		public Nav()
		{
			Pages = new Dictionary<string, string>();
		}
		private Dictionary<string, string> Pages { get; set; }

		public NavFluentInterface To
		{
			get
			{
				Pages?.Clear();
				return new NavFluentInterface(this);
			}
		}

		private static string GetPropertyName([CallerMemberName] string name = "") => name;

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

			public string Go
			{
				get
				{
					if (_nav.Pages.Count == 0)
						return null;

					var path = new StringBuilder();
					foreach (var page in _nav.Pages)
					{
						path.Append(page.Value != null ? $"{page.Key}?{page.Value}/" : $"{page.Key}/");
					}

					return path.ToString().Trim('/');
				}
			}
		}
	}

}
