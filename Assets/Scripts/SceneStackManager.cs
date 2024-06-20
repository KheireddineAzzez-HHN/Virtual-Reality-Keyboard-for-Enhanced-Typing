using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneStackManager : MonoBehaviour
{
    private Stack<string> sceneStack = new Stack<string>();

    public void InitializeSceneStack(List<string> keyboardTypes)
    {
        sceneStack.Clear();
        foreach (var keyboardType in keyboardTypes)
        {
            sceneStack.Push("WaitingScene");  
            sceneStack.Push("TestScene"); 
            sceneStack.Push("TrainScene"); 
        }
    }

    public  void LoadNextScene()
    {
        if (sceneStack.Count > 0)
        {
            string nextScene = sceneStack.Pop();
             SceneManager.LoadSceneAsync(nextScene);
        }
        else
        {
            Debug.Log("All keyboard types have been tested.");
        }
    }

    public bool HasMoreScenes()
    {
        return sceneStack.Count > 0;
    }
}
