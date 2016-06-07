using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

    public AudioClip[] soundChangeArray;

    private static AudioSource audioSource;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound (int soundChangeArrayIndex)
    {
        AudioClip specificSound = soundChangeArray[soundChangeArrayIndex];

        audioSource.clip = specificSound;
        audioSource.Play();
    }

    public void ChangeSoundVolume(float volumeValue)
    {
        audioSource.volume = volumeValue;
    }
}
