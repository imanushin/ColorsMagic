using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;
using ColorsMagic.Common.GameModel;
using ColorsMagic.WP.Common;
using ColorsMagic.WP.Settings;

namespace ColorsMagic.WP.Screens
{
    public sealed class GameViewModel
    {
        private ProgramData _settings;

        public GameColorViewModel[] GameColors { get; private set; } = new GameColorViewModel[0];

        public async Task InitGameAsync(bool forceNewGame)
        {
            _settings = await SettingsManager.Instance.GetCurrentData().ConfigureAwait(true);

            if (forceNewGame || ReferenceEquals(_settings.CurrentGame, null))
            {
                _settings.CurrentGame = CreateNewGame();

                await SettingsManager.Instance.SaveCurrentAsync().ConfigureAwait(true);
            }

            var gameColors = _settings.CurrentGame.Colors;
            GameColors = gameColors.Select((v, i) => new GameColorViewModel(gameColors, i)).ToArray();
        }

        private GameData CreateNewGame()
        {
            var triangleSize = _settings.TriangleSize;
            var trianglesCount = PositionHelper.GetCellsCount(triangleSize);
            var colors = new GameColor[trianglesCount];

            colors[PositionHelper.GetPosition(trianglesCount, GamePosition.Top)] = GameColor.Red;
            colors[PositionHelper.GetPosition(trianglesCount, GamePosition.BottomLeft)] = GameColor.Blue;
            colors[PositionHelper.GetPosition(trianglesCount, GamePosition.BottomRight)] = GameColor.Green;

            colors[PositionHelper.GetPosition(trianglesCount, GamePosition.CenterLeft)] = GameColor.Pink;
            colors[PositionHelper.GetPosition(trianglesCount, GamePosition.CenterRight)] = GameColor.LightBlue;
            colors[PositionHelper.GetPosition(trianglesCount, GamePosition.BottomCenter)] = GameColor.Yellow;

            return new GameData()
            {
                Colors = colors,
                CurrentScore = 0,
                Level = GameLevel.Newbe
            };
        }
        public ICommand GoToMenuCommand
        {
            get
            {
                return new RelayCommand(_ =>
                {
                    NavigationService.Instance.Navigate(typeof(MainMenuView));
                });
            }
        }

        public string GoToMenuText { get; } = "Main Menu";
    }
}