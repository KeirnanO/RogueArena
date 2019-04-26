using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dummy : MonoBehaviour
{

    public Animator animator;
    public GameObject damageObject;

    public void TakeDamage(int damage)
    {
        animator.SetTrigger("hit");

        GameObject newDamageObject = Instantiate(damageObject, FindObjectOfType<Canvas>().transform);
        TextMeshProUGUI damageText = newDamageObject.GetComponentInChildren<TextMeshProUGUI>();
        newDamageObject.transform.position = transform.position;
        damageText.SetText(damage.ToString());


    }

}
