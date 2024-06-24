using System.Collections.Generic;
using UnityEngine;

public class KeyboardAppearance : MonoBehaviour
{
    public static KeyboardAppearance Instance { get; private set; }

    private Dictionary<string, GameObject> keyboardObjects;
    current_Scene_Env env;
    void Awake()
    {
    }
    private void Start()
    {
        env = FindObjectOfType<current_Scene_Env>();

        if (env != null)
        {
            if(env.Scene_Type == KeyboardConfig.env_data_collection.USERCONFIG)
            {
                Debug.Log("No need to hide keyboards");
            }

            else
            {
                FindKeyboardObjects();
                UpdateKeyboards(GameManager.Instance.CurrentKeyboardType);
            }
    

        }
    }
    private void FindKeyboardObjects()
    {
        keyboardObjects = new Dictionary<string, GameObject>();

        foreach (Transform child in transform)
        {
            keyboardObjects[child.gameObject.name] = child.gameObject;
        }
    }

    public void UpdateKeyboards(string currentKeyboardType)
    {
        foreach (var kvp in keyboardObjects)
        {
            if (kvp.Key == currentKeyboardType)
            {
                kvp.Value.SetActive(true);
            }
            else
            {
                kvp.Value.SetActive(false);
            }
        }
    }
}
