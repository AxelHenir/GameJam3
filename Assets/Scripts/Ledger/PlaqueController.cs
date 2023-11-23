using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaqueController : MonoBehaviour
{
    [SerializeField] List<GameObject> listSubjectObjects;
    [SerializeField] List<GameObject> listSiblingObjects;
    [SerializeField] List<GameObject> listAuntObjects;
    [SerializeField] List<GameObject> listMotherObjects;
    [SerializeField] List<GameObject> listFatherObjects;

    [SerializeField] GameObject MuseumPlaquePrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateMuseumPlaque(string personToUpdate, int columnToUpdate,string guess)
    {
        //if there isn't a plaque, spawn one
        //SpawnMuseumPlaque();

        //use personToUpdate to get the correct list of game objects to update the plaques for

        //use columnToUpdate to update the right text field

        //use guess for the new guess to change
    }

    void SpawnMuseumPlaque()
    {
        //spawn as a child of each game object in the list
    }
}
