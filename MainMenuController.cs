using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{

    public TextMeshProUGUI[] selection;
    public GameObject areYouSure;

    public int cursor;

    bool startingNewGame;
    bool cursorBreak;

    // Start is called before the first frame update
    void Start()
    {
        cursor = 2;
        cursorBreak = false;
        SetUI();
    }

    // Update is called once per frame
    void Update()
    {
        if(!startingNewGame)
        {
            MoveCursor(Input.GetAxisRaw("Vertical"));

            if (Input.GetKeyDown("joystick button 0"))
            {
                switch (cursor)
                {
                    case 2:
                        SceneManager.LoadScene("Town");
                        break;
                    case 1:
                        areYouSure.SetActive(true);
                        startingNewGame = true;
                        break;
                    case 0:
                        Application.Quit();
                        break;
                }
            }
        }
        else
        {
            if(Input.GetKeyDown("joystick button 0"))
            {
                StartNewGame();
            }
            else if(Input.GetKeyDown("joystick button 1"))
            {
                startingNewGame = false;
                areYouSure.SetActive(false);
            }
        }
        
    }


    void MoveCursor(float movement)
    {
        if (movement != 0)
        {
            if (!cursorBreak)
            {
                if (movement > 0)
                {
                    cursor++;
                }
                else
                {
                    cursor += 5;
                }

                //Either 0,1,2
                cursor %= 3;
                SetUI();
                cursorBreak = true;
            }
        }
        else
        {
            cursorBreak = false;
        }
    }

    void SetUI()
    {
        foreach(TextMeshProUGUI text in selection)
        {
            text.color = new Color(255,255,255,0.5f);
        }

        selection[cursor].color = new Color(255, 255, 255, 1);
    }

    void StartNewGame()
    {
        PlayerPrefs.SetInt("maxHealth", 10);
        PlayerPrefs.SetInt("maxMana", 5);
        PlayerPrefs.SetInt("health", 10);
        PlayerPrefs.SetInt("mana", 5);
        PlayerPrefs.SetInt("strength", 0);
        PlayerPrefs.SetInt("strengthModifier", 2);
        PlayerPrefs.SetInt("gold", 0);
        PlayerPrefs.SetInt("level", 0);
        PlayerPrefs.SetInt("exp", 0);
        PlayerPrefs.SetInt("areasCleared", 0);
        PlayerPrefs.SetInt("sword", 0);

        SceneManager.LoadScene("Town");
    }

}
