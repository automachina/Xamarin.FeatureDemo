using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using FeatureDemo.Core.Controls;
using FeatureDemo.Forms.Controls;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms;

namespace FeatureDemo.Core.ViewModels
{
    public class CustomWebViewPageViewModel : BaseViewModel
    {
        CustomWebView webView;

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

        HtmlWebViewSource _source; public HtmlWebViewSource Source
        {
            get => _source;
            set => SetProperty(ref _source, value);
        }

		bool isLoading; public bool IsLoading
		{
			get => isLoading;
			set => SetProperty(ref isLoading, value);
		}

        public CustomWebViewPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "CustomWebView";

			NavigatingCommand = new DelegateCommand(Navigating);
            LoadFinishedCommand = new DelegateCommand(LoadingFinished);
            LeftSwipeCommand = new DelegateCommand(LeftSwipe);
            RightSwipeCommand = new DelegateCommand(RightSwipe);
            OnAppearingCommand = new DelegateCommand<object>(OnAppearing);

            //RegisterCallbackCommand = new DelegateCommand<(string, Action<string>)?>((obj) => Debug.WriteLine($"Calling RegisterCallback DelegateCommand.") );
            //RegisterNativeFunctionCommand = new DelegateCommand<(string, Func<string, object[]>)?>((obj) => Debug.WriteLine($"Calling RegisterNativeFunction DelegateCommand."));

            IsLoading = false;
            //Source = new Xamarin.Forms.HtmlWebViewSource();
            //Source.BaseUrl = "http://localhost:5000/index.html";
            Uri = new Uri("http://10.0.66.8:5000/index.html", UriKind.Absolute);
        }

        void OnAppearing(object obj)
        {
            Debug.WriteLine("OnAppearing!");
            if (obj is CustomWebView)
                webView = obj as CustomWebView;

            RegisterAllCallbacks();
        }

        void RightSwipe()
        {
            Debug.WriteLine("Swipped Right!");
            _navigationService.GoBackAsync();
        }

        void LeftSwipe()
        {
            Debug.WriteLine("Swipped Left!");
            _navigationService.GoBackAsync();
        }

        void Navigating()
		{
            Debug.WriteLine("Is Navigating!");
			IsLoading = true;
		}

        void LoadingFinished()
        {
            Debug.WriteLine("LoadingFinished!");
            IsLoading = false;
        }

        async Task<bool> GoBack()
        {
            return await _navigationService.GoBackAsync(null, true, true);
        }

        void RegisterAllCallbacks()
        {
            RegisterCallback("callCS",(obj) => Debug.WriteLine($"callCS called from JS!!: {obj}"));
            RegisterCallback("navigateBack", (obj) => {
                Debug.WriteLine($"navigateBack Called from JS!");
                GoBack();
            });
        }

        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);
            Debug.WriteLine(args.PropertyName);
            switch (args.PropertyName)
            {
                case "RegisterCallbackCommand":
                    Debug.WriteLineIf(RegisterCallbackCommand != null ,"RegisterCallback has been bound to the component!");
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
            //webView?.RegisterCallback(name, action);
            RegisterCallbackCommand?.Execute((name, action));
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

