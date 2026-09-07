using UnityEngine;

public class Coin : MonoBehaviour
{
    private CoinSpawner spawner;

    [SerializeField] private AudioSource _SE;

    private void Start()
    {
        spawner = FindFirstObjectByType<CoinSpawner>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            spawner.EnableSpawn();
        }

        if (collision.gameObject.CompareTag("Pin"))
        {

            if (_SE != null)
            {
                _SE.Play();
            }
        }
    }
}