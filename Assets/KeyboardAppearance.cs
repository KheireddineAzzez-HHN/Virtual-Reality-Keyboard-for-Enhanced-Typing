using System.Collections.Generic;
using UnityEngine;

public class KeyboardAppearance : MonoBehaviour
{
    public static KeyboardAppearance Instance { get; private set; }

    private Dictionary<string, GameObject> keyboardObjects;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            FindKeyboardObjects();
        }
        else
        {
            Destroy(gameObject);
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
