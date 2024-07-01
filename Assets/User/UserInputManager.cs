using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using MongoDB.Driver;
using MongoDB.Bson;
using System;
public class UserInputManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField userNameInput;
    [SerializeField] private TMP_InputField userEmailInput;
    [SerializeField] private TMP_Dropdown userAgeDropdown;
    [SerializeField] private TMP_Dropdown userSexDropdown;
    [SerializeField] private Button saveButton; // Reference to the save button

    public static event Action StartTest;
    void Start()
    {
        InitializeAgeDropdown();
        InitializeGenderDropdown();
        saveButton.onClick.AddListener(SaveUserData); // Add listener for save button click

    }
    private void OnEnable()
    {

    }
    private void FakeSubmit()
    {


        this.saveButton.onClick.Invoke();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            // Call the method that you want to execute
            FakeSubmit();
        }
    }
    private void InitializeAgeDropdown()
    {
        userAgeDropdown.ClearOptions();
        List<string> ageOptions = new List<string>();
        for (int i = 18; i <= 100; i++)
        {
            ageOptions.Add(i.ToString());
        }

        userAgeDropdown.AddOptions(ageOptions);
    }

    private void InitializeGenderDropdown()
    {
        userSexDropdown.ClearOptions();

        List<string> genderOptions = new List<string> { "Male", "Female", "Other" };
        userSexDropdown.AddOptions(genderOptions);
    }

    public void SaveUserData()
    {
        this.saveButton.interactable = false;
        string userId = Guid.NewGuid().ToString();
        UserData userData = new UserData
        {    userId= userId,
            userName = userNameInput.text,
            userAge = int.Parse(userAgeDropdown.options[userAgeDropdown.value].text),
            userSex = userSexDropdown.options[userSexDropdown.value].text
        };
        MongoDBUtility.Instance.InsertUserData(userData);
        GameManager.Instance.UserData = userData;
        
        StartTest.Invoke();
        Debug.Log("User data saved.");

    }


    //private void ShowUserInfoContainer()
    //  {
    //      userInfoContainer.SetActive(true);
    //  userTestContainer.SetActive(false);
    //  }

    //  private void ShowUserTestContainer()
    //  {
    //      userInfoContainer.SetActive(false);
    // userTestContainer.SetActive(true);
    //  }
}
