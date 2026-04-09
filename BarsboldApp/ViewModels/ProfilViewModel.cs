using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using BarsboldApp.Models;

namespace BarsboldApp.ViewModels
{
    public class ProfilViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<ItemModel> Items => AjoutViewModel.ItemsList;

        public bool IsListEmpty => Items.Count == 0;
        public bool IsNotEmpty => Items.Count > 0;

        public ProfilViewModel()
        {
            // S'abonner aux changements de la collection
            Items.CollectionChanged += (s, e) => 
            {
                OnPropertyChanged(nameof(IsListEmpty));
                OnPropertyChanged(nameof(IsNotEmpty));
            };
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
