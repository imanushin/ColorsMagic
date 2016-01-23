using System;
using Windows.UI;
using ColorsMagic.WP.Settings;

namespace ColorsMagic.WP
{
    public static class GameColorExtensions
    {
        public static Color GetColor(this GameColor color)
        {
            switch (color)
            {
                case GameColor.None:
                    return Color.FromArgb(255, 255, 255, 255);
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
}