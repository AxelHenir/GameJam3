using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorruptionHospital : MonoBehaviour
{
    [SerializeField] CorruptionController CorruptionController;
    CorruptionPlayer corruptionPlayer;
    /// <summary>
    /// Determines when objects should or shouldn't be interactable depending on where they are according to the corruption
    /// </summary>
    /// 
    public bool isPlayerOutsideCylinder;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerOutsideCylinder = false;
        }

    }
    

    //Anything inside this object is not corrupted
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("HospitalInteractable")) //this tag is put on the parent of the visual trigger obj
        {            
            if(other.transform.GetChild(0).GetComponent<VisualTrigger>() != null)
                other.transform.GetChild(0).GetComponent<VisualTrigger>().isCorrupted = false; //controls interaction
            CorruptionController.SetOneUncorruptionMaterial(other.gameObject);
            //Debug.Log("No corruption");
        }

        if (other.CompareTag("HospitalNonInteractable")) //this tag is put on every obj that can be corrupted - but not interactable
        {
            CorruptionController.SetOneUncorruptionMaterial(other.gameObject);
            //Debug.Log("No corruption");
        }
    }

    //Anything outside this object is corrupted
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("HospitalInteractable")) //this tag is put on the parent of the visual trigger obj
        {
            if (other.transform.GetChild(0).GetComponent<VisualTrigger>() != null)
                other.transform.GetChild(0).GetComponent<VisualTrigger>().isCorrupted = true; //controls interaction

            CorruptionController.SetOneCorruptionMaterial(other.gameObject);
            Debug.Log("Start corruption on "+other.name);

        }

        if (other.CompareTag("HospitalNonInteractable")) //this tag is put on every obj that can be corrupted - but not interactable
        {
            CorruptionController.SetOneCorruptionMaterial(other.gameObject);
            Debug.Log("Start corruption on " + other.name);

        }

        if (other.CompareTag("Player")) //if the player is exiting
        {
            isPlayerOutsideCylinder = true;
        }        
    }
}
