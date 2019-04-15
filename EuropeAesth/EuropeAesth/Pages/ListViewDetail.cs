using EuropeAesth.Component;
using EuropeAesth.Custom;
using EuropeAesth.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace EuropeAesth.Pages
{
    public class ListViewDetail : ContentPage
    {
        public DisplayInfo _DisplayInfo
        {
            get { return (DisplayInfo)GetValue(_DisplayInfoProperty); }
            set { SetValue(_DisplayInfoProperty, value); }
        }
        public static readonly BindableProperty _DisplayInfoProperty = BindableProperty.Create("UnderText", typeof(DisplayInfo), typeof(ListViewDetail), default(DisplayInfo));

        public string _Description
        {
            get { return (string)GetValue(_DescriptionProperty); }
            set { SetValue(_DescriptionProperty, value); }
        }
        public static readonly BindableProperty _DescriptionProperty = BindableProperty.Create("_Description", typeof(string), typeof(ListViewDetail), default(string));

        DisplayInfo displayInfo;
        public ListViewDetail(ImageScrollViewModel News)
        {
            BindingContext = this;
            displayInfo = DeviceDisplay.MainDisplayInfo;
            var Y = new Yazilar();
            var aa = News.ImageUrl.IndexOf('.');
            var yazi = News.ImageUrl.Substring(0, aa);
            string desc = Y.DYazilar[yazi];
            _Description = desc;

            var Image = new Image
            {

                Source = News.ImageUrl,
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.StartAndExpand,
            };
            var Header = new Label
            {
                Text = News.Title,
                Margin = new Thickness(5),
                FontAttributes = FontAttributes.Bold,
                TextColor = Color.Black
            };
            var Description = new Label
            {
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                Margin = new Thickness(5)
            };
            Description.SetBinding(Label.TextProperty, new Binding("_Description"));

            var NewGrid = new ExGrid();
            var btnBack = new ImageButton
            {
                Source = "ic_back.png",
                Margin = new Thickness(15),
                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.End,
               

            };
            
            btnBack.Clicked += (s, e) =>
            {
                App.Current.MainPage = new TabbedMainPage();

            };

            var st = new StackLayout
            {
                HorizontalOptions = LayoutOptions.StartAndExpand,
                VerticalOptions = LayoutOptions.StartAndExpand,

                Children = {
                        Image,
                        Header,
                        Description,
                        btnBack
                    }
            };
            NewGrid.Children.Add(st);

            var scview = new ScrollView()
            {
                Content = NewGrid 
            };


            Content = scview;

        }
    }
}