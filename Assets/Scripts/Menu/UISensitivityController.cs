using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UISensitivityController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI UINumberValue;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeSensitivity(Slider slider)
    {
        float tempNewValue = slider.value;
        //round to one decimal number
        decimal decimalValue = System.Math.Round((decimal)tempNewValue, 1);

        //convert back to float to send values
        float newValue = (float)decimalValue;

        //Debug.Log("new sensitivity value: "+ newValue);

        //update sensitivity value in the global manager  --> then updates the first person controller's sensitivity in the next scenes
        GlobalSManager.SetMouseLookSensitivity(newValue);

        //update the number on the menu UI 
        string newValueStr = newValue.ToString();
        UINumberValue.SetText(newValueStr);
    }
}
