using System;
using Xamarin.Forms;

namespace Sample.CustomRenderers
{
    public class MyToolbarItem:ToolbarItem
    {
        public static BindableProperty IsVisibleProperty = BindableProperty.Create(
            nameof(IsVisible),
            typeof(bool),
            typeof(MyToolbarItem),
            true,
            defaultBindingMode: BindingMode.OneWay
        );

        public bool IsVisible
        {
            get { return (bool)GetValue(IsVisibleProperty); }
            set { SetValue(IsVisibleProperty, value); }
        }
    }
}
