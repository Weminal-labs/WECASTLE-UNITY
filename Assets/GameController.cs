using Newtonsoft.Json;
using System.Collections;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private string id;
    private PlayerInfo playerInfo;
    [SerializeField]
    private Button logOut;
    public GameObject text;
    [SerializeField]
    private GameObject lv, exp, wood, gold, meat;
    [SerializeField]
    private string json;
    // Start is called before the first frame update

    [SerializeField] UnityEvent OnEnter;
    [SerializeField] UnityEvent OnLeave;
    [SerializeField] GameObject spawnController;
    float spawnInterval = 330;
    void Start()
    {
        /*RequestAddress();*/
        ReceiveAddress("SSSS");
        if (logOut != null)
        {
            logOut.onClick.AddListener(RequestLogOut);
        }

        StartCoroutine(SpawnControllerRoutine());

    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator SpawnControllerRoutine()
    {
        while (true)
        {
            OnEnter?.Invoke();

            // Instantiate a new SpawnController
            GameObject newSpawnController = Instantiate(spawnController);

            // Wait for 20 seconds
            yield return new WaitForSeconds(30f);

            // Destroy the existing SpawnController
            Destroy(newSpawnController);

            // Start counting down for the next instantiation
            yield return new WaitForSeconds(spawnInterval - 30f);
        }
    }

    public void ReceiveAddress(string id)
    {
        this.id = id;
        playerInfo = new PlayerInfo(id);
        PlayerInfoJson info = new PlayerInfoJson(playerInfo);
        Debug.Log(JsonConvert.SerializeObject(info));
        text.GetComponent<TextMeshProUGUI>().SetText(id);
        lv.GetComponent<TextMeshProUGUI>().SetText(playerInfo.getLv().ToString());
        exp.GetComponent<Slider>().maxValue = playerInfo.getMaxExp();
        exp.GetComponent<Slider>().value = playerInfo.getExp();
        wood.GetComponent<TextMeshProUGUI>().SetText(playerInfo.getCurWood().ToString());
        meat.GetComponent<TextMeshProUGUI>().SetText(playerInfo.getCurMeat().ToString());
        gold.GetComponent<TextMeshProUGUI>().SetText(playerInfo.getCurGold().ToString());
    }
    [DllImport("__Internal")]
    public static extern void RequestLogOut();
    [DllImport("__Internal")]
    public static extern void RequestAddress();
    public void setMaterials(int m, int g, int w)
    {
        playerInfo.setCurMeat(m);
        playerInfo.setCurGold(g);
        playerInfo.setCurWood(w);
        wood.GetComponent<TextMeshProUGUI>().SetText(playerInfo.getCurWood().ToString());
        meat.GetComponent<TextMeshProUGUI>().SetText(playerInfo.getCurMeat().ToString());
        gold.GetComponent<TextMeshProUGUI>().SetText(playerInfo.getCurGold().ToString());
    }
    public PlayerInfo getPlayer()
    {
        return playerInfo;
    }


}
