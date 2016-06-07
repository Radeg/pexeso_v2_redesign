using UnityEngine;
using System.Collections;

public class SetStartVolume : MonoBehaviour {

	private MusicManager musicManager;
    private SoundManager soundManager;

	void Start () {
		musicManager = GameObject.FindObjectOfType<MusicManager> ();
		float volume = PlayerPrefsManager.GetMasterVolume ();
		musicManager.ChangeVolume (volume);

        soundManager = GameObject.FindObjectOfType<SoundManager>();
        float volumeSound = PlayerPrefsManager.GetMasterSoundVolume();
        soundManager.ChangeSoundVolume(volumeSound);
    }
}