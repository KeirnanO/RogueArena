using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    //Alot of the varibles are ugly, i apologize

    public GameObject pauseMenu;

    PauseMenu PauseMenuController;

    PlayerMovement playerController;

    private void Start()
    {
        playerController = GetComponent<PlayerMovement>();

        PauseMenuController = pauseMenu.GetComponent<PauseMenu>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("joystick button 6"))
        {
            if (!pauseMenu.activeSelf)
            {
                Time.timeScale = 0;
                pauseMenu.SetActive(true);
                PauseMenuController.UpdateUI();
                playerController.enabled = false;
            }
            else
            {
                Time.timeScale = 1;
                pauseMenu.SetActive(false);
                playerController.enabled = true;
            }
        }
    }
}
