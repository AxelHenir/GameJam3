using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHandlerMech : MonoBehaviour
{
    /// <summary>
    /// Handles the calling of sounds at specific moments for mechanics
    /// </summary>
    public static AudioHandlerMech Instance { get; private set; }


    [SerializeField] private List<AudioSource> audioSources = new List<AudioSource>();
    private static Dictionary<string, AudioSource> audioSourceDictionary = new Dictionary<string, AudioSource>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            // DontDestroyOnLoad(gameObject);
            InitializeAudioSourcesDynamically();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private static void InitializeAudioSourcesDynamically()
    {
        // Get child objects of the AudioClips GameObject and add their AudioSource components to audioSources list
        Transform audioClipsTransform = GameObject.Find("AudioClips").transform; // Assuming "AudioClips" is the name of your GameObject
        int childCount = audioClipsTransform.childCount;

        for (int i = 0; i < childCount; i++)
        {
            Transform child = audioClipsTransform.GetChild(i);
            AudioSource audioSource = child.GetComponent<AudioSource>();

            if (audioSource != null )
            {
                //audioSources.Add( audioSource );
                audioSourceDictionary.Add(child.name, audioSource); // Add to dictionary for easy access
            }
            else
            {
                Debug.LogWarning("AudioSource component not found in child object: " + child.name);
            }
        }
    }

    public void PlaySound(string sourceName)
    {
        if (audioSourceDictionary.ContainsKey(sourceName))
        {
            audioSourceDictionary[sourceName].Play();
        }
        else
        {
            Debug.LogWarning("Audio source not found: " + sourceName);
        }
    }
}
