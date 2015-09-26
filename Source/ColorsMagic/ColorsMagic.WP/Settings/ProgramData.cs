using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorsMagic.WP.Settings
{
    public sealed class ProgramData
    {
        public GameData CurrentGame
        {
            get;
            set;
        }

        public int TriangleSize { get; } = 10;
    }
}
