using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace HexGrid {

    public enum ECoordSystem {
        Offset,
        Axial,
        Cube
    }

    public enum EOffsetGridType {
        OddR,
        EvenR,
        OddQ,
        EvenQ
    }

    public class TupleList<T1, T2> : List<Tuple<T1, T2>> {
        public void Add(T1 item, T2 item2) {
            Add(new Tuple<T1, T2>(item, item2));
        }
    }

    public class HexMapOffset {

        ECoordSystem CoordSystem;
        EOffsetGridType OffsetGridType;

        public HexTile[,] Tiles;

        public int TileSize;

        public int Width;
        public int Height;

        //Used as a lookup for directions, changes along with the EOffsetGridType
        //TODO: Make this into a struct maybe
        public TupleList<int,int> EvenDirections;
        public TupleList<int,int> OddDirections;

        public EHexGridOrientation Orientation;

        public HexMapOffset(int x, int y, int tileSize) {
            Width = x;
            Height = y;
            TileSize = tileSize;
            CoordSystem = ECoordSystem.Offset;
            OffsetGridType = EOffsetGridType.EvenQ;
            Orientation = EHexGridOrientation.Flat;
            Tiles = new HexTile[Width, Height];
            SetUpVars();
            CreateGrid();
            DefineDirections();
        }

        //Depending on the offset, there will be different neighbours
        //Not sure how correct this is, but I should be fine
        private void DefineDirections() {
            switch (OffsetGridType) {
                case EOffsetGridType.EvenQ:
                    EvenDirections = new TupleList<int, int> {
                        { 1, 1 }, { 1, 0 }, { 0, -1 }, { -1, 0 }, { -1, 1 }, { 0, 1 }};
                    OddDirections = new TupleList<int, int> {
                        { 1, 0 }, { 1, -1 }, { 0, -1 }, { -1, -1 }, { -1, 0 }, { 0, 1 }}; ;
                    break;
                case EOffsetGridType.OddQ:
                    EvenDirections = new TupleList<int, int> {
                        { 1, 0 }, { 1, -1 }, { 0, -1 }, { -1, -1 }, { -1, 0 }, { 0, 1 }};
                    OddDirections = new TupleList<int, int> {
                        { 1, 1 }, { 1, 0 }, { 0, -1 }, { -1, 0 }, { -1, 1 }, { 0, 1 }}; ;
                    break;
                case EOffsetGridType.OddR:
                    EvenDirections = new TupleList<int, int> {
                        { 1, 0 }, { 0, -1 }, { -1, -1 }, { -1, 0 }, { -1, 1 }, { 0, 1 }};
                    OddDirections = new TupleList<int, int> {
                        { 1, 0 }, { 1, -1 }, { 0, -1 }, { -1, 0 }, { 0, 1 }, { 1, 1 }}; ;
                    break;
                case EOffsetGridType.EvenR:
                    EvenDirections = new TupleList<int, int> {
                        { 1, 0 }, { 1, -1 }, { 0, -1 }, { -1, 0 }, { 0, 1 }, { 1, 1 }};
                    OddDirections = new TupleList<int, int> {
                        { 1, 0 }, { 0, -1 }, { -1, -1 }, { -1, 0 }, { -1, 1 }, { 0, 1 }}; ;
                    break;
            }
        }
        private void SetUpVars() {
            HexTile h = new HexTile(0, 0, TileSize, Orientation);
            HexHelper.HDistance = HexHelper.GetHDistanceNeighbour(h);
            HexHelper.Height = HexHelper.GetHeight(h);
            HexHelper.VDistance = HexHelper.GetWidth(h);
            HexHelper.Width = HexHelper.GetVDistanceNeighbour(h);
        }

        private void CreateGrid() {
            for (int i = 0; i < Width; i++) {
                for (int j = 0; j < Height; j++) {
                    switch (OffsetGridType) {
                        case EOffsetGridType.OddR:
                            //If it is odd, offset by TileSize 
                            if (j % 2 == 1) {
                                Tiles[i, j] = new HexTile((int)HexHelper.OffSetX(i * HexHelper.HDistance, 1), (int)(j * HexHelper.Height * 3 / 4), TileSize, EHexGridOrientation.Pointed);
                            }
                            else {
                                Tiles[i, j] = new HexTile((int)(i * HexHelper.HDistance), (int)(j * HexHelper.Height * 3 / 4), TileSize, EHexGridOrientation.Pointed);
                            }
                            break;
                        case EOffsetGridType.EvenR:
                            if (j % 2 == 0) {
                                Tiles[i, j] = new HexTile((int)HexHelper.OffSetX(i * HexHelper.HDistance, 1), (int)(j * HexHelper.Height * 3 / 4), TileSize, EHexGridOrientation.Pointed);
                            }
                            else {
                                Tiles[i, j] = new HexTile((int)(i * HexHelper.HDistance), (int)(j * HexHelper.Height * 3 / 4), TileSize, EHexGridOrientation.Pointed);
                            }
                            break;
                        case EOffsetGridType.OddQ:
                            if (i % 2 == 1) {
                                Tiles[i, j] = new HexTile((int)(i * HexHelper.HDistance), (int)HexHelper.OffSetY((int)(j * HexHelper.Height), 1), TileSize, EHexGridOrientation.Flat);
                            }
                            else {
                                Tiles[i, j] = new HexTile((int)(i * HexHelper.HDistance), (int)(j * HexHelper.Height), TileSize, EHexGridOrientation.Flat);
                            }
                            break;
                        case EOffsetGridType.EvenQ:
                            if (i % 2 == 0) {
                                Tiles[i, j] = new HexTile((int)(i * HexHelper.HDistance), (int)HexHelper.OffSetY((int)(j * HexHelper.Height), 1), TileSize, EHexGridOrientation.Flat);
                            }
                            else {
                                Tiles[i, j] = new HexTile((int)(i * HexHelper.HDistance), (int)(j * HexHelper.Height), TileSize, EHexGridOrientation.Flat);
                            }
                            break;
                    }
                    Tiles[i, j].gridX = i;
                    Tiles[i, j].gridY = j;
                }
            }
        }

        public Polygon GetHexPoly(int x, int y) {
            Polygon p = new Polygon();
            p.Points = Tiles[x, y].Points;
            return p;
        }

        public HexTile GetHexTileAt(int x, int y) {
            return Tiles[x, y];
        }


        public HexTile OffSetNeighbour(HexTile h, int direction) {
            var parity = h.gridY & 1;
            //We are even
            if (parity == 0) {
                int X = h.gridX + EvenDirections.ElementAt(direction).Item1;
                int Y = h.gridY + EvenDirections.ElementAt(direction).Item2;
                Console.WriteLine("Neighbour at direction: {0} of tile ({1},{2}) is ({3},{4})", direction, h.gridX, h.gridY, X, Y);
                return Tiles[X, Y];
            }
            //We are odd
            int x = h.gridX + OddDirections.ElementAt(direction).Item1;
            int y = h.gridY + OddDirections.ElementAt(direction).Item2;
            Console.WriteLine("Neighbour at direction: {0} of tile ({1},{2}) is ({3},{4})", direction, h.gridX, h.gridY, x, y);
            return Tiles[x, y];
        }

    }
}
