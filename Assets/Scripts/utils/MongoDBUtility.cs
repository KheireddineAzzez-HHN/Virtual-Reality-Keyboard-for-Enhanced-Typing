using System.Collections.Generic;
using UnityEngine;
using MongoDB.Bson;
using MongoDB.Driver;

public class MongoDBUtility : MonoBehaviour
{
    private MongoClient client;
    private IMongoDatabase database;
    private IMongoCollection<BsonDocument> collection;

     private string connectionString = "mongodb+srv://unityLab:UFoHeI10inPOUUPs@unitycluster.jtjndx8.mongodb.net/?retryWrites=true&w=majority&appName=UnityCluster";
    [SerializeField] public string databaseName;
    [SerializeField] public string collectionName;
    void Start()
    {
        ConnectToMongoDB();
    }

    private void ConnectToMongoDB()
    {
        client = new MongoClient(connectionString);
        database = client.GetDatabase(databaseName);
        collection = database.GetCollection<BsonDocument>(collectionName);
        Debug.Log("Connected to MongoDB");
    }

    public void InsertTypingData(TypingController.TypingData data)
    {
        BsonDocument document = new BsonDocument
        {
            { "expected", data.expected },
            { "typed", data.typed },
            { "timeTaken", data.timeTaken },
            { "keystrokeCount", data.keystrokeCount },
            { "errorRate", data.errorRate },
            { "accuracyInCharacters", data.accuracyInCharacters },
            { "accuracyInWords", data.accuracyInWords },
            { "accuracyInKeystrokes", data.accuracyInKeystrokes },
            { "typingSpeed", data.typingSpeed },
            { "keystrokesPerCharacter", data.keystrokesPerCharacter },
            { "sessionTime", data.sessionTime }
        };

        collection.InsertOne(document);
        Debug.Log("Typing data inserted into MongoDB");
    }
}
