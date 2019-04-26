using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    StatController playerStats;

    public TextMeshProUGUI healthText;
    public TextMeshProUGUI manaText;
    public TextMeshProUGUI strengthText;
    public TextMeshProUGUI strengthModifierText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI expText;

    private void Awake()
    {
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<StatController>();
    }

    private void Update()
    {
        if(Input.GetKeyDown("joystick button 3"))
        {
            SceneManager.LoadScene("Menu");
            Time.timeScale = 1;
        }
    }

    public void UpdateUI()
    {               
        healthText.SetText(playerStats.health + " / " + playerStats.maxHealth);
        manaText.SetText(playerStats.mana + " / " + playerStats.maxMana);
        strengthText.SetText(playerStats.strength.ToString());
        strengthModifierText.SetText("( +" + playerStats.strengthModifier + " )");
        levelText.SetText(playerStats.level.ToString());
        expText.SetText(playerStats.exp + " / " + playerStats.expNeeded);
    }
}
