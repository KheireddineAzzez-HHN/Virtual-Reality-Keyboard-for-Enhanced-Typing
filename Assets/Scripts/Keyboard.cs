using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Keyboard : MonoBehaviour
{

    public static Keyboard Instance { get; private set; }

    public Dictionary<KeyboardConfig.KeyNames, key> keys = new Dictionary<KeyboardConfig.KeyNames, key>();

    private Dictionary<key, int> activeKeyCollisions = new Dictionary<key, int>();

    private Dictionary<key, int> activeKeyCollisionsRayCast = new Dictionary<key, int>();



    public key keyToType = null;
    public bool animationtriggered = false;
    public bool Keydetected = false;

    private KeyboardVisualAudioEffects keyboardEffects = new KeyboardVisualAudioEffects();

    public static event Action<key, KeyboardConfig.keyStatus> OnKeyTypevisualEffect;

    public static event Action<key, KeyboardConfig.keyStatus> OnKeyTypeAduioEffect;

    public static event Action<HashSet<keyPart>, KeyboardConfig.RayCast> OnKeypartsRayCast;





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

            keyToType.animationControl.PressKey();
            Keydetected = true;

        }
    }

    private void handleExistCollision_Keys() {

        OnKeyTypevisualEffect.Invoke(keyToType, KeyboardConfig.keyStatus.REALESED);
        print(keyToType.extractedKeyName);

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

                newKey.OnKeyRayCastEnter += keyPartsRaycastEnter;
                newKey.OnKeyRayCastExit += keyPartsRaycastExist;

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

            activeKeyCollisions.Clear();
            keyToType = null;
            Keydetected = false;
            return true;
        }
        return false;
    }


    public void keyPartsRaycastEnter(key key)
    {


        HashSet<keyPart> keyparts = new HashSet<keyPart>(key.rayCastedkeyparts.Keys);


        OnKeypartsRayCast.Invoke(keyparts, KeyboardConfig.RayCast.RAYCASTENTER);

    }
    public void keyPartsRaycastExist(key key)
    {


    }
}
