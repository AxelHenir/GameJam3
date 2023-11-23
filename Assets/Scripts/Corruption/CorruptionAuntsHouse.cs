using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorruptionAuntsHouse : MonoBehaviour
{
    /// <summary>
    /// Determines when objects should or shouldn't be interactable depending on where they are according to the corruption
    /// </summary>


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    /*
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AuntsHouseInteractable"))
        {
            other.transform.GetChild(0).GetComponent<VisualTrigger>().isCorrupted = false;
          
            //Debug.Log("No corruption");
        }
    }
    */

    //Anything inside this object is not corrupted
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("AuntsHouseInteractable"))
        {
            other.transform.GetChild(0).GetComponent<VisualTrigger>().isCorrupted = false;
            //Debug.Log("No corruption");
        }
    }

    //Anything outside this object is corrupted
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("AuntsHouseInteractable"))
        {
            other.transform.GetChild(0).GetComponent<VisualTrigger>().isCorrupted = true;
            Debug.Log("Start corruption");

        }
    }
}
