using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class keyPart : MonoBehaviour
{
    public KeyboardConfig.KeyPartNames keyPartName;
    public KeyboardConfig.KeyNames keyname;

    public event Action<keyPart> OnKeyPartCollisionEnter;
    public event Action<keyPart> OnKeyPartCollisionExit;
    private key parentKey;
    public float keypartWeight = 0.00f;
    public Vector2Int cordinte;
    void Start()
    {
        addComponents();

        cordinte = key_utils.GetCoordinatesFromName(keyPartName.ToString());
        keypartWeight = key_utils.CalculateWeight(cordinte.x, cordinte.y);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void addComponents()
    {

        BoxCollider boxCollider= gameObject.AddComponent<BoxCollider>();
        parentKey = GetComponentInParent<key>();
        boxCollider.isTrigger = true;
    }
    void OnTriggerEnter(Collider other)
    {
        parentKey.OnKeyPartCollisionEnter(this);

    }

    void OnTriggerExit(Collider other)
    {
        
         parentKey.OnKeyPartCollisionExit(this);
      
    }


}
