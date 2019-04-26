using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordScript : MonoBehaviour
{
    StatController stats;

    private void Start()
    {
        stats = GetComponentInParent<StatController>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag.Equals("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();

            enemy.TakeDamage(stats.GetStrength(), transform.position);
        }

        if(collision.gameObject.tag.Equals("Dummy"))
        {
            Dummy dummy = collision.GetComponent<Dummy>();
            dummy.TakeDamage(stats.GetStrength());
        }
    }
}
