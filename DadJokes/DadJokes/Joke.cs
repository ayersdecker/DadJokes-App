using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DadJokes
{
    public class Joke
    {
        public string Text { get; set; }

        public Joke()
        {
            Text = "N/A";
        }
        public Joke(string text)
        {
            Text = text;
        }
        
    }
}
