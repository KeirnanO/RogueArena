using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeEnemy : MonoBehaviour
{
    public GameObject bullet;

    public int attackSpeed;
    float attackTimer;
    bool attacking;

    Transform player;
    Enemy thisEnemy;
    Animator animator;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        thisEnemy = GetComponent<Enemy>();
        animator = GetComponent<Animator>();

        attackTimer = 0.0f;
        attacking = false;
    }

    private void Update()
    {
        if(attackTimer >= attackSpeed)
        {
            StartCoroutine(Attack());
            attackTimer = 0.0f;
        }

        if(!attacking)
        {
            attackTimer += Time.deltaTime;
        }

    }

    IEnumerator Attack()
    {
        attacking = true;
        animator.SetTrigger("attack");

        //Hardcoded attack animation delay
        yield return new WaitForSeconds(3.1f);

        //Shoots 3 bullets with a delay inbetween
        //Bullet go towards player position
        for (int i = 0; i < 5; i++)
        {
            //Create new projectile
            Projectile newBullet = Instantiate(bullet, transform.position, Quaternion.identity).GetComponent<Projectile>();

            //Fix direction
            newBullet.damage = thisEnemy.strength;
            newBullet.LookAt(player.position, 0);
            newBullet.AddForce(player.position);

            yield return new WaitForSeconds(0.2f);
        }

        attacking = false;
    }

}
