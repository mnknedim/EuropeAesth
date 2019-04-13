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
	public partial class TemsilciView: ContentPage
	{
        public TemsilciModel Temsilci
        {
            get => (TemsilciModel)GetValue(TemsilciProperty);
            set => SetValue(TemsilciProperty, value);
        }
        public static readonly BindableProperty TemsilciProperty = BindableProperty.Create("Temsilci", typeof(TemsilciModel), typeof(TemsilciView), default(TemsilciModel));
        public TemsilciView()
		{
			InitializeComponent ();
            BindingContext = this;
            
		}
	}
}