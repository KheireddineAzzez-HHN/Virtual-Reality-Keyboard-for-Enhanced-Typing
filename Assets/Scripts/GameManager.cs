using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public  TypingManager typingManager ;
    [SerializeField]
    private string nextSceneName;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
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
}
