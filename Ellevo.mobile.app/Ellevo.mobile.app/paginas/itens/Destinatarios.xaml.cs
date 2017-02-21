using Ellevo.mobile.app.modelos;
using Ellevo.mobile.app.objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Ellevo.mobile.app.paginas.itens
{
    public partial class Destinatarios : ContentPage
    {
        public IEnumerable<InstrucaoDestinatario> destinatarios { get; set; }
        public IEnumerable<Usuario> Usuarios { get; set; }
        public Destinatarios()
        {
            
            InitializeComponent();
            GetData();
        }
        private async void GetData()
        {
            destinatarios = await ApiReader.GetDataFromApi<IEnumerable<InstrucaoDestinatario>>("/api/v1/mob/instrucao/DestinatariosGrupos");
            //GruposList.ItemsSource = destinatarios;

            GruposList.ItemSelected += async (object sender, SelectedItemChangedEventArgs e) =>
            {
                var dest = (InstrucaoDestinatario)GruposList.SelectedItem;

                
                //DestinatariosViewModel dvm = new modelos.DestinatariosViewModel
                //{
                //    Id = dest.UsuarioId.ToString(),
                //    Nome = dest.Nome
                //};
                var usu = await ApiReader.GetDataFromApi<IEnumerable<InstrucaoDestinatario>>("/api/v1/mob/instrucao/destinatariosusuarios?grupoId=" + dest.UsuarioId.ToString());

                var g = new Grouping<InstrucaoDestinatario, InstrucaoDestinatario>((InstrucaoDestinatario)GruposList.SelectedItem, usu);

                //var usuariosDataTemplate = new DataTemplate(() =>
                //{
                //    StackLayout stack = new StackLayout();
                //    var label = new Label();
                //    label.SetBinding(Label.TextProperty, "Nome");
                //    stack.Children.Add(label);
                //    return new ViewCell { View = stack };
                //});
                //var usuariosList = new ListView { ItemsSource = destinatariosUsuarios, ItemTemplate = usuariosDataTemplate };

            };
        }
    }
}
