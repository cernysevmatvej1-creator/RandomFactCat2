using ApiDeepSeekl.ViewModel;

namespace ApiDeepSeekl
{
    public partial class MainPage : ContentPage
    {
        
        MainPageViewModel viewModel;
        public MainPage(MainPageViewModel mainPageViewModel)
        {
            InitializeComponent();
            BindingContext = mainPageViewModel;
            viewModel = mainPageViewModel;  
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await viewModel.ListFacts();
        }

    }
}
