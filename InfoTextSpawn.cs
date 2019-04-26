using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoTextSpawn : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        float randomX = Random.Range(-0.6f, 0.6f);
        float randomY = Random.Range(1, 1.5f);


        transform.Translate(new Vector3(randomX, randomY, 0));

        Destroy(gameObject, 1.5f);
    }
}
