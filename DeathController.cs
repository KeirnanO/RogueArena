using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathController : MonoBehaviour
{
    Rigidbody2D rb;
    PlayerMovement player;
    Pause pause;

    public GameObject Blackscreen;
    public GameObject GameOverUi;
    GameObject[] HUD;

    bool getInput;

    private void Awake()
    {
        HUD = GameObject.FindGameObjectsWithTag("HUD");

        player = GetComponent<PlayerMovement>();
        pause = GetComponent<Pause>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(getInput)
        {
            if(Input.GetKeyDown("joystick button 7"))
            {
                SceneManager.LoadScene("Menu");
            }
        }
    }

    public IEnumerator Death()
    {
        rb.velocity = new Vector2(0, 0);
        rb.isKinematic = true;

        float elapsedTime = 0;

        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<Rigidbody2D>().gravityScale = 0;

        foreach (GameObject g in HUD)
        {
            g.SetActive(false);
        }

        Vector3 movement = new Vector3(0, -1, 0);
        Vector3 startingPos = transform.position;

        Instantiate(Blackscreen, new Vector3(0, 0, 0), Quaternion.identity);

        while (transform.position.x != 0 || transform.position.y != -1)
        {
            elapsedTime += Time.deltaTime * 2/3;
            transform.position = Vector3.Lerp(startingPos, movement, elapsedTime);

            yield return new WaitForEndOfFrame();
        }

        Instantiate(GameOverUi, FindObjectOfType<Canvas>().transform);
        getInput = true;

    }
}
