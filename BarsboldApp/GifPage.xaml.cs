namespace BarsboldApp;

public partial class GifPage : ContentPage
{
    public GifPage()
    {
        InitializeComponent();
        LoadGif();
    }

    private async void LoadGif()
    {
        try
        {
            using var stream = await FileSystem.OpenAppPackageFileAsync("mongif.gif");
            using var memoryStream = new MemoryStream();
            await stream.CopyToAsync(memoryStream);
            var base64 = Convert.ToBase64String(memoryStream.ToArray());

            var html = $@"
<!DOCTYPE html>
<html>
<head>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <style>
        body {{
            margin: 0;
            padding: 0;
            display: flex;
            justify-content: center;
            align-items: center;
            height: 100vh;
            background-color: #F5F5F5;
        }}
        img {{
            max-width: 100%;
            max-height: 100%;
            object-fit: contain;
        }}
    </style>
</head>
<body>
    <img src='data:image/gif;base64,{base64}' alt='Animation'>
</body>
</html>";

            GifWebView.Source = new HtmlWebViewSource { Html = html };
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Erreur chargement GIF: {ex.Message}");
        }
    }

    private async void OnBackButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}
