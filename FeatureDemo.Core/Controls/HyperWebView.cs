using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Input;
using FeatureDemo.Core.Extensions;
using FeatureDemo.Core.Helpers;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Unity;
using Xamarin.Forms;
using XLabs;
using XLabs.Forms.Controls;
using XLabs.Serialization;
using unity = Microsoft.Practices.Unity;

namespace FeatureDemo.Core.Controls
{
    public class HyperWebView : HybridWebView
    {
		static unity.IUnityContainer _container;
		[unity.Dependency]
		internal static unity.IUnityContainer Container
		{
			get => _container;
            set => _container = value;
		}

        public static readonly BindableProperty RegisterCallbackCommandProperty =
            BindableProperty.Create(nameof(RegisterCallbackCommand), typeof(ICommand), typeof(HyperWebView), null, BindingMode.OneWayToSource);

        public ICommand RegisterCallbackCommand
        {
            get => (ICommand)GetValue(RegisterCallbackCommandProperty);
            set => SetValue(RegisterCallbackCommandProperty, value);
        }

		public static readonly BindableProperty RegisterNativeFunctionCommandProperty =
			BindableProperty.Create(nameof(RegisterNativeFunctionCommand), typeof(ICommand), typeof(HyperWebView), null, BindingMode.OneWayToSource);

		public ICommand RegisterNativeFunctionCommand
		{
			get => (ICommand)GetValue(RegisterNativeFunctionCommandProperty);
			set => SetValue(RegisterNativeFunctionCommandProperty, value);
		}

        public static readonly BindableProperty LoadFromContentCommandProperty =
            BindableProperty.Create(nameof(LoadFromContentCommand), typeof(ICommand), typeof(HyperWebView), null, BindingMode.OneWayToSource);

        public ICommand LoadFromContentCommand
        {
            get => (ICommand)GetValue(LoadFromContentCommandProperty);
            set => SetValue(LoadFromContentCommandProperty, value);
        }

		public static readonly BindableProperty LoadContentCommandProperty =
			BindableProperty.Create(nameof(LoadContentCommand), typeof(ICommand), typeof(HyperWebView), null, BindingMode.OneWayToSource);

		public ICommand LoadContentCommand
		{
			get => (ICommand)GetValue(LoadContentCommandProperty);
			set => SetValue(LoadContentCommandProperty, value);
		}

		public static readonly BindableProperty RegisteredActionsProperty =
            BindableProperty.Create(nameof(RegisteredActions), typeof(Dictionary<string, Action<string>>), typeof(HyperWebView),
		   null, BindingMode.TwoWay, null, OnRegisteredActionsChanged, OnRegisteredActionsChanging, null, null);

		public Dictionary<string, Action<string>> RegisteredActions
		{
			get
			{
				var methodName = MethodBase.GetCurrentMethod().Name;
                return this.GetPrivateFieldValue<Dictionary<string, Action<string>>>(methodName, true);
			}

            set
            {
                var methodName = MethodBase.GetCurrentMethod().Name;
                var currentValue = this.GetPrivateFieldValue<Dictionary<string, Action<string>>>(methodName, true);
                if (currentValue == value) return;

                this.SetPrivateFieldValue(methodName, value, true);
                OnPropertyChanged(nameof(methodName));
            }
		}

		static void OnRegisteredActionsChanging(BindableObject bindable, object oldValue, object newValue)
		{

		}

		static void OnRegisteredActionsChanged(BindableObject bindable, object oldValue, object newValue)
		{

		}

		public static readonly BindableProperty RegisteredFunctionsProperty =
			BindableProperty.Create(nameof(RegisteredFunctions), typeof(Dictionary<string, Func<string, object[]>>), typeof(HyperWebView),
									null, BindingMode.TwoWay, null, OnRegisteredFunctionsChanged, OnRegisteredFunctionsChanging, null, null);

		public Dictionary<string, Func<string, object[]>> RegisteredFunctions
		{
			get
			{
                var methodName = MethodBase.GetCurrentMethod().Name.TrimStart("get_".ToCharArray());
                return this.GetPrivateFieldValue<Dictionary<string, Func<string, object[]>>>(methodName, true);
			}

			set
			{
				var methodName = MethodBase.GetCurrentMethod().Name;
                var currentValue = this.GetPrivateFieldValue<Dictionary<string, Func<string, object[]>>>(methodName, true);
				if (currentValue == value) return;

				this.SetPrivateFieldValue(methodName, value, true);
				OnPropertyChanged(nameof(methodName));
			}
		}

		static void OnRegisteredFunctionsChanging(BindableObject bindable, object oldValue, object newValue)
		{

		}

		static void OnRegisteredFunctionsChanged(BindableObject bindable, object oldValue, object newValue)
		{

		}

        public event EventHandler<EventArgs<Uri>> NavigatingEvent
        {
            add => Navigating += value;
            remove => Navigating -= value;
        }

        public event EventHandler LoadFinishedEvent
        {
            add => LoadFinished += value;
            remove => LoadFinished -= value;
        }

        public event EventHandler LeftSwipeEvent
        {
            add => LeftSwipe += value;
            remove => LeftSwipe -= value;
        }

        public event EventHandler RightSwipeEvent
        {
            add => RightSwipe += value;
            remove => RightSwipe -= value;
        }


        public HyperWebView() : this(Container?.Resolve<IJsonSerializer>())
        {
        }

        public HyperWebView(IJsonSerializer jsonSerializer) : base(jsonSerializer)
        {
            RegisterCallbackCommand = 
                new DelegateCommand<(string, Action<string>)?>(RegisterCallbackDelegate);

            RegisterNativeFunctionCommand = 
                new DelegateCommand<(string, Func<string, object[]>)?>(RegisterNativeFunctionDelegate);

            LoadFromContentCommand =
                new DelegateCommand<(string,string)?>(LoadFromContentDelegate);

            LoadContentCommand =
                new DelegateCommand<(string, string)?>(LoadContentDelegate);

        }

        void RegisterCallbackDelegate((string name, Action<string> action)? _args)
        {
            if (!_args.HasValue) return;

            var args = _args.Value;
            if (!string.IsNullOrEmpty(args.name) && args.action != null && !TryGetAction(args.name, out var action))
			{
                RegisterCallback(args.name, args.action);
			}
        }

        void RegisterNativeFunctionDelegate((string name, Func<string, object[]> func)? _args)
        {
			if (!_args.HasValue) return;

			var args = _args.Value;
            if (!string.IsNullOrEmpty(args.name) && args.func != null && TryGetFunc(args.name, out var func))
			{
                RegisterNativeFunction(args.name, args.func);
			}
        }

        void LoadFromContentDelegate((string contentFullName, string baseUrl)? _args)
		{
			if (!_args.HasValue) return;

			var args = _args.Value;
            if(!string.IsNullOrEmpty(args.contentFullName))
                LoadFromContent(args.contentFullName, args.baseUrl);
        }

        void LoadContentDelegate((string content, string baseUrl)? _args)
		{
			if (!_args.HasValue) return;

			var args = _args.Value;
            if(!string.IsNullOrEmpty(args.content))
                LoadContent(args.content, args.baseUrl);
        }

        public bool TryGetAction(string name, out Action<string> action)
        {
            var methodName = MethodBase.GetCurrentMethod().Name;
            var parameters = new object[]{ name, null };
            var result = this.InvokePrivateMethod<bool>(methodName, parameters);
            action = result ? (Action<string>)parameters[1] : null;
            return result;
        }

        public bool TryGetFunc(string name, out Func<string, object[]> func)
        {
			var methodName = MethodBase.GetCurrentMethod().Name;
			var parameters = new object[] { name, null };
            var result = this.InvokePrivateMethod<bool>(methodName, parameters);
            func = result ? (Func<string, object[]>)parameters[1] : null;
            return result;
        }
    }
}
