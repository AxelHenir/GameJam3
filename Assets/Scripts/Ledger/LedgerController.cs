using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LedgerController : MonoBehaviour
{
    // Stores the player's character
    public FirstPersonController playerController;

    // GameObject which houses the Ledger UI
    public GameObject ledgerUI;
    private bool isLedgerActive;

    // Buttons
    Button exitButton;
    Button submitAnswersButton;

    private AudioHandlerMech audioHandler;

    private KeyCode LedgerKey = KeyCode.Tab;

    private void Awake()
    {
        KeyCode[] PlayersKeyBindings = GlobalSManager.GetKeyBindings();
        if (PlayersKeyBindings[7] != LedgerKey)
            LedgerKey = PlayersKeyBindings[7]; //7 for ledger key binding
    }

    void Start()
    {
        // Ledger begins closed
        ledgerUI.SetActive(false);
        isLedgerActive = false;
		Cursor.lockState = CursorLockMode.Locked;

        audioHandler = GameObject.Find("AudioHandler").GetComponent<AudioHandlerMech>(); //assumes we have the AudioHandlerMech on an object with this name
    }

    void Update()
    {
        // Check if the TAB key is pressed
        if (Input.GetKeyDown(LedgerKey))
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
        // Disable the player's movement & camera controls
        playerController.playerCanMove = false;
        playerController.cameraCanMove = false;

        // Enable the cursor to move freely
        Cursor.lockState = CursorLockMode.None;

        // Set the UI to active
        ledgerUI.SetActive(true);
        isLedgerActive = true;

        //play opening ledger sound
        AudioHandlerMech.Instance.PlaySound("book-open");

    }

    // Closes the ledger
    public void closeLedgerUI()
    {       
        // Set the UI to inactive
        ledgerUI.SetActive(false);
        isLedgerActive = false;

        // Set player's cursor to locked
        Cursor.lockState = CursorLockMode.Locked;

        // Enable the player's movement controls
        playerController.playerCanMove = true;
        playerController.cameraCanMove = true;

        //play closing ledger sound
        AudioHandlerMech.Instance.PlaySound("book-open");
    }

    public bool GetIsLedgerActive()
    {
        return isLedgerActive;
    }
}
