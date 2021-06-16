using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectVariation : MonoBehaviour
{
    public AudioClip[] clipArray;
    [SerializeField] private AudioSource effectSource;
    private int clipIndex;
    private float pitchMin = 0.95f;
    private float pitchMax = 1.05f;
    [SerializeField] private float volumeMin = 0.5f;
    [SerializeField] private float volumeMax = 0.8f;

    private void Awake()
    {
        if( effectSource == null)
        {
            effectSource = GetComponent<AudioSource>();
        }
       
        if (effectSource.playOnAwake == true)
        {
            PlayRandom();
        }
        
    }

    void PlayRoundRobin()
    {
        effectSource.pitch = Random.Range(pitchMin, pitchMax);
        effectSource.volume = Random.Range(volumeMin, volumeMax);

        if (clipIndex < clipArray.Length)
        {
            effectSource.PlayOneShot(clipArray[clipIndex]);
            clipIndex++;
        }

        else
        {
            clipIndex = 0;
            effectSource.PlayOneShot(clipArray[clipIndex]);
            clipIndex++;
        }
    }
    public void PlayRandom()
    {
        effectSource.pitch = Random.Range(pitchMin, pitchMax);
        effectSource.volume = Random.Range(volumeMin, volumeMax);

        clipIndex = RepeatCheck(clipIndex, clipArray.Length);
        effectSource.PlayOneShot(clipArray[clipIndex]);
    }

    int RepeatCheck(int previousIndex, int range)
    {
        int index = Random.Range(0, range);

        while (index == previousIndex)
        {
            index = Random.Range(0, range);
        }
        return index;
    }

}
