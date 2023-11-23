using DescantRuntime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualTrigger : MonoBehaviour
{
    bool isPlayerInRange; //keep track of whether the player is in range

    private GameObject visualCue; //holds all of the visual cue content

    private FirstPersonController playerController;
    private GameObject ObjectThatIsInteractedWith;

    [SerializeField] GameObject ui; //holds descant script'
    [SerializeField] TextAsset thisScript;
    private DescantConversationUI descantUIScript;

    //Make sure the sphere collider to this object is in a big enough range, and is isTrigger
    void Awake()
    {
        descantUIScript = ui.GetComponent<DescantConversationUI>();

        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>();

        visualCue = this.transform.GetChild(0).gameObject;


        CheckHideUI();

        descantUIScript.conversationDone += CheckHideUI;

        isPlayerInRange = false;

        //get the parent object of Visual Trigger:
        ObjectThatIsInteractedWith = this.transform.parent.gameObject;
    }



    void CheckHideUI()
    {
        if(descantUIScript.isResponseType)
        {
            Invoke(nameof(HideUI), 2f);
        }
        else
        {
            HideUI();
        }
    }

    void HideUI()
    {
        ui.SetActive(false);
        playerController.playerCanMove = true;
        //playerController.lockCursor = true;
        Cursor.lockState = CursorLockMode.Locked;
        playerController.cameraCanMove = true;
    }

    void ShowUI()
    {
        
        playerController.playerCanMove = false;
        playerController.cameraCanMove = false;
        //playerController.lockCursor = false;
        Cursor.lockState = CursorLockMode.None;
        ui.SetActive(true);
    }

    private void Start()
    {
        visualCue.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (visualCue != null)
        {

            if (isPlayerInRange)
            {
                //show visual cue if player is in range
                visualCue.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E)) //interaction happens
                {
                    Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
                    RaycastHit hit;

                    if (gameObject != null && Physics.Raycast(ray, out hit))
                    {
                        if(hit.transform.gameObject == ObjectThatIsInteractedWith) //if the ray hit object is the object that is being interacted with, then proceed with interaction
                        {
                            Debug.Log("VISUAL TRIGGER INTERACT WITH " + ObjectThatIsInteractedWith.name);

                            //remove visual cues
                            isPlayerInRange = false;

                            //initialize interaction!

                            //descantUIScript.InitializeDialogue(descantUIScript.descantGraph); //text asset for this object / person
                            descantUIScript.InitializeDialogue(thisScript); //text asset for this object / person
                            Debug.Log("dialogue shown");
                            ShowUI();
                        }                       

                    }                

                }
            }
            else
            {
                //don't show visual cue if player is not in range
                visualCue.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isPlayerInRange = true;
            //Debug.Log("IN RANGE");

            if (visualCue != null)
            {
                //show visual cue if player is in range
                visualCue.SetActive(true);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isPlayerInRange = true;
            if (visualCue != null)
            {
                //show visual cue if player is in range
                visualCue.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isPlayerInRange = false;
            //Debug.Log("OUT OF RANGE");

            if (visualCue != null)
            {
                //don't show visual cue if player is not in range
                visualCue.SetActive(false);
            }
        }
    }
}
