using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CorruptionController : MonoBehaviour
{
    //Gets the info from the GameManager for what's currently happening, how many 
    [SerializeField] GameplayManager gameManager;
    //Updates the memories with corruption or not

    //Need the center of the memory as point of reference to add corruption to everything outside a radius distance
    //Corruption meter

    // Reference to the FirstPersonController script
    [SerializeField] FirstPersonController firstPersonController;
    private Vector3 PlayerStartingPosition = new Vector3(0,1.5f,0);
    private Vector3 PlayerStartingRotation = new Vector3(0,0,0);
    
    // The distance the player needs to travel for the scene to be corrupted
    public float corruptionThreshold; //100

    // The total distance the player had traveled the last time the scene was corrupted
    private float lastCorruptionDistance = 0.0f;

    // Cylinder object that determines where the corruption is
    [SerializeField] GameObject corruptionHospitalObj;
    [SerializeField] GameObject corruptionFuneralObj;
    [SerializeField] GameObject corruptionAuntsHouseObj;
    [SerializeField] GameObject HospitalTutorialText;
    [SerializeField] GameObject AuntsHouseTutorialText;
    private Vector3 initialScale = new Vector3(50.0f,5.0f,50.0f); //starting scale
    private Vector3 finalScale = new Vector3(5.0f,5.0f,5.0f); //ending scale
    private Material CorruptionMaterial;

    // Holds the Memory Perimeter 
    [SerializeField] public HospitalMemory HospitalMemory;
    [SerializeField] public FuneralMemory FuneralMemory;
    [SerializeField] public AuntsHouseMemory AuntsHouseMemory;

    // Reference to the CorruptionStatusBar script
    //public CorruptionStatusBar CorruptionStatusBar;

    //The current level of corruption
    public float corruptionLevel;

    public float corruptionSpeed;

    [SerializeField] Material UncorruptedMaterial;
    [SerializeField] Material CorruptedMaterial;

    //Reference to the Slider component of the status bar
    [SerializeField] Slider statusBar;

    //bool isResetting;

    // Start is called before the first frame update
    void Start()
    {
        CorruptionMaterial = corruptionHospitalObj.GetComponent<Renderer>().material;
        statusBar.value = 0;
        //corruptionSpeed = 2;

        //isResetting = false;

        HospitalTutorialText.SetActive(false);
        AuntsHouseTutorialText.SetActive(false);

        corruptionThreshold = 100.0f;
    }

    public void ResetCorruption()
    {
        //isResetting = true;
        HospitalMemory.isInHospitalMemory = false;
        FuneralMemory.isInFuneralMemory = false;
        AuntsHouseMemory.isInAuntsHouseMemory = false;

        gameManager.ResetGame();    

        //Reset the player's position to the start
        firstPersonController.GetComponent<Transform>().position = PlayerStartingPosition;
        firstPersonController.GetComponent<Transform>().rotation = Quaternion.Euler(PlayerStartingRotation);

        //Reset the distance travelled by the player
        firstPersonController.distanceTraveled = 0.0f;

        //reset values
        corruptionLevel = 0.0f;
        statusBar.value = 0.0f;
        lastCorruptionDistance = 0.0f;

        //reset corruption values
        //CorruptionMaterial

        //isResetting = false;

        HospitalTutorialText.SetActive(false);
        AuntsHouseTutorialText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("outside lvl: " + corruptionLevel + " statusBar: " + statusBar.value + " dist: " + firstPersonController.distanceTraveled);

        //If the Player is in one of the memories, continue the corruption counter
        if (HospitalMemory.isInHospitalMemory || FuneralMemory.isInFuneralMemory || AuntsHouseMemory.isInAuntsHouseMemory)
        {        
            //Check if the player has reached the max corruption
            if(firstPersonController.distanceTraveled - lastCorruptionDistance >= corruptionThreshold)
            {
                //Update the last corruption distance
                lastCorruptionDistance = firstPersonController.distanceTraveled;

                Debug.Log("The corruption bar is now full!!!");

                HospitalTutorialText.SetActive(true);
                AuntsHouseTutorialText.SetActive(true);

                //TODO: reboot the SCENE/system maybe? --> need to display visual feedback / warning beforehand
                //ResetCorruption();
            }

            //Calculates the current corruption 
            corruptionLevel = firstPersonController.distanceTraveled / corruptionThreshold;
                
            //Debug.Log("during lvl: " + corruptionLevel + " statusBar: " + statusBar.value + " dist: "+ firstPersonController.distanceTraveled);
            
        }

        //Corrupt the scene
        CorruptScene();

        //Update the status bar & corruption object
        UpdateCorruptionSlider();
        UpdateCorruptionObject();

        //When the player presses Q, reset the corruption
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ResetCorruption();
        }
    }

    void CorruptScene()
    {
        //Debug.Log("corrupting...");

        //corruption logic here

        //SetCorruptionMaterials();
    }

    void UpdateCorruptionObject()
    {
        //Update the scale of the sphere       
        corruptionHospitalObj.transform.localScale = Vector3.Lerp(initialScale, finalScale, corruptionLevel);
        corruptionFuneralObj.transform.localScale = Vector3.Lerp(initialScale, finalScale, corruptionLevel);
        corruptionAuntsHouseObj.transform.localScale = Vector3.Lerp(initialScale, finalScale, corruptionLevel);

        Color color = CorruptionMaterial.color;
        color.a = corruptionLevel;
        CorruptionMaterial.color = color;
    }

    void UpdateCorruptionSlider()
    {
        //Update the status bar to the corruption level
        statusBar.value = corruptionLevel;
    }

    public void SetOneCorruptionMaterial(GameObject obj)
    {
        obj.GetComponent<Renderer>().material = CorruptedMaterial;

        //if the object is a rigged one / has a skinned mesh:
        if (obj.GetComponent<SkinnedMeshRenderer>() != null)
        {
            //obj.GetComponent<SkinnedMeshRenderer>().material = CorruptedMaterial;
            SetSkinnedMaterial(obj, CorruptedMaterial);
        }       
    }
    public void SetOneUncorruptionMaterial(GameObject obj)
    {
        obj.GetComponent<Renderer>().material = UncorruptedMaterial;

        //if the object is a rigged one / has a skinned mesh:
        if (obj.GetComponent<SkinnedMeshRenderer>() != null)
        {
            //obj.GetComponent<SkinnedMeshRenderer>().material = UncorruptedMaterial;
            SetSkinnedMaterial(obj, UncorruptedMaterial);
        }            
    }

    void SetSkinnedMaterial(GameObject obj, Material newMaterial)
    {

        SkinnedMeshRenderer renderer = obj.GetComponent<SkinnedMeshRenderer>();


        if (renderer != null)
        {
            // Create a new array with the new material
            Material[] materials = new Material[renderer.materials.Length];
            for (int i = 0; i < materials.Length; i++)
            {
                materials[i] = newMaterial;
            }

            // Change the materials
            renderer.materials = materials;
        }
        else
        {
            Debug.LogError("SkinnedMeshRenderer not found on the other GameObject.");
        }
    }
}
