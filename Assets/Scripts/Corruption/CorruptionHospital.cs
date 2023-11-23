using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorruptionHospital : MonoBehaviour
{
    GameObject HospitalMemory;

    // Start is called before the first frame update
    void Start()
    {
        HospitalMemory = this.transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Anything inside this object is not corrupted
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("VisualTrigger"))
        {
            if(other.transform.transform.IsChildOf(HospitalMemory.transform))
            {
                //if the object is a child of the hospital memory, remove corruption
                other.gameObject.GetComponent<VisualTrigger>().isCorrupted = false;
            }            
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("VisualTrigger"))
        {
            if (other.transform.transform.IsChildOf(HospitalMemory.transform))
            {
                //if the object is a child of the hospital memory, remove corruption
                other.gameObject.GetComponent<VisualTrigger>().isCorrupted = false;
            }
        }
    }

    //Anything outside this object is corrupted
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("VisualTrigger"))
        {
            if (other.transform.transform.IsChildOf(HospitalMemory.transform))
            {
                //if the object is a child of the hospital memory, add corruption
                other.gameObject.GetComponent<VisualTrigger>().isCorrupted = true;
                Debug.Log("Start corruption");
            }
        }
    }
}
