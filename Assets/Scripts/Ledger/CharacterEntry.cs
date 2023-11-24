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

    public void updateNameGuess(int index){
        switch (index){
            case 0: nameGuess = "Alex"; break;
            case 1: nameGuess = "Sarah"; break;
            case 2: nameGuess = "Siovan"; break;
        }
        string personToUpdate = correctRel;
        plaqueController.UpdateMuseumPlaque(personToUpdate, 0, nameGuess);
    }

    public void updateRelGuess(int index){
        switch (index){
            case 0: relGuess = "Mother"; break;
            case 1: relGuess = "Subject"; break;
            case 2: relGuess = "Aunt"; break;
            case 3: relGuess = "Sibling"; break;
            case 4: relGuess = "Father"; break;
        }
        string personToUpdate = correctRel;
        plaqueController.UpdateMuseumPlaque(personToUpdate, 1, relGuess);
    }

    public void updateRoleGuess(int index){
        switch (index){
            case 0: roleGuess = "None"; break;
            case 1: roleGuess = "Murderer"; break;
            case 2: roleGuess = "Conspirator"; break;
            case 3: roleGuess = "Survivor"; break;
        }
        string personToUpdate = correctRel;
        plaqueController.UpdateMuseumPlaque(personToUpdate, 2, roleGuess);
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
