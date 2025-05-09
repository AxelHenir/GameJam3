using DescantRuntime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualTrigger : MonoBehaviour
{
    bool isPlayerInRange; //keep track of whether the player is in range

    private GameObject visualCue; //holds all of the visual cue content

    private GameObject visualCueLook; //holds the object for the raycast/looking outline
    [SerializeField] public GameObject EIconObject; //holds the object that will show the E keyboard prompt

    private FirstPersonController playerController;
    private GameObject ObjectThatIsInteractedWith;
    public AudioController dialogueAudioController;

    private KeyCode InteractKey = KeyCode.E;
    [SerializeField] GameObject ui; //conversation UI that holds descant script
    [SerializeField] TextAsset thisDialogueScript;
    public AudioClip soundToPlay;
    private DescantConversationUI descantUIScript;

    public bool isCorrupted;

    //Make sure the sphere collider to this object is in a big enough range, and is isTrigger

    private bool isInDialogue;

    [SerializeField] LedgerController ledgerController;

    void Awake()
    {
        descantUIScript = ui.GetComponent<DescantConversationUI>();

        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>();

        visualCue = this.transform.Find("VisualCue").gameObject;

        visualCueLook = this.transform.Find("VisualCue").Find("VisualCueLook").gameObject;
        visualCueLook.SetActive(false);
        

        CheckHideUI();

        descantUIScript.conversationDone += CheckHideUI;

        isPlayerInRange = false;

        //get the parent object of Visual Trigger:
        ObjectThatIsInteractedWith = this.transform.parent.gameObject;

        isCorrupted = false;

        //make sure the outline meshes match the parent mesh
        //visualCue.GetComponent<MeshFilter>().mesh = ObjectThatIsInteractedWith.GetComponent<MeshFilter>().mesh;
        //visualCueLook.GetComponent<MeshFilter>().mesh = ObjectThatIsInteractedWith.GetComponent<MeshFilter>().mesh;

        //get audio controller
        //dialogueAudioController = GameObject.Find("BGSoundsDialogue").GetComponent<AudioController>();

        KeyCode[] PlayersKeyBindings = GlobalSManager.GetKeyBindings();
        if(PlayersKeyBindings != null && PlayersKeyBindings[4] != InteractKey)
        {
            
            InteractKey = PlayersKeyBindings[4]; //4 for interact key

            //change letter for EIcon
            EIconObject.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = InteractKey.ToString();
        }

    }



    void CheckHideUI()
    {
        if(descantUIScript.isResponseType)
        {
            Invoke(nameof(HideUI), 0.2f);            
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

        //stop sounds  
        if (soundToPlay != null) dialogueAudioController.FadeOutClip(soundToPlay);

        isInDialogue = false;
    }

    void ShowUI()
    {
        isInDialogue = true;

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

            if (isPlayerInRange && !isCorrupted) //don't display visual cue and prevent interaction if the corruption is true
            {
                //show visual cue if player is in range
                visualCue.SetActive(true);

                Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
                RaycastHit hit;

                // Define the radius of the sphere cast
                float radius = 0.3f;

                //Displays the second outline if the player is looking at the object
                if(gameObject != null && Physics.SphereCast(ray, radius, out hit) && EIconObject != null) //Physics.SphereCast(ray, radius, out hit)
                {
                    if (hit.transform.gameObject == ObjectThatIsInteractedWith) //if the ray hit object is the object that is being interacted with, then proceed with interaction
                    {
                        visualCueLook.SetActive(true);
                        if(IsUIActive())
                        {
                            EIconObject.SetActive(false); 
                        }  
                        else
                        {
                            EIconObject.SetActive(true); //show E icon only if the ui is not there
                        }
                    }
                    else
                    {
                        visualCueLook.SetActive(false);
                        EIconObject.SetActive(false);
                    }
                }
                else
                {
                    visualCueLook.SetActive(false);
                    EIconObject.SetActive(false);
                }

                if (Input.GetKeyDown(InteractKey)) //interaction happens
                {                    
                    if (gameObject != null && Physics.Raycast(ray, out hit)) //Physics.Raycast(ray, out hit)
                    {
                        if (hit.transform.gameObject == ObjectThatIsInteractedWith) //if the ray hit object is the object that is being interacted with, then proceed with interaction
                        {
                            Debug.Log("VISUAL TRIGGER INTERACT WITH " + ObjectThatIsInteractedWith.name);

                            //remove visual cues
                            isPlayerInRange = false;

                            //initialize interaction!

                            //descantUIScript.InitializeDialogue(descantUIScript.descantGraph); //text asset for this object / person
                            descantUIScript.InitializeDialogue(thisDialogueScript); //text asset for this object / person
                            Debug.Log("dialogue shown");
                            ShowUI();
                            if (soundToPlay != null) dialogueAudioController.FadeInClip(soundToPlay); //start sounds 
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
                EIconObject.SetActive(false);
            }
        }
    }

    public void SetThisDialogueScript(TextAsset script)
    {
        thisDialogueScript = script;
    }

    public bool GetIsInDialogue()
    {
        return isInDialogue;
    }

    public bool IsUIActive()
    {
        if (ledgerController != null && ledgerController.GetIsLedgerActive())
        {
            return true;
        }
        else if (GetIsInDialogue())
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
