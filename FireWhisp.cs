using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWhisp : MonoBehaviour
{
    Enemy thisEnemy;
    Transform player;

    Rigidbody2D rb;

    public float movespeed;
    public int attackSpeed;

    public GameObject fireball;

    Animator animator;

    
    float attackTimer;
    bool attacking;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        thisEnemy = GetComponent<Enemy>();

        rb = GetComponent<Rigidbody2D>();

        attacking = false;
        attackTimer = 0.0f;  
    }

    // Update is called once per frame
    void Update()
    {
        if (!attacking)
        {
            Move();
        }

        if(attackTimer > attackSpeed)
        {
            attackTimer = 0.0f;
            StartCoroutine(Attack());
        }
        else if(!attacking)
        {
            attackTimer += Time.deltaTime;
        }

    }



    void Move()
    {
        Vector2 distanceApart = transform.position - player.position;
        Vector2 normalDistance = distanceApart.normalized;

        transform.Translate(normalDistance * -movespeed * Time.deltaTime);
    }

    IEnumerator Attack()
    {
        attacking = true;
        animator.SetTrigger("attack");

        //Stop all Movement
        rb.velocity = new Vector2(0, 0);

        //Hardcoded animation length
        yield return new WaitForSeconds(1f);

        //Create new projectile
        Projectile newFireball = Instantiate(fireball, transform.position, Quaternion.identity).GetComponent<Projectile>();

        //Fix direction
        newFireball.damage = thisEnemy.strength;
        newFireball.LookAt(player.position, 90);
        newFireball.AddForce(player.position);

        //Buffer between actions
        yield return new WaitForSeconds(1f);

        attacking = false;

    }
}
