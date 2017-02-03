using Ellevo.mobile.app.objects;
using System;
using System.Linq;

using Xamarin.Forms;

namespace Ellevo.mobile.app.pages
{
    public partial class ListaAcompanhamentos : ContentPage
    {
        public ListaAcompanhamentos()
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
            var acompanhamentos = await ApiReader.GetDataFromApi<Conta>("/api/v1/mob/conta");
            listView.ItemsSource = acompanhamentos.OrderByDescending(x => x.ContaId);
        }
    }
}
