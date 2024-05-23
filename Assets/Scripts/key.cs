using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class key : MonoBehaviour
{
    public KeyboardConfig.KeyNames keyName;

    public Dictionary<KeyboardConfig.KeyPartNames, keyPart> key_parts = new Dictionary<KeyboardConfig.KeyPartNames, keyPart>();

    public  Dictionary<keyPart, bool> collidedKeyParts = new Dictionary<keyPart, bool>();

    public float keyWeigh = 0f;

    public keyPart keyFrame;
    public string extractedKeyName;
    public event Action<key> OnKeyCollisionEnter;
    public event Action<key> OnKeyCollisionExit;

    void Start()
    {
        PopulateKeysParts();
        extractedKeyName = key_utils.Extrat_keyName(this.keyName);
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


}
