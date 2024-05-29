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

    private Renderer renderer;
    private Color originalColor;
    public key parentKey;
    public float keypartWeight=0.0f ;
    public Vector2Int cordinte;
    public Vector3 boxsize;
    public KeyboardConfig.MaskLayers KeypartLayerName = KeyboardConfig.MaskLayers.KEYPARTLAYER;
    public float ColorThumbHover=0.0f;
    private  keyPart()
    {
  

    }

    void Start()
    {
        cordinte = key_utils.GetCoordinatesFromName(keyPartName.ToString());

        if (KeyboardConfig.KeypartsBaseWeight.Contains(keyPartName))
        {
            keypartWeight = KeyboardConfig.basicWeightKeypart;
            ColorThumbHover = keypartWeight * KeyboardConfig.keyPartBaseWeightColorMultiplicator;
        }
        else
        {
            keypartWeight = key_utils.CalculateWeight(cordinte.x, cordinte.y);
            ColorThumbHover = keypartWeight * KeyboardConfig.keyPatNormalWeightColorMultplicator;

        }



        addComponents();
       // Material liquidFillMaterial = Resources.Load<Material>("Materials/DefaultSahder");

    
        renderer = GetComponent<Renderer>();

        if (renderer != null)
        {
          originalColor = renderer.material.color;

        }

        gameObject.layer = LayerMask.NameToLayer(KeypartLayerName.ToString());
    }

    void Update()
    {
    }

    void addComponents()
    {
        BoxCollider boxCollider = gameObject.AddComponent<BoxCollider>();
        parentKey = GetComponentInParent<key>();
        boxCollider.isTrigger = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (Enum.TryParse<KeyboardConfig.KeyboardInteractiveTag>(other.tag, true,
            out KeyboardConfig.KeyboardInteractiveTag tagValue) && Enum.IsDefined(typeof(KeyboardConfig.KeyboardInteractiveTag), tagValue))
        {
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

    public void ChangeColorBasedOnWeight(Color color)
    {
        if (renderer != null)
        {
          renderer.material.color = color;


        }
    }

    public void ResetColor()
    {
        if (renderer != null)
        {
         renderer.material.color = originalColor;


        }
    }
}
