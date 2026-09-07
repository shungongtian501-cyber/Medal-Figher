using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private HoleBase[] holes;
    [SerializeField] private Image[] rewardLights;
    [SerializeField] float _waittime = 0.2f;
    [SerializeField] UnityEvent Activate;
    public int feverCoin = 0;

    public int playerCoin = 30;     // 所持コイン
    public int earnedCoin = 0;      // 換金予定コイン

    [SerializeField] private Text haveCoinText;
    [SerializeField] private Text Winstext;

    [SerializeField] private GameObject Infomationtext;
    public static GameManager Instance;

    int _bairituCountor = 1; 
    public bool IsOpeningFloor { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (CoinData.Instance != null)
        {
            playerCoin += CoinData.Instance.feverCoin;
            CoinData.Instance.feverCoin = 0;
        }
        else
        {
            Debug.LogWarning("CoinData.Instance が存在しません！");
        }

        UpdateUI();
    }

    public void UpdateUI()
    {
        haveCoinText.text = "所持コイン枚数：" + playerCoin.ToString();
        Winstext.text = "ゲット" + earnedCoin.ToString();
    }

    public void OpenAllFloors()
    {
        StartCoroutine(OpenAllFloorsCoroutine());
    }

    private IEnumerator OpenAllFloorsCoroutine()
    {
        // 穴判定停止
        IsOpeningFloor = true;

        // 演出待ち
        yield return new WaitForSeconds(_waittime);

        foreach (HoleBase hole in holes)
        {
            hole.OpenFloor();
        }

        // コインが落ち切るまで待つ
        yield return new WaitForSeconds(1.0f);

        // 新しいゲーム開始
        CloseAllFloors();
        BingoManager.Instance.ResetBingo();
    }

    public void CloseAllFloors()
    {
        foreach (HoleBase hole in holes)
        {
            hole.ResetHole();
        }

        // 次のゲーム開始
        IsOpeningFloor = false;
        _bairituCountor += 1;
    }

    // ビンゴ報酬追加
    public void AddReward(int coin)
    {
        earnedCoin += coin;
        UpdateUI();
    }

    // 換金ボタン
    public void CashOut()
    {
        playerCoin += earnedCoin;
        earnedCoin = 0;

        ShowRewardLight(0);

        UpdateUI();

        OpenAllFloors();
        BingoManager.Instance.ResetBingo();
    }
    public void SetEarnedCoin(int coin)
    {
        earnedCoin = coin;
        ShowRewardLight(coin);
        UpdateUI();
    }
    public void ShowRewardLight(int reward)
    {
        Debug.Log($"配列数 : {rewardLights.Length}");

        for (int i = 0; i < rewardLights.Length; i++)
        {
            if (rewardLights[i] == null)
            {
                Debug.LogError($"rewardLights[{i}] が設定されていません！");
                continue;
            }

            Color color = rewardLights[i].color;
            color.a = 0.3f;
            rewardLights[i].color = color;
        }

        switch (reward)
        {
            case 4: SetLight(0); break;
            case 6: SetLight(1); break;
            case 10: SetLight(2); break;
            case 15: SetLight(3); break;
            case 20: SetLight(4); break;
            case 30: SetLight(5); break;
            case 50: SetLight(6); break;
            case 99: SetLight(7); break;
        }
    }
    private void SetLight(int index)
    {
        Color color = rewardLights[index].color;
        color.a = 1f;
        rewardLights[index].color = color;
    }
    public void Finish()
    {
        Debug.Log("Finish");
        Infomationtext.SetActive(true);
        //Application.Quit();
    }
    public void Cancel()
    {
        Infomationtext.SetActive(false);
    }
    public void Result()
    {
        SceneManager.LoadScene("ResultScene");
        Infomationtext.SetActive(false);
    }
}