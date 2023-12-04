using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private string currentScene;
    [SerializeField] MenuPanelSwitcher panelSwitcher;

    [SerializeField] Image fadeImage;
    float fadeDuration = 7f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadScreen(string screenName)
    {
        GlobalSManager.SetLastScene("screenName");
        Debug.Log("Clicked " + screenName);
        panelSwitcher.LoadScreen(screenName);
    }

    public void LoadScene(string sceneName)
    {
        GlobalSManager.SetLastScene("Menu");
        Debug.Log("Clicked " + sceneName);

        if(sceneName.Equals("Gameplay"))
        {
            StartCoroutine(FadeImage());
        }
        else
        {
            SceneManager.LoadScene(sceneName);
        }
        

    }

    public static void QuitGame()
    {
        // save any game data here
#if UNITY_EDITOR
        // Application.Quit() does not work in the editor so
        // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }


    IEnumerator FadeImage()
    {
        //wait before fading
        yield return new WaitForSecondsRealtime(0.5f);

        // loop over seconds
        for (float i = 0; i <= fadeDuration; i += Time.deltaTime)
        {
            //Debug.Log("color: "+i);
            // set color with i as alpha
            fadeImage.color = new Color(0, 0, 0, i);

            if(i > 4f)
            {
                break;
            }

            yield return null;
        }

        //load game after fade is done
        SceneManager.LoadScene("Gameplay");
    }
}
