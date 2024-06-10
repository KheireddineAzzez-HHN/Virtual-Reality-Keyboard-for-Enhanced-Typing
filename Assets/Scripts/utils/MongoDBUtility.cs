using System.Collections.Generic;
using UnityEngine;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;

public class MongoDBUtility : MonoBehaviour
{
    private MongoClient client;
    private IMongoDatabase database;
    private IMongoCollection<BsonDocument> collection;
    [SerializeField] private string usersCollectionName = "Users";
    [SerializeField] private string ConfigCollectionName = "GlobalConfig";

    private IMongoCollection<BsonDocument> usersCollection;
    private IMongoCollection<BsonDocument> configCollection;

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
        usersCollection = database.GetCollection<BsonDocument>(usersCollectionName);
        configCollection = database.GetCollection<BsonDocument>(ConfigCollectionName);

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
            { "userId", data.userId },
            {"keyboardType",data.KeyboardType }

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


    public async Task<GlobalConfig> FetchGlobalConfig()
    {

            var result = await configCollection.Find(new BsonDocument()).FirstOrDefaultAsync();

            if (result != null)
            {
                var gloves = result["Gloves"].AsBsonDocument;
                var controllerKeyboard = result["ControllerKeyboard"].AsBsonDocument;
                var session = result["session"].AsBsonDocument;
            var keyboardTypesBsonArray = result["KeyboardTypes"].AsBsonArray;
            var waitingDuration = result["waitingTimeSeconds"].AsInt32;  
           
            var glovesConfig = new GlovesConfig
                {
                    BuzzThumb = (float)gloves["BuzzThumb"].AsDouble,
                    ForceFeedbackThumb = (float)gloves["ForceFeedbackThumb"].AsDouble,
                    ForceFeedbackIndex = (float)gloves["ForceFeedbackIndex"].AsDouble,
                    ForceFeedbackMiddle = (float)gloves["ForceFeedbackMiddle"].AsDouble,
                    ForceFeedbackRing = (float)gloves["ForceFeedbackRing"].AsDouble,
                    ForceFeedbackPinky = (float)gloves["ForceFeedbackPinky"].AsDouble
                };

                var controllerKeyboardConfig = new ControllerKeyboardConfig
                {
                    VibrationLevel = (float)controllerKeyboard["VibrationLevel"].AsDouble
                };

                var sessionConfig = new SessionConfig
                {
                    PhraseToTypeRealTest = session["PhraseToTypeRealTest"].AsInt32,
                    PhraseToTypeFakeTest = session["PhraseToTypeFakeTest"].AsInt32
                };

            List<string> keyboardTypes = new List<string>();

            foreach (var type in keyboardTypesBsonArray)
            {
                keyboardTypes.Add(type.AsString);
            }
            return new GlobalConfig
            {
                Gloves = glovesConfig,
                ControllerKeyboard = controllerKeyboardConfig,
                Session = sessionConfig,
                KeyboardTypes = keyboardTypes,
                WaitingDuration= waitingDuration
            };
            }
            else
            {
                Debug.LogError("No global config found in the collection: " + ConfigCollectionName);
                return null;
            }
       
    }

    public void InsertUserData(UserData userData)
    {

        BsonDocument document = new BsonDocument
        {
            { "userId", userData.userId },
            { "userName", userData.userName },
            { "userAge", userData.userAge },
            { "userSex", userData.userSex }
        };

        usersCollection.InsertOne(document);
        Debug.Log("User data inserted into MongoDB");
    }
}
