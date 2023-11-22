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

    public void updateNameGuess(int index){
        switch (index){
            case 0: nameGuess = "Alex"; break;
            case 1: nameGuess = "Sarah"; break;
            case 2: nameGuess = "Siovan"; break;
        }
    }

    public void updateRelGuess(int index){
        switch (index){
            case 0: relGuess = "Brother"; break;
            case 1: relGuess = "Sister"; break;
            case 2: relGuess = "Daughter"; break;
        }
    }

    public void updateRoleGuess(int index){
        switch (index){
            case 0: roleGuess = "None"; break;
            case 1: roleGuess = "Murderer"; break;
            case 2: roleGuess = "Conspirator"; break;
        }
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
