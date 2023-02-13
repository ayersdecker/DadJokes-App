using Newtonsoft.Json;
using System.Threading.Tasks;

namespace DadJokes;

public partial class JokeViewPage : ContentPage
{

    public JokeViewPage()
    {
        InitializeComponent();
        BindingContext = this;
        Handle();
        

    }
    private async void Handle()
    {
        JokeView.ItemsSource = await SearchJoke(30);
    }
    private static async Task<List<Joke>> SearchJoke(int limit)
    {
        // Default List for Jokes & URL 
        List<Joke> list = new List<Joke>();
        string url = $"https://icanhazdadjoke.com/search?limit={limit}";

        // New Request for JSON
        HttpClient client = new HttpClient();
        //client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (platform; rv:geckoversion) Gecko/geckotrail Firefox/firefoxversion");
        client.DefaultRequestHeaders.Add("Accept", "application/json");
        HttpResponseMessage response = await client.GetAsync(url);
        response.EnsureSuccessStatusCode();

        //JSON Fun Time
        string content = await response.Content.ReadAsStringAsync();
        dynamic result = JsonConvert.DeserializeObject<dynamic>(content);

        foreach (dynamic item in result.results) 
        {
            Joke joke = new Joke();
            joke.Text = item.joke;
            list.Add(joke); 
        }
        return list;

    }
}