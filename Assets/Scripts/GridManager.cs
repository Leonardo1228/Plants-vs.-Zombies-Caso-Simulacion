using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;

    public int rows = 6;
    public int cols = 9;
    public float cellSize = 1f;
    private GameObject[,] grid;

    private void Awake()
    {
        Instance = this;
        grid = new GameObject[cols, rows];
    }

    // Convierte coordenadas de grid a mundo
    public Vector2 GridToWorld(int col, int row)
    {
        float offsetX = (cols - 1) * cellSize / 2f;
        float offsetY = (rows - 1) * cellSize / 2f;

        return new Vector2(
            col * cellSize - offsetX,
            row * cellSize - offsetY
        );
    }

    // Verifica si una posición está dentro del grid
    public bool IsValidPosition(int col, int row)
    {
        return col >= 0 && col < cols && row >= 0 && row < rows;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        float offsetX = (cols - 1) * cellSize / 2f;
        float offsetY = (rows - 1) * cellSize / 2f;

        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                Vector2 pos = new Vector2(
                    c * cellSize - offsetX,
                    r * cellSize - offsetY
                );

                Gizmos.DrawWireCube(pos, Vector3.one * cellSize);
            }
        }
    }

    public void PlaceObject(GameObject obj, int col, int row)
    {
        if (!IsValidPosition(col, row)) return;

        grid[col, row] = obj;
        obj.transform.position = GridToWorld(col, row);

        Peashooter pea = obj.GetComponent<Peashooter>();
        if (pea != null)
            pea.row = row;
    }
    public GameObject GetObject(int col, int row)
    {
        if (!IsValidPosition(col, row)) return null;
        return grid[col, row];
    }

    public bool IsOccupied(int col, int row)
    {
        return GetObject(col, row) != null;
    }

}
