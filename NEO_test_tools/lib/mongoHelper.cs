using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using MongoDB.Bson.IO;
using System.IO;

namespace NEO_test_tools.lib
{
    public class mongoHelper
    {
        public string mongodbConnStr_testnet = string.Empty;
        public string mongodbDatabase_testnet = string.Empty;
        public string neoCliJsonRPCUrl_testnet = string.Empty;

        public string mongodbConnStr_mainnet = string.Empty;
        public string mongodbDatabase_mainnet = string.Empty;
        public string neoCliJsonRPCUrl_mainnet = string.Empty;

        public mongoHelper()
        {
            string configStr = File.ReadAllText(Directory.GetCurrentDirectory() + "\\mongodbsettings.json");
            JObject config = JObject.Parse(configStr);

            mongodbConnStr_testnet = (string)config["mongodbConnStr_testnet"];
            mongodbDatabase_testnet = (string)config["mongodbDatabase_testnet"];
            neoCliJsonRPCUrl_testnet = (string)config["neoCliJsonRPCUrl_testnet"];

            mongodbConnStr_mainnet = (string)config["mongodbConnStr_mainnet"];
            mongodbDatabase_mainnet = (string)config["mongodbDatabase_mainnet"];
            neoCliJsonRPCUrl_mainnet = (string)config["neoCliJsonRPCUrl_mainnet"];
        }

        public JArray GetData(string mongodbConnStr, string mongodbDatabase, string coll, string findFliter, string sortStr)
        {
            var client = new MongoClient(mongodbConnStr);
            var database = client.GetDatabase(mongodbDatabase);
            var collection = database.GetCollection<BsonDocument>(coll);

            List<BsonDocument> query = collection.Find(BsonDocument.Parse(findFliter)).Sort(sortStr).ToList();
            client = null;

            if (query.Count > 0)
            {
                var jsonWriterSettings = new JsonWriterSettings { OutputMode = JsonOutputMode.Strict };
                JArray JA = JArray.Parse(query.ToJson(jsonWriterSettings));
                foreach (JObject j in JA)
                {
                    j.Remove("_id");
                }
                return JA;
            }
            else { return new JArray(); }
        }

        public JArray GetDataPages(string mongodbConnStr, string mongodbDatabase, string coll, string sortStr, int pageCount, int pageNum, string findBson = "{}")
        {
            var client = new MongoClient(mongodbConnStr);
            var database = client.GetDatabase(mongodbDatabase);
            var collection = database.GetCollection<BsonDocument>(coll);

            List<BsonDocument> query = collection.Find(BsonDocument.Parse(findBson)).Sort(sortStr).Skip(pageCount * (pageNum - 1)).Limit(pageCount).ToList();
            client = null;

            if (query.Count > 0)
            {

                var jsonWriterSettings = new JsonWriterSettings { OutputMode = JsonOutputMode.Strict };
                JArray JA = JArray.Parse(query.ToJson(jsonWriterSettings));
                foreach (JObject j in JA)
                {
                    j.Remove("_id");
                }
                return JA;
            }
            else { return new JArray(); }
        }

        public long GetDataCount(string mongodbConnStr, string mongodbDatabase, string coll)
        {
            var client = new MongoClient(mongodbConnStr);
            var database = client.GetDatabase(mongodbDatabase);
            var collection = database.GetCollection<BsonDocument>(coll);

            var txCount = collection.Find(new BsonDocument()).Count();

            client = null;

            return txCount;
        }

        public long GetDataCount(string mongodbConnStr, string mongodbDatabase, string coll, string findBson)
        {
            var client = new MongoClient(mongodbConnStr);
            var database = client.GetDatabase(mongodbDatabase);
            var collection = database.GetCollection<BsonDocument>(coll);

            var txCount = collection.Find(BsonDocument.Parse(findBson)).Count();

            client = null;

            return txCount;
        }

        public JArray Getdatablockheight(string mongodbConnStr, string mongodbDatabase)
        {
            int blockDataHeight = -1;
            int txDataHeight = -1;
            int utxoDataHeight = -1;
            int notifyDataHeight = -1;
            int totalsysfeeDataHeight = -1;
            int NEP5DataHeight = -1;
            int fulllogDataHeight = -1;

            var client = new MongoClient(mongodbConnStr);
            var database = client.GetDatabase(mongodbDatabase);

            //var collection = database.GetCollection<BsonDocument>("block");
            //var sortBson = BsonDocument.Parse("{index:-1}");
            //var query = collection.Find(new BsonDocument()).Sort(sortBson).Limit(1).ToList();
            //if (query.Count > 0)
            //{blockDataHeight = (int)query[0]["index"];}

            //collection = database.GetCollection<BsonDocument>("tx");
            //sortBson = BsonDocument.Parse("{blockindex:-1}");
            //query = collection.Find(new BsonDocument()).Sort(sortBson).Limit(1).ToList();
            //if (query.Count > 0)
            //{ txDataHeight = (int)query[0]["blockindex"]; }

            var collection = database.GetCollection<BsonDocument>("system_counter");
            var query = collection.Find(new BsonDocument()).ToList();
            if (query.Count > 0)
            {
                foreach (var q in query)
                {
                    if ((string)q["counter"] == "block") { blockDataHeight = (int)q["lastBlockindex"]; txDataHeight = blockDataHeight; };
                    if ((string)q["counter"] == "utxo") { utxoDataHeight = (int)q["lastBlockindex"]; };
                    if ((string)q["counter"] == "notify") { notifyDataHeight = (int)q["lastBlockindex"]; };
                    if ((string)q["counter"] == "totalsysfee") { totalsysfeeDataHeight = (int)q["lastBlockindex"]; };
                    if ((string)q["counter"] == "NEP5") { NEP5DataHeight = (int)q["lastBlockindex"]; };
                    if ((string)q["counter"] == "fulllog") { fulllogDataHeight = (int)q["lastBlockindex"]; };
                }
            }

            client = null;

            JObject J = new JObject
            {
                { "blockDataHeight", blockDataHeight },
                { "txDataHeight", txDataHeight },
                { "utxoDataHeight", utxoDataHeight },
                { "notifyDataHeight", notifyDataHeight },
                { "totalsysfee", totalsysfeeDataHeight },
                { "NEP5", NEP5DataHeight },
                { "fulllogDataHeight", fulllogDataHeight }
            };
            JArray JA = new JArray
            {
                J
            };

            return JA;
        }

        public void InsertOneDataByCheckKey(string mongodbConnStr, string mongodbDatabase, string coll, JObject Jdata, string key, string value)
        {
            var client = new MongoClient(mongodbConnStr);
            var database = client.GetDatabase(mongodbDatabase);
            var collection = database.GetCollection<BsonDocument>(coll);

            var query = collection.Find("{'" + key + "':'" + value + "'}").ToList();
            if (query.Count == 0)
            {
                string strData = Newtonsoft.Json.JsonConvert.SerializeObject(Jdata);
                BsonDocument bson = BsonDocument.Parse(strData);
                bson.Add("getTime", DateTime.Now);
                collection.InsertOne(bson);
            }

            client = null;
        }

        public decimal GetTotalSysFeeByBlock(string mongodbConnStr, string mongodbDatabase, int blockindex)
        {
            var client = new MongoClient(mongodbConnStr);
            var database = client.GetDatabase(mongodbDatabase);
            var collection = database.GetCollection<BsonDocument>("block_sysfee");

            decimal totalSysFee = 0;
            var query = collection.Find("{'index':" + blockindex + "}").ToList();
            if (query.Count > 0)
            {
                totalSysFee = decimal.Parse(query[0]["totalSysfee"].AsString);
            }
            else
            {
                totalSysFee = -1;
            }

            client = null;

            return totalSysFee;
        }
    }
}
