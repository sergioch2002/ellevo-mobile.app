using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Ellevo.mobile.app.paginas.novas
{
    public partial class NovaProvidencia : ContentPage
    {
        private string _tarefaId;
        public NovaProvidencia(string tarefaId)
        {
            this._tarefaId = tarefaId;
            InitializeComponent();
        }
        private void OnConfClicked(object sender, EventArgs e)
        {
            Providencia providencia = new Providencia
            {


            };
        }
    }
}
