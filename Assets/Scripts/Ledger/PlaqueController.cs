using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlaqueController : MonoBehaviour
{
    [SerializeField] List<GameObject> listSubjectObjects;
    [SerializeField] List<GameObject> listSiblingObjects;
    [SerializeField] List<GameObject> listAuntObjects;
    [SerializeField] List<GameObject> listMotherObjects;
    [SerializeField] List<GameObject> listFatherObjects;

    [SerializeField] GameObject MuseumPlaquePrefab;

    GameObject CurrentMuseumPlaque;
    TMP_Text CurrentPlaqueText;
    List<GameObject> allMuseumPlaques;

    //--- Plaque Visibility Button ---

    private bool isPlaqueOn;
    [SerializeField] Button PlaqueVisibilityButton;
    TextMeshProUGUI PlaqueButtonText;

    private AudioHandlerMech audioHandler;

    void Start()
    {
        allMuseumPlaques = new List<GameObject>();

        PlaqueButtonText = PlaqueVisibilityButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>();

        isPlaqueOn = true; // default on

        audioHandler = GameObject.Find("AudioHandler").GetComponent<AudioHandlerMech>(); //assumes we have the AudioHandlerMech on an object with this name
    }

    // Update is called once per frame
    void Update()
    {
        //makes all plaques face player at all times
        if(allMuseumPlaques.Count != 0)
        {
            RotateAllPlaquesTowardsPlayer();
        }


    }

    public void UpdateMuseumPlaque(string personToUpdate, int columnToUpdate, string guess)
    {
        //use personToUpdate to get the correct list of game objects to update the plaques for
        List<GameObject> listToModify = VariableValueFromName(personToUpdate);

        //Debug.Log("Person to update: "+personToUpdate);
        
        if(listToModify[0].transform.Find("MuseumPlaque(Clone)") != null)
        {
            CurrentMuseumPlaque = listToModify[0].transform.Find("MuseumPlaque(Clone)").gameObject;
        }
        else
        {
            //if there isn't a plaque, spawn one
            SpawnMuseumPlaque(listToModify);
        }

        
        List<GameObject> listofMuseumPlaques = GetMuseumPlaqueList(listToModify);
        foreach(GameObject obj in listofMuseumPlaques)
        {
            allMuseumPlaques.Add(obj); //ISSUE: keeps adding even if exceeds the number of existing plaques
        }
        

        //use columnToUpdate to update the right text field
        //use guess for the new guess to change
        UpdateTextField(listofMuseumPlaques, columnToUpdate, guess);

        UpdateVisibility();
    }

    //spawn as a child of each game object in the list
    void SpawnMuseumPlaque(List<GameObject> listToModify)
    {      
        foreach(GameObject parentObj in listToModify)
        {
            Debug.Log("current person spawn plaque: "+parentObj.name+" and position of x:"+parentObj.transform.position.x+" z:"+parentObj.transform.position.z);
            Vector3 pedestalPosition = new Vector3(parentObj.transform.position.x, parentObj.transform.position.y, parentObj.transform.position.z); // .y - 0.25f

            //spawn the museum plaque & pedestal
            CurrentMuseumPlaque = GameObject.Instantiate(MuseumPlaquePrefab, pedestalPosition, Quaternion.identity, parentObj.transform);

            //RotatePlaqueTowardsPlayer();

            //add the pedestal height to the character
            //parentObj.transform.position += new Vector3(0, 0.25f, 0);
        }
    }

    void UpdateTextField( List<GameObject> listofMuseumPlaques, int columnToUpdate, string guess)
    {

        //Debug.Log("Guess to add: "+guess);
        foreach (GameObject museumPlaque in listofMuseumPlaques)
        {
            //RotatePlaqueTowardsPlayer();
            if (columnToUpdate == 0)
            {
                CurrentPlaqueText = museumPlaque.transform.Find("Plaque").transform.Find("PlaqueTextName").GetComponent<TMP_Text>();
                CurrentPlaqueText.text = ""+guess+".";
                //Debug.Log("Guess to add to Name: " + guess);
            }
            else if (columnToUpdate == 1)
            {
                CurrentPlaqueText = museumPlaque.transform.Find("Plaque").transform.Find("PlaqueTextRelationship").GetComponent<TMP_Text>();
                CurrentPlaqueText.text = "\n\n"+guess+".";
                //Debug.Log("Guess to add to Relationship: " + guess);
            }
            else if (columnToUpdate == 2)
            {
                CurrentPlaqueText = museumPlaque.transform.Find("Plaque").transform.Find("PlaqueTextRole").GetComponent<TMP_Text>();
                CurrentPlaqueText.text = "\n\n\n" + guess + ".";
                //Debug.Log("Guess to add to Role: " + guess);
            }
        }         
    }

    //make sure all plaques have the current visibility level
    void UpdateVisibility()
    {
        if(isPlaqueOn)
        {
            foreach(GameObject obj in allMuseumPlaques)
            {
                obj.SetActive(true);
            }
        }
        else
        {
            foreach (GameObject obj in allMuseumPlaques)
            {
                obj.SetActive(false);
            }
        }
    }

    void HidePlaques()
    {
        isPlaqueOn = false;
    }

    void ShowPlaques()
    {
        isPlaqueOn = true;
    }

    public void SetVisibility()
    {
        AudioHandlerMech.Instance.PlaySound("ui_button_simple_click_03");

        if (isPlaqueOn) //if currently on, turn them off
        {
            HidePlaques();


            Color newColor = PlaqueVisibilityButton.image.color;
            newColor.a = 0.5f; //lower opacity
            PlaqueVisibilityButton.image.color = newColor;
            PlaqueButtonText.text = "Plaques OFF"; //should display the current status of the plaques
        }
        else //if currently off, turn them on
        {
            ShowPlaques();

            Color newColor = PlaqueVisibilityButton.image.color;
            newColor.a = 1.0f; //full opacity
            PlaqueVisibilityButton.image.color = newColor;
            PlaqueButtonText.text = "Plaques ON"; //should display the current status of the plaques

        }        

        UpdateVisibility();
    }

    List<GameObject> VariableValueFromName(string name)
    {
        //Debug.Log("Found "+name);
        if(name == "Subject")
        {
            return listSubjectObjects;
        }
        else if(name == "Sibling")
        {
            return listSiblingObjects;
        }
        else if (name == "Aunt")
        {
            return listAuntObjects;
        }
        else if (name == "Mother")
        {
            return listMotherObjects;
        }
        else if (name == "Father")
        {
            return listFatherObjects;
        }
        else
        {
            return null;
        }
    }

    List<GameObject> GetMuseumPlaqueList(List<GameObject> listOfCharacterObjects)
    {
        List<GameObject> listOfPlaques = new List<GameObject>();

        foreach(GameObject parentObj in listOfCharacterObjects)
        {
            listOfPlaques.Add(parentObj.transform.Find("MuseumPlaque(Clone)").gameObject);
        }

        return listOfPlaques;
    }

    void RotatePlaqueTowardsPlayer()
    {
        //get reference to the player to get the rotation
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        //rotate museum plaque towards player
        Vector3 lookPos = player.transform.position - CurrentMuseumPlaque.transform.position;
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        CurrentMuseumPlaque.transform.rotation = rotation;
    }
    void RotateAllPlaquesTowardsPlayer()
    {
        foreach (GameObject obj in allMuseumPlaques)
        {
            //get reference to the player to get the rotation
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            //rotate museum plaque towards player
            Vector3 lookPos = player.transform.position - obj.transform.position;
            lookPos.y = 0;
            Quaternion rotation = Quaternion.LookRotation(lookPos);
            obj.transform.rotation = rotation;
        }        
    }
}
