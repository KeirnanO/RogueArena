using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BlacksmithController : MonoBehaviour
{
    StatController stats;
    PlayerMovement playerMovement;

    public GameObject blacksmithUI;

    public Image[] swordUpgrades;

    //Is here because making a sword script is longer since i only have 5 swords so far
    //Just changing the sprite makes it faster for now
    public SpriteRenderer playerSword;
    public Sprite[] swordSprites;
    public Color[] nameColours;

    public string[] swordNames;
    public string[] swordInfo;
    public int[] swordPrice;
    [SerializeField]
    public bool[] owned;
    [SerializeField]
    public bool[] equipped;

    public int[] strengthModifiers;

    int cursor;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI infoText;
    public TextMeshProUGUI priceText;

    public GameObject notOwned;
    public GameObject ownedText;
    public GameObject equippedText;

    //Stops constant cursor movement
    bool cursorBreak;

    // Start is called before the first frame update
    void Start()
    {
        stats = GameObject.FindGameObjectWithTag("Player").GetComponent<StatController>();
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();

        cursor = 0;

        SetUI();

    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = 0;
        playerMovement.enabled = false;

        MoveCursor(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if(Input.GetKeyDown("joystick button 1"))
        {
            Time.timeScale = 1;
            playerMovement.enabled = true;
            blacksmithUI.SetActive(false);
        }

        if(Input.GetKeyDown("joystick button 0"))
        {
            HandlePurchases();
        }

    }

    //This is ugly, but im too lazy to move things around
    //And it looks kinda cool
    void MoveCursor(float horizontal, float vertical)
    {
        //Move cursor horizontally
        if (Mathf.Abs(horizontal) > Mathf.Abs(vertical))
        {
            if (!cursorBreak)
            {
                switch (cursor)
                {
                    case 0:
                        if (horizontal > 0)
                        {
                            cursor = 2;
                        }
                        break;
                    case 1:
                        if (horizontal > 0)
                        {
                            cursor = 2;
                        }
                        break;
                    case 2:
                        if (horizontal > 0)
                        {
                            cursor = 3;
                        }
                        else
                        {
                            cursor = 0;
                        }
                        break;
                    case 3:
                        if (horizontal < 0)
                        {
                            cursor = 2;
                        }
                        break;
                    case 4:
                        if (horizontal < 0)
                        {
                            cursor = 2;
                        }
                        break;
                }
            }

            cursorBreak = true;
            SetUI();
        }
        //Move cursor Vertically
        else if (Mathf.Abs(vertical) > Mathf.Abs(horizontal))
        {
            if (!cursorBreak)
            {
                switch (cursor)
                {
                    case 0:
                        if (vertical < 0)
                        {
                            cursor = 1;
                        }
                        break;
                    case 1:
                        if (vertical > 0)
                        {
                            cursor = 0;
                        }
                        break;
                    case 2:
                        break;
                    case 3:
                        if (vertical < 0)
                        {
                            cursor = 4;
                        }
                        break;
                    case 4:
                        if (vertical > 0)
                        {
                            cursor = 3;
                        }
                        break;

                }
            }

            cursorBreak = true;
            SetUI();
        }
        else
        {
            cursorBreak = false;
        }

        

    }

    void HandlePurchases()
    {
        if(!owned[cursor])
        {
            if(stats.gold >= swordPrice[cursor])
            {
                stats.gold -= swordPrice[cursor];
                owned[cursor] = true;
                Equip();
            }
        }
        else if(owned[cursor])
        {
            Equip();
        }

        SetUI();
    }

    //Equips currently selected sword
    void Equip()
    {
        //Unequip all swords
        for(int i = 0; i < equipped.Length; i++)
        {
            equipped[i] = false;
        }

        //Equip this sword
        equipped[cursor] = true;
        stats.strengthModifier = strengthModifiers[cursor];
        stats.equippedSword = cursor;

        stats.Save();

    }

    void SetUI()
    {
        foreach(Image sword in swordUpgrades)
        {
            sword.color = Color.grey;
        }
        swordUpgrades[cursor].color = Color.white;

        nameText.SetText(swordNames[cursor]);
        nameText.color = nameColours[cursor];
        infoText.SetText(swordInfo[cursor]);
       
        if(equipped[cursor])
        {
            notOwned.SetActive(false);
            ownedText.SetActive(false);
            equippedText.SetActive(true);
        }
        else if(owned[cursor])
        {
            notOwned.SetActive(false);
            ownedText.SetActive(true);
            equippedText.SetActive(false);
        }
        else
        {
            notOwned.SetActive(true);
            ownedText.SetActive(false);
            equippedText.SetActive(false);

            priceText.SetText(swordPrice[cursor].ToString());
        }

    }

}
