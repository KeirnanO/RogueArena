using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public GameObject portalText;
    public string sceneName;

    Animator playerAnimator;
    PlayerMovement playerMovement;
    StatController playerStats;

    private void Awake()
    {
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<StatController>();
        playerAnimator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        portalText.SetActive(true);

        if(Input.GetKeyDown("joystick button 3"))
        {
            StartCoroutine(Teleport());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        portalText.SetActive(false);
    }

    IEnumerator Teleport()
    {
        playerAnimator.SetTrigger("teleportOut");
        playerStats.Save();
        playerMovement.enabled = false;
        
        yield return new WaitForSeconds(1.1f);

        SceneManager.LoadScene(sceneName);       
    }
}
