using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesSpawner : MonoBehaviour
{
    public float waitTime = 3.0f;
    private float time = 0f;
    public GameObject obstacles;

    public float direction;
    // Update is called once per frame
    void Update()
    {
        if(time > waitTime){
            GameObject go = Instantiate(obstacles);
            go.transform.position = transform.position + new Vector3(0f, 
            Random.Range(-direction, direction), 0f);
            time = 0f;

            Destroy(go, 10);//destroys after 5 seconds
        }
        time += Time.deltaTime;
    }
}
