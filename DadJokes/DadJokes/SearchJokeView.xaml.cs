using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Net.Http;

namespace DadJokes;

public partial class SearchJokeView : ContentPage
{
    public ObservableCollection<Joke> jokes = new ObservableCollection<Joke>();
	public SearchJokeView()
	{
		InitializeComponent();
        BindingContext= this;
        JokeView.ItemsSource = jokes;
	}

    private async void Search_Clicked(object sender, EventArgs e)
    {
        jokes.Clear();
		string url;
		if(!(String.IsNullOrEmpty(Entry.Text))) {url = $"https://icanhazdadjoke.com/search?term={Entry.Text}&limit={30}";}
        else { url = $"https://icanhazdadjoke.com/search?limit={30}";}


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
            jokes.Add(joke);
        }
        if(jokes.Count == 0) { jokes.Add(new Joke("No Jokes Found")); }
        

    }
}