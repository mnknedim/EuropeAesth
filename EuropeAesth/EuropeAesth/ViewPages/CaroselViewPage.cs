﻿using CarouselView.FormsPlugin.Abstractions;
using EuropeAesth.Custom;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace EuropeAesth.ViewPages
{
	public class CaroselViewPage : ContentView
	{
        public CarouselViewControl carousel;
        public class ImageSlider
        {
            public string ImageUrl { get; set; }
            public string Detail { get; set; }
        }
        public CaroselViewPage ()
		{
            Padding = new Thickness(0);
            ObservableCollection<ImageSlider> collection = new ObservableCollection<ImageSlider>{

                new ImageSlider{ ImageUrl= "sacekimi.png" , Detail ="Saç Ekimi"},
                new ImageSlider{ ImageUrl= "doktorlar.png", Detail ="Doktorlarımız" },
                new ImageSlider{ ImageUrl= "fueyontemi.png", Detail ="Fue yöntemi nedir?" },
            };

            StackLayout body = new StackLayout()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.Start,
                HeightRequest = 182,

            };

            carousel = new CarouselViewControl()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,

            };

            DataTemplate template = new DataTemplate(() =>
            {
                var DetailGrid = new ExGrid() { HeightRequest = 30, VerticalOptions = LayoutOptions.EndAndExpand};
                DetailGrid.Children.Add(new StackLayout()
                {
                    BackgroundColor = Color.Black,
                    Opacity = 0.6,
                    
                },0,0);

                var lblHeader = new Label() {TextColor = Color.White };
                lblHeader.SetBinding(Label.TextProperty, "Detail");
                DetailGrid.Children.Add(lblHeader, 0, 0);

                var SliderGrid = new ExGrid() { VerticalOptions = LayoutOptions.EndAndExpand };

                var image = new Image() {
                    HeightRequest = 200,
                    WidthRequest = 120,
                    VerticalOptions = LayoutOptions.EndAndExpand
                };
                image.SetBinding(Image.SourceProperty, "ImageUrl");

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

            carousel.ItemsSource = collection;

            body.Children.Add(carousel);


            StackLayout stack = new StackLayout()
            {
                Children = {
                    body
                },
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Color.White
            };

            Content = stack;
        }

    }

    
}