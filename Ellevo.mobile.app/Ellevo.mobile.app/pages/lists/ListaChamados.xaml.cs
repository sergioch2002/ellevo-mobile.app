using Ellevo.mobile.app.objects;
using System;
using System.Linq;

using Xamarin.Forms;

namespace Ellevo.mobile.app.pages
{
    public partial class ListaChamados : ContentPage
    {
        public ListaChamados()
        {
            InitializeComponent();
            SizeChanged += OnSizeChanged;
            GetData();
        }
        private void OnSizeChanged(object sender, EventArgs e)
        {
            this.BackgroundImage = Height > Width ? "fundosemlogo.png" : "fundosemlogoH1024.png";

        }
        private async void GetData()
        {
            var chamados = await ApiReader.GetDataFromApi<Chamado>("/api/v1/mob/chamado");
            listView.ItemsSource = chamados.OrderByDescending(x => x.ChamadoId);
        }
    }
}
