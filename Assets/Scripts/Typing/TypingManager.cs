using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
public class TypingManager : MonoBehaviour
{
    [SerializeField] private TypingController typingController;
    [SerializeField] private PhraseLoader phraseLoader;
    [SerializeField] private TMP_Text displayText;
    [SerializeField] private Button nextPhraseButton;
    [SerializeField] private TMP_Text phraseCounterText;

    [SerializeField] private int totalPhrases = 0;

    private int phrasesTyped = 0;

    private string userId;
    private string Wainting_message = "Please click on the button to display the next phrase";

    private current_Scene_Env currentEnv;

    private void OnEnable()
    {

        phraseLoader.OnPhrasesLoaded += InitializeTypingTest;
      
    }
    void Start()
    {

        currentEnv = FindAnyObjectByType<current_Scene_Env>();
    }


    public void wainting_Next_phrase()
    {
        typingController.myInputField.DeactivateInputField();
        displayText.text = Wainting_message;


    }
    private void InitializeTypingTest()
    {
        // Load the first phrase after phrases are loaded
        this.totalPhrases = GameManager.Instance.GetCurrentEnv().phrases_to_type;

        LoadNewPhrase();
        typingController.OnWordCountChange += HandleWordCountChange;
        nextPhraseButton.onClick.AddListener(NextPhraseButtonClicked);
    }

    private void LoadNewPhrase()
    {

        if (phrasesTyped >= totalPhrases)
        {    
            EndTest();
            GameManager.Instance.CompleteCurrentPhase();
            return;
        }
        Phrase phrase = phraseLoader.GetRandomPhrase();
        if (phrase != null)
        {
            typingController.myInputField.ActivateInputField();

            string typedText = typingController.myInputField.text;
            displayText.text = phrase.Text;
            nextPhraseButton.interactable = false;
            typingController.ClearText();
            phrasesTyped++;
            UpdatePhraseCounter();
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

            TypingData data= typingController.RecordTypingData(displayText.text);
            data.userId = GameManager.Instance.UserData.userId;
            data.currentPhrase = phrasesTyped;
            data.KeyboardType = GameManager.Instance.CurrentKeyboardType;

            MongoDBUtility.Instance.InsertTypingData(data);

            wainting_Next_phrase();
            nextPhraseButton.interactable = finishing_Status;
            


        }
    }
    private void UpdatePhraseCounter()
    {
        phraseCounterText.text = $"{totalPhrases - phrasesTyped}";
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

    private void EndTest()
    {
        Debug.Log("Test completed!");
    }

}