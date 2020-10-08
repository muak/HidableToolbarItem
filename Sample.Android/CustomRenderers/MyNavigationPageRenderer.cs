using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Android.Content;
using AndroidX.AppCompat.Widget;
using Sample.CustomRenderers;
using Sample.Droid.CustomRenderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.Platform.Android.AppCompat;

[assembly: ExportRenderer(typeof(MyNavigationPage), typeof(MyNavigationPageRenderer))]
namespace Sample.Droid.CustomRenderers
{
    [Android.Runtime.Preserve(AllMembers = true)]
    public class MyNavigationPageRenderer:NavigationPageRenderer
    {
        Toolbar _toolbar;

        public MyNavigationPageRenderer(Context context):base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<NavigationPage> e)
        {
            base.OnElementChanged(e);
            if(e.OldElement != null)
            {
                Element.Popped -= Element_Popped;
            }

            if (e.NewElement != null)
            {
                _toolbar = (Toolbar)GetChildAt(0);
                Element.Popped += Element_Popped;                
            }
        }

        protected override void Dispose(bool disposing)
        {
            if(disposing)
            {
                if(Element != null)
                {
                    Element.Popped -= Element_Popped;                    
                }
                _toolbar = null;
            }
            base.Dispose(disposing);
        }

        private void Element_Popped(object sender, NavigationEventArgs e)
        {
            SetToolbarItemVisibility();
        }

        protected override Task<bool> OnPushAsync(Page view, bool animated)
        {
            SetToolbarItemVisibility();
            return base.OnPushAsync(view, animated);
        }

        protected override void OnToolbarItemPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnToolbarItemPropertyChanged(sender, e);
            if (e.PropertyName == MyToolbarItem.IsVisibleProperty.PropertyName)
            {
                SetToolbarItemVisibility();
            }
        }

        void SetToolbarItemVisibility()
        {
            var curPage = Element.CurrentPage;

            if (!curPage.ToolbarItems.OfType<MyToolbarItem>().Any())
            {
                return;
            }

            for(var i = 0; i < curPage.ToolbarItems.Count; i++)
            {
                var item = curPage.ToolbarItems[i];
                var menuItem = _toolbar.Menu.GetItem(i);

                if (item is MyToolbarItem myToolbarItem)
                {
                    menuItem.SetVisible(myToolbarItem.IsVisible);                    
                }               
            }            
        }
    }
}
