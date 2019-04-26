using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoilOnHit : MonoBehaviour
{

    public float speed;
    public float height;

    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    public void Recoil(int direction)
    {
        rb.velocity = new Vector2(0, 0);
        rb.AddForce(new Vector2(direction * speed, height));
    }


}
