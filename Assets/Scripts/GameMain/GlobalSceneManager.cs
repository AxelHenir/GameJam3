using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalSManager
{
    private static string lastScene = "";

    private static int TotalNumberOfLabels; 
    private static int numberOfNamesGuessedCorrectly;
    private static int numberOfRelsGuessedCorrectly;
    private static int numberOfRolesGuessedCorrectly;

    public static string GetLastScene()
    {
        return lastScene;
    }
    public static void SetLastScene(string scene)
    {
        lastScene = scene;
    }

    public static int GetnumberOfNamesGuessedCorrectly()
    {
         return numberOfNamesGuessedCorrectly; 
    }
    public static void SetnumberOfNamesGuessedCorrectly(int value)
    {
        numberOfNamesGuessedCorrectly = value;
    }
    public static int GetnumberOfRelsGuessedCorrectly()
    {
        return numberOfRelsGuessedCorrectly;
    }
    public static void SetnumberOfRelsGuessedCorrectly(int value)
    {
        numberOfRelsGuessedCorrectly = value;
    }
    public static int GetnumberOfRolesGuessedCorrectly()
    {
        return numberOfRolesGuessedCorrectly;
    }
    public static void SetnumberOfRolesGuessedCorrectly(int value)
    {
        numberOfRolesGuessedCorrectly = value;
    }

    public static int GetTotalNumberOfLabels()
    {
        return TotalNumberOfLabels;
    }
    public static void SetTotalNumberOfLabels(int value)
    {
        TotalNumberOfLabels = value;
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
