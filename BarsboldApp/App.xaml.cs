using Plugin.LocalNotification;
using Microsoft.Maui.Controls;

namespace BarsboldApp;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        // Demande de permission au démarrage (Android 13+ / iOS)
        LocalNotificationCenter.Current.RequestNotificationPermission();

        MainPage = new AppShell();
    }
}