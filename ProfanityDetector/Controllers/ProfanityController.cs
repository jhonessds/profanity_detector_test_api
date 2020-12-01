﻿using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Newtonsoft.Json;
using System;
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

        public ProfanityController()
        {
            _profanityFilter = new ProfanityFilter.ProfanityFilter();
            _profanityFilter.AddProfanity(WordList());
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
            IMongoClient client = new MongoClient(Environment.GetEnvironmentVariable("MongoConnectionString"));
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

        [HttpPost]
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
    }
}