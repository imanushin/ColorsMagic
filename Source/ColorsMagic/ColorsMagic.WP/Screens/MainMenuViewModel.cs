using System.Windows.Input;
using Windows.UI.Xaml;
using ColorsMagic.WP.Common;

namespace ColorsMagic.WP.Screens
{
    public sealed class MainMenuViewModel
    {
        public string CreateNewGameText { get; } = "New Game";

        public ICommand CreateNewGameCommand
        {
            get
            {
                return new TaskCommand(async () =>
                {
                    await ViewModels.GameViewModel.InitGameAsync(true).ConfigureAwait(true);
                    NavigationService.Instance.Navigate(typeof (GameView));
                });
            }
        }

        public string ContinueGameText { get; } = "Continue Game";

        public Visibility ContinueGameVisible { get; } = Visibility.Visible;

        public ICommand ContinueGameCommand
        {
            get
            {
                return new TaskCommand(async () =>
                {
                    await ViewModels.GameViewModel.InitGameAsync(false).ConfigureAwait(true);
                    NavigationService.Instance.Navigate(typeof(GameView));
                });
            }
        }

        public string AboutText { get; } = "About";

        public ICommand AboutCommand
        {
            get
            {
                return new RelayCommand(_ =>
                {
                    NavigationService.Instance.Navigate(typeof(AboutView));
                });
            }
        }


        public string BuyFullVersionText { get; } = "Buy full version";

        public Visibility BuyFullVersionVisible { get; } = Visibility.Visible;

        public ICommand BuyFullVersionCommand { get; }
    }
}