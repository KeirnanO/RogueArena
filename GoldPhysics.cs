using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldPhysics : MonoBehaviour
{
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        float randomX = Random.Range(-1.0f, 1.0f);

        //so all the gold isnt in one space
        transform.Translate(new Vector3(randomX, 0, 0));
        //Little jump animation when being dropped
        rb.AddForce(new Vector3(0, 600));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag.Equals("Ground"))
        {
            rb.gravityScale = 0;
            rb.velocity = new Vector2(0, 0);
        }
    }
}
