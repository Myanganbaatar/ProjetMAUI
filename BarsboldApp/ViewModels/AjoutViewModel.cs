using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using BarsboldApp.Models;

namespace BarsboldApp.ViewModels
{
    public class AjoutViewModel : INotifyPropertyChanged
    {
        private string titre = string.Empty;
        private string description = string.Empty;
        private string imagePath = string.Empty;

        public static ObservableCollection<ItemModel> ItemsList { get; set; } = new ObservableCollection<ItemModel>();

        public string Titre
        {
            get => titre;
            set
            {
                if (titre != value)
                {
                    titre = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Description
        {
            get => description;
            set
            {
                if (description != value)
                {
                    description = value;
                    OnPropertyChanged();
                }
            }
        }

        public string ImagePath
        {
            get => imagePath;
            set
            {
                if (imagePath != value)
                {
                    imagePath = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand AjouterCommand { get; }
        public ICommand ChoisirImageCommand { get; }

        public AjoutViewModel()
        {
            AjouterCommand = new Command(async () => await AjouterItem());
            ChoisirImageCommand = new Command(async () => await ChoisirImage());
        }

        private async Task ChoisirImage()
        {
            try
            {
                var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
                {
                    Title = "Choisir une image"
                });

                if (result != null)
                {
                    ImagePath = result.FullPath;
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Erreur", $"Impossible de sélectionner l'image: {ex.Message}", "OK");
            }
        }

        private async Task AjouterItem()
        {
            if (string.IsNullOrWhiteSpace(Titre))
            {
                await Application.Current.MainPage.DisplayAlert("Erreur", "Veuillez saisir un titre", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(Description))
            {
                await Application.Current.MainPage.DisplayAlert("Erreur", "Veuillez saisir une description", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(ImagePath))
            {
                await Application.Current.MainPage.DisplayAlert("Erreur", "Veuillez choisir une image", "OK");
                return;
            }

            var nouvelItem = new ItemModel
            {
                Titre = Titre,
                Description = Description,
                ImagePath = ImagePath,
                DateAjout = DateTime.Now
            };

            ItemsList.Add(nouvelItem);

            await Application.Current.MainPage.DisplayAlert("Succès", "L'élément a été ajouté avec succès", "OK");

            // Réinitialiser les champs
            Titre = string.Empty;
            Description = string.Empty;
            ImagePath = string.Empty;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
