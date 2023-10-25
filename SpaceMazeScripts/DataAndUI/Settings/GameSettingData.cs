using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public enum PresetFrameRate { minimum24, low30, low_medium45, medium60, high120, veryHigh144, tooHigh240, infinite }

public class GameSettingData
{
    public static GameSettingData instance = new();

    //GRAPHICS
    readonly int[] fps = { 24, 30, 45, 60, 120, 144, 240, -1 };
    public int actualFPS = (int)PresetFrameRate.medium60;

    //CAMERA
    public const int MIN_CAM_SENS = 1, MAX_CAM_SENS = 1000;
    [Range(MIN_CAM_SENS, MAX_CAM_SENS)]public float camSensitivity = 200;

    //VOLUME
    public const float MIN_VOLUME = -80, MAX_VOLUME = 20;
    public float mainVolume = 0;

    //MAZE SIZE
    public const int MIN_MAZE_SIZE = 5, MAX_MAZE_SIZE = 20;
    [Range(MIN_MAZE_SIZE, MIN_MAZE_SIZE)]public int maxMazeSize = 5;

    readonly string fileName = Application.dataPath + "/gameSettings.json";

    string dataString;

    public void Load()
    {
        if (File.Exists(fileName))
        {
            dataString = File.ReadAllText(fileName);
            instance = JsonUtility.FromJson<GameSettingData>(dataString);
            instance.SetFrameRate(actualFPS);
            instance.SetCamSensitivity(camSensitivity);
            instance.SetMainVolume(mainVolume);
            instance.SetMaxMazeSize(maxMazeSize);
        }
        else
        {
            instance.Save();
        }
    }

    public  void Save()
    {
        string jsonData = JsonUtility.ToJson(instance);
        File.WriteAllText(fileName, jsonData);
    }

    public void SetMainVolume(float value)
    {
        mainVolume = value;
        Save();
    }

    public void SetCamSensitivity(float value)
    {
        camSensitivity = value;
        Save();
    }

    public void SetFrameRate(int set)
    {
        Application.targetFrameRate = fps[set];
        actualFPS = set;
        Save();
    }

    public void SetMaxMazeSize(int set)
    {
        maxMazeSize = set;
        Save();
    }

}
