using EuropeAesth.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EuropeAesth.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ListViewDetail : ContentPage
	{
        public YaziModel SecYazi
        {
            get { return (YaziModel)GetValue(SecYaziProperty); }
            set { SetValue(SecYaziProperty, value); }
        }
        public static readonly BindableProperty SecYaziProperty =
            BindableProperty.Create(nameof(SecYazi), typeof(YaziModel), typeof(ListViewDetail), default(YaziModel));
        public ListViewDetail ()
		{
			InitializeComponent ();
            BindingContext = this;

        }
    }
}