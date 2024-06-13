using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Upravovac.Model
{
    public  class Upravovator
    {
        public string _content;
        public string[] splited;
        public string word;
        public string firstLetter;

        public Upravovator()
        {
            
        }

        public void Upravuj(string content)
        {
            _content = content;
            _content = _content.ToLower();
            splited = _content.Split(';', StringSplitOptions.RemoveEmptyEntries);
            var formattedWords = splited.Select(word => char.ToUpper(word[0]) + word.Substring(1).ToLower());
            _content = string.Join(Environment.NewLine, formattedWords);
        }
    }
}
