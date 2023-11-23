using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuneralMemory : MonoBehaviour
{
    //Script attached to the container that encapsulates the Funeral Memory
    public bool isInFuneralMemory;

    FirstPersonController firstPersonController;

    void Start()
    {
        isInFuneralMemory = false;
    }


    void Update()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Debug.Log("We are in the funeral memory");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInFuneralMemory = true;
            Debug.Log("We are in the funeral memory");

            //Reset the last position right as we enter the memory to reset the distance travelled
            firstPersonController = other.gameObject.GetComponent<FirstPersonController>();

            firstPersonController.lastPosition = other.transform.position + new Vector3(0, 0.1f, 0);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInFuneralMemory = false;
        }
    }
}
