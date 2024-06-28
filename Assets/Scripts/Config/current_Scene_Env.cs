using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class current_Scene_Env : MonoBehaviour
{

    public KeyboardConfig.env_data_collection Scene_Type;
    
    public int phrases_to_type;

    [SerializeField]
    public List<GameObject> ListToActive_When_Controller = new List<GameObject>();

    public string keyboard_type = "";
    public static event Action newEnv;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ShowController()
    {


        if (Need_Controller())
        {
            foreach(GameObject gameobject in ListToActive_When_Controller)
            {
                if(gameobject != null)
                {
                    gameobject.SetActive(true);

                }
            }

        }
        else
        {

            foreach (GameObject gameobject in ListToActive_When_Controller)
            {

                gameobject.SetActive(false);
            }


        }
    }
    private bool Need_Controller()
    {

        string current_keyboard = GameManager.Instance.CurrentKeyboardType;
        if (current_keyboard.Contains("Controller"))
        {

            return true;
        }
        return  false;
    }
    public void update_current_Scene_Env()
    {

        if (Scene_Type == KeyboardConfig.env_data_collection.Prod)
        {
           ShowController();

            this.phrases_to_type = this.phrases_to_type = GameManager.Instance.configManager.GetCurrentConfig().Session.PhraseToTypeRealTest;




        }
        else if (Scene_Type== KeyboardConfig.env_data_collection.Test){
           ShowController();

            this.phrases_to_type = GameManager.Instance.configManager.GetCurrentConfig().Session.PhraseToTypeFakeTest;

        }

        else if (Scene_Type == KeyboardConfig.env_data_collection.USERCONFIG)
        {

            Debug.Log("WelcomeToUSerConfig Scene");

        }
    }



}
