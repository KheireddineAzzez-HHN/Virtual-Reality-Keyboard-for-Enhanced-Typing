using System.Collections.Generic;
using UnityEngine;
using MongoDB.Bson;
using MongoDB.Driver;

public class MongoDBUtility : MonoBehaviour
{
    private MongoClient client;
    private IMongoDatabase database;
    private IMongoCollection<BsonDocument> collection;

    private static MongoDBUtility _instance;
    public static MongoDBUtility Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<MongoDBUtility>();

                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject();
                    _instance = singletonObject.AddComponent<MongoDBUtility>();
                    singletonObject.name = typeof(MongoDBUtility).ToString() + " (Singleton)";
                }
            }

            return _instance;
        }
    }

    private string connectionString = "mongodb+srv://unityLab:UFoHeI10inPOUUPs@unitycluster.jtjndx8.mongodb.net/?retryWrites=true&w=majority&appName=UnityCluster";
    [SerializeField] public string databaseName;
    [SerializeField] public string collectionName;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
            ConnectToMongoDB();
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void ConnectToMongoDB()
    {
        client = new MongoClient(connectionString);
        database = client.GetDatabase(databaseName);
        collection = database.GetCollection<BsonDocument>(collectionName);
        Debug.Log("Connected to MongoDB");
    }

    public void InsertTypingData(TypingData data)
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
            { "sessionTime", data.sessionTime },
            { "userId", data.userId }

        };

        collection.InsertOne(document);
        Debug.Log("Typing data inserted into MongoDB");
    }

    public void UpdateCollectionName(string newCollectionName)
    {

        if (collectionName == newCollectionName)
        {
            Debug.Log("The collectionName has been already seted ");
        }
        else
        {

     
        collectionName = newCollectionName;
        collection = database.GetCollection<BsonDocument>(collectionName);
        Debug.Log($"Collection name updated to {newCollectionName}");

        }
    }
}
