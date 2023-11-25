using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingController : MonoBehaviour
{
    /// <summary>
    /// Receives the information about the player's ledger to change the ending accordingly
    /// </summary>

    int TotalNumberOfLabels = 15;
    int numberOfNamesGuessedCorrectly = 0;
    int numberOfRelsGuessedCorrectly = 0;
    int numberOfRolesGuessedCorrectly = 0;
    List<CharacterEntry> CorrectlyGuessedEntries;

    // Start is called before the first frame update
    void Start()
    {
        TotalNumberOfLabels = GlobalSManager.GetTotalNumberOfLabels();
        numberOfNamesGuessedCorrectly = GlobalSManager.GetNumberOfNamesGuessedCorrectly();
        numberOfRelsGuessedCorrectly = GlobalSManager.GetNumberOfRelsGuessedCorrectly();
        numberOfRolesGuessedCorrectly = GlobalSManager.GetNumberOfRolesGuessedCorrectly();
        CorrectlyGuessedEntries = GlobalSManager.GetCorrectlyGuessedEntries();

        Debug.Log(numberOfNamesGuessedCorrectly+"/5 correct name guesses");
        Debug.Log(numberOfRelsGuessedCorrectly + "/5 correct rel guesses");
        Debug.Log(numberOfRolesGuessedCorrectly + "/5 correct role guesses");

        // can print the names of the correctly guessed characters
        foreach (CharacterEntry correctEntry in CorrectlyGuessedEntries)
        {
            Debug.Log(correctEntry.correctName + " was guessed correctly!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
