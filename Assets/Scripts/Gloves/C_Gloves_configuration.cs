using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SGCore;
using SGCore.Haptics;

public class C_Gloves_configuration:MonoBehaviour


{

    public bool testFbb = false;
    public bool testVib = false;
    public Dictionary<SGCore.Finger, float> SlidersValuesVibration = new Dictionary<Finger, float>();
    public Dictionary<SGCore.Finger, float> SlidersValuesForceFeedback = new Dictionary<Finger, float>();
    public HapticGlove right_hand = null;
    public HapticGlove left_hand = null;


    private void Start()
    {
        Connected_HapticsGloves();
    }
    public HapticGlove[]  Connected_HapticsGloves()
    {
      HapticGlove[] Gloves =  SGCore.Nova.NovaGlove.GetHapticGloves(true);
        foreach (HapticGlove glove in Gloves)
        {

            if (glove.IsRight())
            {
                right_hand = glove;

            }
            else
            {

                left_hand = glove;
            }

        }
        return Gloves;
    }

    public void SetFBB(HapticGlove[] Gloves, Dictionary<Finger,float> Fingers)

    {

     foreach(HapticGlove glove in Gloves){ 
            
            foreach (KeyValuePair<Finger, float> item in  Fingers) {
                glove.QueueFFBLevel(item.Key, item.Value);

            }
        }
    }


    public void SetVib(HapticGlove[] Gloves, Dictionary<Finger, float> Fingers)
    {
        foreach (HapticGlove glove in Gloves)
        {
            foreach (KeyValuePair<Finger, float> item in Fingers)
            {
                Debug.Log("Iam in vib " + item.Key.ToString()+" val "+item.Value);
                glove.QueueVibroLevel(item.Key, item.Value);


            }
            glove.SendHaptics();
        }

    }
    public void Stop_Vib()
    {

        this.testVib = false;
        HapticGlove[] gloves = Connected_HapticsGloves();
        foreach (HapticGlove glove in gloves)
        {
            glove.StopVibrations();
        }


    } 

   public void Stop_fbb()
    {
        this.testFbb = false;

        HapticGlove[] gloves = Connected_HapticsGloves();
        foreach (HapticGlove glove in gloves)
        {
            glove.StopHaptics();
        }
    }
    public void Handle_Fbb()
    {
        HapticGlove[] HapGloves = Connected_HapticsGloves();
        SetFBB(HapGloves, SlidersValuesForceFeedback);
    }

    public void Handle_Vib()
    {
        HapticGlove[] HapGloves = Connected_HapticsGloves();
        SetVib(HapGloves, SlidersValuesVibration);

    }

    public void sendVib(string Hand)
    {

        if (Hand == "LEFT")
        {
            left_hand.SendHaptics();
        }

        if (Hand == "RIGHT")
        {


            left_hand.SendHaptics();
        }




    }
    public void stopVib(string Hand)
    {
        if (Hand == "LEFT")
        {
            left_hand.StopVibrations();
        }

        if (Hand == "RIGHT")
        {


            left_hand.StopVibrations();
        }


    }
    void Update()
    {

    }




}
