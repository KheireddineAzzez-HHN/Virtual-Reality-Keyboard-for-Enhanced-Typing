using UnityEngine;
using System;
using System.Collections.Generic;
public class KeyboardVisualAudioEffects : MonoBehaviour
{
    // Static instance of the class

    public static KeyboardVisualAudioEffects Instance { get; private set; }
    public C_Gloves_configuration glovesConfiguration;
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
        ThumbCap.onRayCastKeypart += HandlekeypartsRayCast;

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
            if (key.keyFrame.Collided_with == KeyboardConfig.KeyboardInteractiveTag.Thumb_cap_left)
            {
         glovesConfiguration.sendVib("LFFT");

            }
            if (key.keyFrame.Collided_with == KeyboardConfig.KeyboardInteractiveTag.Thumb_cap_right)
            {
            glovesConfiguration.sendVib("RIGHT");

            }
        }

        else
        {

            HandleKeyRelease(key);

        }
    }

    private void HandleKeyPresss(key key) {

        key.animationControl.PressKey(key.keyName);
        key.keyaudio.PlaySoundByPath(key.keySoundPath);
    
    }
    private void HandleKeyRelease(key key)
    {

        key.animationControl.ReleaseKey(key.keyName);

    }


    public void HandlekeypartsRayCast ( keyPart keypart ,KeyboardConfig.RayCast RayCastHandle)
    {

     if (RayCastHandle.Equals(KeyboardConfig.RayCast.RAYCASTENTER))
        {

    
            Color  assigned_color=AssignKeypartColort(KeyboardConfig.startColor, KeyboardConfig.endColor, keypart.ColorThumbHover);
            keypart.ChangeColorBasedOnWeight(assigned_color);
        }

     else if (RayCastHandle.Equals(KeyboardConfig.RayCast.RAYCASTEXIT))
        {

            keypart.ResetColor();

        }

    }

    private Color AssignKeypartColort(Color startColor, Color endColor, float keypartWeight)
    {
       
        Color weightColor = Color.Lerp(startColor, endColor, keypartWeight);

        return weightColor;
    }

    public  static float BaseWEeightPartsWeight(Color startColor, Color endColor, float keypartWeight)
    {
        return 0f;
    }

}
