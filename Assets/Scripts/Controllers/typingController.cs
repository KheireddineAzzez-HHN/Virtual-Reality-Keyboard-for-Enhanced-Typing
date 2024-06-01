using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class typingController : MonoBehaviour
{

    public List<TypedWord> Paragraph = new List<TypedWord>();
    public  
    void Start()
    {
        

    }

    void Update()
    {
        
    }

    private void newWordsToRegister(string typedWord)
    {

        print(typedWord);

    }
    private void OnEnable()
    {
        Keyboard.OnNewWordTyped += this.newWordsToRegister;
    }

}
