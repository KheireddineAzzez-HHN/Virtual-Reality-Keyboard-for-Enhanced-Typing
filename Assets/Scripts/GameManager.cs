using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public TypingManager typingManager;
    [SerializeField]
    private string nextSceneName;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded; // Register the event
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
        currentEnv envComponent = FindObjectOfType<currentEnv>();
        if (envComponent != null)
        {
            if (envComponent.ENV == KeyboardConfig.env_data_collection.Prod)
            {
                MongoDBUtility.Instance.UpdateCollectionName("Prod");
            }
            else if (envComponent.ENV == KeyboardConfig.env_data_collection.Test)
            {
                MongoDBUtility.Instance.UpdateCollectionName("Test");
            }
        }
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ChangeToNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }

    public string NextSceneName
    {
        get { return nextSceneName; }
        set { nextSceneName = value; }
    }

    public currentEnv env_type()
    {
        currentEnv envComponent = FindObjectOfType<currentEnv>();
        return envComponent;
    }
}
