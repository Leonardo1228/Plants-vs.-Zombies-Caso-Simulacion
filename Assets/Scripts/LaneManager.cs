using System.Collections.Generic;
using UnityEngine;

public class LaneManager : MonoBehaviour
{
    public static LaneManager Instance;

    private List<Zombie>[] lanes;

    public int totalRows = 6;

    void Awake()
    {
        Instance = this;

        lanes = new List<Zombie>[totalRows];

        for (int i = 0; i < totalRows; i++)
        {
            lanes[i] = new List<Zombie>();
        }
    }

    public void RegisterZombie(Zombie z, int row)
    {
        lanes[row].Add(z);
    }

    public void RemoveZombie(Zombie z, int row)
    {
        lanes[row].Remove(z);
    }

    public List<Zombie> GetZombiesInRow(int row)
    {
        return lanes[row];
    }
}
