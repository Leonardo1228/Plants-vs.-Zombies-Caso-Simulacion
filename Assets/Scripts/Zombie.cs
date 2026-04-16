using UnityEngine;

public class Zombie : MonoBehaviour
{
    public ZomState currentState;

    public float speed = 0.5f;
    public float attackDamage = 1f;
    public float attackRate = 1f;

    private float timer;
    private PlantBase targetPlant;

    public int row;

    void Start()
    {
        currentState = ZomState.Moving;
        LaneManager.Instance.RegisterZombie(this, row);
    }

    void OnDestroy()
    {
        if (LaneManager.Instance != null)
            LaneManager.Instance.RemoveZombie(this, row);
    }

    void Update()
    {
        switch (currentState)
        {
            case ZomState.Moving:
                UpdateMoving();
                break;

            case ZomState.Attacking:
                UpdateAttacking();
                break;
        }
    }

    void UpdateMoving()
    {
        Move();

        if (DetectPlant())
        {
            currentState = ZomState.Attacking;
            timer = 0f;
        }
    }

    void UpdateAttacking()
    {
        if (targetPlant == null)
        {
            currentState = ZomState.Moving;
            return;
        }

        timer += Time.deltaTime;

        if (timer >= attackRate)
        {
            DealDamage();
            timer = 0f;
        }
    }

    void Move()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;
    }

    bool DetectPlant()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, 0.5f);

        if (hit != null)
        {
            PlantBase plant = hit.GetComponent<PlantBase>();

            if (plant != null)
            {
                targetPlant = plant;
                return true;
            }
        }

        return false;
    }

    void DealDamage()
    {
        if (targetPlant == null) return;

        Health hp = targetPlant.GetComponent<Health>();

        if (hp != null)
        {
            hp.TakeDamage(attackDamage);
        }
    }
}
