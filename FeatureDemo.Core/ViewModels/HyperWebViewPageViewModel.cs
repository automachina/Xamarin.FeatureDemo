using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using FeatureDemo.Core.Controls;
using Prism.Commands;
using Xamarin.Forms;

namespace FeatureDemo.Core.ViewModels
{
    public class HyperWebViewPageViewModel : BaseViewModel
    {
        HyperWebView webView;

        public DelegateCommand NavigatingCommand { get; set; }
        public DelegateCommand LoadFinishedCommand { get; set; }
        public DelegateCommand LeftSwipeCommand { get; set; }
        public DelegateCommand RightSwipeCommand { get; set; }
        public DelegateCommand<object> OnAppearingCommand { get; set; }


        DelegateCommand<(string, Action<string>)?> _registerCallbackCommand; 
        public DelegateCommand<(string,Action<string>)?> RegisterCallbackCommand
        {
            get => _registerCallbackCommand;
            set => SetProperty(ref _registerCallbackCommand, value); 
        }

        DelegateCommand<(string, Func<string, object[]>)?> _registerNativeFunctionCommand;
        public DelegateCommand<(string,Func<string,object[]>)?> RegisterNativeFunctionCommand 
        {
            get => _registerNativeFunctionCommand;
            set => SetProperty(ref _registerNativeFunctionCommand, value);
        }

        DelegateCommand<(string, string)?> _loadFromContentCommand;
        public DelegateCommand<(string, string)?> LoadFromContentCommand
		{
            get => _loadFromContentCommand;
            set => SetProperty(ref _loadFromContentCommand, value);
		}

        DelegateCommand<(string, string)?> _loadContentCommand;
        public DelegateCommand<(string, string)?> LoadContentCommand
		{
            get => _loadContentCommand;
            set => SetProperty(ref _loadContentCommand, value);
		}

        Uri _uri; public Uri Uri
		{
			get => _uri;
			set => SetProperty(ref _uri, value);
		}

        WebViewSource _source; public WebViewSource Source
        {
            get => _source;
            set => SetProperty(ref _source, value);
        }

		bool isLoading; public bool IsLoading
		{
			get => isLoading;
			set => SetProperty(ref isLoading, value);
		}

        public HyperWebViewPageViewModel()
        {
            Title = "HyperWebView";

			NavigatingCommand = new DelegateCommand(Navigating);
            LoadFinishedCommand = new DelegateCommand(LoadingFinished);
            LeftSwipeCommand = new DelegateCommand(LeftSwipe);
            RightSwipeCommand = new DelegateCommand(RightSwipe);
            OnAppearingCommand = new DelegateCommand<object>(OnAppearing);

            //RegisterCallbackCommand = new DelegateCommand<(string, Action<string>)?>((obj) => Debug.WriteLine($"Calling RegisterCallback DelegateCommand.") );
            //RegisterNativeFunctionCommand = new DelegateCommand<(string, Func<string, object[]>)?>((obj) => Debug.WriteLine($"Calling RegisterNativeFunction DelegateCommand."));

            IsLoading = false;

            Uri = new Uri("https://nwcu.com",UriKind.Absolute);
            Source = "https://nwcu.com"; 
        }

        void OnAppearing(object obj)
        {
            Debug.WriteLine("OnAppearing!");
            if (obj is HyperWebView)
                webView = obj as HyperWebView;
        }

        void RightSwipe()
        {
            Debug.WriteLine("Swipped Right!");
        }

        void LeftSwipe()
        {
            Debug.WriteLine("Swipped Left!");
        }

        void Navigating()
		{
			IsLoading = true;
		}

        void LoadingFinished()
        {
            Debug.WriteLine("LoadingFinished!");
            IsLoading = false;
        }

        void RegisterAllCallbacks()
        {
            RegisterCallback("callCS",(obj) => Debug.WriteLine("callCS called from JS!!"));
        }

        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);
            Debug.WriteLine(args.PropertyName);
            switch (args.PropertyName)
            {
                case "RegisterCallbackCommand":
                    Debug.WriteLineIf(RegisterCallbackCommand != null ,"RegisterCallback has been bound to the component!");
                    RegisterAllCallbacks();
                    break;
				case "RegisterNativeFunctionCommand":
					Debug.WriteLineIf(RegisterCallbackCommand != null, "RegisterNativeFunction has been bound to the component!");
					break;
				case "LoadFromContentCommand":
					Debug.WriteLineIf(RegisterCallbackCommand != null, "LoadFromContent has been bound to the component!");
					break;
				case "LoadContentComand":
					Debug.WriteLineIf(RegisterCallbackCommand != null, "LoadContent has been bound to the component!");
					break;
                default:
                    break;
            }
        }

		public override void OnNavigatedTo(Prism.Navigation.NavigationParameters parameters)
		{
			string urlString;
			if (parameters.ContainsKey("url"))
			{
				urlString = parameters.GetValue<string>("url");
                if (Uri.TryCreate(urlString, UriKind.Absolute, out _uri))
				{
					RaisePropertyChanged("Url");
				}

			}

			if (parameters.ContainsKey("title"))
			{
				Title = parameters.GetValue<string>("title");
			}
		}

		void RegisterCallback(string name, Action<string> action)
		{
            webView?.RegisterCallback(name, action);
		}

		void RegisterNativeFunction(string name, Func<string, object[]> func)
		{
			RegisterNativeFunctionCommand?.Execute((name, func));
		}

		void LoadFromContent(string contentFullName, string url = null)
		{
			LoadFromContentCommand?.Execute((contentFullName, url));
		}

		void LoadContent(string content, string url = null)
		{
			LoadContentCommand?.Execute((content, url));
		}
	}
}

