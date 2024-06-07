using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class TypingData 
{


    public string expected;
    public string typed;
    public float timeTaken;
    public int keystrokeCount;
    public float errorRate;
    public float accuracyInCharacters;
    public float accuracyInWords;
    public float accuracyInKeystrokes;
    public float typingSpeed;
    public int keystrokesPerCharacter;
    public DateTime sessionTime;

    public string userId;

}
