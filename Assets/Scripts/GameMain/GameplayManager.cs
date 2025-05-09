using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

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
    List<CharacterEntry> CorrectlyGuessedEntries;

    [SerializeField] CorruptionController corruptionController;    

    private AudioHandlerMech audioHandler;

    [SerializeField] Image fadeImage;
    float fadeDuration = 7f;

    [SerializeField] private GameObject TutorialTextObj;

    private void Awake()
    {
        KeyCode[] PlayersKeyBindings = GlobalSManager.GetKeyBindings();
        if(PlayersKeyBindings != null )
        {
            TutorialTextObj.GetComponent<TextMeshPro>().text = "Press " + PlayersKeyBindings[4].ToString() + " to interact"; //4 for interact key
        }



        //TODO

        //add InputManager to be able to do: InputManager.SetAxis(axisName, newPositiveKey, newNegativeKey);

    }

    void Start()
    {
        numberOfReboots = 0;
        audioHandler = GameObject.Find("AudioHandler").GetComponent<AudioHandlerMech>(); //assumes we have the AudioHandlerMech on an object with this name
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void verifyPlayerGuesses()
    {
        // Create a new list to store the correctly guessed CharacterEntries
        CorrectlyGuessedEntries = new List<CharacterEntry>();

        // Get all the CharacterEntry components from the children of the parentObject
        CharacterEntry[] characterEntries = parentOfCharacterEntries.GetComponentsInChildren<CharacterEntry>();

        // Iterate through each CharacterEntry and call the verifyName method
        foreach (CharacterEntry characterEntry in characterEntries)
        {
            bool isNameCorrect = characterEntry.verifyName();
            bool isRelCorrect = characterEntry.verifyRel();
            bool isRoleCorrect = characterEntry.verifyRole();

            if (isNameCorrect)
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

            if (isRelCorrect)
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

            if (isRoleCorrect)
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

            // If all verifications are true, add the CharacterEntry to the CorrectlyGuessedEntries list
            if (isNameCorrect && isRelCorrect) // && isRoleCorrect
            {
                CorrectlyGuessedEntries.Add(characterEntry);
            }
        }

        AudioHandlerMech.Instance.PlaySound("ui_stamp_02");

        SetPlayersGuesses();
        LoadEnding();
    }

    public void ResetGame()
    {
        numberOfReboots++;
        Debug.Log("Resetting corruption");

        /*
        if(numberOfReboots >= 1)
        {
            corruptionController.corruptionThreshold = 60.0f; 
        }*/
    }

    public void LoadEnding()
    {
        //SceneManager.LoadScene("Ending");
        StartCoroutine(WaitAndLoadEnding());
    }

    public void SetPlayersGuesses()
    {
        GlobalSManager.SetTotalNumberOfLabels(TotalNumberOfLabels);
        GlobalSManager.SetNumberOfNamesGuessedCorrectly(numberOfNamesGuessedCorrectly);
        GlobalSManager.SetNumberOfRelsGuessedCorrectly(numberOfRelsGuessedCorrectly);
        GlobalSManager.SetNumberOfRolesGuessedCorrectly(numberOfRolesGuessedCorrectly);
        GlobalSManager.SetCorrectlyGuessedEntries(CorrectlyGuessedEntries);
    }

    IEnumerator WaitAndLoadEnding()
    {
        //yield return new WaitForSecondsRealtime(1f);       

        yield return new WaitForSecondsRealtime(1.5f);
        SceneManager.LoadScene("Ending");
    }

    IEnumerator FadeImage()
    {
        //wait before fading
        yield return new WaitForSecondsRealtime(0.5f);

        // loop over seconds
        for (float i = 0; i <= fadeDuration; i += Time.deltaTime)
        {
            //Debug.Log("color: "+i);
            // set color with i as alpha
            fadeImage.color = new Color(0, 0, 0, i);

            if (i > 4f)
            {
                break;
            }

            yield return null;
        }

        //load game after fade is done
        SceneManager.LoadScene("Gameplay");
    }
}
