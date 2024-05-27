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

    public event Action<keyPart> OnKeyPartRayCastEnter;
    public event Action<keyPart> OnKeyPartRayCastExist;

    private key parentKey;
    public float keypartWeight = 0.00f;
    public Vector2Int cordinte;
    public Vector3 boxsize;
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
        if (Enum.TryParse<KeyboardConfig.KeyboardInteractiveTag>(other.tag, true, 
            out KeyboardConfig.KeyboardInteractiveTag tagValue) && Enum.IsDefined(typeof(KeyboardConfig.KeyboardInteractiveTag), tagValue)) {

            parentKey.OnKeyPartCollisionEnter(this);

        }

    }

    void OnTriggerExit(Collider other)
    {
        if (Enum.TryParse<KeyboardConfig.KeyboardInteractiveTag>(other.tag, true,
            out KeyboardConfig.KeyboardInteractiveTag tagValue) && Enum.IsDefined(typeof(KeyboardConfig.KeyboardInteractiveTag), tagValue))
        {

            parentKey.OnKeyPartCollisionExit(this);


        }

    }

    public void changeColort()
    {


    }
}
