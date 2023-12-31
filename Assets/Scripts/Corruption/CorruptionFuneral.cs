using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorruptionFuneral : MonoBehaviour
{
    [SerializeField] CorruptionController CorruptionController;
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
        if (other.CompareTag("FuneralInteractable"))
        {
            other.transform.GetChild(0).GetComponent<VisualTrigger>().isCorrupted = false;
          
            //Debug.Log("No corruption");
        }
    }
    */

    //Anything inside this object is not corrupted
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("FuneralInteractable"))
        {
            if (other.transform.GetChild(0).GetComponent<VisualTrigger>() != null)
                other.transform.GetChild(0).GetComponent<VisualTrigger>().isCorrupted = false;
            CorruptionController.SetOneUncorruptionMaterial(other.gameObject);
            //Debug.Log("No corruption");
        }
    }

    //Anything outside this object is corrupted
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("FuneralInteractable"))
        {
            if (other.transform.GetChild(0).GetComponent<VisualTrigger>() != null)
                other.transform.GetChild(0).GetComponent<VisualTrigger>().isCorrupted = true;
            CorruptionController.SetOneCorruptionMaterial(other.gameObject);
            Debug.Log("Start corruption");

        }
    }
}
