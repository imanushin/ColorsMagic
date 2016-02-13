using CheckContracts;
using JetBrains.Annotations;

namespace ColorsMagic.Common.GameModel
{
    public sealed class GameModel
    {
        public GameModel([NotNull] GameData data)
        {
            Validate.ArgumentIsNotNull(data, nameof(data));

            Data = data;
            Graph = new CellsGraph(this);
        }

        public GameData Data { get; }

        public CellsGraph Graph { get; }

    }
}