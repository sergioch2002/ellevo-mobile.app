using Ellevo.mobile.app.objects;
using Ellevo.mobile.app.pages.lists;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Ellevo.mobile.app.pages
{
    public partial class AprovacoesInicial : TabbedPage
    {
        public int numTarefas { get; set; }
        public int numChamados { get; set; }
        public AprovacoesInicial()
        {
            GetData();
            this.Title = "Aprovações";
            this.Children.Add(new ListaAprovacoesChamados(numChamados));
            this.Children.Add(new ListaAprovacoesTarefas(numTarefas));
            this.BarBackgroundColor = Color.FromHex("#2DBDB6");
            
        }

        private async void GetData()
        {
            numTarefas = await ApiReader.GetDataFromApi<int>("/api/v1/mob/tarefa/TotalEmAprovacao");
            numChamados = await ApiReader.GetDataFromApi<int>("/api/v1/mob/chamado/TotalEmAprovacao");
            
        }
    }
}
