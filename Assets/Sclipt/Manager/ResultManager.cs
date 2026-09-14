using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultManager : MonoBehaviour
{
    [SerializeField] private Text finalCoinText;

    [SerializeField] private float countUpTime = 0.8f;

    private int finalCoin;

    private void Start()
    {
        finalCoin = PlayerPrefs.GetInt("FinalCoin", 0);

        StartCoroutine(CountUp());
    }

    private IEnumerator CountUp()
    {
        int currentCoin = 0;

        float timer = 0f;

        // 最初は0枚
        finalCoinText.text = "0枚";

        while (timer < countUpTime)
        {
            timer += Time.deltaTime;

            float progress = timer / countUpTime;

            // 0 → 最終枚数
            currentCoin = Mathf.RoundToInt(
                Mathf.Lerp(0, finalCoin, progress)
            );

            finalCoinText.text = currentCoin + "枚";

            yield return null;
        }

        // 最後は必ず正確な枚数
        finalCoinText.text = "結果 :"+ finalCoin + "枚";
    }

    public void Replay()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}