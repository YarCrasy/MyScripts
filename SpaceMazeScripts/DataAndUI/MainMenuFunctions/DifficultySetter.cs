using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static GameSettingData;

public class DifficultySetter : MonoBehaviour
{
    readonly string[] difficulty = { "EASY", "NORMAL", "HARD"};
    [SerializeField] TextMeshProUGUI mazeSizeDisplay, difficultyDisplay;
    [SerializeField] Slider setter;

    private void Awake()
    {
        instance.Load();
        setter.minValue = MIN_MAZE_SIZE;
        setter.maxValue = MAX_MAZE_SIZE;
        setter.value = instance.maxMazeSize;

        SetiDifficultyDisplay(instance.maxMazeSize);
        mazeSizeDisplay.text = instance.maxMazeSize + "X" + instance.maxMazeSize;

    }

    public void SetDifficulty(float set)
    {
        mazeSizeDisplay.text = set + "X" + set;

        SetiDifficultyDisplay((int)set);

        instance.Load();
        instance.SetMaxMazeSize((int)set);
    }

    void SetiDifficultyDisplay(int set)
    {
        if (set >= 5 && set <= 10) difficultyDisplay.text = difficulty[0];
        else if (set > 10 && set <= 15) difficultyDisplay.text = difficulty[1];
        else if (set > 15 && set <= 20) difficultyDisplay.text = difficulty[2];
        else difficultyDisplay.text =
                "if you are seeing this:" +
                "\nCONGRATS, you found an easteregg and a logically impossible bug." +
                "\nIt doesn't affect the game, so... keep enjoying";
    }
}
