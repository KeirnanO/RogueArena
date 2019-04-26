using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeMovement : MonoBehaviour
{

    Transform player;
    Rigidbody2D rb;

    public float movespeed;
    public float jumpForce;
    public float airSpeed;

    public int jumpChance;

    public int maxWaitTime;

    Animator animator;

    bool moving = false;
    bool jumping = false;
    public bool grounded = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();

        
    }

    private void Update()
    {
        //If slime is not doing anything
       if(!jumping && !moving && grounded)
        {
            int action = Random.Range(0, 100);

            //60% chance to jump
            if(action > 100 - jumpChance)
            {
                StartCoroutine(Jump());
            }
            else //40% chance to move
            {
                StartCoroutine(Move());
            }
        }
    }

    //Move Towards player
    IEnumerator Move()
    {

        int direction = GetDirection();

        moving = true;

        //Random move duration
        int duration = Random.Range(0, 3);

        float timeElapsed = 0.0f;

        //Set move animation
        animator.SetBool("moving", true);

        while(timeElapsed < duration)
        {
            timeElapsed += Time.deltaTime;

            transform.Translate(new Vector2(movespeed * direction * Time.deltaTime, 0));

            yield return new WaitForEndOfFrame();
        }

        //Stop move animation
        animator.SetBool("moving", false);

        //Create buffer zone between actions
        yield return new WaitForSeconds(Random.Range(1,maxWaitTime));

        moving = false;
    }

    IEnumerator Jump()
    {
        int direction = GetDirection();

        //Jumped and is no longer grounded
        grounded = false;
        jumping = true;

        animator.SetTrigger("jump");

        //Hardcoded jump delay
        yield return new WaitForSeconds(0.32f);

        rb.velocity = new Vector2(0, 0);
        rb.AddForce(new Vector2(airSpeed * direction, jumpForce));

        //Do nothing while in the air
        while(!grounded)
        {
            yield return null;
        }

        //Once grounded play landing animation
        animator.SetTrigger("land");

        //Buffer between actions
        yield return new WaitForSeconds(Random.Range(1,maxWaitTime));

        //Not jumping anymore
        jumping = false;
                     
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag.Equals("Ground"))
        {
            grounded = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Ground"))
        {
            grounded = false;
        }
    }

    //Returns the facing direction so it moves towards the player
    int GetDirection()
    {
        //Get the difference in position of this Slime and Player
        float distanceApart = transform.position.x - player.position.x;

        //Return X direction towards the player
        if (distanceApart > 0)
        {
           return -1;
        }
        else
        {
            return 1;
        }
    }

}
