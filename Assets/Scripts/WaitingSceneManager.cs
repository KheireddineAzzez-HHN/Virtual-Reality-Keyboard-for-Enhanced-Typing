using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class WaitingSceneManager : MonoBehaviour
{
    public TMP_Text waitingText; // Text to display the countdown
    private int waitingDuration;
    [SerializeField] private TMP_Text staticMessage ;
    void Start()
    {
        staticMessage.text = "You can now remove the head-mounted device to answer some post-task questions.";
        StartCoroutine(FetchAndWait());
    }

    IEnumerator FetchAndWait()
    {
        // Fetch the configuration from MongoDB
        GlobalConfig config = GameManager.Instance.configManager.GetCurrentConfig();
        if (config != null)
        {
            waitingDuration = config.WaitingDuration;
        }
        else
        {
            waitingText.text = "Error fetching configuration. Please try again.";
            yield break;
        }

        // Start the countdown
        for (int i = waitingDuration; i > 0; i--)
        {
            waitingText.text = $"Please wait for {i} seconds.";
            yield return new WaitForSeconds(1);
        }

        // Transition to the next scene in the stack
        GameManager.Instance.sceneStackManager.LoadNextScene();
    }
}
