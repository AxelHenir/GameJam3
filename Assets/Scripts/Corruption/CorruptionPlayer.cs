using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorruptionPlayer : MonoBehaviour
{
    bool isPlayerCorrupted;

    public static AudioSource corruptionAudioSource;
    static float fadeInTime;

    // Start is called before the first frame update
    void Start()
    {
        fadeInTime = 1.0f;

        corruptionAudioSource = this.gameObject.GetComponent<AudioSource>();
        isPlayerCorrupted = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool GetIsPlayerCorrupted()
    {
        return isPlayerCorrupted;
    }

    public void SetPlayerCorrupted(bool iscorrupted)
    {
        isPlayerCorrupted = iscorrupted;

        if(isPlayerCorrupted) //if the player IS corrupted, play the music
        {
            FadeInClip();
        }
        else //if the player IS NOT corrupted, stop the music
        {
            FadeOutClip();
        }
    }


    public void FadeInClip()
    {
        StartCoroutine(FadeIn());
    }

    public void FadeOutClip()
    {
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeIn()
    {
        if (corruptionAudioSource != null && !corruptionAudioSource.isPlaying)
        {
            corruptionAudioSource.volume = 0;
            corruptionAudioSource.Play();

            while (corruptionAudioSource.volume < 1)
            {
                corruptionAudioSource.volume += Time.deltaTime / fadeInTime;

                yield return null;
            }
        }
    }

    private IEnumerator FadeOut()
    {
        if (corruptionAudioSource != null && corruptionAudioSource.isPlaying)
        {
            corruptionAudioSource.volume = 1;

            while (corruptionAudioSource.volume > 0)
            {
                corruptionAudioSource.volume -= Time.deltaTime / fadeInTime;

                yield return null;
            }

            corruptionAudioSource.Stop();
        }
    }
}
