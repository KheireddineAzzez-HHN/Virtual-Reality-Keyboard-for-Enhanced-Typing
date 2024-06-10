using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class UserData
{

    public string userId;
    public string userName;
    public int userAge;
    public string userSex;
}

[System.Serializable]
public class GlovesConfig
{
    public float BuzzThumb { get; set; }
    public float ForceFeedbackThumb { get; set; }
    public float ForceFeedbackIndex { get; set; }
    public float ForceFeedbackMiddle { get; set; }
    public float ForceFeedbackRing { get; set; }
    public float ForceFeedbackPinky { get; set; }
}

[System.Serializable]
public class ControllerKeyboardConfig
{
    public float VibrationLevel { get; set; }
}

[System.Serializable]
public class SessionConfig
{
    public int PhraseToTypeRealTest { get; set; }
    public int PhraseToTypeFakeTest { get; set; }
}

[System.Serializable]
public class GlobalConfig
{
    public GlovesConfig Gloves { get; set; }
    public ControllerKeyboardConfig ControllerKeyboard { get; set; }
    public SessionConfig Session { get; set; }
    public List<string> KeyboardTypes { get; set; }
    public int WaitingDuration { get; set; }  

}