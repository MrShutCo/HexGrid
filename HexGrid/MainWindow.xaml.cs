using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HexGrid {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        HexMapOffset map;

        public MainWindow() {
            InitializeComponent();
            map = new HexMapOffset(20, 20, 16);
            for (int x = 0; x < 20; x++) {
                for (int y = 0; y < 20; y++) {
                    Polygon p = map.GetHexPoly(x, y);
                    p.Stroke = Brushes.Black;
                    MapViewer.Children.Add(p);
                }
            }
            for (int i = 0; i < 5; i++)
            map.OffSetNeighbour(map.GetHexTileAt(5, 5), i);
        }
    }
}
