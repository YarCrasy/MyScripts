using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MenuType { None = -1, Pause, Settings, Quit, Credits, GameOver}

public class MenuController : MonoBehaviour
{
    public static MenuController MenuCtrlInstance;

    [SerializeField] SceneController sceneCtrl;

    [Tooltip("Pause, Settings, Quit, Credits, GameOver")]
    [SerializeField] GameObject[] menus;

    MenuType[] enabledMenus = new MenuType[3];
    bool menuEnabled = false;
    int enabledMenuIndex = 0;

    private void Awake()
    {
        MenuCtrlInstance = this;
    }

    private void Start()
    {
        if (sceneCtrl == null) sceneCtrl = SceneController.instance;
    }

    private void Update()
    {
        EscInput();
    }

    void EscInput()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (sceneCtrl.actualScene == SceneName.MainMenu)
            {
                if (menuEnabled)
                {
                    DisableLastestMenu();
                }
                else
                {
                    SwitchMenuEnabled(MenuType.Quit);
                }
            }
            else if(enabledMenus[enabledMenuIndex] != MenuType.GameOver)
            {
                if (menuEnabled)
                {
                    DisableLastestMenu();
                    if (!menuEnabled)
                    {
                        SetCursorLock(true);
                        SetTimeScalePause(false);
                    }
                }
                else
                {
                    SwitchMenuEnabled(MenuType.Pause);
                    SetCursorLock(false);
                    SetTimeScalePause(true);
                    menuEnabled = true;
                }
            }
        }
    }

    public void EnableMenu(int menu)
    {
        menuEnabled = true;
        menus[menu].SetActive(true);
        enabledMenus[enabledMenuIndex] = (MenuType)menu;
        enabledMenuIndex++;
    }

    public void EnableMenu(MenuType menu)
    {
        EnableMenu((int)menu);
    }

    public void DisableMenu(int menu)
    {
        if(enabledMenuIndex > 0)
        {
            menus[menu].SetActive(false);
            enabledMenuIndex--;
            if (enabledMenuIndex == 0)
            {
                menuEnabled = false;
            }
        }
    }

    void DisableMenu(MenuType menu)
    {
        DisableMenu((int)menu);
    }

    void DisableLastestMenu()
    {
        DisableMenu(enabledMenus[enabledMenuIndex-1]);
    }

    void SwitchMenuEnabled(MenuType menu)
    {
        if (!menus[(int)menu].activeSelf)
        {
            EnableMenu(menu);
        }
        else
        {
            DisableMenu(menu);
        }
    }

    public void SetCursorLock(bool set)
    {
        if (set) Cursor.lockState = CursorLockMode.Locked;
        else Cursor.lockState = CursorLockMode.None;
    }

    public void SetTimeScalePause(bool set)
    {
        if (set) Time.timeScale = 0;
        else Time.timeScale = 1;
    }

    public bool IsMenuEnabled()
    {
        return menuEnabled;
    }
}
