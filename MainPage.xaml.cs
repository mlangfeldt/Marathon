using Marathon.Models;
using Newtonsoft.Json;
namespace Marathon;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        Title = "Marathon Manager";
        FillPicker();
    }

    public void FillPicker()
    {
        var client = new HttpClient();
        client.BaseAddress = new Uri("https://joewetzel.com/fvtc/marathon/");//make sure https
        var Response = client.GetAsync("races/").Result;
        var wsJson = Response.Content.ReadAsStringAsync().Result;

        var RaceObject = JsonConvert.DeserializeObject<RaceCollection>(wsJson);

        RacePicker.ItemsSource = RaceObject.races;

    }

    
}