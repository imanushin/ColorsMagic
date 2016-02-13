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
        private GameModel _currentModel;

        public GameColorViewModel[] GameColors { get; private set; } = new GameColorViewModel[0];

        public async Task InitGameAsync(bool forceNewGame)
        {
            _settings = await SettingsManager.Instance.GetCurrentData().ConfigureAwait(true);

            if (forceNewGame || ReferenceEquals(_settings.CurrentGame, null))
            {
                _currentModel = CreateNewGame();
                _settings.CurrentGame = _currentModel.Data;

                await SettingsManager.Instance.SaveCurrentAsync().ConfigureAwait(true);
            }
            else
            {
                _currentModel = new GameModel(_settings.CurrentGame);
            }

            var gameColors = _currentModel.Data.Colors;
            GameColors = gameColors.Select((v, i) => new GameColorViewModel(gameColors, i)).ToArray();
        }

        private GameModel CreateNewGame()
        {
            var triangleSize = _settings.TriangleSize;
            var trianglesCount = PositionHelper.GetCellsCount(triangleSize);
            var colors = new GameColor[trianglesCount];

            colors[PositionHelper.GetPosition(trianglesCount, GamePosition.Top).Index] = GameColor.Red;
            colors[PositionHelper.GetPosition(trianglesCount, GamePosition.BottomLeft).Index] = GameColor.Blue;
            colors[PositionHelper.GetPosition(trianglesCount, GamePosition.BottomRight).Index] = GameColor.Green;

            colors[PositionHelper.GetPosition(trianglesCount, GamePosition.CenterLeft).Index] = GameColor.Pink;
            colors[PositionHelper.GetPosition(trianglesCount, GamePosition.CenterRight).Index] = GameColor.LightBlue;
            colors[PositionHelper.GetPosition(trianglesCount, GamePosition.BottomCenter).Index] = GameColor.Yellow;

            var newGameData = new GameData()
            {
                Colors = colors,
                CurrentScore = 0,
                Level = GameLevel.Newbe
            };

            return new GameModel(newGameData);
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