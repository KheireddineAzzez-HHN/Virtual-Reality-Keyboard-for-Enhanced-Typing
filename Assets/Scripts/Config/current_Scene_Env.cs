using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class current_Scene_Env : MonoBehaviour
{

    public KeyboardConfig.env_data_collection Scene_Type;
    
    public int phrases_to_type = 0;
 
    public string keyboard_type = "";
    public static event Action newEnv;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void update_current_Scene_Env()
    {

        if (Scene_Type == KeyboardConfig.env_data_collection.Prod)
        {
            this.phrases_to_type = this.phrases_to_type = GameManager.Instance.configManager.GetCurrentConfig().Session.PhraseToTypeFakeTest;




        }
        else if (Scene_Type== KeyboardConfig.env_data_collection.Test){

            this.phrases_to_type = GameManager.Instance.configManager.GetCurrentConfig().Session.PhraseToTypeFakeTest;

        }


        if (Scene_Type == KeyboardConfig.env_data_collection.Prod)
        {
            this.phrases_to_type = this.phrases_to_type = GameManager.Instance.configManager.GetCurrentConfig().Session.PhraseToTypeFakeTest;




        }
        else if (Scene_Type == KeyboardConfig.env_data_collection.USERCONFIG)
        {

            Debug.Log("WelcomeToUSerConfig Scene");

        }
    }



}
