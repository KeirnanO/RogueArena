using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] enemiesRemaining;
    public GameObject portals;

    public StatController playerStats;

    bool arenaCleared;

    private void Awake()
    {
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<StatController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //8 = Normal Enemies
        //9 = Does Not Collide with Ground/Walls
        //10 = Does Not Collide with Platforms
        //11 = Collides With everything - Player
        //12 = Ground
        //13 = Platforms

        //Enemies do not collide with one another
        Physics2D.IgnoreLayerCollision(8 , 8);
        Physics2D.IgnoreLayerCollision(8 , 9);
        Physics2D.IgnoreLayerCollision(8 , 10);

        //9 does not collide with ground or platforms -- Some Projectiles
        Physics2D.IgnoreLayerCollision(9, 10);
        Physics2D.IgnoreLayerCollision(9 , 12);       
        Physics2D.IgnoreLayerCollision(9 , 13);       

        //10 does not collide with platforms only -- Some Enemies
        Physics2D.IgnoreLayerCollision(10 , 13);       
    }

    private void Update()
    {
        enemiesRemaining = GameObject.FindGameObjectsWithTag("Enemy");

        if(enemiesRemaining.Length == 0 && !arenaCleared)
        {
            arenaCleared = true;
            playerStats.areasCleared++;
            portals.SetActive(true);
        }


    }

}
