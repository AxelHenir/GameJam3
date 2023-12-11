using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgerPopupController : MonoBehaviour
{
    [SerializeField] GameObject LedgerAreYouSurePopup;

    private AudioHandlerMech audioHandler;
    // Start is called before the first frame update
    void Start()
    {        
        audioHandler = GameObject.Find("AudioHandler").GetComponent<AudioHandlerMech>(); //assumes we have the AudioHandlerMech on an object with this name
        //popup begins closed
        LedgerAreYouSurePopup.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowPopup()
    {
        AudioHandlerMech.Instance.PlaySound("ui_button_simple_click_04");
        LedgerAreYouSurePopup.SetActive(true);
    }

    public void HidePopup()
    {
        AudioHandlerMech.Instance.PlaySound("ui_button_simple_click_03");
        LedgerAreYouSurePopup.SetActive(false);
    }
}
