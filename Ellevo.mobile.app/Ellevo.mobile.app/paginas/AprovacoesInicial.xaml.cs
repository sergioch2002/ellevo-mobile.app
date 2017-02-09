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
        public AprovacoesInicial()
        {
            this.Title = "Aprovações";
            this.Children.Add(new ListaAprovacoesChamados());
            this.Children.Add(new ListaAprovacoesTarefas());
            this.BarBackgroundColor = Color.FromHex("#2DBDB6");
            
        }

    }
}
