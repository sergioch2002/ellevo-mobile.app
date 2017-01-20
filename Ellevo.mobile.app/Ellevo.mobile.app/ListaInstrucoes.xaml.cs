using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Ellevo.mobile.app
{
    public partial class ListaInstrucoes : ContentPage
    {
        public ListaInstrucoes()
        {
            InitializeComponent();

            listView.ItemsSource = new List<Color> { Color.Aqua, Color.Black, Color.Blue, Color.Fuchsia, Color.Gray, Color.Green, Color.Lime, Color.Maroon, Color.Navy, Color.Olive, Color.Pink, Color.Purple, Color.Red, Color.Silver, Color.Teal, Color.White, Color.Yellow };
        }
    }
}
