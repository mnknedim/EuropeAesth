using EuropeAesth.Custom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

using Xamarin.Forms;

namespace EuropeAesth.Component
{
	public class ButtonView : ContentView
	{
        public string ImageUrl
        {
            get { return (string)GetValue(ImageUrlProperty); }
            set { SetValue(ImageUrlProperty, value); }
        }
        public static readonly BindableProperty ImageUrlProperty = BindableProperty.Create("ImageUrl", typeof(string), typeof(ButtonView), default(string));

        public string UnderText
        {
            get { return (string)GetValue(UnderTextProperty); }
            set { SetValue(UnderTextProperty, value); }
        }
        public static readonly BindableProperty UnderTextProperty = BindableProperty.Create("UnderText", typeof(string), typeof(ButtonView), default(string));
        public Page PageName
        {
            get { return (Page)GetValue(PageNameProperty); }
            set { SetValue(PageNameProperty, value); }
        }
        public static readonly BindableProperty PageNameProperty = BindableProperty.Create("PageName", typeof(Page), typeof(ButtonView), default(Page));

        ExButtonImage BtnImage = new ExButtonImage() { HeightRequest = 125, WidthRequest = 125};
        Label SubTitle = new Label() {
            FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
            FontAttributes = FontAttributes.Bold,
            HorizontalTextAlignment = TextAlignment.Center
        };
        public Command TappedCommand
        {
            get => (Command)GetValue(TappedCommandProperty);
            set => SetValue(TappedCommandProperty, value);
        }
        public static readonly BindableProperty TappedCommandProperty = BindableProperty.Create("TappedCommand", typeof(Command), typeof(ButtonView), default(Command));

        public ButtonView ()
		{

            Margin = new Thickness(5, 20);
            HorizontalOptions = LayoutOptions.CenterAndExpand;
            VerticalOptions = LayoutOptions.CenterAndExpand;
            Content = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Margin = new Thickness(10),
                Children = {
                    BtnImage,
                    SubTitle
                }
            };
            var tabSenderObject = this;
            Helpers.Gesture.TappedGesture((s, e) => TappedCommand?.Execute(tabSenderObject));
            GestureRecognizers.Add(Helpers.Gesture.TappedGesture((s,e)=>TappedCommand?.Execute(tabSenderObject)));
		}

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == UnderTextProperty.PropertyName)
                SubTitle.Text = UnderText;

            if (propertyName == ImageUrlProperty.PropertyName)
                BtnImage.Source = ImageUrl;

        }
    }
}