using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(LineRenderer))]

public class fakeRayCast : MonoBehaviour
{
    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();

        // Set the width of the LineRenderer
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;

        // Set the number of positions
        lineRenderer.positionCount = 2;
    }

    void Update()
    {
        // Define the ray starting point and direction
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        // Perform the ray cast
        if (Physics.Raycast(ray, out hit))
        {
            // Set the positions of the LineRenderer to draw the ray
            lineRenderer.SetPosition(0, ray.origin);
            lineRenderer.SetPosition(1, hit.point);

            // Optionally, you can log the hit object
            Debug.Log("Hit: " + hit.collider.name);
        }
        else
        {
            // If the ray doesn't hit anything, draw the ray to a far point
            lineRenderer.SetPosition(0, ray.origin);
            lineRenderer.SetPosition(1, ray.origin + ray.direction * 100);
        }
    }
}