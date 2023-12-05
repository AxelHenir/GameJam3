using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class CharacterEntry : MonoBehaviour
{
    // Each entry in the ledger is made from:
    public Sprite charPortrait;
    public String nameGuess, relGuess, roleGuess;

    // Hidden to the player, the "correct" answers:
    public String correctName, correctRel, correctRole;

    [SerializeField] PlaqueController plaqueController;

    private AudioHandlerMech audioHandler;

    private void Start()
    {
        audioHandler = GameObject.Find("AudioHandler").GetComponent<AudioHandlerMech>(); //assumes we have the AudioHandlerMech on an object with this name
    }

    public void updateNameGuess(int index){
        switch (index){
            case 0: nameGuess = "Please select"; break;
            case 1: nameGuess = "Michelle Khan"; break;
            case 2: nameGuess = "Noa Park"; break;
            case 3: nameGuess = "Robin Park"; break;
            case 4: nameGuess = "Ryan Park"; break;
            case 5: nameGuess = "Jesse Richard"; break;
        }
        string personToUpdate = correctRel;
        plaqueController.UpdateMuseumPlaque(personToUpdate, 0, nameGuess);
        AudioHandlerMech.Instance.PlaySound("pencil-scratch");
    }

    public void updateRelGuess(int index){
        switch (index){
            case 0: relGuess = "Please select"; break;
            case 1: relGuess = "Aunt"; break;
            case 2: relGuess = "Father"; break;
            case 3: relGuess = "Mother"; break;
            case 4: relGuess = "Sibling"; break;
            case 5: relGuess = "Subject"; break;
        }
        string personToUpdate = correctRel;
        plaqueController.UpdateMuseumPlaque(personToUpdate, 1, relGuess);
        AudioHandlerMech.Instance.PlaySound("pencil-scratch");
    }

    public void updateRoleGuess(int index){
        switch (index){
            case 0: roleGuess = "Please select"; break;
            case 1: roleGuess = "None"; break;
            case 2: roleGuess = "Perpetrator"; break;
            case 3: roleGuess = "Complicit"; break;
            case 4: roleGuess = "Survivor"; break;
            case 5: roleGuess = "Victim"; break; 
                //new role: Supporter? 
        }
        string personToUpdate = correctRel;
        plaqueController.UpdateMuseumPlaque(personToUpdate, 2, roleGuess);
        AudioHandlerMech.Instance.PlaySound("pencil-scratch");
    }

    public bool verifyName(){
        return (nameGuess == correctName);
    }

    public bool verifyRel(){
        return (relGuess == correctRel);
    }

    public bool verifyRole(){
        return (roleGuess == correctRole);
    }
}
