using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalSManager
{
    private static string lastScene = "";

    private static int TotalNumberOfLabels; 
    private static int NumberOfNamesGuessedCorrectly;
    private static int NumberOfRelsGuessedCorrectly;
    private static int NumberOfRolesGuessedCorrectly;
    private static List<CharacterEntry> CorrectlyGuessedEntries;

    public static string GetLastScene()
    {
        return lastScene;
    }
    public static void SetLastScene(string scene)
    {
        lastScene = scene;
    }

    public static int GetNumberOfNamesGuessedCorrectly()
    {
         return NumberOfNamesGuessedCorrectly; 
    }
    public static void SetNumberOfNamesGuessedCorrectly(int value)
    {
        NumberOfNamesGuessedCorrectly = value;
    }
    public static int GetNumberOfRelsGuessedCorrectly()
    {
        return NumberOfRelsGuessedCorrectly;
    }
    public static void SetNumberOfRelsGuessedCorrectly(int value)
    {
        NumberOfRelsGuessedCorrectly = value;
    }
    public static int GetNumberOfRolesGuessedCorrectly()
    {
        return NumberOfRolesGuessedCorrectly;
    }
    public static void SetNumberOfRolesGuessedCorrectly(int value)
    {
        NumberOfRolesGuessedCorrectly = value;
    }

    public static int GetTotalNumberOfLabels()
    {
        return TotalNumberOfLabels;
    }
    public static void SetTotalNumberOfLabels(int value)
    {
        TotalNumberOfLabels = value;
    }

    public static List<CharacterEntry> GetCorrectlyGuessedEntries()
    {
        if(CorrectlyGuessedEntries != null)
        {
            return CorrectlyGuessedEntries;
        }
        else
        {
            return new List<CharacterEntry>();
        }
        
    }
    public static void SetCorrectlyGuessedEntries(List<CharacterEntry> value)
    {
        CorrectlyGuessedEntries = value;
    }
}
    public class GlobalSceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
