using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BarsboldApp.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private string mainTitle = "Explorateur de Pays";
        private string subTitle = "Le monde à portée de main";

        public ObservableCollection<string> CountryImages { get; set; }

        public MainViewModel()
        {
            CountryImages = new ObservableCollection<string>
            {
                "dotnet_bot.png",
                "airplane.png",
                "location.png"
            };
        }

        public string MainTitle
        {
            get => mainTitle;
            set
            {
                if (mainTitle != value)
                {
                    mainTitle = value;
                    OnPropertyChanged();
                }
            }
        }

        public string SubTitle
        {
            get => subTitle;
            set
            {
                if (subTitle != value)
                {
                    subTitle = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
