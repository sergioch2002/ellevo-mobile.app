using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Ellevo.mobile.app.paginas.itens
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DestinatariosUsuarios : ContentPage
    {
        public DestinatariosUsuarios(IEnumerable<InstrucaoDestinatario> destinatarios)
        {
            InitializeComponent();
            UsuariosList.ItemsSource = destinatarios;

            UsuariosList.ItemSelected += (object sender, SelectedItemChangedEventArgs e) =>
            {
                DisplayAlert("aaa", "usuario escolhido", "sair");
            };
            //BindingContext = new ContentPageViewModel();
        }
    }

    class DestinatariosUsuariosViewModel : INotifyPropertyChanged
    {

        public DestinatariosUsuariosViewModel()
        {
            IncreaseCountCommand = new Command(IncreaseCount);
        }

        int count;

        string countDisplay = "You clicked 0 times.";
        public string CountDisplay
        {
            get { return countDisplay; }
            set { countDisplay = value; OnPropertyChanged(); }
        }

        public ICommand IncreaseCountCommand { get; }

        void IncreaseCount() =>
            CountDisplay = $"You clicked {++count} times";


        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName]string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    }
}
