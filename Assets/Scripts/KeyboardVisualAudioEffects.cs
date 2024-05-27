using UnityEngine;
using System;
using System.Collections.Generic;
public class KeyboardVisualAudioEffects : MonoBehaviour
{
    // Static instance of the class
    public static KeyboardVisualAudioEffects Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
         
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
    }
    void Update()
    {

    }
    void OnEnable()
    {
        Keyboard.OnKeyTypevisualEffect += HandleKeyTyped;
        Keyboard.OnKeypartsRayCast += HandlekeypartsRayCast;

    }

    void OnDisable()
    {
        Keyboard.OnKeyTypevisualEffect -= HandleKeyTyped;
    }

    private void HandleKeyTyped(key key, KeyboardConfig.keyStatus keystatus)
    {
        if (keystatus.Equals(KeyboardConfig.keyStatus.PRESSED))
        {
            HandleKeyPresss(key);

          
        }

        else
        {

            HandleKeyRelease(key);

        }
    }

    private void HandleKeyPresss(key key) {

        key.animationControl.PressKey();
        key.keyaudio.PlaySoundByPath(key.keySoundPath);
    
    }
    private void HandleKeyRelease(key key)
    {

        key.animationControl.ReleaseKey();

    }


    public void HandlekeypartsRayCast ( HashSet<keyPart> keyParts ,KeyboardConfig.RayCast RayCastHandle)
    {

     if (RayCastHandle.Equals(KeyboardConfig.RayCast.RAYCASTENTER))
        {
            print("Ray Cast Enter");
        }

    }
}
