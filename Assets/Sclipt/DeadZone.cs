using UnityEngine;

public class DeadZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Coin"))
            return;

        // コインを削除
        Destroy(other.gameObject);

        // GameManagerが存在しない場合
        if (GameManager.Instance == null)
        {
            Debug.LogError("GameManager.Instance がありません！");
            return;
        }

        // まだゲーム中か確認
        if (!GameManager.Instance.IsPlaying)
            return;

        // 3球使い切った場合
        if (GameManager.Instance.RemainingCoinCount <= 0)
        {
            Debug.Log("3球すべて失敗！GAME OVER");

            GameManager.Instance.GameOver();
            return;
        }

        // まだ残りの球がある場合
        CoinSpawner spawner = FindAnyObjectByType<CoinSpawner>();

        if (spawner != null)
        {
            spawner.EnableSpawn();
        }
        else
        {
            Debug.LogError("CoinSpawnerが見つかりません！");
        }
    }
}