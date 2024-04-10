using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;

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
    void Start()
    {
        /*RequestAddress();*/
        ReceiveAddress("SSSS");
        if (logOut != null)
        {
            logOut.onClick.AddListener(RequestLogOut);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReceiveAddress(string id)
    {
        this.id = id;
        playerInfo = new PlayerInfo(id);
        Debug.Log(JsonConvert.SerializeObject(playerInfo));
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

}
