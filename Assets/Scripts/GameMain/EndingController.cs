using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    [SerializeField] GameObject Letter;
    [SerializeField] List<TextAsset> EndingScripts;

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

        foreach(CharacterEntry correctEntry in CorrectlyGuessedEntries)
        {

            /*
             * Murderer + Aunt
             * Murderer + P1
             * Murderer + P2
             * Murderer + Brother
             * 
             * Murderer + Subject
             * 
             * Conspirator + P1
             * 
             * Victim + Brother
            */

        }




        VisualTrigger LetterVisualTrigger = Letter.transform.GetChild(0).GetComponent<VisualTrigger>();

        //Pick the ending based on the guesses:
        /*
        //If the correct number of guessed entries is 0, 1 or 2
        if (CorrectlyGuessedEntries.Count <= 2) 
        {
            //Set the dialogue script to the bad one
            LetterVisualTrigger.SetThisDialogueScript(EndingScripts[0]); //0 is bad ending          
        }
        //If the correct number of guessed entries is 3 or 4
        else if (CorrectlyGuessedEntries.Count <= 4)
        {
            //Set the dialogue script to the okay one
            LetterVisualTrigger.SetThisDialogueScript(EndingScripts[1]); //1 is okay ending
        }
        //If the correct number of guessed entries is 5
        else if (CorrectlyGuessedEntries.Count == 5)
        {
            //Set the dialogue script to the amazing one
            LetterVisualTrigger.SetThisDialogueScript(EndingScripts[2]); //2 is amazing ending
        }
        */

        //If the correct number of guessed entries is 0, 1 or 2
        if (CorrectlyGuessedEntries.Count <= 4)
        {
            //Set the dialogue script to the okay one
            LetterVisualTrigger.SetThisDialogueScript(EndingScripts[0]); //0 is okay ending          
        }
        //If the correct number of guessed entries is 5
        else if (CorrectlyGuessedEntries.Count == 5)
        {
            //Set the dialogue script to the amazing one
            LetterVisualTrigger.SetThisDialogueScript(EndingScripts[1]); //1 is amazing ending
        }

        //Spawn the letter
        //SpawnTheLetter(3.0f);
    }

    // Update is called once per frame
    void Update()
    {

        //temporary restart button
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Gameplay");
        }        
    }

    void SpawnTheLetter(float timeToWait)
    {
        //StartCoroutine(WaitBeforeDisplayingLetter(timeToWait));
        Letter.SetActive(true);
    }
    /*
    IEnumerator WaitBeforeDisplayingLetter(float timeToWait)
    {
        yield return new WaitForSecondsRealtime(timeToWait);

        
    }*/
}
