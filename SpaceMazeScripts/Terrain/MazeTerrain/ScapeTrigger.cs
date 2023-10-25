using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MenuController;

public class ScapeTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            ScoreManager.scoreInstance.SetWin(true);
            MenuCtrlInstance.EnableMenu(MenuType.GameOver);
            MenuCtrlInstance.SetCursorLock(false);
            MenuCtrlInstance.SetTimeScalePause(true);
        }
    }

    
}
