using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CorruptionController : MonoBehaviour
{
    //Gets the info from the GameManager for what's currently happening, how many 
    //Updates the memories with corruption or not

    //Need the center of the memory as point of reference to add corruption to everything outside a radius distance
    //Corruption meter

    // Reference to the FirstPersonController script
    public FirstPersonController firstPersonController;
    
    // The distance the player needs to travel for the scene to be corrupted
    public float corruptionThreshold = 100.0f;

    // The total distance the player had traveled the last time the scene was corrupted
    private float lastCorruptionDistance = 0.0f;

    //cylinder object that determines where the corruption is
    [SerializeField] GameObject corruptionHospitalObj;
    public Vector3 initialScale = new Vector3(50.0f,10.0f,50.0f); //starting scale
    public Vector3 finalScale = new Vector3(10.0f,10.0f,10.0f); //ending scale
    private Material CorruptionMaterial;

    public GameObject HospitalMemory;

    // Reference to the CorruptionStatusBar script
    //public CorruptionStatusBar CorruptionStatusBar;

    //The current level of corruption
    public float corruptionLevel;

    //Reference to the Slider component of the status bar
    [SerializeField] Slider statusBar;

    // Start is called before the first frame update
    void Start()
    {
        CorruptionMaterial = corruptionHospitalObj.GetComponent<Renderer>().material;
        statusBar.value = 0;
    }

    void ResetCorruption()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Check if the player has reached the max corruption
        if(firstPersonController.distanceTraveled - lastCorruptionDistance >= corruptionThreshold)
        {
            //Update the last corruption distance
            lastCorruptionDistance = firstPersonController.distanceTraveled;

            Debug.Log("The corruption bar is now full!!!");
            
            //TODO: reboot the SCENE/system maybe? --> need to display visual feedback / warning beforehand
        }

        //Calculates the current corruption 
        corruptionLevel = firstPersonController.distanceTraveled / corruptionThreshold;

        //Corrupt the scene
        CorruptScene();

        //Update the status bar & corruption object
        UpdateCorruptionObject();
        UpdateCorruptionSlider();
    }

    void CorruptScene()
    {
        //Debug.Log("corrupting...");
        
        //corruption logic here

        //make certain items not inside the cylinder corruption inaccessible

    }

    void UpdateCorruptionObject()
    {
        //Update the scale of the sphere       
        corruptionHospitalObj.transform.localScale = Vector3.Lerp(initialScale, finalScale, corruptionLevel);//+= new Vector3(-0.5f, 0, -0.5f);

        Color color = CorruptionMaterial.color;
        color.a = corruptionLevel;
        CorruptionMaterial.color = color;
    }

    void UpdateCorruptionSlider()
    {
        //Update the status bar to the corruption level
        statusBar.value = corruptionLevel;
    }
}
