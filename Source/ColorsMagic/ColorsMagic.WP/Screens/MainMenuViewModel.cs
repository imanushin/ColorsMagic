using Windows.UI.Xaml;

namespace ColorsMagic.WP.Screens
{
    public sealed class MainMenuViewModel
    {
        public string CreateNewGameText { get; } = "New Game";

        public string ContinueGameText { get; } = "Continue Game";

        public Visibility ContinueGameVisible { get; } = Visibility.Visible;

        public string BuyFullVersionText { get; } = "Buy full version";

    }
}