using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoldPickup : MonoBehaviour
{
    public GameObject goldTextObject;

    StatController stats;

    //This is only used when there are gold modifiers
    //Even tho i dont have that yet in this game, i wanted to add this part anyways
    TextMeshProUGUI goldText;

    private void Start()
    {
        stats = GetComponent<StatController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag.Equals("Gold"))
        {
            GameObject newGoldText = Instantiate(goldTextObject, FindObjectOfType<Canvas>().transform);
            goldText = newGoldText.GetComponentInChildren<TextMeshProUGUI>();
            newGoldText.transform.position = transform.position;

            //Here we would set GoldText to 10 * whatever the multiplier is -- which would be in statController

            stats.gold += 10; //* goldmodifier

            Destroy(collision.gameObject);
        }
    }
}
