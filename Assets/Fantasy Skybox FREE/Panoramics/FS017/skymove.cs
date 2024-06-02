using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class InvertSphere : MonoBehaviour
{
    public Material skyboxMaterial;
    public float rotationSpeed = 1.0f;

    void Update()
    {
        float rotation = Time.time * rotationSpeed;
        skyboxMaterial.SetFloat("_Rotation", rotation);
    }
}
