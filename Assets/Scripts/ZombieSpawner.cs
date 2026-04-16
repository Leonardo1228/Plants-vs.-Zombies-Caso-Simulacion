using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject zombiePrefab;
    public GameObject coneZombiePrefab;

    public int totalZombies = 10;
    public float spawnInterval = 2f;

    private float timer;
    private int spawnedZombies = 0;



    void Update()
    {
        if (spawnedZombies >= totalZombies) return;

        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnZombie();
            timer = 0f;
        }
    }
    GameObject GetRandomZombie()
    {
        if (Random.value < 0.7f)
            return zombiePrefab;
        else
            return coneZombiePrefab;
    }
    void SpawnZombie()
    {
        int row = Random.Range(0, GridManager.Instance.rows);
        int col = GridManager.Instance.cols - 1;

        Vector2 pos = GridManager.Instance.GridToWorld(col, row);

        GameObject prefab = GetRandomZombie();

        GameObject zombieObj = Instantiate(prefab, pos, Quaternion.identity);

        Zombie z = zombieObj.GetComponent<Zombie>();

        if (z != null)
            z.row = row;


        GameManager.Instance.RegisterZombie();
        spawnedZombies++;
    }

    
}
