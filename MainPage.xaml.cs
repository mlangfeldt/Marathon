using Marathon.Models;
using Newtonsoft.Json;
namespace Marathon;

public partial class MainPage : ContentPage
{
    private RaceCollection RaceObject;
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

        RaceObject = JsonConvert.DeserializeObject<RaceCollection>(wsJson);

        RacePicker.ItemsSource = RaceObject.races;//data for race picker

    }


    private void RacePicker_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        var SelectedRace = ((Picker)sender).SelectedIndex; //every picker is entry, must cast sender, this object is picker object
        var RaceID = RaceObject.races[SelectedRace].id;

        var client = new HttpClient();
        client.BaseAddress = new Uri("https://joewetzel.com/fvtc/marathon/");//make sure https
        var Response = client.GetAsync("results/" + RaceID).Result;
        var wsJson = Response.Content.ReadAsStringAsync().Result;
        
        var ResultObject = JsonConvert.DeserializeObject<ResultCollection>(wsJson);

        var CellTemplate = new DataTemplate(typeof(TextCell));
        CellTemplate.SetBinding(TextCell.TextProperty, "name");
        CellTemplate.SetBinding(TextCell.DetailProperty, "detail");

        lstResults.ItemTemplate = CellTemplate;
        lstResults.ItemsSource = ResultObject.results;

    }
}