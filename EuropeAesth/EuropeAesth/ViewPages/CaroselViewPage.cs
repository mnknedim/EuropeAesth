using CarouselView.FormsPlugin.Abstractions;
using EuropeAesth.Custom;
using EuropeAesth.Model;
using EuropeAesth.Pages;
using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Runtime.CompilerServices;
using System.Text;

using Xamarin.Forms;

namespace EuropeAesth.ViewPages
{
	public class CaroselViewPage : ContentView
	{
        public CarouselViewControl carousel;
        static int LastPosition = 0;
        static ObservableCollection<YaziModel> yaziForClick;
       public Command TappedCommand
        {
            get => (Command)GetValue(TappedCommandProperty);
            set => SetValue(TappedCommandProperty, value);
        }
        public static readonly BindableProperty TappedCommandProperty = BindableProperty.Create("TappedCommand", typeof(Command), typeof(CaroselViewPage), default(Command));

        public  ObservableCollection<YaziModel> Obs_Yazi
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
            };
            carousel = new CarouselViewControl()
            {
                
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HeightRequest = 250,
                PositionSelectedCommand = PositionCommand
            };
            carousel.PositionSelected += Carousel_PositionSelected;
           
            
            DataTemplate template = new DataTemplate(() =>
            {
                var DetailGrid = new ExGrid() { HeightRequest = 30, VerticalOptions = LayoutOptions.EndAndExpand , Margin = new Thickness(0,-30,0,30)};
                DetailGrid.Children.Add(new StackLayout()
                {
                    BackgroundColor = Color.Black,
                    Opacity = 0.6,
                    
                },0,0);

                var lblHeader = new Label() {TextColor = Color.White,Margin= new Thickness(10,0,0,0) };
                lblHeader.SetBinding(Label.TextProperty, "Baslik");
                DetailGrid.Children.Add(lblHeader, 0, 0);

                var SliderGrid = new ExGrid() { VerticalOptions = LayoutOptions.FillAndExpand };

                var image = new Image() {
                    HeightRequest = 230,
                    WidthRequest = 120,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    Aspect = Aspect.Fill,
                    Margin = new Thickness(0, 0, 0, 30),
                };
            
                var tabGest = new TapGestureRecognizer();
                tabGest.Tapped += TabGest_Tapped;
                 
                image.GestureRecognizers.Add(tabGest);
                image.SetBinding(Image.SourceProperty, "ImageUrl");

                //MessagingCenter.Subscribe<string>(this, "UpdateOrInsertOrDelete", (sender) => {
                //    ResimYukle();
                //});

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
            carousel.ShowIndicators = true;
            body.Children.Add(carousel);
            Content = body;
        }

        private async void TabGest_Tapped(object sender, EventArgs e)
        {
            var selected = yaziForClick.Where(x => x.Id == LastPosition.ToString()).FirstOrDefault();
            await Navigation.PushAsync(new ListViewDetail() { SecYazi = selected });
        }

        private void Carousel_PositionSelected(object sender, PositionSelectedEventArgs e)
        {
            LastPosition = e.NewValue;
        }


        public Command PositionCommand = new Command(() => {
        });

       

        private async void ResimYukle()
        {
            var tumYazilar = await firebase.Child("Yazilar").OnceAsync<YaziModel>();
            var osbFirst5 = tumYazilar.OrderByDescending(x => x.Object.Tarih).Take(5);
            Obs_Yazi = new ObservableCollection<YaziModel>();
            if (tumYazilar != null)
            {
                int id = 0;
                foreach (var item in osbFirst5)
                {
                    item.Object.Id = id.ToString();
                    Obs_Yazi.Add(item.Object);
                    id++;

                }
            }
            yaziForClick = Obs_Yazi;
            carousel.ItemsSource = Obs_Yazi;
            carousel.BindingContext = Obs_Yazi;

            //CheckChange();

        }

        private void CheckChange()
        {
            firebase.Child("Yazilar")
                 .AsObservable<YaziModel>()
                 .Subscribe(yazi =>
                 {
                     var firstFive = Obs_Yazi.Take(5);
                     var rs = firstFive.Any(x => x.Id == yazi.Key);
                     if (!rs)
                     {
                         ResimYukle();
                     }
                 });

           
        }

      
    }

    
    }