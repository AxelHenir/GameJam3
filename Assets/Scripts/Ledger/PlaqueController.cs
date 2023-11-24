using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateMuseumPlaque(string personToUpdate, int columnToUpdate, string guess)
    {
        //use personToUpdate to get the correct list of game objects to update the plaques for
        List<GameObject> listToModify = VariableValueFromName(personToUpdate);

        Debug.Log("Person to update: "+personToUpdate);
        
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

        //use columnToUpdate to update the right text field
        //use guess for the new guess to change
        UpdateTextField(listofMuseumPlaques, columnToUpdate, guess);



    }

    //spawn as a child of each game object in the list
    void SpawnMuseumPlaque(List<GameObject> listToModify)
    {      
        foreach(GameObject parentObj in listToModify)
        {
            Vector3 pedestalPosition = new Vector3(parentObj.transform.position.x, parentObj.transform.position.y - 0.25f, parentObj.transform.position.z);

            //spawn the museum plaque & pedestal
            CurrentMuseumPlaque = GameObject.Instantiate(MuseumPlaquePrefab, pedestalPosition, Quaternion.identity, parentObj.transform);

            //get reference to the player to get the rotation
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            //rotate museum plaque towards player
            Vector3 lookPos = player.transform.position - CurrentMuseumPlaque.transform.position;
            lookPos.y = 0;
            Quaternion rotation = Quaternion.LookRotation(lookPos);
            CurrentMuseumPlaque.transform.rotation = rotation;

            //add the pedestal height to the character
            parentObj.transform.position += new Vector3(0, 0.25f, 0);
        }
    }

    void UpdateTextField( List<GameObject> listofMuseumPlaques, int columnToUpdate, string guess)
    {
        Debug.Log("Guess to add: "+guess);
        foreach (GameObject museumPlaque in listofMuseumPlaques)
        {
            if (columnToUpdate == 0)
            {
                CurrentPlaqueText = museumPlaque.transform.Find("Plaque").transform.Find("PlaqueTextName").GetComponent<TMP_Text>();
                CurrentPlaqueText.text = ""+guess+".";
                Debug.Log("Guess to add to Name: " + guess);
            }
            else if (columnToUpdate == 1)
            {
                CurrentPlaqueText = museumPlaque.transform.Find("Plaque").transform.Find("PlaqueTextRelationship").GetComponent<TMP_Text>();
                CurrentPlaqueText.text = "\n\n"+guess+".";
                Debug.Log("Guess to add to Relationship: " + guess);
            }
            else if (columnToUpdate == 2)
            {
                CurrentPlaqueText = museumPlaque.transform.Find("Plaque").transform.Find("PlaqueTextRole").GetComponent<TMP_Text>();
                CurrentPlaqueText.text = "\n\n\n" + guess + ".";
                Debug.Log("Guess to add to Role: " + guess);
            }
        }
        /*
        foreach (GameObject museumPlaque in listofMuseumPlaques)
        {
            Transform childTransform;
            if (columnToUpdate == 0)
            {
                childTransform = museumPlaque.transform.Find("Plaque").transform.Find("PlaqueTextName");
            }
            else if (columnToUpdate == 1)
            {
                childTransform = museumPlaque.transform.Find("Plaque").transform.Find("PlaqueTextRelationship");
            }
            else // columnToUpdate == 2
            {
                childTransform = museumPlaque.transform.Find("Plaque").transform.Find("PlaqueTextRole");
            }

            if (childTransform == null)
            {
                Debug.LogError("Child object not found");
            }
            else
            {
                CurrentPlaqueText = childTransform.GetComponent<TMP_Text>();
                if (CurrentPlaqueText == null)
                {
                    Debug.LogError("TMP_Text component not found");
                }
                else
                {
                    CurrentPlaqueText.text = "\n\n\n" + guess + ".";
                }
            }
        }*/
         
    }

    List<GameObject> VariableValueFromName(string name)
    {
        Debug.Log("Found "+name);
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
}
