using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatManager : MonoBehaviour
{

    public TextMeshProUGUI levelText;

    public Image healthBar;
    public TextMeshProUGUI healthText;

    public Image manaBar;
    public TextMeshProUGUI manaText;

    public TextMeshProUGUI goldText;

    public StatController playerStats;

    // Update is called once per frame
    void Update()
    {
        float health = playerStats.health;
        float maxHealth = playerStats.maxHealth;
        float mana = playerStats.mana;
        float maxMana = playerStats.maxMana;

        levelText.SetText("Lv: " + playerStats.level);

        healthBar.fillAmount = health / maxHealth;
        healthText.SetText(playerStats.health + " / " + maxHealth);

        manaBar.fillAmount = mana / maxMana;
        manaText.SetText(playerStats.mana + " / " + maxMana);

        goldText.SetText(playerStats.gold.ToString());
    }
}
