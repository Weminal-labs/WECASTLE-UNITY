using Newtonsoft.Json;
using System.Collections;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] lockHero;
    public GameObject pageMain, pageHero, loadingContract;
    public Button pInfo, pHero;
    public ChoseHero heroControll;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < lockHero.Length; i++)
        {
            lockHero[i].gameObject.SetActive(heroControll.GetLock()[i]);
        }
        pInfo.onClick.AddListener(setPageMain);
        pHero.onClick.AddListener(setPageHero);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void setPageMain()
    {
        pageMain.SetActive(true);
        pageHero.SetActive(false);
    }
    public void setPageHero()
    {
        pageMain.SetActive(false);
        pageHero.SetActive(true);
    }
    /*[DllImport("__Internal")]
    public static extern void RequestLogOut();*/
    public void upDateLock()
    {
        for (int i = 0; i < lockHero.Length; i++)
        {
            lockHero[i].gameObject.SetActive(heroControll.GetLock()[i]);
        }
    }
    public void DoneLoad()
    {
        loadingContract.SetActive(false);
    }
}
