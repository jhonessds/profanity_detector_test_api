using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Newtonsoft.Json;
using ProfanityDetector.Extensions;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ProfanityDetector.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProfanityController : ControllerBase

    {
        private ProfanityFilter.ProfanityFilter _profanityFilter;
        private readonly IConfiguration _config;

        public ProfanityController(IConfiguration config)
        {
            _profanityFilter = new ProfanityFilter.ProfanityFilter();
            _profanityFilter.AddProfanity(WordList());
            _config = config;
        }

        [HttpGet]
        public IActionResult GetWord(string term)
        {
            var isProfanity = _profanityFilter.IsProfanity(term);

            var termAfterLeet = term.LeetDecode();
            var profanityLog = new ProfanityLog(termAfterLeet, isProfanity, "Validate Word", term);
            SaveChanges(profanityLog);

            return Ok(profanityLog);
        }

        [HttpGet]
        [Route("~/profanity/sentence")]
        public IActionResult GetSentence(string sentence)
        {
            var result = _profanityFilter.ContainsProfanity(sentence);
            var sentenceAfterLeet = sentence.LeetDecode();
            var profanityLog = new ProfanityLog(sentenceAfterLeet, result, "Validate Sentence", sentence);
            SaveChanges(profanityLog);

            return Ok(profanityLog);
        }

        private void SaveChanges(ProfanityLog profanityLog)
        {
            IMongoClient client = new MongoClient(_config.GetValue<string>("MongoConnectionString"));
            IMongoDatabase database = client.GetDatabase("ProfanityLog");
            IMongoCollection<ProfanityLog> colProfanityLog = database.GetCollection<ProfanityLog>("ProfanityLog");

            colProfanityLog.InsertOne(profanityLog);
        }

        private string[] WordList()
        {
            var file = System.IO.File.ReadAllText("wordList.json");
            var txtJson = JsonConvert.DeserializeObject<List<BadWord>>(file);
            string[] wordList = txtJson.Select(x => x.Word).ToArray();

            return wordList;
        }

        [HttpGet]
        [Route("~/profanity/words")]
        public IActionResult AddWord(string word, string language = "pt")
        {
            var file = System.IO.File.ReadAllText("wordList.json");
            var words = JsonConvert.DeserializeObject<List<BadWord>>(file);

            var contains = words.Find(x => x.Word == word && x.Language == language);
            if (contains != null)
            {
                return BadRequest("Já xingada ლ( ̅°̅ ੪ ̅°̅ )ლ");
            }

            var add = new BadWord(word, language);
            words.Add(add);

            using (StreamWriter outputFile = new StreamWriter("wordList.json"))
            {
                string json = JsonConvert.SerializeObject(words);
                outputFile.Write(json);
            }

            return Ok("Que boca suja heim meu jovem ಠᴗಠ");
        }

        [HttpDelete]
        [Route("~/profanity/words/word")]
        public IActionResult ToSpaceWord(string word)
        {
            var file = System.IO.File.ReadAllText("wordList.json");
            var words = JsonConvert.DeserializeObject<List<BadWord>>(file);

            var badWord = words.Find(x => x.Word == word);
            if (badWord == null)
            {
                return NotFound(@"Não encontrada ¯\_(ツ)_/¯");
            }
            words.Remove(badWord);

            using (StreamWriter outputFile = new StreamWriter("wordList.json"))
            {
                string json = JsonConvert.SerializeObject(words);
                outputFile.Write(json);
            }

            return Ok("Palavra removida ( ͡° ͜ʖ ͡°)");
        }
    }
}