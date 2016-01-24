using JetBrains.Annotations;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using CheckContracts;

namespace ColorsMagic.Common.GameModel
{
    public sealed class GameColorViewModel : INotifyPropertyChanged
    {
        private readonly GameColor[] _realColors;
        private readonly int _index;

        public GameColorViewModel([NotNull] GameColor[] realColors, int index)
        {
            Validate.ArgumentIsNotNull(realColors, nameof(realColors));

            _realColors = realColors;
            _index = index;
        }

        public GameColor Color => _realColors[_index];


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
