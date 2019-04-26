using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody2D rb;

    public float movespeed;
    public int damage;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 4f);
    }

    //To go through objects simply change the layer and use Physics2D.ignoreLayerCollision
    private void OnCollisionEnter2D(Collision2D collision)
    {
              string tag = collision.gameObject.tag;
        switch (tag)
        {
            case "Ground":
                Destroy(gameObject);
                break;
            case "Player":
                PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();
                player.TakeDamage(damage, transform.position);
                Destroy(gameObject);
                break;
            case "Wall":
                Destroy(gameObject);
                break;

        }
    }

    public void LookAt(Vector3 target, float offsetAngle)
    {
        Vector3 dir = transform.position - target;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - offsetAngle, Vector3.forward);
    }

    public void AddForce()
    {
        rb.AddForce(new Vector2(movespeed, 0));
    }

    public void AddForce(Vector3 target)
    {

        Vector3 direction = target - transform.position;

        direction.Normalize();

        rb.AddForce(direction * movespeed);
    }

}
