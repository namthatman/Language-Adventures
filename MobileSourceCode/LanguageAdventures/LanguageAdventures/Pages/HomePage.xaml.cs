using Xamarin.Forms;
using Xamarin.Forms.Xaml;

// Home page is the first page user will navigate to after join team.
namespace LanguageAdventures.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : TabbedPage
    {
        public HomePage()
        {
            Navigation.PushModalAsync(new MainPage());
            InitializeComponent();
        }

    }
}