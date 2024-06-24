using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private List<string> keyboardTypes;
    public SceneStackManager sceneStackManager;

    public UserData UserData { get; set; }
    public string KeyboardType { get; set; } // Add KeyboardType

    [SerializeField]
    private string nextSceneName;
    public ConfigManager configManager;
   
    private int keyboardTypeIndex;

    public string CurrentKeyboardType { get; set; }

    public  current_Scene_Env envComponent;
    public static event Action TypingCompleted;

    void Awake()
    {
        if (Instance == null)
        {
            InitializeConfig();

            Instance = this;
            DontDestroyOnLoad(gameObject);

            SceneManager.sceneLoaded += OnSceneLoaded; // Register the event


        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {

    }
    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // Unregister the event
    }
    private void OnEnable()
    {
        UserInputManager.StartTest += this.StartTest;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Check the environment type and update the collection name


        envComponent = FindObjectOfType<current_Scene_Env>();

        if (envComponent != null)
        {
            if (envComponent.Scene_Type == KeyboardConfig.env_data_collection.Prod)
            {
                MongoDBUtility.Instance.UpdateCollectionName("Prod");
                envComponent.update_current_Scene_Env();
                UpdateKeyboardAppearance();


            }
            else if (envComponent.Scene_Type == KeyboardConfig.env_data_collection.Test)
            {
                MongoDBUtility.Instance.UpdateCollectionName("Test");
                envComponent.update_current_Scene_Env();
                UpdateKeyboardAppearance();


            }
            else if(envComponent.Scene_Type == KeyboardConfig.env_data_collection.USERCONFIG)
            {

                envComponent.update_current_Scene_Env();


            }
            else if(envComponent.Scene_Type == KeyboardConfig.env_data_collection.WAITING)
            {

                envComponent.update_current_Scene_Env();

            }

            UpdateKeyboardAppearance();

        }
    }

  

    private async void InitializeConfig()
    {
        GlobalConfig config = await configManager.LoadGlobalConfig();

        if (config != null)
        {
            keyboardTypeIndex = 0;

            keyboardTypes = config.KeyboardTypes;
            sceneStackManager.InitializeSceneStack(keyboardTypes);


        }
        CurrentKeyboardType = keyboardTypes[keyboardTypeIndex];

    }




    public void CompleteCurrentPhase()
    {

        if (envComponent.Scene_Type == KeyboardConfig.env_data_collection.Prod)
        {
            keyboardTypeIndex++;
            if (keyboardTypeIndex < keyboardTypes.Count)
            {
                CurrentKeyboardType = keyboardTypes[keyboardTypeIndex];


                sceneStackManager.LoadNextScene();
            }
            else
            {
                Debug.Log("All keyboard types have been tested.");
                // Optionally handle completion of all tests
            }
        }
        else if(envComponent.Scene_Type == KeyboardConfig.env_data_collection.Test)
        {
            sceneStackManager.LoadNextScene();

        }



    }

    public  void StartTest()
    {
        sceneStackManager.LoadNextScene();

    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);


    }

    public void ChangeToNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }

    public current_Scene_Env GetCurrentEnv()
    {
        return this.envComponent;
    }

    private void UpdateKeyboardAppearance()
    {
        KeyboardAppearance.Instance.UpdateKeyboards(CurrentKeyboardType);
    }

}
