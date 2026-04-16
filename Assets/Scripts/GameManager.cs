using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Prefabs")]
    public GameObject plantPrefab;
    public GameObject repeaterPrefab;
    public GameObject wallNutPrefab;

    [Header("Setup")]
    [Range(0f, 1f)]
    public float randomPlantChance = 0.4f;

    public int startColumns = 2; 

    private bool gameOver = false;

    private int zombiesAlive = 0;
    private bool gameEnded = false;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        GenerateInitialPlants();
    }

    void GenerateInitialPlants()
    {
        int rows = GridManager.Instance.rows;

        // 🔹 Columna 0 → SOLO Peashooter
        for (int r = 0; r < rows; r++)
        {
            SpawnPlant(plantPrefab, 0, r);
        }

        // 🔹 Columnas 1 y 2 → aleatorias
        for (int c = 1; c <= startColumns; c++)
        {
            for (int r = 0; r < rows; r++)
            {
                if (Random.value < randomPlantChance)
                {
                    if (!GridManager.Instance.IsOccupied(c, r))
                    {
                        GameObject prefab = GetRandomPlant();
                        SpawnPlant(prefab, c, r);
                    }
                }
            }
        }
    }
    GameObject GetRandomPlant()
    {
        float rand = Random.value;

        if (rand < 0.5f)
            return plantPrefab;
        else if (rand < 0.7f)
            return repeaterPrefab;
        else
            return wallNutPrefab;
    }
    void SpawnPlant(GameObject prefab, int col, int row)
    {
        GameObject plant = Instantiate(prefab);
        GridManager.Instance.PlaceObject(plant, col, row);
    }
    public void RegisterZombie()
    {
        zombiesAlive++;
    }
    public void ZombieDied()
    {
        zombiesAlive--;

        if (zombiesAlive <= 0 && !gameEnded)
        {
            Victory();
        }
    }
    void Victory()
    {
        gameEnded = true;

        Debug.Log("Tu casa esta a salvo.");

        Time.timeScale = 0f;
    }
    public void GameOver()
    {
        if (gameOver) return;

        gameOver = true;

        Debug.Log("GAME OVER. Los zombies se comieron tu cerebro");

        Time.timeScale = 0f;

    }


}
