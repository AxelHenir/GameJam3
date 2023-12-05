using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    /// <summary>
    /// Handles calling sounds to fade in and out for dialogue
    /// </summary>
    static GameObject DialogueBGSoundsObj;
    public static AudioSource dialogueAudioSource;
    public static AudioSource ambientVoidAudioSource;

    static float fadeInTime;
    float ambientMaxVolume;

    // Start is called before the first frame update
    void Awake()
    {        
        fadeInTime = 1.0f;

        DialogueBGSoundsObj = this.gameObject;//GameObject.Find("BGSoundsDialogue").gameObject;
        dialogueAudioSource = DialogueBGSoundsObj.GetComponent<AudioSource>();

        ambientVoidAudioSource = GameObject.Find("BGSoundsAmbientVoid").GetComponent<AudioSource>();

        ambientMaxVolume = ambientVoidAudioSource.volume;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FadeInClip(AudioClip clip)
    {
        StartCoroutine(FadeIn(clip));
    }

    public void FadeOutClip(AudioClip clip)
    {
        StartCoroutine(FadeOut(clip));
    }

    private IEnumerator FadeIn(AudioClip clip)
    {
        // lower the volume on the ambient sounds
        while (ambientVoidAudioSource.volume > 0.1f)
        {
            ambientVoidAudioSource.volume -= Time.deltaTime / fadeInTime;
            yield return null;
        }

        //fade in the dialogue clip
        dialogueAudioSource.clip = clip;
        dialogueAudioSource.volume = 0;
        dialogueAudioSource.Play();

        while (dialogueAudioSource.volume < 1)
        {
            dialogueAudioSource.volume += Time.deltaTime / fadeInTime;

            yield return null;
        }
    }

    private IEnumerator FadeOut(AudioClip clip)
    {
        if(dialogueAudioSource != null && dialogueAudioSource.isPlaying)
        {
            //fade out the dialogue clip
            dialogueAudioSource.clip = clip;
            dialogueAudioSource.volume = 1;

            while (dialogueAudioSource.volume > 0)
            {
                dialogueAudioSource.volume -= Time.deltaTime / fadeInTime;

                yield return null;
            }

            dialogueAudioSource.Stop();

            //increase the volume of the ambient clip back up
            while (ambientVoidAudioSource.volume < ambientMaxVolume)
            {
                ambientVoidAudioSource.volume += Time.deltaTime / fadeInTime;
                yield return null;
            }
        }
    }
}
