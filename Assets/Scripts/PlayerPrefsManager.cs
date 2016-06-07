using UnityEngine;
using System.Collections;

public class PlayerPrefsManager : MonoBehaviour {

	const string MASTER_VOLUME_KEY = "master_volume";
    const string MASTER_SOUND_VOLUME_KEY = "master_sound_volume";
    const string DIFFICULTY_KEY = "difficulty";
	const string LEVEL_KEY = "level_unlocked_";

	public static void SetMasterVolume (float volume) {
		if (volume >= 0f && volume <= 1f) {
			PlayerPrefs.SetFloat (MASTER_VOLUME_KEY, volume);
		} else {
			Debug.LogError ("Master volume out of range");
		}
	}

	public static float GetMasterVolume () {
		return PlayerPrefs.GetFloat (MASTER_VOLUME_KEY);
	}

    public static void SetMasterSoundVolume(float volume)
    {
        if (volume >= 0f && volume <= 1f)
        {
            PlayerPrefs.SetFloat(MASTER_SOUND_VOLUME_KEY, volume);
        }
        else
        {
            Debug.LogError("Master sound volume out of range");
        }
    }

    public static float GetMasterSoundVolume()
    {
        return PlayerPrefs.GetFloat(MASTER_SOUND_VOLUME_KEY);
    }

    public static void UnlockLevel (int level) {
		if (level <= Application.levelCount - 1) {
			PlayerPrefs.SetInt (LEVEL_KEY + level.ToString(), 1);
		} else {
			Debug.LogError ("Trying to level not in build order");
		}
	}

	public static bool IsLevelUnlocked (int level) {
		int levelValue = PlayerPrefs.GetInt (LEVEL_KEY + level.ToString());
		bool isLevelUnlocked = (levelValue == 1);

		if (level <= Application.levelCount - 1) {
			return isLevelUnlocked;
		} else {
			Debug.LogError ("Level is not in build order");
			return false;
		}
	}

	public static void SetDifficulty (float difficulty) {
		if (difficulty >= 1f && difficulty <= 3f) {
			PlayerPrefs.SetFloat (DIFFICULTY_KEY, difficulty);
		} else {
			Debug.LogError ("Difficulty is out of range");
		}
	}

	public static float GetDifficulty () {
		return PlayerPrefs.GetFloat (DIFFICULTY_KEY);
	}
}
