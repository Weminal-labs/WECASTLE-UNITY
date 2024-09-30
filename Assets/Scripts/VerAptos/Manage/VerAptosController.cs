using UnityEngine;
using System.Collections;
using TMPro;
using System.Runtime.InteropServices;

public class VerAptosController : MonoBehaviour
{
    public static VerAptosController instance;
    public int wave = 1;
    private int points;
    
    [SerializeField] private float waveInterval = 15f; // Time between waves in seconds

    public GameObject loseScreen;
    public TextMeshProUGUI waveText;
    public TextMeshProUGUI waveLoseText;
    public TextMeshProUGUI pointsText;

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
    }

    private void Start()
    {
        StartCoroutine(IncreaseWaveCoroutine());
    }

    private IEnumerator IncreaseWaveCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(waveInterval);
            IncreaseWave();
        }
    }

    private void IncreaseWave()
    {
        wave++;
        waveText.SetText("Wave:\n" + wave);
        // You can add any additional logic here that should occur when the wave increases
    }

    // You can keep the Update method if you need it for other purposes
    private void Update()
    {
        
    }

    public void ShowLoseScreen()
    {
        loseScreen.SetActive(true);
        waveLoseText.SetText($"Wave: {wave}");
        pointsText.SetText($"Points: {points}");
        PushRewardForPlayer(points);
    }

    public void updatePoints(int type)
    {
        int calPoints = type * 4 +1;
        points += calPoints;
    }
    [DllImport("__Internal")]
    public static extern void PushRewardForPlayer(int Points);
}