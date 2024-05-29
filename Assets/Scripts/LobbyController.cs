using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] lockHero;
    public GameObject pageMain, pageHero, loading, loadBar,container;
    public Button pInfo, pHero;
    public ChoseHero heroControll;
    // Start is called before the first frame update
    void Start()
    {
        StaticLobbySend.numHero = 0;
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
    public void upDateLock()
    {
        for (int i = 0; i < lockHero.Length; i++)
        {
            lockHero[i].gameObject.SetActive(heroControll.GetLock()[i]);
        }
    }
    public void mapGoblin()
    {
        StaticLobbySend.numMap = 0;
        loading.SetActive(true);
        container.SetActive(false);
        StartCoroutine(LoadLevelAsync());
    }
    public void mapSkeleton()
    {
        StaticLobbySend.numMap = 1;
        loading.SetActive(true);
        container.SetActive(false);
        StartCoroutine(LoadLevelAsync());
    }
    public void mapTNT()
    {
        StaticLobbySend.numMap = 2;
        loading.SetActive(true);
        container.SetActive(false);
        StartCoroutine(LoadLevelAsync());
    }
    public void mapArmorOrc()
    {
        StaticLobbySend.numMap = 3;
        loading.SetActive(true);
        container.SetActive(false);
        StartCoroutine(LoadLevelAsync());
    }
    public void mapEliteOrc()
    {
        StaticLobbySend.numMap = 4;
        loading.SetActive(true);
        container.SetActive(false);
        StartCoroutine(LoadLevelAsync());
    }
    IEnumerator LoadLevelAsync()
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(2);

        while (!loadOperation.isDone)
        {
            float prgressValue = Mathf.Clamp01(loadOperation.progress / 0.9f);
            loadBar.GetComponent<Slider>().value = prgressValue;
            yield return null;
        }
    }
}
