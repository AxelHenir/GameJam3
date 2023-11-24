using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameplayManager : MonoBehaviour
{    
    //Holds all important information we would like to know about the game at all time

    // Stores the current guesses of the player
    //public List<CharacterEntry> playerGuesses;

    // List of all Pictures
    // public List<Sprite> characterPortraits;
    // // List of all Names
    // public List<String> namesList;
    // // List of all Relationships
    // public List<String> relationshipsList;
    // // List of all MRS
    // public List<String> mysteryRoleList;

    public GameObject parentOfCharacterEntries;

    private int numberOfReboots;

    //Tracking player guesses:
    int TotalNumberOfLabels = 15;
    int numberOfNamesGuessedCorrectly = 0;
    int numberOfRelsGuessedCorrectly = 0;
    int numberOfRolesGuessedCorrectly = 0;


    void Start()
    {
        numberOfReboots = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void verifyPlayerGuesses()
    {

        // Get all the CharacterEntry components from the children of the parentObject
        CharacterEntry[] characterEntries = parentOfCharacterEntries.GetComponentsInChildren<CharacterEntry>();

        // Iterate through each CharacterEntry and call the verifyName method
        foreach (CharacterEntry characterEntry in characterEntries)
        {
            if (characterEntry.verifyName())
            {
                // Do something if the name is correct
                //Debug.Log(characterEntry.correctName + "'s name was correctly identified!");
                numberOfNamesGuessedCorrectly++;
            }
            else
            {
                // Do something if the name is not correct
                //Debug.Log(characterEntry.correctName + "'s name is incorrect!");
            }

            // To verify the Relationship and Role, we call verifyRel() and verifyRole() as above ^

            if (characterEntry.verifyRel())
            {
                // Do something if the name is correct
                //Debug.Log(characterEntry.correctRel + "'s relationship was correctly identified!");
                numberOfRelsGuessedCorrectly++;
            }
            else
            {
                // Do something if the name is not correct
                //Debug.Log(characterEntry.correctRel + "'s relationship is incorrect!");
            }

            if (characterEntry.verifyRole())
            {
                // Do something if the name is correct
                //Debug.Log(characterEntry.correctRole + "'s role was correctly identified!");
                numberOfRolesGuessedCorrectly++;
            }
            else
            {
                // Do something if the name is not correct
                //Debug.Log(characterEntry.correctRole + "'s role is incorrect!");
            }
        }

        SetPlayersGuesses();
        LoadEnding();
    }

    public void ResetGame()
    {
        numberOfReboots++;
        Debug.Log("Resetting corruption");

    }

    public void LoadEnding()
    {
        SceneManager.LoadScene("Ending");
    }

    public void SetPlayersGuesses()
    {
        GlobalSManager.SetTotalNumberOfLabels(TotalNumberOfLabels);
        GlobalSManager.SetnumberOfNamesGuessedCorrectly(numberOfNamesGuessedCorrectly);
        GlobalSManager.SetnumberOfRelsGuessedCorrectly(numberOfRelsGuessedCorrectly);
        GlobalSManager.SetnumberOfRolesGuessedCorrectly(numberOfRolesGuessedCorrectly);
    }
}
