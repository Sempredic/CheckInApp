﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CheckInStation
{
    public class MongoCRUD
    {
        private IMongoDatabase db;

        private static MongoCRUD instance = null;


        public MongoCRUD()
        {

        }

        public bool DBConnectionStatus()
        {

            bool status = true;

            try
            {
                var command = new BsonDocument { { "dbstats", 1 } };
                var result = db.RunCommand<BsonDocument>(command);
            }
            catch (Exception e)
            {
                status = false;
            }

            return status;
        }

        public void ConnectToDB(string dbConnString)
        {
            var client = new MongoClient("mongodb+srv://311015:GErnamS8VyBxir64@cluster0-qvscx.mongodb.net/test?retryWrites=true&w=majority");
            var database = client.GetDatabase(dbConnString);

            //var client = new MongoClient();
            db = client.GetDatabase(dbConnString);


        }

        public static MongoCRUD GetInstance()
        {
            if (instance == null)
            {
                instance = new MongoCRUD();
            }
            return instance;
        }

        public bool RecordExists<T>(string table, string id, string fieldName)
        {
            List<T> list = null;
            bool exists = false;
            var collection = db.GetCollection<T>(table);

            var filter = Builders<T>.Filter.Eq(fieldName, id);
            list = collection.Find(filter).ToList();


            if (list.Count != 0)
            {
                exists = true;
            }

            return exists;
        }

        public void InsertRecord<T>(string table, T record, string id, string caseID)
        {
            var collection = db.GetCollection<T>(table);


            Type typeParameterType = typeof(T);

            if (typeParameterType.Name == "SerialInfo")
            {
                var serialCollection = db.GetCollection<SerialInfo>("Serial");

                collection.InsertOne(record);

                serialCollection.FindOneAndUpdate(c => c.serial == id,
                                Builders<SerialInfo>.Update.Set(c => c.caseID, caseID));

            }
            else if (typeParameterType.Name == "CaseInfo")
            {
                if (!RecordExists<T>(table, id, "caseID"))
                {
                    var caseCollection = db.GetCollection<CaseInfo>("Cases");

                    collection.InsertOne(record);


                }
                else
                {
                    CaseInfo a = (CaseInfo)(object)record;
                    var caseCollection = db.GetCollection<CaseInfo>("Cases");
                    caseCollection.FindOneAndUpdate(c => c.caseID == id,
                                Builders<CaseInfo>.Update.Set(c => c.curLoc, a.curLoc));
                    caseCollection.FindOneAndUpdate(c => c.caseID == id,
                               Builders<CaseInfo>.Update.Set(c => c.ageInfo, a.ageInfo));

                }

            }
            else if (typeParameterType.Name == "AreaInfo")
            {
                if (!RecordExists<T>(table, id, "areaName"))
                {
                    var caseCollection = db.GetCollection<AreaInfo>("Areas");

                    collection.InsertOne(record);


                }
            }

        }

        public void AppendRecord<T>(string table, string id, LocationData ld)
        {
            var collection = db.GetCollection<T>(table);

            var filter = Builders<T>.Filter.Eq("serial", id);
            var update = Builders<T>.Update.Push("locationData", ld);
            var result = collection.UpdateOne(filter, update);


            UpdateLastLocations(ld.ID, id);
            UpdateCaseSerials(id);
        }

        public void AppendRecord<T>(string table, string id, LocationObject ld)
        {
            var collection = db.GetCollection<T>(table);

            var filter = Builders<T>.Filter.Eq("areaName", id);
            var update = Builders<T>.Update.Push("locationsList", ld);
            var result = collection.UpdateOne(filter, update);

        }

        public bool RemoveRecord<T>(string table,Guid id)
        {
            var collection = db.GetCollection<T>(table);

            var filter = Builders<T>.Filter.Eq("ID",id);

            return collection.DeleteOne(filter).IsAcknowledged;
        }

        public void UpdateLocationCases(LocationObject lo, AreaInfo ai, CaseInfo ci)
        {
            var collection = db.GetCollection<AreaInfo>("Areas");

            Console.WriteLine(lo.locName + " UPDATED");

            collection.FindOneAndUpdate(c => c.areaName == ai.areaName && c.locationsList.Any(s => s.locName == lo.locName),
                            Builders<AreaInfo>.Update.Push(c => c.locationsList[-1].casesList, ci));
        }

        public void UpdateLastLocations(string id, string serial)
        {
            if (RecordExists<SerialInfo>("Serial", serial, "serial"))
            {
                SerialInfo list = LoadRecords<SerialInfo>("Serial", "serial", serial)[0];
                var collection = db.GetCollection<SerialInfo>("Serial");


                //loop through each locationData object and if object doesn't match the id of most recent entry then set last location to false, otherwise true
                foreach (LocationData ld in list.locationData)
                {

                    // Save the entire document back to the database

                    if (ld.ID != id)
                    {

                        collection.FindOneAndUpdate(c => c.serial == serial && c.locationData.Any(s => s.ID == ld.ID),
                            Builders<SerialInfo>.Update.Set(c => c.locationData[-1].lastLocation, false));

                    }
                    else
                    {
                        collection.FindOneAndUpdate(c => c.serial == serial && c.locationData.Any(s => s.ID == ld.ID),
                            Builders<SerialInfo>.Update.Set(c => c.locationData[-1].lastLocation, true));

                        collection.FindOneAndUpdate(c => c.serial == serial,
                            Builders<SerialInfo>.Update.Set(c => c.caseID, ld.curCase));
                    }

                }


            }


        }

        public void UpdateCaseSerials(string serial)
        {
            if (RecordExists<SerialInfo>("Serial", serial, "serial"))
            {

                SerialInfo si = LoadRecords<SerialInfo>("Serial", "serial", serial)[0];

                if (RecordExists<CaseInfo>("Cases", si.caseID, "caseID"))
                {
                    var collection = db.GetCollection<CaseInfo>("Cases");

                    CaseInfo ci = LoadRecords<CaseInfo>("Cases", "caseID", si.caseID)[0];

                    if (!ci.serialList.Contains(si))
                    {
                        collection.FindOneAndUpdate(c => c.caseID == ci.caseID,
                            Builders<CaseInfo>.Update.Push(c => c.serialList, si));
                    }

                }

            }
        }

        public List<T> LoadRecords<T>(string table, string type, string item)
        {

            List<T> list = new List<T>();

            if (db != null)
            {
                var collection = db.GetCollection<T>(table);

                Type typeParameterType = typeof(T);

                if (typeParameterType.Name == "SerialInfo")
                {
                    var filter = Builders<T>.Filter.Eq(type, item);
                    list = collection.Find(filter).ToList();

                }else if (typeParameterType.Name == "AreaInfo")
                {
                    if (type != null)
                    {
                        var filter = Builders<T>.Filter.Eq(type, item);
                        list = collection.Find(filter).ToList();
                    }
                    else
                    {
                        var filter = new BsonDocument();
                        list = collection.Find(filter).ToList();
                    }
                }else if (typeParameterType.Name == "CaseInfo")
                {
                    if (type != null && item != null)
                    {
                        var filter = Builders<T>.Filter.Eq(type, item);
                        list = collection.Find(filter).ToList();
                    }

                }
                else if (typeParameterType.Name == "LoginModel")
                {
                    var filter = Builders<T>.Filter.Eq(type, item);
                    list = collection.Find(filter).ToList();
                }
                else if (typeParameterType.Name == "SkuInfo")
                {
                    var filter = Builders<T>.Filter.Eq(type, item);
                    list = collection.Find(filter).ToList();
                }

            }
            else
            {
                Console.WriteLine("ERROR THIS IS NULL");
            }


            return list;
        }

        public DateTime GetServerTime()
        {
            var serverStatusCmd = new BsonDocumentCommand<BsonDocument>(new BsonDocument { { "serverStatus", 1 } });
            var result = db.RunCommand(serverStatusCmd);
            var localTime = result["localTime"].ToLocalTime();

            return localTime;
        }


    }

    public class LocationData
    {

        public string ID;
        public string userID;
        public string location;
        public string time;
        public string date;
        public string curCase;
        public bool lastLocation;

        public LocationData()
        {
            ID = GenerateID();
        }

        string GenerateID()
        {
            long i = 1;

            foreach (byte b in System.Guid.NewGuid().ToByteArray())
            {
                i *= ((int)b + 1);
            }

            return string.Format("{0:x}", i - System.DateTime.Now.Ticks);
        }
    }

    public class SerialInfo
    {
        [BsonId]
        public Guid ID;
        public string serial;
        public string caseID;
        public string skuID;
        public List<LocationData> locationData;

        public SerialInfo()
        {
            locationData = new List<LocationData>();
  
        }
    }

    public class AreaInfo
    {
        [BsonId]
        public Guid ID;
        public string areaName;
        public List<LocationObject> locationsList;

        public AreaInfo()
        {
            locationsList = new List<LocationObject>();
        }

    }

    public class LocationObject
    {

        public string locName;
        public List<CaseInfo> casesList;

        public LocationObject()
        {
            casesList = new List<CaseInfo>();
        }
    }

    public class AgeInfo
    {

        public int days;
        public int hours;
        public int minute;
    }
    public class CaseInfo
    {
        [BsonId]
        public Guid ID;
        public string caseID;
        public string ageInfo;
        public string curLoc;
        public List<SerialInfo> serialList;

        public CaseInfo()
        {
            serialList = new List<SerialInfo>();
        }
    }

    public class SkuInfo
    {
        [BsonId]
        public ObjectId ID;
        public string skuID;
        public string deviceName;
        public string deviceType;
        public string deviceCondition;
        public string unitPrice;

    }
}