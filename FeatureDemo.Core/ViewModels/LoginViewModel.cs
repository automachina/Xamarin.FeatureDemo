using System.Threading.Tasks;
using Prism.Commands;
using Prism.Navigation;

namespace FeatureDemo.Core.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        INavigationService _navigationService;

        public LoginViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            SignInCommand = new DelegateCommand(async () => await SignIn());
            NotNowCommand = new DelegateCommand(async () => await _navigationService.NavigateAsync("Root"));
        }

        string message = string.Empty;
        public string Message
        {
            get { return message; }
            set { SetProperty(ref message, value); }
        }

        public DelegateCommand NotNowCommand { get; }
        public DelegateCommand SignInCommand { get; }

        async Task SignIn()
        {
            try
            {
                IsBusy = true;
                Message = "Signing In...";

                // Log the user in
                await TryLoginAsync();
            }
            finally
            {
                Message = string.Empty;
                IsBusy = false;

                if (Settings.IsLoggedIn)
                    await _navigationService.NavigateAsync("Root");
            }
        }

        public static async Task<bool> TryLoginAsync()
        {
            return await Task.FromResult(true);
        }
    }
}
