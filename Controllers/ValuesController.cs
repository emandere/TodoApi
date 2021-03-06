﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            MongoClient _client;
            MongoServer _server;
            MongoDatabase _db;

            string databasename = System.IO.File.ReadAllText(@"/var/run/secrets/mongodb/account/database-name");
            string databaseuser = System.IO.File.ReadAllText(@"/var/run/secrets/mongodb/account/database-user");
            string databasepass = System.IO.File.ReadAllText(@"/var/run/secrets/mongodb/account/database-password");

            _client = new MongoClient($"mongodb://{databaseuser}:{databasepass}@mongodb/{databasename}");
           
            _server = _client.GetServer();
            _db = _server.GetDatabase(databasename);
            var pairs = new List<String>();
            foreach(Person pair in _db.GetCollection<Person>("people").FindAll())
            {
               pairs.Add(pair.name); 
            }

            

            return pairs;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }

    public class Person
    {
        public ObjectId Id { get; set; }
        [BsonElement("name")]
        public string name{get;set;}
    }
}
