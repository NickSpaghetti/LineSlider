using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawnerController : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> Obstacles;
    public float SpawnRate;
    void Start()
    {
        StartCoroutine("SpawnObsticals");
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SpawnObsticals()
    {
        while (true)
        {
            Spawn();

            GameManager.GameMangerInstance.UpdateScore();

            yield return new WaitForSeconds(SpawnRate);
        }
    }

    private void Spawn()
    {
        int randomObstacle = Random.Range(0, Obstacles.Count);
        Instantiate(Obstacles[randomObstacle], Obstacles[randomObstacle].transform.position, Obstacles[randomObstacle].transform.rotation);

    }

}
