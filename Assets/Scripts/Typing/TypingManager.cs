using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
public class TypingManager : MonoBehaviour
{
    [SerializeField]
    public TypingController typingController;
    [SerializeField]
    public  PhraseLoader phraseLoader;
    [SerializeField]
    private TMP_Text displayText;
    [SerializeField]
    private Button nextPhraseButton;
    [SerializeField] 
    public int totalPhrases = 10;

    private string userId;
    private string Wainting_message = "Please click on the button to display the next phrase";

    private currentEnv currentEnv;
    private void OnEnable()
    {
        phraseLoader.OnPhrasesLoaded += InitializeTypingTest;
        currentEnv = GameManager.Instance.env_type();
        if (currentEnv.ENV == KeyboardConfig.env_data_collection.Prod) { 
        userId = Guid.NewGuid().ToString();
        }
        else
        {
            userId = "Test";

        }
    }
    void Start()
    {
    }
    public void wainting_Next_phrase()
    {
        typingController.myInputField.DeactivateInputField();
        displayText.text = Wainting_message;


    }
    private void InitializeTypingTest()
    {
        // Load the first phrase after phrases are loaded
        LoadNewPhrase();
        typingController.OnWordCountChange += HandleWordCountChange;
        nextPhraseButton.onClick.AddListener(NextPhraseButtonClicked);
    }

    private void LoadNewPhrase()
    {
        Phrase phrase = phraseLoader.GetRandomPhrase();
        if (phrase != null)
        {
            typingController.myInputField.ActivateInputField();

            string typedText = typingController.myInputField.text;
            displayText.text = phrase.Text;
            nextPhraseButton.interactable = false;
            typingController.ClearText(); 
        }
    }

    private void HandleWordCountChange(int wordCount)
    {
        string typedText = typingController.myInputField.text;

        bool endsWithPeriod = typedText.EndsWith(".");


        int expectedWordCount = typingController.CountWords(displayText.text);
        bool finishing_Status= (wordCount == expectedWordCount && endsWithPeriod);

        if (finishing_Status)
        {
            typingController.RecordTypingData(displayText.text);
            wainting_Next_phrase();
            nextPhraseButton.interactable = finishing_Status;
            


        }
    }

    public void NextPhraseButtonClicked()
    {

        LoadNewPhrase();
    }

    void OnDestroy()
    {
        if (typingController != null)
        {
            typingController.OnWordCountChange -= HandleWordCountChange;
        }
        if (phraseLoader != null)
        {
            phraseLoader.OnPhrasesLoaded -= InitializeTypingTest;
        }
    }
}