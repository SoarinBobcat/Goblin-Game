using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MenuManager : MonoBehaviour
{
    public int menuNumber = 0;
    public bool subMenu = false;

    public bool pressed = false;

    public Image Start;
    public Image Info;
    public Image Quit;
    public Image Close;

    public GameObject panel;

    public void CloseGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("DebugRoom");
    }

    public void NewSelection(GameObject btn)
    {
        EventSystem.current.SetSelectedGameObject(btn);
    }
}


