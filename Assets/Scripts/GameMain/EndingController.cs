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

    // Start is called before the first frame update
    void Start()
    {
        TotalNumberOfLabels = GlobalSManager.GetTotalNumberOfLabels();
        numberOfNamesGuessedCorrectly = GlobalSManager.GetnumberOfNamesGuessedCorrectly();
        numberOfRelsGuessedCorrectly = GlobalSManager.GetnumberOfRelsGuessedCorrectly();
        numberOfRolesGuessedCorrectly = GlobalSManager.GetnumberOfRolesGuessedCorrectly();

        Debug.Log(numberOfNamesGuessedCorrectly+"/5 correct name guesses");
        Debug.Log(numberOfRelsGuessedCorrectly + "/5 correct rel guesses");
        Debug.Log(numberOfRolesGuessedCorrectly + "/5 correct role guesses");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
