using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int health;
    public bool isInterupptable;
    public bool isRecoilable;

    public int strength;
    public int expReward;

    StatController playerStats;

    RecoilOnHit recoil;
    Animator animator;

    public GameObject gold;
    public GameObject damageObject;


    public int maxGoldDrop;
    bool dead;

    // Start is called before the first frame update
    void Start()
    {
        recoil = GetComponent<RecoilOnHit>();
        animator = GetComponent<Animator>();

        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<StatController>();
        dead = false;
    }

    private void Update()
    {
        if(health <= 0 && !dead)
        {
            dead = true;
            Die();
        }
    }

    public void TakeDamage(int damage, Vector3 sourcePosition)
    {
        //Lose health
        health -= damage;

        //Spawn damage text
        GameObject newDamageObject = Instantiate(damageObject, FindObjectOfType<Canvas>().transform);
        TextMeshProUGUI damageText = newDamageObject.GetComponentInChildren<TextMeshProUGUI>();
        newDamageObject.transform.position = transform.position;
        damageText.SetText(damage.ToString());

        if (isRecoilable)
        {
            //Recoil Back
            if (transform.position.x > sourcePosition.x)
            {
                recoil.Recoil(1);
            }
            else
            {
                recoil.Recoil(-1);
            }

            if (isInterupptable)
            {
                animator.SetTrigger("hit");
            }
        }

        
    }

    IEnumerator DropGold()
    {
        //70% chance for every enemy to drop gold
        int dropChance = Random.Range(0, 100);

        if(dropChance > 30)
        {
            for(int i = 0; i < maxGoldDrop; i++)
            {
                Instantiate(gold, transform.position, Quaternion.identity);

                //Wait a split second for each gold drop
                yield return null;
            }
        }

        //Moved here because sometimes the enemy will get destoyed before the drop gold coroutine finishes
        Destroy(gameObject);
    }

    void Die()
    {
        //Reward exp
        playerStats.RewardExp(expReward);

        //Enemy disappears
        animator.SetBool("dead", true);
        GetComponent<BoxCollider2D>().enabled = false;

        //Chance to drop gold
        StartCoroutine(DropGold());
    }


}
