using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static EnergyFuse;

public class GoalDisplay : MonoBehaviour
{
    const string REMAINING_MSG = "Remaing Fuse: ", EXIT_MSG = "Find the exit";
    public static GoalDisplay Instance;

    TextMeshProUGUI txt;

    private void Awake()
    {
        Instance = this;
        txt = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        UpdateDisplay();
    }

    public void UpdateDisplay()
    {
        if (activatedFuse < maxFuseInScene) txt.text = REMAINING_MSG + activatedFuse + "/" + maxFuseInScene;
        else txt.text = EXIT_MSG;
    }


}
