using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using BarsboldApp.Models;
using Microsoft.Maui.Storage;
using Plugin.LocalNotification;
using Plugin.LocalNotification.AndroidOption;

namespace BarsboldApp.ViewModels
{
    public class AjoutViewModel : INotifyPropertyChanged
    {
        private string titre = string.Empty;
        private string description = string.Empty;
        private string imagePath = string.Empty;
        private string titreError = string.Empty;
        private string descriptionError = string.Empty;
        private string imageError = string.Empty;

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
                    ValidateTitre();
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
                    ValidateDescription();
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
                    ValidateImage();
                }
            }
        }

        public string TitreError
        {
            get => titreError;
            set
            {
                if (titreError != value)
                {
                    titreError = value;
                    OnPropertyChanged();
                }
            }
        }

        public string DescriptionError
        {
            get => descriptionError;
            set
            {
                if (descriptionError != value)
                {
                    descriptionError = value;
                    OnPropertyChanged();
                }
            }
        }

        public string ImageError
        {
            get => imageError;
            set
            {
                if (imageError != value)
                {
                    imageError = value;
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

        private void ValidateTitre()
        {
            TitreError = string.IsNullOrWhiteSpace(Titre) ? "Le titre est obligatoire" : string.Empty;
        }

        private void ValidateDescription()
        {
            DescriptionError = string.IsNullOrWhiteSpace(Description) ? "La description est obligatoire" : string.Empty;
        }

        private void ValidateImage()
        {
            ImageError = string.IsNullOrWhiteSpace(ImagePath) ? "Veuillez sélectionner une image" : string.Empty;
        }

        private bool ValidateForm()
        {
            ValidateTitre();
            ValidateDescription();
            ValidateImage();

            return string.IsNullOrEmpty(TitreError) && 
                   string.IsNullOrEmpty(DescriptionError) && 
                   string.IsNullOrEmpty(ImageError);
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
                await Application.Current?.MainPage?.DisplayAlert("Erreur", $"Impossible de sélectionner l'image: {ex.Message}", "OK");
            }
        }

        private async Task AjouterItem()
        {
            if (!ValidateForm())
            {
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

            // Notification si activée
            if (Microsoft.Maui.Storage.Preferences.Default.Get("NotifsActivees", true))
            {
                var notification = new NotificationRequest
                {
                    NotificationId = 2000,
                    Title = "📖 Nouvel élément !",
                    Description = $"L'élément '{Titre}' a été ajouté à votre collection.",
                    BadgeNumber = 1,
                    Android = new AndroidOptions
                    {
                        IconSmallName = new AndroidIcon("logos")
                    }
                };
                LocalNotificationCenter.Current.Show(notification);
            }

            await Application.Current?.MainPage?.DisplayAlert("Succès", "L'élément a été ajouté avec succès", "OK");

            // Réinitialiser les champs
            Titre = string.Empty;
            Description = string.Empty;
            ImagePath = string.Empty;
            TitreError = string.Empty;
            DescriptionError = string.Empty;
            ImageError = string.Empty;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
