using System;
using Prism.Mvvm;
using Prism.Navigation;
using Reactive.Bindings;

namespace Sample.ViewModels
{
    public class MainPageViewModel:BindableBase
    {
        public ReactiveCommand GoNextCommand { get; } = new ReactiveCommand();        
        public ReactiveCommand MenuCommand { get; }
        public ReactivePropertySlim<bool> Visible1 { get; } = new ReactivePropertySlim<bool>();
        public ReactivePropertySlim<bool> Visible2 { get; } = new ReactivePropertySlim<bool>();

        public MainPageViewModel(INavigationService navigationService)
        {
            MenuCommand = new ReactivePropertySlim<bool>(false).ToReactiveCommand();

            GoNextCommand.Subscribe(_ => {
                navigationService.NavigateAsync("SecondPage");
            });


        }
    }
}
