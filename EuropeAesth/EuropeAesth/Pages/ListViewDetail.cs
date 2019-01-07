using EuropeAesth.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace EuropeAesth.Pages
{
	public class ListViewDetail : ContentPage
	{
		public ListViewDetail (ImageScrollViewModel News)
		{
            string desc = "Saç ekimi saçların yönlerine dikkat edilerek ve kök ünitelerinin içerdikleri kök sayılarına göre" +
                " doğal bir saç çizgisi belirlenerek doğadaki durumlarına uygun olarak ekilirse ekimin yapıldığı belli olmaz." +
                " Ekilecek kökler asker gibi yanyana dizilmemelidir. Saçı dökülmemiş kişilerde de kökler belirli bir ince " +
                "düzensizlik içinde yerleşmişlerdir.Kısacası doğadaki saçların taklit edildiği takdirde ekim yapıldığı belli olmaz." +
                " Bunun dışında uygun olmayan cerrahi teknik ve ekipman kullanıldığında saç köklerinde minik çukurluklar gözlenebilir. " +
                "Bunları daha çok lazer ve bazı kanal açma iğneleri ile uygulama yapıldığında gözlemlediğimizden 6 yıldır lazer kullanımını " +
                "bıraktık ve saç köklerinin yerleştirilmesinde kullandığımız aletleri de geliştirerek saç köklerinin boyutlarına uygun " +
                "delik açma aletleri geliştirdik. Bu sayede bu tip bir komplikasyona artık rastlamamaktayız. FUE ile normal ekim arasındaki " +
                "fark  FUE yani köklerin tek tek alınma tekniği ile şerit yöntemi arasında sadece köklerin alınma yöntemleri arasında fark vardır. " +
                "Ekimde bir farklılık yoktur. Yani sonuç olarak FUE yönteminin şerit yöntemine üstünlüğü ense bölgesinde iz ve ilk günlerdeki " +
                " gerginliğin olmamasıdır.Saç ekimi saçların yönlerine dikkat edilerek ve kök ünitelerinin içerdikleri kök sayılarına göre" +
                " doğal bir saç çizgisi belirlenerek doğadaki durumlarına uygun olarak ekilirse ekimin yapıldığı belli olmaz." +
                " Ekilecek kökler asker gibi yanyana dizilmemelidir. Saçı dökülmemiş kişilerde de kökler belirli bir ince " +
                "düzensizlik içinde yerleşmişlerdir.Kısacası doğadaki saçların taklit edildiği takdirde ekim yapıldığı belli olmaz." +
                " Bunun dışında uygun olmayan cerrahi teknik ve ekipman kullanıldığında saç köklerinde minik çukurluklar gözlenebilir. " +
                "Bunları daha çok lazer ve bazı kanal açma iğneleri ile uygulama yapıldığında gözlemlediğimizden 6 yıldır lazer kullanımını " +
                "bıraktık ve saç köklerinin yerleştirilmesinde kullandığımız aletleri de geliştirerek saç köklerinin boyutlarına uygun " +
                "delik açma aletleri geliştirdik. Bu sayede bu tip bir komplikasyona artık rastlamamaktayız. FUE ile normal ekim arasındaki " +
                "fark  FUE yani köklerin tek tek alınma tekniği ile şerit yöntemi arasında sadece köklerin alınma yöntemleri arasında fark vardır. " +
                "Ekimde bir farklılık yoktur. Yani sonuç olarak FUE yönteminin şerit yöntemine üstünlüğü ense bölgesinde iz ve ilk günlerdeki " +
                " gerginliğin olmamasıdır.";

            var Image = new Image {
               
                Source = "doktor2.png",
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.StartAndExpand,
            };
            var Header = new Label {
                Text = News.Title , Margin = new Thickness(5),
                FontAttributes = FontAttributes.Bold,
                TextColor = Color.Black
            };
            var Description = new Label {
                Text = desc,
                Margin = new Thickness(5)
            };

            Content = new StackLayout {
                HorizontalOptions = LayoutOptions.StartAndExpand,
                VerticalOptions = LayoutOptions.StartAndExpand,
                Children = {
                    Image,
                    Header,
                    Description
                }
			};


            

        }
	}
}