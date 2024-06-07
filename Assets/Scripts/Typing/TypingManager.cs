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

    private void OnEnable()
    {
        phraseLoader.OnPhrasesLoaded += InitializeTypingTest;

    }
    void Start()
    {
        // Subscribe to the event to know when phrases are loaded
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