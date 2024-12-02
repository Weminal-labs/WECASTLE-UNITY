using UnityEngine;
using System.Collections;
using TMPro;
using System.Runtime.InteropServices;

public class VerAptosController : MonoBehaviour
{
    public static VerAptosController instance;
    public int wave = 1;
    private int points;
    
    [SerializeField] private float waveInterval = 20f; // Time between waves in seconds

    public GameObject loseScreen;
    public GameObject loseIcon, winIcon;
    public TextMeshProUGUI textResult;
    public TextMeshProUGUI waveText;
    public TextMeshProUGUI waveTimeText;
    public TextMeshProUGUI waveLoseText;
    public TextMeshProUGUI pointsText;

    [Header("Place Holder")]
    [SerializeField] LevelUpUI levelUpUI;
    [SerializeField] Transform iconHolder;
    [SerializeField] Transform playerPos;
    [Header("List Replace")]
    [SerializeField] GameObject[] listIcon;
    [SerializeField] GameObject[] listMainSkill;
    [SerializeField] GameObject[] listCharacter;
    [Header("UI")]
    public GameObject loadingScreen;

    public bool isDone;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            points = 0;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        isDone = false;
        /*SettingForGameOpen(0);*/
        

    }

    private void Start()
    {
        GameCanPlay();/*
        loadingScreen.SetActive(true);*/
    }

    private IEnumerator IncreaseWaveCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.0f);
            if(waveInterval>0)
            {
                waveInterval -= 1.0f;
                waveTimeText.SetText(waveInterval.ToString());
            }
            else
            {
                if (wave >= 15)
                {
                    break;
                }
                IncreaseWave();
                waveInterval = 20.0f;
            }
        }
    }

    private void IncreaseWave()
    {
        wave++;
        waveText.SetText("Wave:\n" + wave);
    }

    // You can keep the Update method if you need it for other purposes
    private void Update()
    {
        
    }

    public void ShowLoseScreen()
    {
        loseScreen.SetActive(true);
        loseIcon.SetActive(true);
        textResult.SetText("You Lose");
        waveLoseText.SetText($"Wave: {wave}");
        pointsText.SetText($"Points: {points}");
        if (!isDone)
        {
            StartCoroutine(updatePointsCoroutine());
        }
    }
    public void ShowWinScreen()
    {
        loseScreen.SetActive(true);
        winIcon.SetActive(true);
        textResult.SetText("Victory");
        waveLoseText.SetText($"Wave: {wave}");
        pointsText.SetText($"Points: {points}");
        if (!isDone)
        {
            StartCoroutine(updatePointsCoroutine());
        }
    }
    public IEnumerator updatePointsCoroutine()
    {
        isDone = true;
        yield return new WaitForSeconds(1.0f);
        PushRewardForPlayer(points);
    }

    public void updatePoints(int type)
    {
        if(type == -1)
        {
            points += 300;
            ShowWinScreen();
        }
        else
        {
            int calPoints = type * 4 +1;
            points += calPoints;
        }
    }


    public void SettingForGameOpen(int id)
    {
        levelUpUI.levelUpUIElements[2] = listMainSkill[id];
        Instantiate(listIcon[id], iconHolder);
        Instantiate(listCharacter[id], playerPos);
        Time.timeScale = 1.0f;
        StartCoroutine(IncreaseWaveCoroutine());
        loadingScreen.SetActive(false);
    }

    [DllImport("__Internal")]
    public static extern void GameCanPlay();

    [DllImport("__Internal")]
    public static extern void PushRewardForPlayer(int points);

    public static void PushRewardForPlayerJS(int points)
    {
        #if UNITY_WEBGL && !UNITY_EDITOR
            PushRewardForPlayer(points);
        #else
            Debug.Log($"PushRewardForPlayer called with {points} points");
        #endif
    }
}