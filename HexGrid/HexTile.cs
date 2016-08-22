using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace HexGrid {

    public enum EHexGridOrientation {
        Pointed,
        Flat
    }

    public class HexTile {

        public PointCollection Points;

        public EHexGridOrientation Orientation;

        //These are the centers, actual positions;
        public double X, Y;

        public int gridX, gridY;

        //Distance from center to corner
        public double Size;

        //public double Width;

        //public double Height;

        //public double VDistanceToNeigh { get {  } }

        //public double HDistanceToNeigh { get {  } }

        public HexTile(int x, int y, int size, EHexGridOrientation Ori) {
            X = x + 32;
            Y = y + 32;
            Size = size;
            Orientation = Ori;
            Points = HexCorners();
        }

        private PointCollection HexCorners() {
            PointCollection p = new PointCollection();
            for (int i = 0; i < 6; i++) {
                double angledeg;
                if (Orientation == EHexGridOrientation.Flat) {
                    angledeg = 60 * i;
                }
                else {
                    angledeg = 60 * i + 30;
                }
                
                var anglerad = Math.PI / 180 * angledeg;
                p.Add(new System.Windows.Point(X + Size * Math.Cos(anglerad), Y + Size * Math.Sin(anglerad)));
            }
            return p;
        }

    }
}
