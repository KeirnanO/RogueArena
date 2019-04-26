using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeonGenerator : MonoBehaviour
{
    public GameObject slime;
    public GameObject whisp;
    public GameObject jumpySlime;
    public GameObject eye;

    public BoxCollider2D slimeSpawnPosition;
    public BoxCollider2D whispSpawnPosition;
    public BoxCollider2D eyeSpawnPosition;

    StatController playerStats;

    int maxDifficulty;
    int difficulty;


    private void Awake()
    {
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<StatController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        maxDifficulty = GetDifficulty();
        GenerateDungeon();
    }

    void GenerateDungeon()
    {
        while(difficulty < maxDifficulty)
        {
            int spawnChance = Random.Range(0, maxDifficulty*10 - difficulty*10);

            if(spawnChance < 80)
            {
                SpawnSlime();
            }
            else if(spawnChance < 140)
            {
                SpawnJumpy();
            }
            else if(spawnChance < 180)
            {
                SpawnWhisp();
            }
            else
            {
                SpawnEye();
            }
        }
    }

    void SpawnSlime()
    {
        float maxX = slimeSpawnPosition.size.x;
        Instantiate(slime, slimeSpawnPosition.transform.position + new Vector3(Random.Range(0, maxX), 0, 0), Quaternion.identity);
        difficulty += 6;
    }

    void SpawnWhisp()
    {
        float maxX = whispSpawnPosition.size.x;
        float maxY = whispSpawnPosition.size.y;
        Instantiate(whisp, whispSpawnPosition.transform.position + new Vector3(Random.Range(0, maxX), Random.Range(0, maxY), 0), Quaternion.identity);
        difficulty += 15;
    }

    void SpawnJumpy()
    {
        float maxX = slimeSpawnPosition.size.x;
        Instantiate(jumpySlime, slimeSpawnPosition.transform.position + new Vector3(Random.Range(0, maxX), 0, 0), Quaternion.identity);
        difficulty += 10;
    }

    void SpawnEye()
    {
        float maxX = eyeSpawnPosition.size.x;
        float maxY = eyeSpawnPosition.size.y;
        Instantiate(eye, eyeSpawnPosition.transform.position + new Vector3(Random.Range(0, maxX), Random.Range(0, maxY), 0), Quaternion.identity);
        difficulty += 24;
    }

    int GetDifficulty()
    {
        return 5 + (playerStats.level * 5) + (playerStats.areasCleared * 1);
    }
}
