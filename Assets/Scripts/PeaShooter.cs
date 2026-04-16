using UnityEngine;

public class Peashooter : PlantBase
{
    public PeaState currentState;

    public GameObject projectilePrefab;
    public float attackCooldown = 1.5f;

    private float timer;


    void Start()
    {
        currentState = PeaState.Idle;
    }

    void Update()
    {
        switch (currentState)
        {
            case PeaState.Idle:
                UpdateIdle();
                break;

            case PeaState.Attacking:
                UpdateAttacking();
                break;
        }
    }

    void UpdateIdle()
    {
        if (GetClosestZombie() != null)
        {
            currentState = PeaState.Attacking;
            timer = 0f;
        }
    }

    void UpdateAttacking()
    {
        Zombie target = GetClosestZombie();

        if (target == null)
        {
            currentState = PeaState.Idle;
            return;
        }

        timer += Time.deltaTime;

        if (timer >= attackCooldown)
        {
            Shoot();
            timer = 0f;
        }
    }

    Zombie GetClosestZombie()
    {
        var zombies = LaneManager.Instance.GetZombiesInRow(row);

        Zombie closest = null;
        float minDist = Mathf.Infinity;

        foreach (var z in zombies)
        {
            if (z == null) continue;

            float dist = z.transform.position.x - transform.position.x;

            if (dist > 0 && dist < minDist)
            {
                minDist = dist;
                closest = z;
            }
        }

        return closest;
    }

    protected virtual void Shoot()
    {
        GameObject proj = Instantiate(projectilePrefab);
        proj.transform.position = transform.position + Vector3.right * 0.5f;
    }
}