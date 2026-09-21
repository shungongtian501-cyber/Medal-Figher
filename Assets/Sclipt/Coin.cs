using UnityEngine;

public class Coin : MonoBehaviour
{
    private CoinSpawner spawner;

    private void Start()
    {
        spawner = FindFirstObjectByType<CoinSpawner>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            GameManager.Instance.GameOver();
        }
    }
}