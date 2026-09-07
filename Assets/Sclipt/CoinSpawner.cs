using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private GameObject CoinPrefab;

    public bool CanSpawn = true;

    private int _bairitu = 1;

    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Space))
            return;

        // まだコインを出せないなら終了
        if (!CanSpawn)
            return;

        // メダルがない
        if (GameManager.Instance.playerCoin <= 0)
        {
            GameManager.Instance.Result();
            return;
        }

        // コイン生成
        Instantiate(CoinPrefab);

        // ★生成した瞬間にロック
        CanSpawn = false;

        GameManager.Instance.playerCoin -= _bairitu;
        GameManager.Instance.UpdateUI();

        Debug.Log("コイン生成");
    }

    public void EnableSpawn()
    {
        CanSpawn = true;

        Debug.Log("次のコインを生成可能");
    }
}