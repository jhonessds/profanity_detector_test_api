using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Newtonsoft.Json;
using System;

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
            var result = _profanityFilter.IsProfanity(term);
            var profanityLog = new ProfanityLog(term, result, "Validate Word");
            SaveChanges(profanityLog);

            return Ok(profanityLog);
        }

        [HttpGet]
        [Route("/profanity/sentence")]
        public IActionResult GetSentence(string sentence)
        {
            var result = _profanityFilter.ContainsProfanity(sentence);

            var profanityLog = new ProfanityLog(sentence, result, "Validate Sentence");
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
            var txtJson = JsonConvert.DeserializeObject<string[]>(file);
            string[] wordList = txtJson;
            return wordList;
        }
    }
}