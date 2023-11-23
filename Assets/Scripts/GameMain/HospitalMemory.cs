using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HospitalMemory : MonoBehaviour
{
    //Script attached to the container that encapsulates the Hospital Memory
    public bool isInHospitalMemory;

    FirstPersonController firstPersonController;

    void Start()
    {
        isInHospitalMemory = false;
    }

    
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            //Debug.Log("We are in the hospital memory");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInHospitalMemory = true;
            Debug.Log("We are in the hospital memory");

            //Reset the last position right as we enter the memory to reset the distance travelled
            firstPersonController = other.gameObject.GetComponent<FirstPersonController>();

            firstPersonController.lastPosition = other.transform.position + new Vector3(0,0.1f,0);
        }            
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInHospitalMemory = false;
        }
    }
}
