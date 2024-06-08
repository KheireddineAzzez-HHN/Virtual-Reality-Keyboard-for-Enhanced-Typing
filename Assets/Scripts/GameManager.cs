using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public TypingManager typingManager;

    public UserData UserData { get; set; }
    public string KeyboardType { get; set; } // Add KeyboardType

    [SerializeField]
    private string nextSceneName;
    public ConfigManager configManager;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded; // Register the event

            InitializeConfig();

        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // Unregister the event
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Check the environment type and update the collection name
        current_Scene_Env envComponent = FindObjectOfType<current_Scene_Env>();
        if (envComponent != null)
        {
            if (envComponent.Scene_Type == KeyboardConfig.env_data_collection.Prod)
            {
                MongoDBUtility.Instance.UpdateCollectionName("Prod");
            }
            else if (envComponent.Scene_Type == KeyboardConfig.env_data_collection.Test)
            {
                MongoDBUtility.Instance.UpdateCollectionName("Test");
            }
        }
    }

    private async void InitializeConfig()
    {
        GlobalConfig config = await configManager.LoadGlobalConfig();
        if (config != null)
        {
            KeyboardType = config.KeyboardType;
            if (KeyboardType == "Controllers_With_Keyboard")
            {
                configManager.ApplyControllerKeyboardConfig(config.ControllerKeyboard);
            }
            else if (KeyboardType == "Gloves_With_Keyboard")
            {
                configManager.ApplyGlovesConfig(config.Gloves);
            }
        }
    }

    private void HandleControllersKeyboardConfig(ControllerKeyboardConfig config)
    {
        // Implement special treatment for Controllers_Keyboard here
        Debug.Log("Handling Controllers_Keyboard configuration");
        Debug.Log($"VibrationLevel: {config.VibrationLevel}");
        // Apply the VibrationLevel to the appropriate system
    }

    private void HandleGlovesConfig(GlovesConfig config)
    {
        // Implement special treatment for Gloves configuration here
        Debug.Log("Handling Gloves configuration");
        Debug.Log($"BuzzThumb: {config.BuzzThumb}");
        Debug.Log($"ForceFeedbackThumb: {config.ForceFeedbackThumb}");
        Debug.Log($"ForceFeedbackIndex: {config.ForceFeedbackIndex}");
        Debug.Log($"ForceFeedbackMiddle: {config.ForceFeedbackMiddle}");
        Debug.Log($"ForceFeedbackRing: {config.ForceFeedbackRing}");
        Debug.Log($"ForceFeedbackPinky: {config.ForceFeedbackPinky}");
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
        return FindObjectOfType<current_Scene_Env>();
    }
}
