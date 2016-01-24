using ColorsMagic.Common.GameModel;

namespace ColorsMagic.WP.Settings
{
    public sealed class GameData
    {
        public GameColor[] Colors { get; set; }

        public GameLevel Level { get; set; }

        public int CurrentScore { get; set; }
    }
}