
using System.Collections.Generic;
using UnityEngine;
using System;

public class ThumbCap : MonoBehaviour
{
    public Vector3 RaycastBox;
    public LayerMask keyPartLayerMask;

    private List<keyPart> lastKeyParts = new List<keyPart>();
    public static event Action<keyPart, KeyboardConfig.RayCast> onRayCastKeypart;
    private BoxCollider boxCollider;

     
    private bool lastPartsCleared = false;
    private void Start()
    {
        boxCollider = gameObject.GetComponent<BoxCollider>();
        RaycastBox = new Vector3(boxCollider.size.x * MathF.Pow(10,-2), boxCollider.size.y*MathF.Pow(10, -2), boxCollider.size.z* MathF.Pow(10, -2));
    }

    void Update()
    {
        if (Keyboard.Keydetected == false)
        {
            DetectKeyPartsCovered();

            lastPartsCleared = false;
        }
        else if (Keyboard.Keydetected && !lastPartsCleared)
        {

            foreach (keyPart keypart in lastKeyParts)
            {

                onRayCastKeypart?.Invoke(keypart, KeyboardConfig.RayCast.RAYCASTEXIT);

            }
            lastKeyParts.Clear();

            lastPartsCleared = true;
        }

    }

    private void DetectKeyPartsCovered()
    {
        Vector3 boxCenter = transform.TransformPoint(boxCollider.center );

        // Get all colliders within the box
        Collider[] hitColliders = Physics.OverlapBox(boxCenter, RaycastBox * 0.5f, transform.rotation, keyPartLayerMask);

        List<keyPart> hitKeyParts = new List<keyPart>();

        foreach (var hitCollider in hitColliders)
        {
            // Get the keyPart component from the hit object
            keyPart hitKeyPart = hitCollider.GetComponent<keyPart>();

            if (hitKeyPart != null)
            {
                hitKeyParts.Add(hitKeyPart);

                // Trigger the OnKeyPartRayCastEnter event if this is a new key part
                if (!lastKeyParts.Contains(hitKeyPart))
                {
                    onRayCastKeypart?.Invoke(hitKeyPart, KeyboardConfig.RayCast.RAYCASTENTER);
                }
            }
        }

        // Reset colors for key parts that are no longer being hit
        foreach (var keyPart in lastKeyParts)
        {
            if (!hitKeyParts.Contains(keyPart))
            {
                onRayCastKeypart?.Invoke(keyPart, KeyboardConfig.RayCast.RAYCASTEXIT);
            }
        }

        lastKeyParts = hitKeyParts;
    }


}