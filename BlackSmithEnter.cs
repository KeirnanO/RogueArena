using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackSmithEnter : MonoBehaviour
{

    public GameObject blacksmithUI;


    private void OnTriggerStay2D(Collider2D collision)
    {

        if (Input.GetKeyDown("joystick button 3"))
        {
            blacksmithUI.SetActive(true);
        }

    }
}
