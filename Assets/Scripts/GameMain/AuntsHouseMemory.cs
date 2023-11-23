using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuntsHouseMemory : MonoBehaviour
{
    //Script attached to the container that encapsulates the Aunt's House Memory
    public bool isInAuntsHouseMemory;

    FirstPersonController firstPersonController;

    void Start()
    {
        isInAuntsHouseMemory = false;
    }


    void Update()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Debug.Log("We are in the aunt's house memory");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInAuntsHouseMemory = true;
            Debug.Log("We are in the aunt's house memory");

            //Reset the last position right as we enter the memory to reset the distance travelled
            firstPersonController = other.gameObject.GetComponent<FirstPersonController>();

            firstPersonController.lastPosition = other.transform.position + new Vector3(0, 0.1f, 0);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInAuntsHouseMemory = false;
        }
    }
}
