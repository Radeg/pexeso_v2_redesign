using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public float autoLoadNextLevelAfter;
    public GameObject modalWindow;
    public GameObject spawnGrid;

    public bool isPaused;

    void Start()
    {
        if (autoLoadNextLevelAfter <= 0)
        {
            print("Level auto load disabled");
        }
        else Invoke("LoadNextLevel", autoLoadNextLevelAfter);
        isPaused = false;
    }

    public void LoadLevel(string name)
    {
        Time.timeScale = 1.0f;
        Application.LoadLevel(name);
    }

    public void QuitRequest()
    {
        Application.Quit();
    }

    public void LoadNextLevel()
    {
        Application.LoadLevel(Application.loadedLevel + 1);
    }

    public void PauseGame()
    {
        if (!isPaused)
        {
            Time.timeScale = 0.0f;
            isPaused = true;
            modalWindow.SetActive(true);
            CardController.isClickable = false;
            spawnGrid.transform.Translate(0, 0, 90f);
        }  else
        {
            Time.timeScale = 1.0f;
            isPaused = false;
            modalWindow.SetActive(false);
            CardController.isClickable = true;
            spawnGrid.transform.Translate(0, 0, -90f);
        }
    }

    public void UnPauseGame()
    {
        Time.timeScale = 1.0f;
        isPaused = false;
        modalWindow.SetActive(false);
        CardController.isClickable = true;
        spawnGrid.transform.Translate(0, 0, -90f);
    }
}
