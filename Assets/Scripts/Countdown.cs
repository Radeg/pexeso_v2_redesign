using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour {

    public Image foregroundImage;
    public float maxTime;
    public LevelManager levelManager;

    public bool isCountingDown;

    void Start ()
    {
        foregroundImage = GetComponent<Image>();
        isCountingDown = true;
    }

    void Update ()
    {
        if (isCountingDown)
        {
            foregroundImage.fillAmount -= 1.0f / maxTime * Time.deltaTime;

            if (foregroundImage.fillAmount <= 0.0f)
            {
                levelManager.LoadLevel("02b Lose Screen");
            }
        }
    }
}
