using System.ComponentModel;

namespace FeatureDemo.Model.Client
{
    public enum ActionType
    {
        [Description("Open Url or Http source in WebView")]
        WebView,
        [Description("Open Url in native browser")]
        Browser,
        [Description("Launches Dialer with number.")]
        Call,
        [Description("Create new SMS targeting a number")]
        Sms,
        [Description("Launch native email client")]
        Email,
        [Description("Navigate to Native Screen")]
        Navigate
    }
}
