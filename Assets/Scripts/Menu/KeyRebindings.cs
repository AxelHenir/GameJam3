using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeyRebindings : MonoBehaviour
{

    //variable for input reference
    //[SerializeField] private FirstPersonController playerController;
    [SerializeField] private TextMeshProUGUI bindingDisplayNameText;
    [SerializeField] private GameObject startRebindObject;
    [SerializeField] private GameObject waitingForInputObject;

    //private InputActionRebindingExtensions.RebindingOperation rebindingOperation;
    private KeyCode rebindingKey;
    private string KeyType;
    private bool isWaitingOnInput;
    private int[] values;
    private bool[] keys;
    private bool isRebound;
    private bool isCancelled;


    //--------------------    

    private string horAxisName = "Horizontal";
    private KeyCode newHorPositiveKey = KeyCode.D;
    private KeyCode newHorNegativeKey = KeyCode.A;

    private string vertAxisName = "Vertical";
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
        startRebindObject.SetActive(false);
        waitingForInputObject.SetActive(true);

        isRebound = false;
        isCancelled = false;


        //check which key is being rebound
        if (keyType == "Interact")
        {
            KeyType = "Interact";
            rebindingKey = newInteractKey;
            Debug.Log("initial key: "+newInteractKey.ToString());
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

            for (int i = 0; i < values.Length; i++)
            {
                //if the key is true and the key is not the same as the current key
                if (keys[i] && rebindingKey != (KeyCode)values[i])
                {
                    rebindingKey = (KeyCode)values[i];

                    Debug.Log("new input: " + rebindingKey.ToString());
                }
                //reset the keys array to all false again
                keys[i] = false;
            }

            if (KeyType == "Interact")
            {
                newInteractKey = rebindingKey;
                playerKeybindings[4] = newInteractKey;
                Debug.Log("new interact key: " + newInteractKey.ToString());
            }

            //send the new key to GlobalManager
            // do InputManager.SetAxis(axisName, newPositiveKey, newNegativeKey);
            //set keybinding
            GlobalSManager.SetKeyBindings(playerKeybindings);
            //InputManager.SetAxis(axisName, newPositiveKey, newNegativeKey);

            //display new key
            bindingDisplayNameText.text = rebindingKey.ToString();

            startRebindObject.SetActive(true);
            waitingForInputObject.SetActive(false);

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
            startRebindObject.SetActive(true);
            waitingForInputObject.SetActive(false);

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
                if (keys[i] && ((KeyCode)values[i] == KeyCode.Escape || (KeyCode)values[i] == rebindingKey))
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
}

