using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IInteractable{
    public void Interact();
}
public class Interactor : MonoBehaviour
{

    public Transform InteractorSource;
    public float InteractRange;
    void Start()
    {
    }

    void Update()
    {
        // Check if holding E
        if(Input.GetKeyDown(KeyCode.E)){

            Ray r = new Ray(InteractorSource.position, InteractorSource.forward);

            // Check range
            if(Physics.Raycast(r, out RaycastHit hitInfo, InteractRange)){

                // Check if object is interactable
                if(hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactiveObj)){
                
                    // Interact
                    interactiveObj.Interact();
                }
            }
        }
    }
}
