using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexGrid {
    public static class HexHelper {

        public static double Height;
        public static double Width;

        public static double VDistance;
        public static double HDistance;

        public static double GetHeight(HexTile h) {
            if (h.Orientation == EHexGridOrientation.Pointed)
                { return h.Size * 2; }
            return Math.Sqrt(3) / 2 * h.Size * 2;
        }

        public static double GetWidth(HexTile h) {
            if (h.Orientation == EHexGridOrientation.Flat) { return h.Size * 2; }
            return Math.Sqrt(3) / 2 * GetHeight(h);
        }


        public static double GetHDistanceNeighbour(HexTile h) {
            if (h.Orientation == EHexGridOrientation.Flat) { return GetWidth(h) * 3/4; }
            return GetWidth(h);
        }

        public static double GetVDistanceNeighbour(HexTile h) {
            if (h.Orientation == EHexGridOrientation.Flat) { return GetHeight(h); }
            return GetHeight(h) * 3/4;
        }

        #region Offset Methods

        public static int OffSetX(double startX, int offset) {
            return (int)(startX + (Width * offset) / 2);
        }

        public static double OffSetY(int startY, int offset) {
            return startY + (Height * offset) / 2;
        }

        #endregion

    }
}
