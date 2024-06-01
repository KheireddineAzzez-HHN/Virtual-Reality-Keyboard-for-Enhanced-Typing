using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_keypart 
{
    public KeyboardConfig.KeyPartNames KeyPartName { get; set; }
    public KeyboardConfig.KeyNames KeyName { get; set; }
    public float KeyPartWeight { get; set; }
    public Vector2Int Coordinate { get; set; }
    public Vector3 BoxSize { get; set; }
    public float ColorThumbHover { get; set; }
    public string LayerName { get; set; }
}
