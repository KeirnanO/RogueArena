using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatController : MonoBehaviour
{
    public int maxHealth;
    public int health;

    public int maxMana;
    public int mana;

    public int exp;
    public int expNeeded;

    public int level;

    public int gold;

    public int strength;
    public int strengthModifier;

    public GameObject damageObject;
    public GameObject levelUpObject;

    //I dont know how to save swordsprites-- so this is how im gonna do
    //This would not work if there were more than 5 swords
    public Sprite[] swordSprites;
    public int equippedSword;
    public SpriteRenderer sword;

    DeathController deathController;

    public int areasCleared;


    private void Start()
    {
        maxHealth = PlayerPrefs.GetInt("maxHealth", 10);
        maxMana = PlayerPrefs.GetInt("maxMana", 5);
        health = PlayerPrefs.GetInt("health", maxHealth);
        mana = PlayerPrefs.GetInt("mana", maxMana);
        strength = PlayerPrefs.GetInt("strength", 0);
        strengthModifier = PlayerPrefs.GetInt("strengthModifier", 2);
        gold = PlayerPrefs.GetInt("gold", 0);
        areasCleared = PlayerPrefs.GetInt("areasCleared", 0);
        equippedSword = PlayerPrefs.GetInt("sword", 0);

        sword.sprite = swordSprites[equippedSword];

        exp = PlayerPrefs.GetInt("exp", 0);
        level = PlayerPrefs.GetInt("level", 0);
        expNeeded = 5 + level * 2 + Mathf.RoundToInt(Mathf.Pow(level, 2));

        deathController = GetComponent<DeathController>();
    }


    public void LoseHealth(int damage)
    {
        health -= damage;

        //Spawn damage text
        GameObject newDamageObject = Instantiate(damageObject, FindObjectOfType<Canvas>().transform);
        TextMeshProUGUI damageText = newDamageObject.GetComponentInChildren<TextMeshProUGUI>();
        newDamageObject.transform.position = transform.position;
        damageText.SetText(damage.ToString());

        if(health <= 0)
        {
            Death();
        }

    }

    public void Heal()
    {
        health = maxHealth;
        mana = maxMana;

        Save();
    }

    public int GetStrength()
    {
        return strength + strengthModifier;
    }

    public void Save()
    {
        PlayerPrefs.SetInt("maxHealth", maxHealth);
        PlayerPrefs.SetInt("maxMana", maxMana);
        PlayerPrefs.SetInt("health", health);
        PlayerPrefs.SetInt("mana", mana);
        PlayerPrefs.SetInt("strength", strength);
        PlayerPrefs.SetInt("strengthModifier", strengthModifier);
        PlayerPrefs.SetInt("gold", gold);
        PlayerPrefs.SetInt("level", level);
        PlayerPrefs.SetInt("exp", exp);
        PlayerPrefs.SetInt("areasCleared", areasCleared);
        PlayerPrefs.SetInt("sword", equippedSword);

        sword.sprite = swordSprites[equippedSword];
    }

    public void ResetGame()
    {
        maxHealth = 10;
        maxMana = 5;
        strength = 0;
        strengthModifier = 2;
        gold = 0;
        level = 0;
        exp = 0;
        areasCleared = 0;
        equippedSword = 0;
        Save();

        Heal();

    }

    public void RewardExp(int expReward)
    {
        exp += expReward;

        if (exp >= expNeeded)
        {
            LevelUp();
        }
    }

    void LevelUp()
    {
        //Show level up
        GameObject newLevelUpObject = Instantiate(levelUpObject, FindObjectOfType<Canvas>().transform);
        newLevelUpObject.transform.position = transform.position;

        //Remove exp
        exp -= expNeeded;
        level++;

        //If level is power of 3, strength increase -- dont get too powerful too fast
        if (level % 3 == 0)
        {
            strength++;
        }

        //Max stats increase + get a little health back
        maxHealth += 2;
        health += 2;
        maxMana += 1;
        mana += 1;

        //Reset expneeded
        expNeeded = 5 + level * 2 + Mathf.NextPowerOfTwo(level);

        //Save stats
        Save();
    }

    void Death()
    {
        ResetGame();
        deathController.enabled = true;
        StartCoroutine(deathController.Death());

    }

   

}
