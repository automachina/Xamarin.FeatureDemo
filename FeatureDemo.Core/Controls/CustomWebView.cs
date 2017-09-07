// ***********************************************************************
// Assembly         : XLabs.Forms
// Author           : XLabs Team
// Created          : 12-27-2015
//
// Last Modified By : XLabs Team
// Last Modified On : 01-04-2016
// ***********************************************************************
// <copyright file="HybridWebView.cs" company="XLabs Team">
//     Copyright (c) XLabs Team. All rights reserved.
// </copyright>
// <summary>
//       This project is licensed under the Apache 2.0 license
//       https://github.com/XLabs/Xamarin-Forms-Labs/blob/master/LICENSE
//
//       XLabs is a open source project that aims to provide a powerfull and cross
//       platform set of controls tailored to work with Xamarin Forms.
// </summary>
// ***********************************************************************
//

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using FeatureDemo.Core.Helpers;
using Prism.Commands;
using Xamarin.Forms;
using XLabs;
using XLabs.Ioc;
using XLabs.Serialization;
using XLabs.Serialization.JsonNET;


namespace FeatureDemo.Forms.Controls
{
    /// <summary>
    /// The hybrid web view.
    /// </summary>
    public class CustomWebView : View
    {
        public static readonly BindableProperty UriProperty =
            BindableProperty.Create(nameof(Uri), typeof(Uri), typeof(CustomWebView), default(Uri));
        
        public static readonly BindableProperty SourceProperty =
            BindableProperty.Create(nameof(Source), typeof(WebViewSource), typeof(CustomWebView), default(WebViewSource));

        public static readonly BindableProperty CleanupProperty =
            BindableProperty.Create(nameof(CleanupCalled), typeof(bool), typeof(CustomWebView), false);

        public static readonly BindableProperty AndroidHardwareRenderingProperty =
            BindableProperty.Create(nameof(AndroidHardwareRendering), typeof(bool), typeof(CustomWebView), false);

        public static readonly BindableProperty AndroidAdditionalTouchCallbackProperty =
            BindableProperty.Create(nameof(AndroidAdditionalTouchCallback), typeof(bool), typeof(CustomWebView), true);

        public static readonly BindableProperty RegisteredActionsProperty =
            BindableProperty.Create(nameof(RegisteredActions), typeof(Dictionary<string, Action<string>>), typeof(CustomWebView),
            null, BindingMode.TwoWay, null, OnRegisteredActionsChanged, OnRegisteredActionsChanging, null, null);

		public static readonly BindableProperty RegisterCallbackCommandProperty =
			BindableProperty.Create(nameof(RegisterCallbackCommand), typeof(ICommand), typeof(CustomWebView), null, BindingMode.OneWayToSource);

        public ICommand RegisterCallbackCommand
        {
            get => (ICommand)GetValue(RegisterCallbackCommandProperty);
            set => SetValue(RegisterCallbackCommandProperty, value);
        }

        public static readonly BindableProperty RegisterNativeFunctionCommandProperty =
            BindableProperty.Create(nameof(RegisterNativeFunctionCommand), typeof(ICommand), typeof(CustomWebView), null, BindingMode.OneWayToSource);

        public ICommand RegisterNativeFunctionCommand
        {
            get => (ICommand)GetValue(RegisterNativeFunctionCommandProperty);
            set => SetValue(RegisterNativeFunctionCommandProperty, value);
        }

        public static readonly BindableProperty LoadFromContentCommandProperty =
            BindableProperty.Create(nameof(LoadFromContentCommand), typeof(ICommand), typeof(CustomWebView), null, BindingMode.OneWayToSource);

        public ICommand LoadFromContentCommand
        {
            get => (ICommand)GetValue(LoadFromContentCommandProperty);
            set => SetValue(LoadFromContentCommandProperty, value);
        }

        public static readonly BindableProperty LoadContentCommandProperty =
            BindableProperty.Create(nameof(LoadContentCommand), typeof(ICommand), typeof(CustomWebView), null, BindingMode.OneWayToSource);

        public ICommand LoadContentCommand
		{
			get => (ICommand)GetValue(LoadContentCommandProperty);
			set => SetValue(LoadContentCommandProperty, value);
		}

		ObservableDictionary<string, Action<string>> registeredActions;
		public ObservableDictionary<string, Action<string>> RegisteredActions
		{
			get => registeredActions;
			set
			{
				if (registeredActions == value) return;

				registeredActions = value;
				OnPropertyChanged(nameof(RegisteredActions));
			}
		}

        public static readonly BindableProperty RegisteredFunctionsProperty =
            BindableProperty.Create(nameof(RegisteredFunctions), typeof(Dictionary<string, Func<string, object[]>>), typeof(CustomWebView),
                                    null, BindingMode.TwoWay, null, OnRegisteredFunctionsChanged, OnRegisteredFunctionsChanging, null, null);
        
		ObservableDictionary<string, Func<string, object[]>> registeredFunctions;
		public ObservableDictionary<string, Func<string, object[]>> RegisteredFunctions
		{
			get => registeredFunctions;
			set
			{
				if (registeredFunctions == value) return;

				registeredFunctions = value;
				OnPropertyChanged(nameof(RegisteredFunctions));
			}
		}

        static void OnRegisteredActionsChanging(BindableObject bindable, object oldValue, object newValue)
        {
            
        }

        static void OnRegisteredActionsChanged(BindableObject bindable, object oldValue, object newValue)
        {
            
        }

		static void OnRegisteredFunctionsChanging(BindableObject bindable, object oldValue, object newValue)
		{

		}

		static void OnRegisteredFunctionsChanged(BindableObject bindable, object oldValue, object newValue)
		{

		}


        /// <summary>
        /// The java script load requested
        /// </summary>
        internal EventHandler<string> JavaScriptLoadRequested;

        /// <summary>
        /// The load from content requested
        /// </summary>
        internal EventHandler<LoadContentEventArgs> LoadFromContentRequested;

        /// <summary>
        /// The load content requested
        /// </summary>
        internal EventHandler<LoadContentEventArgs> LoadContentRequested;

        /// <summary>
        /// The left swipe
        /// </summary>
        public EventHandler LeftSwipe;

        /// <summary>
        /// The load finished
        /// </summary>
        public EventHandler LoadFinished;

        /// <summary>
        /// The navigating
        /// </summary>
        public EventHandler<EventArgs<Uri>> Navigating;

        /// <summary>
        /// The right swipe
        /// </summary>
        public EventHandler RightSwipe;

        public EventHandler DownSwipe;

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

		public event EventHandler DownSwipeEvent
		{
			add => DownSwipe += value;
			remove => DownSwipe -= value;
		}

		readonly object injectLock = new object();

        /// <summary>
        /// The JSON serializer.
        /// </summary>
        readonly IJsonSerializer jsonSerializer;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomWebView" /> class.
        /// </summary>
        /// <remarks>HybridWebView will use <see cref="IJsonSerializer" /> configured
        /// with <see cref="Resolver"/> or <see cref="DependencyService"/>. 
        /// If neither one resolves it then <see cref="SystemJsonSerializer"/> will be used.</remarks>
        public CustomWebView() : this((Resolver.IsSet ? Resolver.Resolve<IJsonSerializer>() : null)
                                      ?? DependencyService.Get<IJsonSerializer>() ?? new JsonSerializer())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomWebView" /> class.
        /// </summary>
        /// <param name="jsonSerializer">The JSON serializer.</param>
        public CustomWebView(IJsonSerializer jsonSerializer)
        {
            this.jsonSerializer = jsonSerializer;
            this.registeredActions = new ObservableDictionary<string, Action<string>>();
            this.registeredFunctions = new ObservableDictionary<string, Func<string, object[]>>();
			RegisterCallbackCommand =
				new DelegateCommand<(string, Action<string>)?>(RegisterCallbackDelegate);

			RegisterNativeFunctionCommand =
				new DelegateCommand<(string, Func<string, object[]>)?>(RegisterNativeFunctionDelegate);

			LoadFromContentCommand =
				new DelegateCommand<(string, string)?>(LoadFromContentDelegate);

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
			if (!string.IsNullOrEmpty(args.contentFullName))
				LoadFromContent(args.contentFullName, args.baseUrl);
		}

		void LoadContentDelegate((string content, string baseUrl)? _args)
		{
			if (!_args.HasValue) return;

			var args = _args.Value;
			if (!string.IsNullOrEmpty(args.content))
				LoadContent(args.content, args.baseUrl);
		}

        /// <summary>
        /// Gets or sets the uri.
        /// </summary>
        /// <value>The URI.</value>
        public Uri Uri
        {
            get { return (Uri)GetValue(UriProperty); }
            set { SetValue(UriProperty, value); }
        }

        /// <summary>
        /// Gets or sets the source.
        /// </summary>
        /// <value>The source.</value>
        public WebViewSource Source
        {
            get { return (WebViewSource)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        /// <summary>
        /// Gets or sets the cleanup called flag.
        /// </summary>
        public bool CleanupCalled
        {
            get { return (bool)GetValue(CleanupProperty); }
            set { SetValue(CleanupProperty, value); }
        }

        /// <summary>
        /// Gets or sets android hardware rendering flag.
        /// </summary>
        public bool AndroidHardwareRendering
        {
            get { return (bool)GetValue(AndroidHardwareRenderingProperty); }
            set { SetValue(AndroidHardwareRenderingProperty, value); }
        }

        /// <summary>
        /// Gets or sets android additional touch callback. 
        /// </summary>
        public bool AndroidAdditionalTouchCallback
        {
            get { return (bool)GetValue(AndroidAdditionalTouchCallbackProperty); }
            set { SetValue(AndroidAdditionalTouchCallbackProperty, value); }
        }

        /// <summary>
        /// Registers a native callback.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="action">The action.</param>
        public void RegisterCallback(string name, Action<string> action)
        {
            this.registeredActions.Add(name, action);
        }

        /// <summary>
        /// Removes a native callback.
        /// </summary>
        /// <param name="name">The name of the callback.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool RemoveCallback(string name)
        {
            return this.registeredActions.Remove(name);
        }

        /// <summary>
        /// Registers a native callback and returns data to closure.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="func">The function.</param>
        public void RegisterNativeFunction(string name, Func<string, object[]> func)
        {
            this.registeredFunctions.Add(name, func);
        }

        /// <summary>
        /// Removes a native callback function.
        /// </summary>
        /// <param name="name">The name of the callback.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool RemoveNativeFunction(string name)
        {
            return this.registeredFunctions.Remove(name);
        }

        /// <summary>
        /// Loads from file.
        /// </summary>
        /// <param name="contentFullName">Full name of the content.</param>
        /// <param name="baseUri">Optional base Uri to use for resources.</param>
        public void LoadFromContent(string contentFullName, string baseUri = null)
        {
            this.LoadFromContentRequested?.Invoke(this, new LoadContentEventArgs(contentFullName, baseUri));
        }

        /// <summary>
        /// Loads the content from string content.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="baseUri">Optional base Uri to use for resources.</param>
        public void LoadContent(string content, string baseUri = null)
        {
            this.LoadContentRequested?.Invoke(this, new LoadContentEventArgs(content, baseUri));
        }

        /// <summary>
        /// Injects the java script.
        /// </summary>
        /// <param name="script">The script.</param>
        public void InjectJavaScript(string script)
        {
            lock (this.injectLock)
            {
                this.JavaScriptLoadRequested?.Invoke(this, script);
            }
        }

        /// <summary>
        /// Calls the js function.
        /// </summary>
        /// <param name="funcName">Name of the function.</param>
        /// <param name="parameters">The parameters.</param>
        public void CallJsFunction(string funcName, params object[] parameters)
        {
            var builder = new StringBuilder();

            builder.Append(funcName);
            builder.Append("(");

            for (var n = 0; n < parameters.Length; n++)
            {
                builder.Append(this.jsonSerializer.Serialize(parameters[n]));
                if (n < parameters.Length - 1)
                {
                    builder.Append(", ");
                }
            }

            builder.Append(");");

            InjectJavaScript(builder.ToString());
        }

        /// <summary>
        /// Tries the get action.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="action">The action.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        internal bool TryGetAction(string name, out Action<string> action)
        {
            return this.registeredActions.TryGetValue(name, out action);
        }

        /// <summary>
        /// Tries the get function.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="func">The function.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        internal bool TryGetFunc(string name, out Func<string, object[]> func)
        {
            return this.registeredFunctions.TryGetValue(name, out func);
        }

        /// <summary>
        /// Handles the <see cref="E:LoadFinished" /> event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        internal void OnLoadFinished(object sender, EventArgs e)
        {
            this.LoadFinished?.Invoke(this, e);
        }

        /// <summary>
        /// Handles the <see cref="E:LeftSwipe" /> event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        internal void OnLeftSwipe(object sender, EventArgs e)
        {
            this.LeftSwipe?.Invoke(this, e);
        }

        /// <summary>
        /// Handles the <see cref="E:RightSwipe" /> event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        internal void OnRightSwipe(object sender, EventArgs e)
        {
            this.RightSwipe?.Invoke(this, e);
        }

        internal void OnDownSwipe(object sender, EventArgs e)
        {
            this.DownSwipe?.Invoke(this, e);
        }

        /// <summary>
        /// Called when [navigating].
        /// </summary>
        /// <param name="uri">The URI.</param>
        internal void OnNavigating(Uri uri)
        {
            this.Navigating?.Invoke(this, new EventArgs<Uri>(uri));
        }

        internal void MessageReceived(string message)
        {
            var m = this.jsonSerializer.Deserialize<Message>(message);

            if (m?.Action == null) return;

            Action<string> action;

            if (this.TryGetAction(m.Action, out action))
            {
                action.Invoke(m.Data?.ToString());
                return;
            }

            Func<string, object[]> func;

            if (this.TryGetFunc(m.Action, out func))
            {
                Task.Run(() =>
                {
                    var result = func.Invoke(m.Data.ToString());
                    this.CallJsFunction($"NativeFuncs[{m.Callback}]", result);
                });
            }
        }

        /// <summary>
        /// Remove all Callbacks from this view
        /// </summary>
        public void RemoveAllCallbacks()
        {
            this.registeredActions.Clear();
        }

        /// <summary>
        /// Remove all Functions from this view
        /// </summary>
        public void RemoveAllFunctions()
        {
            this.registeredFunctions.Clear();
        }

        /// <summary>
        ///  Called to immediately free the native web view and
        /// disconnect all callbacks
        /// Note that this web view object will no longer be usable
        /// after this call!
        /// </summary>
        public void Cleanup()
        {
            // This removes the delegates that point to the renderer
            this.JavaScriptLoadRequested = null;
            this.LoadFromContentRequested = null;
            this.LoadContentRequested = null;
            this.Navigating = null;

            // Remove all callbacks
            this.registeredActions.Clear();
            this.registeredFunctions.Clear();

            // Cleanup the native stuff
            CleanupCalled = true;
        }

        internal class LoadContentEventArgs : EventArgs
        {
            internal LoadContentEventArgs(string content, string baseUri)
            {
                this.Content = content;
                this.BaseUri = baseUri;
            }

            public string Content { get; private set; }
            public string BaseUri { get; private set; }
        }

        /// <summary>
        /// Message class for transporting JSON objects.
        /// </summary>
        //[DataContract]
        private class Message
        {
            //[DataMember(Name = "a")]
            public string Action { get; set; }
            //[DataMember(Name = "d")]
            public object Data { get; set; }
            //[DataMember(Name = "c")]
            public string Callback { get; set; }
        }
    }
}
