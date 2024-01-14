using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeyRebindings : MonoBehaviour
{

    //variable for input reference
    //[SerializeField] private FirstPersonController playerController;
    [SerializeField] private TextMeshProUGUI bindingDisplayNameTextInteract;
    [SerializeField] private GameObject startRebindInteractObj;
    [SerializeField] private GameObject waitingForInputInteractObj;

    [SerializeField] private TextMeshProUGUI bindingDisplayNameTextCorruption;
    [SerializeField] private GameObject startRebindCorruptObj;
    [SerializeField] private GameObject waitingForInputCorruptObj;

    [SerializeField] private TextMeshProUGUI bindingDisplayNameTextHorPos;
    [SerializeField] private GameObject startRebindHorPosObj;
    [SerializeField] private GameObject waitingForInputHorPosObj;

    [SerializeField] private TextMeshProUGUI bindingDisplayNameTextHorNeg;
    [SerializeField] private GameObject startRebindHorNegObj;
    [SerializeField] private GameObject waitingForInputHorNegObj;

    [SerializeField] private TextMeshProUGUI bindingDisplayNameTextVertPos;
    [SerializeField] private GameObject startRebindVertPosObj;
    [SerializeField] private GameObject waitingForInputVertPosObj;

    [SerializeField] private TextMeshProUGUI bindingDisplayNameTextVertNeg;
    [SerializeField] private GameObject startRebindVertNegObj;
    [SerializeField] private GameObject waitingForInputVertNegObj;
    
    [SerializeField] private TextMeshProUGUI bindingDisplayNameTextSprint;
    [SerializeField] private GameObject startRebindSprintObj;
    [SerializeField] private GameObject waitingForInputSprintObj;

    [SerializeField] private TextMeshProUGUI bindingDisplayNameTextZoom;
    [SerializeField] private GameObject startRebindZoomObj;
    [SerializeField] private GameObject waitingForInputZoomObj;

    [SerializeField] private TextMeshProUGUI bindingDisplayNameTextLedg;
    [SerializeField] private GameObject startRebindLedgObj;
    [SerializeField] private GameObject waitingForInputLedgObj;
    
    //private InputActionRebindingExtensions.RebindingOperation rebindingOperation;
    private KeyCode rebindingKey;
    private string KeyType;
    private bool isWaitingOnInput;
    private int[] values;
    private bool[] keys;
    private bool isRebound;
    private bool isCancelled;


    //--------------------    

    //private string horAxisName = "Horizontal";
    private KeyCode newHorPositiveKey = KeyCode.D;
    private KeyCode newHorNegativeKey = KeyCode.A;

    //private string vertAxisName = "Vertical";
    private KeyCode newVertPositiveKey = KeyCode.W;
    private KeyCode newVertNegativeKey = KeyCode.S;

    private KeyCode newInteractKey = KeyCode.E;

    private KeyCode newSprintKey = KeyCode.LeftShift;

    private KeyCode newZoomKey = KeyCode.Mouse1;

    private KeyCode newLedgerKey = KeyCode.Tab;

    private KeyCode newCorruptionResetKey = KeyCode.Q;

    private KeyCode[] playerKeybindings;

    private void Awake()
    {
        isWaitingOnInput = false;        
        //get all keycodes
        values = (int[])System.Enum.GetValues(typeof(KeyCode));
        keys = new bool[values.Length];

        playerKeybindings = new KeyCode[]{ newHorPositiveKey, newHorNegativeKey, newVertPositiveKey, newVertNegativeKey, newInteractKey, newSprintKey, newZoomKey, newLedgerKey, newCorruptionResetKey };


        for(int i = 0; i < values.Length; i++)
        {
            //set all keys to false (they aren't picked yet)
            keys[i] = false;
            //we don't want to store joystick values, so we are going to replace them here:
            if ((KeyCode)values[i] == KeyCode.JoystickButton0 || (KeyCode)values[i] == KeyCode.JoystickButton1 || (KeyCode)values[i] == KeyCode.JoystickButton2 || (KeyCode)values[i] == KeyCode.JoystickButton3 || (KeyCode)values[i] == KeyCode.JoystickButton4 || (KeyCode)values[i] == KeyCode.JoystickButton5 || (KeyCode)values[i] == KeyCode.JoystickButton6 || (KeyCode)values[i] == KeyCode.JoystickButton7 || (KeyCode)values[i] == KeyCode.JoystickButton8 || (KeyCode)values[i] == KeyCode.JoystickButton9 || (KeyCode)values[i] == KeyCode.JoystickButton10 || (KeyCode)values[i] == KeyCode.JoystickButton12 || (KeyCode)values[i] == KeyCode.JoystickButton13 || (KeyCode)values[i] == KeyCode.JoystickButton14 || (KeyCode)values[i] == KeyCode.JoystickButton15 || (KeyCode)values[i] == KeyCode.JoystickButton16 || (KeyCode)values[i] == KeyCode.JoystickButton17 || (KeyCode)values[i] == KeyCode.JoystickButton18 || (KeyCode)values[i] == KeyCode.JoystickButton19)
            {
                values[i] = (int)KeyCode.None;
            }
        }
    }

    //gets called by menu button click to rebind
    public void StartRebinding(string keyType)
    {
        Debug.Log("StartRebinding");

        isRebound = false;
        isCancelled = false;


        //check which key is being rebound
        if (keyType == "Interact")
        {
            startRebindInteractObj.SetActive(false);
            waitingForInputInteractObj.SetActive(true);

            KeyType = "Interact";
            rebindingKey = newInteractKey;
            Debug.Log("initial key: "+newInteractKey.ToString());
            isWaitingOnInput = true; //wait for new input in update
        }
        else if(keyType == "Corruption")
        {
            startRebindCorruptObj.SetActive(false);
            waitingForInputCorruptObj.SetActive(true);

            KeyType = "Corruption";
            rebindingKey = newCorruptionResetKey;
            Debug.Log("initial key: " + newCorruptionResetKey.ToString());
            isWaitingOnInput = true; //wait for new input in update
        }
        else if (keyType == "HorizontalPositive")
        {
            startRebindHorPosObj.SetActive(false);
            waitingForInputHorPosObj.SetActive(true);

            KeyType = "HorizontalPositive";
            rebindingKey = newHorPositiveKey;
            Debug.Log("initial key: " + newHorPositiveKey.ToString());
            isWaitingOnInput = true; //wait for new input in update
        }
        else if (keyType == "HorizontalNegative")
        {
            startRebindHorNegObj.SetActive(false);
            waitingForInputHorNegObj.SetActive(true);

            KeyType = "HorizontalNegative";
            rebindingKey = newHorNegativeKey;
            Debug.Log("initial key: " + newHorNegativeKey.ToString());
            isWaitingOnInput = true; //wait for new input in update
        }
        else if (keyType == "VerticalPositive")
        {
            startRebindVertPosObj.SetActive(false);
            waitingForInputVertPosObj.SetActive(true);

            KeyType = "VerticalPositive";
            rebindingKey = newVertPositiveKey;
            Debug.Log("initial key: " + newVertPositiveKey.ToString());
            isWaitingOnInput = true; //wait for new input in update
        }
        else if (keyType == "VerticalNegative")
        {
            startRebindVertNegObj.SetActive(false);
            waitingForInputVertNegObj.SetActive(true);

            KeyType = "VerticalNegative";
            rebindingKey = newVertNegativeKey;
            Debug.Log("initial key: " + newVertNegativeKey.ToString());
            isWaitingOnInput = true; //wait for new input in update
        }
        else if (keyType == "Sprint")
        {
            startRebindSprintObj.SetActive(false);
            waitingForInputSprintObj.SetActive(true);

            KeyType = "Sprint";
            rebindingKey = newSprintKey;
            Debug.Log("initial key: " + newSprintKey.ToString());
            isWaitingOnInput = true; //wait for new input in update
        }
        else if (keyType == "Zoom")
        {
            startRebindZoomObj.SetActive(false);
            waitingForInputZoomObj.SetActive(true);

            KeyType = "Zoom";
            rebindingKey = newZoomKey;
            Debug.Log("initial key: " + newZoomKey.ToString());
            isWaitingOnInput = true; //wait for new input in update
        }
        else if (keyType == "Ledger")
        {
            startRebindLedgObj.SetActive(false);
            waitingForInputLedgObj.SetActive(true);

            KeyType = "Ledger";
            rebindingKey = newLedgerKey;
            Debug.Log("initial key: " + newLedgerKey.ToString());
            isWaitingOnInput = true; //wait for new input in update
        }

        Invoke(nameof(CancelledRebind), 5.0f);
    }

    //gets called when the new input is submitted within the timer
    private void RebindComplete()
    {
        if(isWaitingOnInput && !isRebound)
        {
            Debug.Log("RebindComplete");
            isWaitingOnInput = false;

            //when receive new input:
            //store the new key in the variable
            //replace the text with the new key

            //get the new key
            for (int i = 0; i < values.Length; i++)
            {
                //if the key is true and the key is not the same as the current key, store the key in temp
                if (keys[i] && rebindingKey != (KeyCode)values[i]) //second condition is redundant bc otherwise it would be cancelled but just in case
                {
                    rebindingKey = (KeyCode)values[i];

                    Debug.Log("new input: " + rebindingKey.ToString());
                }
                //reset the keys array to all false again
                keys[i] = false;
            }

            //--rebinding happens--

            //at this point in time,
            //the rebindingKey variable now contains the value of the new key, and "new__Key" still has the old key,
            //the new key and the old key are not equal to each other

            if (KeyType == "Interact")
            {
                Debug.Log("rebindingKey: "+rebindingKey.ToString()+"\nnewInteractKey: "+newInteractKey.ToString());
                //we want to check if the new key is already existing elsewhere in the playerbindings
                CheckIfNewKeyExistsAndSwap(rebindingKey, newInteractKey);

                //rebind to the new key:
                newInteractKey = rebindingKey;
                playerKeybindings[4] = newInteractKey;
                Debug.Log("new interact key: " + newInteractKey.ToString());                

                startRebindInteractObj.SetActive(true);
                waitingForInputInteractObj.SetActive(false);
            }
            else if (KeyType == "Corruption")
            {
                //we want to check if the new key is already existing elsewhere in the playerbindings
                CheckIfNewKeyExistsAndSwap(rebindingKey, newCorruptionResetKey);

                //rebind to the new key:
                newCorruptionResetKey = rebindingKey;
                playerKeybindings[8] = newCorruptionResetKey;
                Debug.Log("new corruption key: " + newCorruptionResetKey.ToString());

                startRebindCorruptObj.SetActive(true);
                waitingForInputCorruptObj.SetActive(false);
            }
            else if (KeyType == "HorizontalPositive")
            {
                //we want to check if the new key is already existing elsewhere in the playerbindings
                CheckIfNewKeyExistsAndSwap(rebindingKey, newHorPositiveKey);

                //rebind to the new key:
                newHorPositiveKey = rebindingKey;
                playerKeybindings[0] = newHorPositiveKey;
                Debug.Log("new HorPos key: " + newHorPositiveKey.ToString());

                startRebindHorPosObj.SetActive(true);
                waitingForInputHorPosObj.SetActive(false);
            }
            else if (KeyType == "HorizontalNegative")
            {
                //we want to check if the new key is already existing elsewhere in the playerbindings
                CheckIfNewKeyExistsAndSwap(rebindingKey, newHorNegativeKey);

                //rebind to the new key:
                newHorNegativeKey = rebindingKey;
                playerKeybindings[1] = newHorNegativeKey;
                Debug.Log("new HorNeg key: " + newHorNegativeKey.ToString());

                startRebindHorNegObj.SetActive(true);
                waitingForInputHorNegObj.SetActive(false);
            }
            else if (KeyType == "VerticalPositive")
            {
                //we want to check if the new key is already existing elsewhere in the playerbindings
                CheckIfNewKeyExistsAndSwap(rebindingKey, newVertPositiveKey);

                //rebind to the new key:
                newVertPositiveKey = rebindingKey;
                playerKeybindings[2] = newVertPositiveKey;
                Debug.Log("new VertPos key: " + newVertPositiveKey.ToString());

                startRebindVertPosObj.SetActive(true);
                waitingForInputVertPosObj.SetActive(false);
            }
            else if (KeyType == "VerticalNegative")
            {
                //we want to check if the new key is already existing elsewhere in the playerbindings
                CheckIfNewKeyExistsAndSwap(rebindingKey, newVertNegativeKey);

                //rebind to the new key:
                newVertNegativeKey = rebindingKey;
                playerKeybindings[3] = newVertNegativeKey;
                Debug.Log("new VertNeg key: " + newVertNegativeKey.ToString());

                startRebindVertNegObj.SetActive(true);
                waitingForInputVertNegObj.SetActive(false);
            }
            else if (KeyType == "Sprint")
            {
                //we want to check if the new key is already existing elsewhere in the playerbindings
                CheckIfNewKeyExistsAndSwap(rebindingKey, newSprintKey);

                //rebind to the new key:
                newSprintKey = rebindingKey;
                playerKeybindings[5] = newSprintKey;
                Debug.Log("new Sprint key: " + newSprintKey.ToString());

                startRebindSprintObj.SetActive(true);
                waitingForInputSprintObj.SetActive(false);
            }
            else if (KeyType == "Zoom")
            {
                //we want to check if the new key is already existing elsewhere in the playerbindings
                CheckIfNewKeyExistsAndSwap(rebindingKey, newZoomKey);

                //rebind to the new key:
                newZoomKey = rebindingKey;
                playerKeybindings[6] = newZoomKey;
                Debug.Log("new Zoom key: " + newZoomKey.ToString());

                startRebindZoomObj.SetActive(true);
                waitingForInputZoomObj.SetActive(false);
            }
            else if (KeyType == "Ledger")
            {
                //we want to check if the new key is already existing elsewhere in the playerbindings
                CheckIfNewKeyExistsAndSwap(rebindingKey, newLedgerKey);

                //rebind to the new key:
                newLedgerKey = rebindingKey;
                playerKeybindings[7] = newLedgerKey;
                Debug.Log("new Ledger key: " + newLedgerKey.ToString());

                startRebindLedgObj.SetActive(true);
                waitingForInputLedgObj.SetActive(false);
            }

            //send the new key to GlobalManager
            // do InputManager.SetAxis(axisName, newPositiveKey, newNegativeKey);
            //set keybinding
            GlobalSManager.SetKeyBindings(playerKeybindings);
            //InputManager.SetAxis(axisName, newPositiveKey, newNegativeKey); 

            //display new key
            bindingDisplayNameTextInteract.text = newInteractKey.ToString();

            bindingDisplayNameTextCorruption.text = newCorruptionResetKey.ToString();

            bindingDisplayNameTextHorPos.text = newHorPositiveKey.ToString();
            bindingDisplayNameTextHorNeg.text = newHorNegativeKey.ToString();

            bindingDisplayNameTextVertPos.text = newVertPositiveKey.ToString();
            bindingDisplayNameTextVertNeg.text = newVertNegativeKey.ToString();

            bindingDisplayNameTextSprint.text = newSprintKey.ToString();
            bindingDisplayNameTextZoom.text = newZoomKey.ToString();

            bindingDisplayNameTextLedg.text = newLedgerKey.ToString();

            isRebound = true;
        }    
    }

    //exits the "waiting for input" screen when the timer runs out or we pressed the back button
    private void CancelledRebind()
    {        
        if(isWaitingOnInput && !isCancelled)
        {
            Debug.Log("CancelledRebind");
            isWaitingOnInput = false;

            rebindingKey = KeyCode.Mouse0;

            startRebindInteractObj.SetActive(true);
            waitingForInputInteractObj.SetActive(false);
            startRebindCorruptObj.SetActive(true);
            waitingForInputCorruptObj.SetActive(false);
            startRebindHorPosObj.SetActive(true);
            waitingForInputHorPosObj.SetActive(false);
            startRebindHorNegObj.SetActive(true);
            waitingForInputHorNegObj.SetActive(false);
            startRebindVertPosObj.SetActive(true);
            waitingForInputVertPosObj.SetActive(false);
            startRebindVertNegObj.SetActive(true);
            waitingForInputVertNegObj.SetActive(false);
            startRebindSprintObj.SetActive(true);
            waitingForInputSprintObj.SetActive(false);
            startRebindZoomObj.SetActive(true);
            waitingForInputZoomObj.SetActive(false);
            startRebindLedgObj.SetActive(true);
            waitingForInputLedgObj.SetActive(false);

            isCancelled = true;
        }                
    }


    private void Update()
    {
        if(isWaitingOnInput)
        {
            for (int i = 0; i < values.Length; i++)
            {
                //get the key that is pressed 
                keys[i] = Input.GetKeyDown((KeyCode)values[i]);

                //first check for these conditions, then if any other key was pressed for RebindComplete
                //if the player pressed escape, cancel the waiting for input || or if the player pressed the same key as the initial key, cancel
                if (keys[i] && ((KeyCode)values[i] == KeyCode.Escape || (KeyCode)values[i] == rebindingKey || (KeyCode)values[i] == KeyCode.Mouse0))
                {
                    Debug.Log("manual cancel");
                    CancelledRebind();
                    isWaitingOnInput = false;
                    keys[i] = false;
                    return;
                }

                //if the player pressed a key, call RebindComplete
                if (keys[i])
                {
                    Debug.Log("new input pressed");

                    Debug.Log("pressed "+((KeyCode) values[i]).ToString());

                    
                    RebindComplete();
                    isWaitingOnInput = false;
                    return;
                }                
            }            
        }
    }

    private void CheckIfNewKeyExistsAndSwap(KeyCode tempNewKey, KeyCode tempOldKey)
    {
        Debug.Log("Check if need to swap");
        //we want to check if the new key is already existing elsewhere in the playerbindings
        for (int i = 0; i < playerKeybindings.Length; i++)
        {
            //Debug.Log("playerbindings[" + i + "]: " + playerKeybindings[i].ToString()+"\nrebindingKey: " + rebindingKey.ToString() + "\nnewInteractKey: " + newInteractKey.ToString());
            if (playerKeybindings[i] == tempNewKey)
            {
                Debug.Log("yes swap");
                playerKeybindings[i] = tempOldKey;                
                //Debug.Log("new, playerbindings[" + i + "]: " + playerKeybindings[i].ToString());
                //don't forget to set it to the according variable too since we use it to display
                SetKeyByPlayerBindingIndex(i, tempOldKey); 
                return;
            }
        }
    }

    private void SetKeyByPlayerBindingIndex(int i, KeyCode newKeyValue)
    {
        if(i == 0)
        {
            newHorPositiveKey = newKeyValue;
        }
        else if(i == 1)
        {
            newHorNegativeKey = newKeyValue;
        }
        else if (i == 2)
        {
            newVertPositiveKey = newKeyValue;
        }
        else if (i == 3)
        {
            newVertNegativeKey = newKeyValue;
        }
        else if (i == 4)
        {
            newInteractKey = newKeyValue;
        }
        else if (i == 5)
        {
            newSprintKey = newKeyValue;
        }
        else if (i == 6)
        {
            newZoomKey = newKeyValue;
        }
        else if (i == 7)
        {
            newLedgerKey = newKeyValue;
        }
        else if (i == 8)
        {
            newCorruptionResetKey = newKeyValue;
        }
    }
}

