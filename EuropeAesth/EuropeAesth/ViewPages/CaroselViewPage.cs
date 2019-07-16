using CarouselView.FormsPlugin.Abstractions;
using EuropeAesth.Custom;
using EuropeAesth.Model;
using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

using Xamarin.Forms;

namespace EuropeAesth.ViewPages
{
	public class CaroselViewPage : ContentView
	{
        public CarouselViewControl carousel;
        public ObservableCollection<YaziModel> Obs_Yazi
        {
            get { return (ObservableCollection<YaziModel>)GetValue(Obs_YaziProperty); }
            set { SetValue(Obs_YaziProperty, value); }
        }
        public static readonly BindableProperty Obs_YaziProperty = BindableProperty.Create("Obs_Yazi", typeof(ObservableCollection<YaziModel>),
            typeof(CaroselViewPage), default(ObservableCollection<YaziModel>));

        FirebaseClient firebase = new FirebaseClient("https://adjuvanclinic.firebaseio.com/");

        public CaroselViewPage ()
		{
            ResimYukle();
            StackLayout body = new StackLayout()
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Color.White,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HeightRequest = 182,
            };
            carousel = new CarouselViewControl()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HeightRequest = 220,
            };

            DataTemplate template = new DataTemplate(() =>
            {
                var DetailGrid = new ExGrid() { HeightRequest = 30, VerticalOptions = LayoutOptions.EndAndExpand};
                DetailGrid.Children.Add(new StackLayout()
                {
                    BackgroundColor = Color.Black,
                    Opacity = 0.6,
                    
                },0,0);

                var lblHeader = new Label() {TextColor = Color.White,Margin= new Thickness(10,0,0,0) };
                lblHeader.SetBinding(Label.TextProperty, "Baslik");
                DetailGrid.Children.Add(lblHeader, 0, 0);

                var SliderGrid = new ExGrid() { VerticalOptions = LayoutOptions.EndAndExpand };

                var image = new Image() {
                    HeightRequest = 220,
                    WidthRequest = 120,
                    VerticalOptions = LayoutOptions.FillAndExpand
                };
                image.SetBinding(Image.SourceProperty, "ImageUrl");
                var yazim = Obs_Yazi.Where(x=>x.ImageUrl == image.Source.ToString());

                SliderGrid.Children.Add(image, 0, 0);
                SliderGrid.Children.Add(DetailGrid, 0, 0);
                
                var stacksample = new StackLayout()
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                };
                stacksample.Children.Add(SliderGrid);

                return stacksample;
            });

            carousel.ItemTemplate = template;
            
            body.Children.Add(carousel);
            Content = body;
        }

       

        private void ImgTabGest_Tapped(object sender, EventArgs e)
        {
            
        }

        private void Carousel_Focused(object sender, FocusEventArgs e)
        {
            //var secilen = (YaziModel)sender;
            //await Navigation.PushAsync(new ListViewDetail() { SecYazi = secilen });
            //YaziList.SelectedItem = null;
        }

        private async void ResimYukle()
        {
            var tumYazilar = await firebase.Child("Yazilar").OnceAsync<YaziModel>();
            var osbFirst5 = tumYazilar.OrderByDescending(x => x.Object.Tarih).Take(5);
            Obs_Yazi = new ObservableCollection<YaziModel>();
            if (tumYazilar != null)
            {
                foreach (var item in osbFirst5)
                    Obs_Yazi.Add(item.Object);
            }

            carousel.ItemsSource = Obs_Yazi;
            carousel.BindingContext = Obs_Yazi;
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName == Obs_YaziProperty.PropertyName)
            {

            }
        }
    }

    
    }