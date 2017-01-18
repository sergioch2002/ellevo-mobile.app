using Ellevo.mobile.app.objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Ellevo.mobile.app
{
    public partial class LoginPage : ContentPage
    {
        Configuracoes conf;
        public LoginPage(Configuracoes config)
        {
            conf = config;
            
            InitializeComponent();

            SetViews();
            Picker picker = new Picker
            {
                Title = "Dominios",
                VerticalOptions = LayoutOptions.StartAndExpand
            };

            foreach (var dominio in conf.ListaDominios)
            {
                picker.Items.Add(dominio.Nome);
            }

            boxLogin.Children.Add(picker);

        }

        private void SetViews()
        {
            lblDominio.Text = conf.NomeSite;
        }

        private void OnEnterClicked(object sender, EventArgs args)
        {
            if (!string.IsNullOrEmpty(txtUsuario.Text) && !string.IsNullOrEmpty(txtSenha.Text))
            {

            }
            else
                return;
        }
    }
}
