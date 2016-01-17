﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI;
using ColorsMagic.WP.Common;
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

        public Color Color
        {
            get
            {
                switch (_realColors[_index])
                {
                    case GameColor.None:
                        return Color.FromArgb(0, 0, 0, 0);
                    case GameColor.Red:
                        return Color.FromArgb(255, 255, 0, 0);
                    case GameColor.Green:
                        return Color.FromArgb(255, 0, 255, 0);
                    case GameColor.Blue:
                        return Color.FromArgb(255, 0, 0, 255);
                    case GameColor.Pink:
                        return Color.FromArgb(255, 255, 0, 255);
                    case GameColor.LightBlue:
                        return Color.FromArgb(255, 0, 255, 255);
                    case GameColor.Yellow:
                        return Color.FromArgb(255, 255, 255, 0);
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}