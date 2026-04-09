using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Storage;
using Plugin.LocalNotification;
using Plugin.LocalNotification.AndroidOption;

namespace BarsboldApp.ViewModels;

public class ParametresViewModel : INotifyPropertyChanged
{
    private bool _isDarkMode;
    private bool _isNotificationsEnabled;

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

    public bool IsNotificationsEnabled
    {
        get => _isNotificationsEnabled;
        set
        {
            if (_isNotificationsEnabled != value)
            {
                _isNotificationsEnabled = value;
                OnPropertyChanged();
                
                Preferences.Default.Set("NotifsActivees", value);

                if (value)
                {
                    var notification = new NotificationRequest
                    {
                        NotificationId = 1000,
                        Title = "🔔 Succès !",
                        Description = "Les notifications sont maintenant activées.",
                        BadgeNumber = 1,
                        Schedule = { NotifyTime = DateTime.Now.AddSeconds(1) },
                        Android = new AndroidOptions
                        {
                            IconSmallName = new AndroidIcon("logos")
                        }
                    };
                    LocalNotificationCenter.Current.Show(notification);
                }
            }
        }
    }

    public ParametresViewModel()
    {
      
        IsDarkMode = Preferences.Default.Get("DarkModeChoisi", false);
        IsNotificationsEnabled = Preferences.Default.Get("NotifsActivees", true);
    }
    
    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}