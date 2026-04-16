using UnityEngine;

public class House : MonoBehaviour
{
    private bool triggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered) return;

        if (other.CompareTag("Zombie"))
        {
            triggered = true;

            Debug.Log("GAME OVER - Zombie llegó a la casa");

            Time.timeScale = 0f;

            if (GameManager.Instance != null)
            {
                GameManager.Instance.GameOver();
            }
        }
    }
}
