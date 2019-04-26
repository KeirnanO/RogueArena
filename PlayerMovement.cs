using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    Rigidbody2D rb;
    RecoilOnHit recoil;
    StatController stats;

    public float movespeed;
    public float jumpForce;

    //Used for sword animation
    public Animator swordAnimator;
    //Used for walking animation
    public Animator playerAnimator;

    bool grounded;

    //Moveable state -- Used for input
    bool moveable;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        recoil = GetComponent<RecoilOnHit>();
        stats = GetComponent<StatController>();

        moveable = true;
    }


    private void Update()
    {
        if (moveable)
        {
            Move(Input.GetAxisRaw("Horizontal"));
            playerAnimator.SetFloat("speed", Mathf.Abs(Input.GetAxis("Horizontal")));
        }
        else
        {
            playerAnimator.SetFloat("speed", 0);
        }

        GetInput();
    }


    private void Move(float direction)
    {

        if (direction != 0)
        {
            transform.Translate(new Vector2(direction * movespeed * Time.deltaTime, 0));

            if (direction > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }      

    }

    //Gets jump key and skillShot
    void GetInput()
    {
        if (Input.GetKeyDown("joystick button 0") && grounded)
        {
            rb.AddForce(new Vector2(0, jumpForce));
            grounded = false;
        }
        if(Input.GetKeyDown("joystick button 2"))
        {
            swordAnimator.SetTrigger("attack");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag.Equals("Ground"))
        {
            grounded = true;
        }

        if(collision.gameObject.tag.Equals("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();

            StartCoroutine(TakeDamage(enemy.strength, transform.position));
        }
        if(collision.gameObject.tag.Equals("Projectile"))
        {
            Projectile projectile = collision.gameObject.GetComponent<Projectile>();
            StartCoroutine(TakeDamage(projectile.damage, transform.position));
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Ground"))
        {
            grounded = false;
        }
    }


    public void SetImmobile(bool immobile)
    {
        moveable = !immobile;
    }

    public IEnumerator TakeDamage(int damage, Vector3 sourcePosition)
    {
        //Lose health
        stats.LoseHealth(damage);

        if (stats.health > 0)
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

            //Immobile 
            SetImmobile(true);

            //For 0.5 seconds
            yield return new WaitForSeconds(0.5f);

            //Can move again
            SetImmobile(false);
        }
        else
        {
            rb.isKinematic = true;
            yield return null;
        }

    }
}
