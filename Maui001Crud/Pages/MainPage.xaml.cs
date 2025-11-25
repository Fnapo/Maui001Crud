using Maui001Crud.Models;
using Maui001Crud.PageModels;

namespace Maui001Crud.Pages
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainPageModel model)
        {
            InitializeComponent();
            BindingContext = model;
        }
    }
}