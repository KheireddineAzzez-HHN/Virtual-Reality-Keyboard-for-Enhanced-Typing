using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
public class Keyboard : MonoBehaviour
{

    public static Keyboard Instance { get; private set; }

    public Dictionary<KeyboardConfig.KeyNames, key> keys = new Dictionary<KeyboardConfig.KeyNames, key>();

    private Dictionary<key, int> activeKeyCollisions = new Dictionary<key, int>();

    private Dictionary<key, int> activeKeyCollisionsRayCast = new Dictionary<key, int>();

    public key keyToType = null;
    public bool animationtriggered = false;
    public static bool Keydetected = false;
    public Collider collidedWith;
    public TMP_InputField inputText;

    public static event Action<key, KeyboardConfig.keyStatus > OnKeyTypevisualEffect;

    public static event Action<key, KeyboardConfig.keyStatus> OnKeyTypeAduioEffect;

    
    public Queue<string> typedKeysQueue = new Queue<string>();



    public void instert_key_to_InputText(string text)
    {
        if(text.Equals(null) || text.Equals(""))
        {
            Debug.LogError("Verify text is cloud not be empty or null");
        }

       inputText.text = inputText.text + text;


    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    void Start()
    {
        PopulateKeys();
    }

    void Update()
    {

        handleEnterCollision_Keys();


    }
    private void handleEnterCollision_Keys() {
        if (activeKeyCollisions.Count > 0 && Keydetected == false)
        {
            keyToType = highestWeigh_Key();
            OnKeyTypevisualEffect.Invoke(keyToType, KeyboardConfig.keyStatus.PRESSED);
            
            keyToType.animationControl.PressKey(keyToType.keyName);
            Keydetected = true;



            // Add typed letter to queue
  

    

        }
    }

    private void handleExistCollision_Keys() {

        OnKeyTypevisualEffect.Invoke(keyToType, KeyboardConfig.keyStatus.REALESED);

        string typedLetter = keyToType.extractedKeyName;

        if (typedLetter == "space")
        {
            typedLetter = " ";
        }
        else if (typedLetter == "point") {

            typedLetter = ".";
        }
        else if (typedLetter == "delete")
        {

            if (inputText.text.Length > 0)
            {
                inputText.text = inputText.text.Substring(0, inputText.text.Length - 1);
                return;
            }




        }

        if (typedLetter.Length == 1)
        {
            instert_key_to_InputText(typedLetter);
            print(typedLetter);


        }
        else if (typedLetter.Length == 1)
        {
            print("keyIsNotsupported");
        }

    }

    private void PopulateKeys()
    {
        foreach (Transform child in transform)
        {
            KeyboardConfig.KeyNames keyName;
            if (System.Enum.TryParse<KeyboardConfig.KeyNames>(child.name, true, out keyName))
            {

                key newKey = child.gameObject.AddComponent<key>();
                newKey.keyName = keyName;

                newKey.OnKeyCollisionEnter += OnKeyCollisionEnter;
                newKey.OnKeyCollisionExit += OnKeyCollisionExit;


                keys[keyName] = newKey;


            }
            else
            {
                Debug.LogError("Key name mismatch or not found in enum: " + child.name);
            }
        }
    }

    private void OnKeyCollisionEnter(key key)
    {
        if (activeKeyCollisions.ContainsKey(key))
        {
            activeKeyCollisions[key]++;
        }
        else
        {
            activeKeyCollisions[key] = 1;
        }

    }

    private void OnKeyCollisionExit(key key)
    {

        if (keyToType != null)
        {
            if (keyToType.collidedKeyParts.Count == 0)
            {


                handleExistCollision_Keys();

                CleanCollided();

            }
        }

    }

    public key highestWeigh_Key() {
        key highestWeightKey = null;
        float highestWeight = 0.0f;


        foreach (var key in activeKeyCollisions.Keys)
        {
            float keyWeight = key.calculateWeight();
            if (keyWeight > highestWeight)
            {

                highestWeight = keyWeight;
                highestWeightKey = key;
            }
        }
        return highestWeightKey;

    }

    public bool CleanCollided()
    {
        if (activeKeyCollisions.Count > 0)
        {
           foreach(key element in activeKeyCollisions.Keys)
            {
                element.keyWeigh = 0;
            }
            activeKeyCollisions.Clear();
            keyToType = null;
            Keydetected = false;
           
            
            return true;
        }
        return false;
    }

    private string GetWordFromQueue()
    {
        string word = string.Concat(typedKeysQueue.ToArray()).Trim();
        typedKeysQueue.Clear();
        return word;
    }

}
