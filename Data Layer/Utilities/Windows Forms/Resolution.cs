using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer.Utilities.Windows_Forms
{
    public class Resolution
    {
        public string ResolutionScale { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public Resolution(int x, int y)
        {
            X = x;
            Y = y;
            ResolutionScale = x.ToString() + "x" + y.ToString();
        }
        
        public Resolution(string resolutionScale, int x, int y)
        {
            this.ResolutionScale = resolutionScale;
            X = x;
            Y = y;
        }

        public override string ToString() => ResolutionScale;
    }
}
