using Newtonsoft.Json;
using System.Threading.Tasks;

namespace DadJokes;

public partial class JokeViewPage : ContentPage
{

    public JokeViewPage()
    {
        InitializeComponent();
        Handle();
    }
    private async void Handle()
    {
        JokeLabel.Text = await GetContent("https://icanhazdadjoke.com/");
    }
    private static async Task<string> GetContent(string url)
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
        return result.joke;
    }
}