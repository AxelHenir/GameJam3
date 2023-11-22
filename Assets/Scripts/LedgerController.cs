using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgerController : MonoBehaviour
{
    //Stores the ledger list where all the labelling happens
    public List<LabelModel> LedgerLabelList;

    // Canvas which houses the Ledger UI
    public GameObject ledgerUI;

    // The various tabs (gameObjects) for the UI
    public GameObject namesTab, relationshipsTab, mysteryRolesTab;

    // Booleans which track which tab is active and if the ledger UI is active
    public bool ledgerUIActive = false;
    public bool namesTabActive, realationshipsTabActive, rolesTabActive = false;
    
    void Start()
    {
        // Ledger begins closed

        // All tabs begin closed

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
        // Disable the player's controls

        // Set the UI to active
        if (ledgerUI != null)
            {
                ledgerUI.SetActive(true);
            }
        
        // Set the default active tab- "Names" for now
        namesTabActive = true;
        realationshipsTabActive = false;
        rolesTabActive = false;
            
        // Call the method which populates the list in the name tab
        loadNamesTab();
    }

    // Closes the ledger
    public void closeLedgerUI()
    {

        // Set all tabs to inactive
        namesTabActive = false;
        realationshipsTabActive = false;
        rolesTabActive = false;

        // Set the UI to inactive
        if (ledgerUI != null)
            {
                ledgerUI.SetActive(false);
            }
        
        // Enable the player's controls
      
    }

    // Loads all items in names tab
    public void loadNamesTab()
    {

        // Disable other tab(s)
        realationshipsTabActive = false;
        rolesTabActive = false;

        // Enable tab
        namesTabActive = true;

        // populate tab
    }

    public void loadRelationshipsTab()
    {

        // Disable other tab(s)
        namesTabActive = false;
        rolesTabActive = false;

        // Enable tab
        realationshipsTabActive = true;

        // populate tab
    }

    public void loadMysteryRolesTab()
    {

        // Disable other tab(s)
        namesTabActive = false;
        realationshipsTabActive = false;

        // Enable tab
        rolesTabActive = true;

        // populate tab
    }
}
