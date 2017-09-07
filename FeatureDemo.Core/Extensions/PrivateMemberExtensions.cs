using System;
using System.Diagnostics.Contracts;
using System.Reflection;

namespace FeatureDemo.Core.Extensions
{
	public static class PrivateMemberExtensions
	{
		/// <summary>
		/// Returns a _private_ Property Value from a given Object. Uses Reflection.
		/// Throws a ArgumentOutOfRangeException if the Property is not found.
		/// </summary>
		/// <typeparam name="T">Type of the Property</typeparam>
		/// <param name="obj">Object from where the Property Value is returned</param>
		/// <param name="propName">Propertyname as string.</param>
		/// <returns>PropertyValue</returns>
		public static T GetPrivatePropertyValue<T>(this object obj, string propName, bool ignoreCase = false)
		{
			if (obj == null) throw new ArgumentNullException("obj");
			BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
			if (ignoreCase) flags = flags | BindingFlags.IgnoreCase;

            PropertyInfo pi = obj.GetType().GetProperty(propName, flags);
			if (pi == null)
				throw new ArgumentOutOfRangeException("propName",
													  string.Format("Property {0} was not found in Type {1}", propName,
																	obj.GetType().FullName));
			return (T)pi.GetValue(obj, null);
		}

		/// <summary>
		/// Returns a private Field Value from a given Object. Uses Reflection.
		/// Throws a ArgumentOutOfRangeException if the Property is not found.
		/// </summary>
		/// <typeparam name="T">Type of the Field</typeparam>
		/// <param name="obj">Object from where the Field Value is returned</param>
		/// <param name="propName">Field Name as string.</param>
		/// <returns>FieldValue</returns>
        public static T GetPrivateFieldValue<T>(this object obj, string propName, bool ignoreCase = false)
		{
			if (obj == null) throw new ArgumentNullException("obj");
			Type t = obj.GetType();
			FieldInfo fi = null;
            BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
            if (ignoreCase) flags = flags | BindingFlags.IgnoreCase;

			while (fi == null && t != null)
			{
                fi = t.GetField(propName, flags);
				t = t.BaseType;
			}
			if (fi == null)
				throw new ArgumentOutOfRangeException("propName",
													  string.Format("Field {0} was not found in Type {1}", propName,
																	obj.GetType().FullName));
			return (T)fi.GetValue(obj);
		}

		/// <summary>
		/// Sets a _private_ Property Value from a given Object. Uses Reflection.
		/// Throws a ArgumentOutOfRangeException if the Property is not found.
		/// </summary>
		/// <typeparam name="T">Type of the Property</typeparam>
		/// <param name="obj">Object from where the Property Value is set</param>
		/// <param name="propName">Propertyname as string.</param>
		/// <param name="val">Value to set.</param>
		/// <returns>PropertyValue</returns>
		public static void SetPrivatePropertyValue<T>(this object obj, string propName, T val, bool ignoreCase = false)
		{
			Type t = obj.GetType();
			BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
			if (ignoreCase) flags = flags | BindingFlags.IgnoreCase;

			if (t.GetProperty(propName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance) == null)
				throw new ArgumentOutOfRangeException("propName",
													  string.Format("Property {0} was not found in Type {1}", propName,
																	obj.GetType().FullName));
			t.InvokeMember(propName,
						   BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.SetProperty |
						   BindingFlags.Instance, null, obj, new object[] { val });
		}


		/// <summary>
		/// Set a private Field Value on a given Object. Uses Reflection.
		/// </summary>
		/// <typeparam name="T">Type of the Field</typeparam>
		/// <param name="obj">Object from where the Property Value is returned</param>
		/// <param name="propName">Field name as string.</param>
		/// <param name="val">the value to set</param>
		/// <exception cref="ArgumentOutOfRangeException">if the Property is not found</exception>
		public static void SetPrivateFieldValue<T>(this object obj, string propName, T val, bool ignoreCase = false)
		{
			if (obj == null) throw new ArgumentNullException("obj");
			Type t = obj.GetType();
			FieldInfo fi = null;
			BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
			if (ignoreCase) flags = flags | BindingFlags.IgnoreCase;
			while (fi == null && t != null)
			{
                fi = t.GetField(propName, flags);
				t = t.BaseType;
			}
			if (fi == null)
				throw new ArgumentOutOfRangeException("propName",
													  string.Format("Field {0} was not found in Type {1}", propName,
																	obj.GetType().FullName));
			fi.SetValue(obj, val);
		}

        public static TReturen InvokePrivateMethod<TReturen>(this object obj, string methName, params object[] parameters)
        {
            if (obj == null) throw new ArgumentNullException("obj");
            BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.IgnoreCase;
            MethodInfo mi = obj.GetType().GetMethod(methName, flags);
			if (mi == null)
				throw new ArgumentOutOfRangeException("methName",
													  string.Format("Method {0} was not found in Type {1}", methName,
																	obj.GetType().FullName));

            var result = mi.Invoke(obj, parameters);
            return result == null ? default(TReturen) : (TReturen)result;
        }
	}
}
