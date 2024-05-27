using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
public class key : MonoBehaviour
{
    public KeyboardConfig.KeyNames keyName;

    public Dictionary<KeyboardConfig.KeyPartNames, keyPart> key_parts = new Dictionary<KeyboardConfig.KeyPartNames, keyPart>();

    public  Dictionary<keyPart, bool> collidedKeyParts = new Dictionary<keyPart, bool>();


    public Dictionary<keyPart, bool> rayCastedkeyparts = new Dictionary<keyPart, bool>();


    public float keyWeigh = 0f;

    private Animator animator;

    public KeyAnimationControl animationControl;
    public keyAudioControl keyaudio=null;


    public string keySoundPath=null;
    public keyPart keyFrame;
    public string extractedKeyName;
    public event Action<key> OnKeyCollisionEnter;
    public event Action<key> OnKeyCollisionExit;

    public event Action<key> OnKeyRayCastEnter;
    public event Action<key> OnKeyRayCastExit;

    void Start()
    {

        PopulateKeysParts();
        extractedKeyName = key_utils.Extrat_keyName(this.keyName);
        keySoundPath = key_utils.keySoundPath(extractedKeyName);
        this.addComponents();

    }

    void Update()
    {
        
    }

    private void PopulateKeysParts()
    {
        foreach (Transform child in transform)
        {
            KeyboardConfig.KeyPartNames keyPartName;
            if (System.Enum.TryParse<KeyboardConfig.KeyPartNames>(child.name.Split('.')[0], true, out keyPartName))
            {

                keyPart newKeyPart = child.gameObject.AddComponent<keyPart>();
                newKeyPart.keyPartName = keyPartName;
                newKeyPart.keyname = this.keyName;
                key_parts[keyPartName] = newKeyPart;

                newKeyPart.OnKeyPartCollisionEnter += OnKeyPartCollisionEnter;
                newKeyPart.OnKeyPartCollisionExit += OnKeyPartCollisionExit;


                newKeyPart.OnKeyPartRayCastEnter += OnkeyPartRayCastEnter;
                newKeyPart.OnKeyPartRayCastExist += OnkeyPartRayCastExist;

                if (keyPartName.Equals(KeyboardConfig.KeyPartNames.frame)){
                    keyFrame = newKeyPart;
                };

            }
            else
            {
                Debug.LogError("Key part name mismatch or not found in enum: " + child.name);
            }
        }
    }


    public void OnkeyPartRayCastEnter(keyPart keypart)


    {

        if (!rayCastedkeyparts.ContainsKey(keypart))
        {

            this.rayCastedkeyparts.Add(keypart, true);
            this.OnKeyRayCastExit.Invoke(this);


        }


    }
    public void OnkeyPartRayCastExist(keyPart keypart)
    {


        if (rayCastedkeyparts.ContainsKey(keypart))
        {

            this.rayCastedkeyparts.Remove(keypart);

            this.OnKeyRayCastExit.Invoke(this);

        }
    }

    public void OnKeyPartCollisionEnter(keyPart part)
    {
        if (!collidedKeyParts.ContainsKey(part))
        {
            collidedKeyParts[part] = true;
            if (collidedKeyParts.Count >= 0)
            {
                OnKeyCollisionEnter?.Invoke(this);
            }
        }
    }

    public void onKeyPartRayCastEnter(keyPart part)
    {

        this.rayCastedkeyparts.Add(part,true);


    }

    public void OnKeyPartCollisionExit(keyPart part)
    {
        if (collidedKeyParts.ContainsKey(part))
        {

            collidedKeyParts.Remove(part);
            if (collidedKeyParts.Count == 0)
            {
                collidedKeyParts.Clear();
                OnKeyCollisionExit?.Invoke(this);
            }
        }
    }


    public float calculateWeight() {

        this.keyWeigh = keyboard_Utils.CalculateTotalWeight(collidedKeyParts,key_parts);

        return this.keyWeigh;

    }

    public bool CleanCollided()
    {
        if (collidedKeyParts.Count > 0)
        {

            collidedKeyParts.Clear();
            return true;
        }
        return false;
    }

    public void addComponents()
    {


         animator = gameObject.GetComponent<Animator>();
        keyaudio = gameObject.AddComponent<keyAudioControl>();


        if (animator == null)
        {
            animator = gameObject.AddComponent<Animator>();
        }
        if (key_utils.IsAlphabetic(this.extractedKeyName)){

            RuntimeAnimatorController animatorController = Resources.Load<RuntimeAnimatorController>("animation/KeyAnimation");

            if (animatorController != null)
            {
                animator.runtimeAnimatorController = animatorController;
                
                animationControl = gameObject.AddComponent<KeyAnimationControl>();
            }
            else
            {
                Debug.LogError("Failed to load Animator Controller. Check the path and make sure it's in a Resources folder.");
            }

        }



    }


}

