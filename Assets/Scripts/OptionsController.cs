using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OptionsController : MonoBehaviour {

	public Slider volumeSlider, soundSlider;
	//public LevelManager levelManager;

	private MusicManager musicManager;
    private SoundManager soundManager;

	void Start () {
		musicManager = GameObject.FindObjectOfType<MusicManager> ();
        soundManager = GameObject.FindObjectOfType<SoundManager>();

        volumeSlider.value = PlayerPrefsManager.GetMasterVolume ();
        soundSlider.value = PlayerPrefsManager.GetMasterSoundVolume();
        //diffSlider.value = PlayerPrefsManager.GetDifficulty ();
    }
	
	void Update () {
		musicManager.ChangeVolume (volumeSlider.value);
        soundManager.ChangeSoundVolume(soundSlider.value);
    }

	public void SaveAndExit () {
		PlayerPrefsManager.SetMasterVolume (volumeSlider.value);
        PlayerPrefsManager.SetMasterSoundVolume(soundSlider.value);
        //PlayerPrefsManager.SetDifficulty (diffSlider.value);

        //levelManager.LoadLevel ("01a Start Menu");
	}

	public void SetDefaults () {
		volumeSlider.value = 0.8f;
        soundSlider.value = 0.8f;
		//diffSlider.value = 3f;
	}
}
