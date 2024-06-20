using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class NextPhrase : MonoBehaviour
{

    public static event Action<bool> IndexFinger;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "IndexNextPhrase")
        {
            IndexFinger.Invoke(true);
            
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "IndexNextPhrase")
        {
            IndexFinger.Invoke(false);

        }
    }
}
