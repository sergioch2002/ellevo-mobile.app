using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Ellevo.mobile.app.paginas.novas
{
    public partial class NovoTramite : ContentPage
    {
        private string _chamadoId;
        public NovoTramite(string chamadoId)
        {
            this._chamadoId = chamadoId;

            InitializeComponent();
        }

        private void OnConfClicked(object sender, EventArgs e)
        {
            Tramite tramite = new Tramite
            {
                
                
            };
        }
    }
}
