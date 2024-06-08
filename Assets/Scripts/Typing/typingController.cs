using UnityEngine;
using TMPro;
using System;
using System.Collections.Generic;

public class TypingController : MonoBehaviour
{
    [SerializeField]
    public TMP_InputField myInputField;
    public event Action<int> OnWordCountChange; // Event to notify when the word count changes

    private List<TypingData> typingData = new List<TypingData>();
    private float startTime;
    private int keystrokeCount;
    private int incorrectKeystrokes;
    public string previousInput;
    private MongoDBUtility mongoDBUtility;
    public int currentPhrase;
    void OnEnable()
    {
        mongoDBUtility = FindObjectOfType<MongoDBUtility>(); 

        myInputField.onValueChanged.AddListener(HandleInput);
    }

    void OnDisable()
    {
        myInputField.onValueChanged.RemoveListener(HandleInput);
    }

    private void HandleInput(string input)
    {
        // Count words every time the input changes
        int wordCount = CountWords(input);
        OnWordCountChange?.Invoke(wordCount);

        // Log the start time on the first character typed
        if (input.Length == 1)
        {
            startTime = Time.time;
            keystrokeCount = 0;
            incorrectKeystrokes = 0;
        }

        // Track keystrokes
        keystrokeCount++;

        // Count incorrect keystrokes
        if (input.Length > 0 && previousInput.Length > 0 && input.Length <= previousInput.Length)
        {
            if (input[input.Length - 1] != previousInput[previousInput.Length - 1])
            {
                incorrectKeystrokes++;
            }
        }

        previousInput = input;
    }


        public void ClearText()
    {
        myInputField.text = "";
        if (typingData.Count > 0)
        {
            TypingData lastEntry = typingData[typingData.Count - 1];
            lastEntry.timeTaken = Time.time - startTime;
            typingData[typingData.Count - 1] = lastEntry;
        }
    }
    public int CountWords(string input)
    {
        return !string.IsNullOrEmpty(input) ? input.Split(new char[] { ' ', '\n', '\t' }, StringSplitOptions.RemoveEmptyEntries).Length : 0;
    }

    public TypingData RecordTypingData(string expected)
    {
        if (!string.IsNullOrEmpty(myInputField.text))
        {
            float timeTaken = Time.time - startTime;
            string expectedText = expected; // Assuming the expected text is set as the placeholder text
            string typedText = myInputField.text;

            TypingData data = new TypingData
            {
                expected = expectedText,
                typed = typedText,
                timeTaken = timeTaken,
                keystrokeCount = keystrokeCount,
                errorRate = TypingMetrics.CalculateErrorRate(expectedText, typedText),
                accuracyInCharacters = TypingMetrics.CalculateAccuracyInCharacters(expectedText, typedText),
                accuracyInWords = TypingMetrics.CalculateAccuracyInWords(expectedText, typedText),
                accuracyInKeystrokes = TypingMetrics.CalculateAccuracyInKeystrokes(incorrectKeystrokes, keystrokeCount),
                typingSpeed = TypingMetrics.CalculateTypingSpeed(typedText, timeTaken),
                keystrokesPerCharacter = TypingMetrics.CalculateKeystrokesPerCharacter(keystrokeCount, typedText.Length),
                sessionTime = DateTime.Now,



            };
            myInputField.text = "";
            previousInput = "";

            typingData.Add(data);

            return data;
        }
        return null;


      
    }

}