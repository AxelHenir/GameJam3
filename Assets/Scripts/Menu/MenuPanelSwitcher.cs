using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPanelSwitcher : MonoBehaviour
{
    public GameObject mainMenuScreen;
    public GameObject CreditsScreen;
    public GameObject SettingsScreen;

    List<GameObject> menuScreens;
    private string currentScreenName;
    private GameObject currentScreen;


    //[SerializeField] private AudioHandlerMech menuAudioHandler; // Reference to the AudioHandler script

    [SerializeField] AudioSource backgroundMusicAS;

    // Start is called before the first frame update
    void Start()
    {
        menuScreens = new List<GameObject>();

        menuScreens.Add(mainMenuScreen);
        menuScreens.Add(CreditsScreen);
        menuScreens.Add(SettingsScreen);

        string lastSceneName = GlobalSManager.GetLastScene();

        if (lastSceneName.Equals("Gameplay"))
        {

        }
        else if (lastSceneName.Equals("") || lastSceneName.Equals("Ending"))
        {
            LoadScreen("MainMenuPanel");

        }

        Cursor.lockState = CursorLockMode.None;

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void LoadScreen(string panelName)
    {
        Debug.Log("newScreen: " + panelName);
        string previousScreen = currentScreenName;
        Debug.Log("prevScreen: " + previousScreen);

        if (currentScreen)
        {
            currentScreen.SetActive(false);
        }


        foreach (GameObject screen in menuScreens)
        {
            if (screen.name.Equals(panelName))
            {
                currentScreenName = screen.name;
                currentScreen = screen;
            }
        }

        if (panelName.Equals("IntroPanel"))
        {
            backgroundMusicAS.Stop(); //stop background music if going to intro panel
            GlobalSManager.SetLastScene("Menu");
        }
        else if (panelName.Equals("DiaryPanel"))
        {
            GlobalSManager.SetLastScene("IntroScreen");
            if (backgroundMusicAS.isPlaying)
            {
                backgroundMusicAS.Stop();
            }
        }
        else if (panelName.Contains("OutroPanel"))
        {
            backgroundMusicAS.Stop();
            GlobalSManager.SetLastScene("Gameplay");
        }
        else if (panelName.Equals("CreditsPanel"))
        {
            //backgroundMusicAS.Stop();
            GlobalSManager.SetLastScene("Credits");
        }
        else
        {
            GlobalSManager.SetLastScene("MainMenu"); //change to menu?
        }


        currentScreen.SetActive(true);
    }
}
