using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Audio;
using static GameSettingData;

public class GamePlayerSettings : MonoBehaviour
{
    [SerializeField] TMP_Dropdown fpsSelect;

    [SerializeField] TMP_InputField camSensDisplay,mainVolDisplay;
    [SerializeField] Slider camSenSlider, mainVolSlider;

    [SerializeField] AudioMixer audMix;

    private void OnEnable()
    {
        instance.Load();
        QualitySettings.vSyncCount = 0;

        fpsSelect.value = instance.actualFPS;

        camSenSlider.minValue = MIN_CAM_SENS;
        camSenSlider.maxValue = MAX_CAM_SENS;
        camSenSlider.value = instance.camSensitivity;
        camSensDisplay.text = camSenSlider.value + "";

        mainVolSlider.minValue = MIN_VOLUME;
        mainVolSlider.maxValue = MAX_VOLUME;
        mainVolSlider.value = instance.mainVolume;
        mainVolDisplay.text = mainVolSlider.value+80 + "";
    }

    public void SetFrameRate(int set)
    {
        instance.SetFrameRate(set);
        fpsSelect.value = set;
    }

    public void SetCamSensitivity()
    {
        instance.SetCamSensitivity(camSenSlider.value);
        camSensDisplay.text = camSenSlider.value + "";
    }

    public void SetCamSensitivity(string set)
    {
        int aux = int.Parse(set);
        instance.SetCamSensitivity(aux);
        camSenSlider.value = aux;
    }

    public void SetMainVolume()
    {
        instance.SetMainVolume(mainVolSlider.value);
        mainVolDisplay.text = (mainVolSlider.value+80) + "";
        audMix.SetFloat("Master", mainVolSlider.value);
    }

    public void SetMainVolume(string set)
    {
        int aux = int.Parse(set);
        aux -= 80;
        instance.SetMainVolume(aux);
        mainVolSlider.value = aux;
    }

}
