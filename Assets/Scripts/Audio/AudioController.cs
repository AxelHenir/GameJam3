using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    static GameObject DialogueBGSoundsObj;
    public static AudioSource dialogueAudioSource;

    static float fadeInTime;


    // Start is called before the first frame update
    void Awake()
    {
        fadeInTime = 1.0f;

        DialogueBGSoundsObj = this.gameObject;//GameObject.Find("BGSoundsDialogue").gameObject;
        dialogueAudioSource = DialogueBGSoundsObj.GetComponent<AudioSource>();
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
            dialogueAudioSource.clip = clip;
            dialogueAudioSource.volume = 1;

            while (dialogueAudioSource.volume > 0)
            {
                dialogueAudioSource.volume -= Time.deltaTime / fadeInTime;

                yield return null;
            }

            dialogueAudioSource.Stop();
        }        
    }
}
