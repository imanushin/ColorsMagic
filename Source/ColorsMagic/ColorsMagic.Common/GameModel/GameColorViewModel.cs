using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ColorsMagic.WP.Settings;
using JetBrains.Annotations;

namespace ColorsMagic.WP.Screens
{
    public sealed class GameColorViewModel : INotifyPropertyChanged
    {
        private readonly GameColor[] _realColors;
        private readonly int _index;

        public GameColorViewModel(GameColor[] realColors, int index)
        {
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
