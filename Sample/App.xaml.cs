using Prism.DryIoc;
using Prism.Ioc;
using Sample.CustomRenderers;
using Sample.Views;

namespace Sample
{
    public partial class App : PrismApplication
    {
        public App()
        {
            InitializeComponent();            
        }

        protected override void OnInitialized()
        {
            NavigationService.NavigateAsync("/MyNavigationPage/MainPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<MyNavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage>();
            containerRegistry.RegisterForNavigation<SecondPage>();
        }
    }
}
