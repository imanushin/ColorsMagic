﻿using System.Threading.Tasks;
using ColorsMagic.WP.GameModel;
using ColorsMagic.WP.Settings;

namespace ColorsMagic.WP.Screens
{
    public sealed class GameViewModel
    {
        private ProgramData _settings;

        public async Task InitGameAsync()
        {
            _settings = await SettingsManager.Instance.GetCurrentData().ConfigureAwait(true);

            if (ReferenceEquals(_settings.CurrentGame, null))
            {
                _settings.CurrentGame = CreateNewGame();

                await SettingsManager.Instance.SaveCurrentAsync().ConfigureAwait(true);
            }

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
    }
}