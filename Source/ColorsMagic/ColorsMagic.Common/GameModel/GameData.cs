namespace ColorsMagic.Common.GameModel
{
    public sealed class GameData
    {
        public GameColor[] Colors { get; set; }

        public GameLevel Level { get; set; }

        public int CurrentScore { get; set; }
    }
}