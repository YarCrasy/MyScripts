using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager scoreInstance;
    [SerializeField] TextMeshProUGUI winLoseDisplay, scoreDisplay;

    const string WIN_MSG = "YOU WIN!!!", LOSE_MSG = "YOU LOSE...";
    bool win = false;

    const int WIN_BONUS = 500;
    float score = 1000;

    private void Awake()
    {
        scoreInstance = this;
    }

    private void Start()
    {
        score = MazeGenerator.instance.maxMazeSize * 1000;
    }

    private void Update()
    {
        ScoreTimer();
    }

    void ScoreTimer()
    {
        if (score > 0)
        {
            score -= Time.deltaTime;
        }
    }

    public void SetWin(bool set)
    {
        win = set;
        if (win)
        {
            score += WIN_BONUS;
            winLoseDisplay.text = WIN_MSG;
        }
        else
        {
            score = 0;
            winLoseDisplay.text = LOSE_MSG;
        }
        scoreDisplay.text = "Score: " + (int)score;
    }

}
