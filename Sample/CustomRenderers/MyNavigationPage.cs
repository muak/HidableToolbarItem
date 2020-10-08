using System;
using Xamarin.Forms;

namespace Sample.CustomRenderers
{
    public class MyNavigationPage:NavigationPage
    {		
		/// <summary>
		/// Android Only
		/// </summary>
		public static BindableProperty StatusBarBackColorProperty =
			BindableProperty.Create(nameof(StatusBarBackColor), typeof(Xamarin.Forms.Color), typeof(MyNavigationPage), Xamarin.Forms.Color.Default,
									defaultBindingMode: Xamarin.Forms.BindingMode.OneWay
								   );

		public Xamarin.Forms.Color StatusBarBackColor {
			get { return (Xamarin.Forms.Color)GetValue(StatusBarBackColorProperty); }
			set { SetValue(StatusBarBackColorProperty, value); }
		}

		public MyNavigationPage() { }
		public MyNavigationPage(Page root) : base(root) { }
	}
}
