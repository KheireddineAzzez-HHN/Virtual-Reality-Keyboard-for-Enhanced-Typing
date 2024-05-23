using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class key_utils : MonoBehaviour
{



    static int GridWidth = KeyboardConfig.columns;
    static  int GridHeight = KeyboardConfig.rows;

    void Start()
    {
    }

    void Update()
    {
        
    }


    

    public static  Vector2Int GetCoordinatesFromName(string name)
    {
        try
        {
            int index;
            int  x = -1, y = -1;

            if (name.StartsWith("top_r"))
            {
                index = int.Parse(name.Replace("top_r", "")) ;
                x = ((index - 1) % 3); ; 
                y =Mathf.FloorToInt((index - 1) % 15 / 3); 
            }


            else if (name.StartsWith("top_l"))
            {

                index = int.Parse(name.Replace("top_l", ""));

                x = 2 - ((index-1) % 3);
                y = Mathf.FloorToInt((index - 1) % 15 / 3);

            }
            else if (name.StartsWith("bottom_r"))
            {
                index = int.Parse(name.Replace("bottom_r", ""));

                x = 2 - ((index - 1) % 3);
                y = 4 - Mathf.FloorToInt((index - 1) % 15 / 3);
            }
            else if (name.StartsWith("bottom_l"))
            {
                index = int.Parse(name.Replace("bottom_l", ""));

                x = 2 - ((index - 1) % 3);
                y = 4 - Mathf.FloorToInt((index - 1) % 15 / 3);
            }

            return new Vector2Int(x, y);
        }
        catch (Exception e)
        {
            Debug.LogError($"Error parsing coordinates from name '{name}': {e.Message}");
            return new Vector2Int(-1, -1); 
        }
    }


    public static float CalculateWeight(int x, int y)
    {
        float maxDistance = Mathf.Sqrt((GridWidth - 1) * (GridWidth - 1) + (GridHeight - 1) * (GridHeight - 1));

        float distance = Mathf.Sqrt(x * x + y * y);

        return 1 - (distance / maxDistance);
    }

    public static string Extrat_keyName(KeyboardConfig.KeyNames keyname)
    {
        return keyname.ToString().Split("_")[1];

    }
}


