using System;
using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AutoCartApp.Handlers
{
    [ContentProperty(nameof(Source))]
    public class ConverterImage : IMarkupExtension
    {
        public string Source { get; set; }
        public object ProvideValue(IServiceProvider serviceProvider)
        {
            //BindableObject targetObject;
            if (Source == null) return null;
            // Do your translation lookup here, using whatever method you require
            Source = $"AutoCartApp.Assets.stock_{Source}.jpg";
            return ImageSource.FromResource(Source, typeof(ConverterImage).GetTypeInfo().Assembly);
        }
    }
}