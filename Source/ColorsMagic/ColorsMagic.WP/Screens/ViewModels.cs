using Windows.UI.Xaml;

namespace ColorsMagic.WP.Screens
{
    internal static class ViewModels
    {
        public static GameViewModel GameViewModel => GetViewModel<GameViewModel>();

        public static LoadingViewModel LoadingViewModel => GetViewModel<LoadingViewModel>();

        private static TValue GetViewModel<TValue>()
        {
            return (TValue)Application.Current.Resources[typeof(TValue).Name];
        }
    }
}