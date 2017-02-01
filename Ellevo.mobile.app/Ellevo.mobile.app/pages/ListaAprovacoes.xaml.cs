using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Ellevo.mobile.app.pages
{
    public partial class ListaAprovacoes : ContentPage
    {
        public ListaAprovacoes()
        {
            InitializeComponent();
            SizeChanged += OnSizeChanged;
        }
        private void OnSizeChanged(object sender, EventArgs e)
        {
            this.BackgroundImage = Height > Width ? "fundosemlogo.png" : "fundosemlogoH1024.png";

        }
    }
}
