using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DescriptionUIController : MonoBehaviour
{
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;

    // Called when a name is clicked
    public void OnNameClick(string name)
    {
        // Update the description text with the clicked name's description
        descriptionText.text = "Description for " + name;
    }
}
