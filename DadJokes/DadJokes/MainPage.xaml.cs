using Newtonsoft.Json;

namespace DadJokes;


public partial class MainPage : ContentPage
{

	public MainPage()
	{
		InitializeComponent();
	}

     
    private static async Task<dynamic> GetContent(string url)
    {

        // New Request for JSON
        HttpClient client = new HttpClient();
        //client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (platform; rv:geckoversion) Gecko/geckotrail Firefox/firefoxversion");
        client.DefaultRequestHeaders.Add("Accept", "application/json");
        HttpResponseMessage response = await client.GetAsync(url);
        response.EnsureSuccessStatusCode();

        //JSON Fun Time
        string content = await response.Content.ReadAsStringAsync();
        dynamic result = JsonConvert.DeserializeObject<dynamic>(content);
        return result;
    }

    private async void GenerateButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new JokeViewPage());
    }
}


