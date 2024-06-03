using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyController : MonoBehaviour



{
    public float rotationSpeed = 1.0f; // Speed of rotation

    void Update()
    {
        // Rotates the skybox material
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * rotationSpeed);
    }


}