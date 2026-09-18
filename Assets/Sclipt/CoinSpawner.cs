using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private CameraFollow cameraFollow;

    [Header("ボール出現位置")]
    [SerializeField] private Transform spawnPoint;

    public bool CanSpawn = true;

    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Space))
            return;

        SpawnCoin();
    }

    public void SpawnCoin()
    {
        if (!CanSpawn)
            return;

        if (GameManager.Instance == null)
        {
            Debug.LogError("GameManager.Instance がありません！");
            return;
        }

        if (!GameManager.Instance.IsPlaying)
            return;

        if (spawnPoint == null)
        {
            Debug.LogError("Spawn Pointが設定されていません！");
            return;
        }

        GameObject coin = Instantiate(
            coinPrefab,
            spawnPoint.position,
            Quaternion.identity
        );

        if (cameraFollow != null)
        {
            cameraFollow.SetTarget(coin.transform);
        }

        CanSpawn = false;

        Debug.Log("ボール生成");
    }

    public void EnableSpawn()
    {
        if (!GameManager.Instance.IsPlaying)
            return;

        CanSpawn = true;

        Debug.Log("ボールを生成可能");
    }
}