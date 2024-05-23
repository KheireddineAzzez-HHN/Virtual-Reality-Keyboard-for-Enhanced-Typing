using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class keyboard_Utils : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public static void PrintActiveKey(Dictionary<key, int> activeKeyCollisions)
    {
        if (activeKeyCollisions.Count > 0)
        {
            key activeKey = null;
            int maxCollisions = 0;
            foreach (var kvp in activeKeyCollisions)
            {
                if (kvp.Value > maxCollisions)
                {
                    activeKey = kvp.Key;
                    maxCollisions = kvp.Value;
                }
            }
            if (activeKey != null)
            {
                Debug.Log($"Active key: {activeKey.keyName}");
            }
        }
        else
        {
            Debug.Log("No active key");
        }
    }


    public static float CalculateTotalWeight(Dictionary<keyPart, bool> collidedKeyParts, Dictionary<KeyboardConfig.KeyPartNames, keyPart> key_parts)
    {
        float totalWeight = 0.0f;
        foreach (var keyPartEntry in collidedKeyParts)
        {
            keyPart part = keyPartEntry.Key;
            totalWeight += part.keypartWeight;
        }
        int maxPossibleCollidedParts = key_parts.Count; 
        float maxWeight = maxPossibleCollidedParts;

        return totalWeight / maxWeight;
  
    }


}
