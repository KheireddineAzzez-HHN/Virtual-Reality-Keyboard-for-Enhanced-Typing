using System;

using System.Collections.Generic;
using UnityEngine;
public class PhraseLoader : MonoBehaviour
{
    public List<Phrase> Phrases = new List<Phrase>();
    public TextAsset phraseFile;
    public event Action OnPhrasesLoaded;
    void Start()
    {
        LoadPhrases();
    }

    private void LoadPhrases()
    {
        string[] lines = phraseFile.text.Split('\n');
        foreach (var line in lines)
        {
            if (!string.IsNullOrWhiteSpace(line))
            {
                Phrases.Add(new Phrase(line.Trim()));
            }
        }

        // Debugging: Log the first 5 phrases to check
        for (int i = 0; i < 5 && i < Phrases.Count; i++)
        {
            Debug.Log("Loaded Phrase: " + Phrases[i].Text);
        }

        OnPhrasesLoaded?.Invoke();

    }

    public Phrase GetRandomPhrase()
    {
        if (Phrases.Count == 0)
        {
            Debug.LogError("No phrases loaded!");
            return null;
        }
        int randomIndex = UnityEngine.Random.Range(0, Phrases.Count);
        return Phrases[randomIndex];
    }
}
