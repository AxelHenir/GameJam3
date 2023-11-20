using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemDemo : MonoBehaviour, IInteractable
{

    public void Interact()
    {
        Debug.Log("You've done it now!");
    }
}
