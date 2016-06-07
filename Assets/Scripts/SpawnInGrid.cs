using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using MadLevelManager;

public class SpawnInGrid : MonoBehaviour
{
    public List<GameObject> prefabs;
    public int width = 10;
    public int height = 10;
    public float offset = 0;
    public float firstCardX = -4.6f;
    public float firstCardY = -1.9f;
    public LevelManager levelManager;
    public Countdown countdown;
    public int numberOfMoves;
    public Text textMoves;
    public Text textEndScore;
    public Text textScore;
    public Animator endScoreAnim;

    private GameObject[,] gridd = new GameObject[30, 30];
    private Renderer[] allCards;

    void Start()
    {
        textMoves.text = "Moves: " + numberOfMoves.ToString();
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                int index = Random.Range(0, prefabs.Count);

                prefabs[index].transform.position = new Vector2(firstCardX, firstCardY);
                GameObject gridprefab = (GameObject)Instantiate(prefabs[index]);
                gridprefab.transform.position = new Vector2(gridprefab.transform.position.x + x * offset,
                                                           gridprefab.transform.position.y + y * offset);
                gridprefab.transform.parent = transform;
                gridd[x, y] = gridprefab;
                prefabs.RemoveAt(index);
            }
        }
    }

    void Update()
    {
        allCards = gameObject.GetComponentsInChildren<Renderer>();
        
        float score = (numberOfMoves + 1) * Mathf.Round(countdown.foregroundImage.fillAmount * 1000);

        textScore.text = "Score: " + score;
        StartCoroutine(NumberOfMoves());
        if (allCards.Length == 0)
        {
            float prevScore = MadLevelProfile.GetLevelFloat(MadLevel.currentLevelName, "end_score");

            if (score > 2000)
            {
                if (score > prevScore)
                {
                    MadLevelProfile.SetLevelFloat(MadLevel.currentLevelName, "end_score", score);
                }

                MadLevelProfile.SetLevelBoolean(MadLevel.currentLevelName, "star_1", true);
                MadLevelProfile.SetLevelBoolean(MadLevel.currentLevelName, "star_2", true);
                MadLevelProfile.SetLevelBoolean(MadLevel.currentLevelName, "star_3", true);

                MadLevelProfile.SetCompleted(MadLevel.currentLevelName, true);
            } else if (score > 1000 && score <= 2000) {
                if (score > prevScore)
                {
                    MadLevelProfile.SetLevelFloat(MadLevel.currentLevelName, "end_score", score);
                }

                MadLevelProfile.SetLevelBoolean(MadLevel.currentLevelName, "star_1", true);
                MadLevelProfile.SetLevelBoolean(MadLevel.currentLevelName, "star_2", true);

                MadLevelProfile.SetCompleted(MadLevel.currentLevelName, true);
            } else if (score <= 1000)
            {
                if (score > prevScore)
                {
                    MadLevelProfile.SetLevelFloat(MadLevel.currentLevelName, "end_score", score);
                }

                MadLevelProfile.SetLevelBoolean(MadLevel.currentLevelName, "star_1", true);

                MadLevelProfile.SetCompleted(MadLevel.currentLevelName, true);
            }
            StartCoroutine(EndScore(score));
         }
    }

    IEnumerator NumberOfMoves()
    {
        int activeCount = GameObject.FindGameObjectsWithTag("Active").Length;

        if (Input.GetMouseButtonDown(0) && activeCount == 2)
        {
            numberOfMoves--;
            textMoves.text = "Moves: " + numberOfMoves.ToString();
            if (numberOfMoves == 0 && (allCards.Length-2) > 0)
            {
                yield return new WaitForSeconds(0.7f);
                levelManager.LoadLevel("02b Lose Screen");
            }
        }
    }

    IEnumerator EndScore(float endScore)
    {
        countdown.isCountingDown = false;
        endScoreAnim.Play("EndScore");
        textEndScore.text = endScore.ToString() + " points";
        yield return new WaitForSeconds(1.5f);
        MadLevel.LoadLevelByName("Level Select");
    }
}