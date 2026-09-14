using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
    [SerializeField] private Text timerText;
    [SerializeField] private float timeLimit = 30f;

    private float timer;
    private bool isRunning = true;

    private void Start()
    {
        timer = timeLimit;
        UpdateTimerUI();
    }

    private void Update()
    {
        if (!isRunning)
            return;

        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            timer = 0f;
            isRunning = false;

            UpdateTimerUI();

            // 時間切れ
            //SceneManager.LoadScene("ResultScene");

            return;
        }

        UpdateTimerUI();
    }

    private void UpdateTimerUI()
    {
        timerText.text = "帰宅まで："　+ timer.ToString("F2");
    }
    public void StopTimer()
    {
        isRunning = false;
    }

    public void ResumeTimer()
    {
        isRunning = true;
    }
}