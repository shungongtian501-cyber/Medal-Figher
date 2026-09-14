using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CountdownManager : MonoBehaviour
{
    [SerializeField] private Text countdownText;
    [SerializeField] private CoinSpawner coinSpawner;
    [SerializeField] private TimerManager timerManager;

    private void Start()
    {
        // カウントダウン中はコインを出せない
        coinSpawner.CanSpawn = false;

        // Timerも停止
        timerManager.StopTimer();

        StartCoroutine(Countdown());
    }

    private IEnumerator Countdown()
    {
        countdownText.gameObject.SetActive(true);

        countdownText.text = "3";
        yield return new WaitForSeconds(1f);

        countdownText.text = "2";
        yield return new WaitForSeconds(1f);

        countdownText.text = "1";
        yield return new WaitForSeconds(1f);

        countdownText.text = "START!";

        // START! と同時にTimer開始
        timerManager.ResumeTimer();

        // コイン生成も開始
        coinSpawner.CanSpawn = true;

        yield return new WaitForSeconds(0.5f);

        countdownText.gameObject.SetActive(false);
    }
}