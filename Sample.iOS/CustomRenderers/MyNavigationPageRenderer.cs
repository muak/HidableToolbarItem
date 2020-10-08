using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sample.CustomRenderers;
using Sample.iOS.CustomRenderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(MyNavigationPage), typeof(MyNavigationPageRenderer))]
namespace Sample.iOS.CustomRenderers
{
    [Foundation.Preserve(AllMembers = true)]
    public class MyNavigationPageRenderer: NavigationRenderer
    {
        public override void PushViewController(UIViewController viewController, bool animated)
        {            
            base.PushViewController(viewController, animated);

            var curPage = (Element as NavigationPage).CurrentPage;
            foreach (var item in curPage.ToolbarItems.OfType<MyToolbarItem>())
            {
                item.PropertyChanged += OnToolbarItemPropertyChanged;
            }
            SetToolbarItemVisibility();
        }

        public override UIViewController PopViewController(bool animated)
        {           
            var curPage = (Element as NavigationPage).CurrentPage;
            foreach(var item in curPage.ToolbarItems.OfType<MyToolbarItem>())
            {
                item.PropertyChanged -= OnToolbarItemPropertyChanged;
            }
            
            return base.PopViewController(animated);
        }
        

        void SetToolbarItemVisibility()
        {            
            var curPage = (Element as NavigationPage).CurrentPage;

            if(!curPage.ToolbarItems.OfType<MyToolbarItem>().Any())
            {
                return;
            }

            var ctrl = ViewControllers.Last();
            if (ctrl.NavigationItem.RightBarButtonItems != null)
            {
                for (var i = 0; i < ctrl.NavigationItem.RightBarButtonItems.Length; i++)
                    ctrl.NavigationItem.RightBarButtonItems[i].Dispose();
            }
            if (ToolbarItems != null)
            {
                for (var i = 0; i < ToolbarItems.Length; i++)
                    ToolbarItems[i].Dispose();
            }

            List<UIBarButtonItem> primaries = null;
            List<UIBarButtonItem> secondaries = null;            

            foreach (var item in curPage.ToolbarItems)
            {               
                if(item is MyToolbarItem myItem)
                {
                    if (!myItem.IsVisible)
                        continue;
                }

                if (item.Order == ToolbarItemOrder.Secondary)
                    (secondaries = secondaries ?? new List<UIBarButtonItem>()).Add(item.ToUIBarButtonItem(true));
                else
                    (primaries = primaries ?? new List<UIBarButtonItem>()).Add(item.ToUIBarButtonItem());
            }

            if (primaries != null)
                primaries.Reverse();
            ctrl.NavigationItem.SetRightBarButtonItems(primaries == null ? new UIBarButtonItem[0] : primaries.ToArray(), false);
            ToolbarItems = secondaries == null ? new UIBarButtonItem[0] : secondaries.ToArray();

            UpdateToolBarVisible();
        }

        void UpdateToolBarVisible()
        {
            if(!(View is UIToolbar secondaryToolbar))
            {
                return;
            }

            if (secondaryToolbar == null)
                return;
            if (TopViewController != null && TopViewController.ToolbarItems != null && TopViewController.ToolbarItems.Any())
            {
                secondaryToolbar.Hidden = false;
                secondaryToolbar.Items = TopViewController.ToolbarItems;
            }
            else
            {
                secondaryToolbar.Hidden = true;
            }

            TopViewController?.NavigationItem?.TitleView?.SizeToFit();
            TopViewController?.NavigationItem?.TitleView?.LayoutSubviews();
        }

        private void OnToolbarItemPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == MyToolbarItem.IsVisibleProperty.PropertyName)
            {
                SetToolbarItemVisibility();
            }
        }
    }
}
