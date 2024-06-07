using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TypingMetrics : MonoBehaviour
{
    public static float CalculateErrorRate(string expected, string typed)
    {
        int levenshteinDistance = LevenshteinDistance(expected, typed);
        return 100f * levenshteinDistance / Math.Max(expected.Length, typed.Length);
    }

    public static float CalculateAccuracyInCharacters(string expected, string typed)
    {
        int errors = LevenshteinDistance(expected, typed);
        return 100f - (100f * errors / expected.Length);
    }

    public static float CalculateAccuracyInWords(string expected, string typed)
    {
        string[] expectedWords = expected.Split(new char[] { ' ', '\n', '\t' }, StringSplitOptions.RemoveEmptyEntries);
        string[] typedWords = typed.Split(new char[] { ' ', '\n', '\t' }, StringSplitOptions.RemoveEmptyEntries);

        int wordsWithErrors = 0;
        int minLen = Math.Min(expectedWords.Length, typedWords.Length);

        for (int i = 0; i < minLen; i++)
        {
            if (expectedWords[i] != typedWords[i])
            {
                wordsWithErrors++;
            }
        }

        wordsWithErrors += Math.Abs(expectedWords.Length - typedWords.Length);
        return 100f - (100f * wordsWithErrors / expectedWords.Length);
    }

    public static float CalculateAccuracyInKeystrokes(int incorrectKeystrokes, int totalKeystrokes)
    {
        return 100f - (100f * incorrectKeystrokes / totalKeystrokes);
    }

    public static float CalculateTypingSpeed(string typed, float timeTaken)
    {
        return (typed.Length / 5f) / (timeTaken / 60f); // Words per minute
    }

    public static int CalculateKeystrokesPerCharacter(int keystrokeCount, int characterCount)
    {
        return keystrokeCount / Math.Max(1, characterCount);
    }

    public static int LevenshteinDistance(string source, string target)
    {
        if (string.IsNullOrEmpty(source))
            return target.Length;
        if (string.IsNullOrEmpty(target))
            return source.Length;

        int sourceLength = source.Length;
        int targetLength = target.Length;

        int[,] distance = new int[sourceLength + 1, targetLength + 1];

        for (int i = 0; i <= sourceLength; i++)
            distance[i, 0] = i;

        for (int j = 0; j <= targetLength; j++)
            distance[0, j] = j;

        for (int i = 1; i <= sourceLength; i++)
        {
            for (int j = 1; j <= targetLength; j++)
            {
                int cost = target[j - 1] == source[i - 1] ? 0 : 1;
                distance[i, j] = Math.Min(
                    Math.Min(distance[i - 1, j] + 1, distance[i, j - 1] + 1),
                    distance[i - 1, j - 1] + cost);
            }
        }

        return distance[sourceLength, targetLength];
    }   
}