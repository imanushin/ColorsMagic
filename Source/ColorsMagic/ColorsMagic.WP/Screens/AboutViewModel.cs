using System.Windows.Input;

namespace ColorsMagic.WP.Screens
{
    public sealed class AboutViewModel
    {
        public string ApplicationInfo { get; } = "APP!!!!";

        public string RateApplicationText { get; } = "Rate application";

        public ICommand RateApplicationCommand { get; }

        public string WriteToAuthorsText { get; } = "About";

        public ICommand WriteToAuthorsCommand { get; }
    }
}