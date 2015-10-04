using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Media;
using ColorsMagic.WP.Common;
using Microsoft.Xaml.Interactivity;

namespace ColorsMagic.WP.Screens
{
    public sealed class LoadingViewModel
    {
        public string LoadingText { get; } = "Loading...";

        public ICommand LoadingCommand => new TaskCommand(OpenFirstPageAsync);

        private static async Task OpenFirstPageAsync()
        {
            await ViewModels.GameViewModel.InitGameAsync(false).ConfigureAwait(true);

            NavigationService.Instance.Navigate(typeof(GameView));
        }
    }
}