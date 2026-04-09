using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Storage;

namespace BarsboldApp.ViewModels;

public class ParametresViewModel : INotifyPropertyChanged
{
    private bool _isDarkMode;

    public bool IsDarkMode
    {
        get => _isDarkMode;
        set
        {
            if (_isDarkMode != value)
            {
                _isDarkMode = value;
                OnPropertyChanged();
                
                
                Application.Current.UserAppTheme = value ? AppTheme.Dark : AppTheme.Light;
                
              
                Preferences.Default.Set("DarkModeChoisi", value);
            }
        }
    }

    public ParametresViewModel()
    {
        IsDarkMode = Preferences.Default.Get("DarkModeChoisi", false);
    }
    
    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}