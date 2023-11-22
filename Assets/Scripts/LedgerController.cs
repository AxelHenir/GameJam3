using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LedgerController : MonoBehaviour
{
    //Stores the ledger list where all the labelling happens
    public List<LabelModel> LedgerLabelList;

    // GameObject which houses the Ledger UI
    public GameObject ledgerUI;

    // GameObjects which house the content of each tab
    public GameObject namesTabContent;
    public GameObject relationshipsTabContent;
    public GameObject mysteryRolesTabContent;

    // Buttons which active a given tab
    public Button namesTabButton;
    public Button relationshipsTabButton;
    public Button mysteryRolesTabButton;
    public Button exitButton;
    
    void Start()
    {
        // Ledger begins closed
        ledgerUI.SetActive(false);

        // Assign the onClick events for each tab button
        // namesTabButton.onClick.AddListener(() => ShowTab(namesTabContent));
        // relationshipsTabButton.onClick.AddListener(() => ShowTab(relationshipsTabContent));
        // mysteryRolesTabButton.onClick.AddListener(() => ShowTab(mysteryRolesTabContent));
        // exitButton.onClick.AddListener(() => closeLedgerUI());

    }

    void Update()
    {
        // Check if the TAB key is pressed
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            // If so, toggle the ledger
            toggleLedger();
        }
    }

    // Checks of ledger is open or not and simply toggles to alt state
    public void toggleLedger()
    {

        if(ledgerUI.activeSelf){

            // Ledger is open, close
            closeLedgerUI();
        } else {

            // Ledger is closed, open
            openLedgerUI();
        }
    }

    // Opens the ledger
    public void openLedgerUI()
    {
        // Disable the player's movement controls

        // Enable the cursor to move freely
        Cursor.lockState = CursorLockMode.None;

        // Set the UI to active
        ledgerUI.SetActive(true);
        
        // Set the starting tab (Names for now)
        ShowTab(namesTabContent);
    }

    // Closes the ledger
    public void closeLedgerUI()
    {

        // Set the UI to inactive
        ledgerUI.SetActive(false);
            
        // Set player's cursor to locked
        Cursor.lockState = CursorLockMode.Locked;

        // Enable the player's movement controls
      
    }

    // Opens the given tab, disabling other tabs
    public void ShowTab(GameObject tabContent)
    {
        // Disable all tab content panels
        namesTabContent.SetActive(false);
        relationshipsTabContent.SetActive(false);
        mysteryRolesTabContent.SetActive(false);

        // Enable the selected tab content panel
        tabContent.SetActive(true);
    }
}
