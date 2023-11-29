using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    void Update()
    {
        Start.color = Color.white;
        Info.color = Color.white;
        Quit.color = Color.white;
        Close.color = Color.white;

        var input = Input.GetAxisRaw("Vertical");

        if (!pressed)
        {
            if (input > 0)
            {
                menuNumber -= 1;
            }
            else if (input < 0)
            {
                menuNumber += 1;
            }
        }

        //Prevent holding down button
        if (input != 0) { pressed = true; } else { pressed = false; }

        //Rotate menu when OOB
        if (menuNumber < 0)
        {
            menuNumber = 2;
        }
        else if (menuNumber > 2)
        {
            menuNumber = 0;
        }

        //Selects current menu option
        if (Input.GetButtonDown("Jump") || Input.GetButtonDown("Submit"))
        {
            if (!subMenu)
            {
                switch (menuNumber)
                {
                    case 0:
                        StartGame();
                        break;
                    case 1:
                        ChangeMenu();
                        panel.SetActive(true);
                        break;
                    case 2:
                        CloseGame();
                        break;
                }
            }
            else
            {
                ChangeMenu();
                panel.SetActive(false);
            }
        }

        //Funny Colours
        if (!subMenu)
        {
            switch (menuNumber)
            {
                case 0:
                    Start.color = Color.cyan;
                    break;
                case 1:
                    Info.color = Color.cyan;
                    break;
                case 2:
                    Quit.color = Color.cyan;
                    break;
            }
        }
        else
        {
            Close.color = Color.cyan;
        }
    }

    public void CloseGame() {
        Application.Quit();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("DebugRoom");
    }

    public void ChangeNum(int num)
    {
        menuNumber = num;
    }

    public void ChangeMenu()
    {
        subMenu = !subMenu;
    }
}


