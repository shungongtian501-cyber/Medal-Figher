using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private CameraFollow cameraFollow;

    public bool CanSpawn = true;


    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Space))
            return;

        if (!CanSpawn)
            return;

        if (GameManager.Instance == null)
        {
            Debug.LogError("GameManager.Instance がありません！");
            return;
        }

        // 一球入魂
        if (!GameManager.Instance.UseCoin())
            return;

        // コイン生成
        GameObject coin = Instantiate(coinPrefab);

        // カメラに生成したコインを教える
        if (cameraFollow != null)
        {
            cameraFollow.SetTarget(coin.transform);
        }

        // 二度目の生成を禁止
        CanSpawn = false;

        Debug.Log("コイン生成");
    }


    public void EnableSpawn()
    {
        if (!GameManager.Instance.IsPlaying)
            return;

        CanSpawn = true;

        Debug.Log("コインを生成可能");
    }
}