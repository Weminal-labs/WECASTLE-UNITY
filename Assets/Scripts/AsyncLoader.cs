using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AsyncLoader : MonoBehaviour
{
    public GameObject loading, container, loadBar;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void mapGoblin()
    {
        StaticLobbySend.numMap = 0;
        loading.SetActive(true);
        container.SetActive(false);
        StartLoadScene();
    }
    public void mapSkeleton()
    {
        StaticLobbySend.numMap = 1;
        loading.SetActive(true);
        container.SetActive(false);
        StartLoadScene();
        
    }
    public void mapTNT()
    {
        StaticLobbySend.numMap = 2;
        loading.SetActive(true);
        container.SetActive(false);
        StartLoadScene();
        
    }
    public void mapArmorOrc()
    {
        StaticLobbySend.numMap = 3;
        loading.SetActive(true);
        container.SetActive(false);
        StartLoadScene();
        
    }
    public void mapEliteOrc()
    {
        StaticLobbySend.numMap = 4;
        loading.SetActive(true);
        container.SetActive(false);
        StartLoadScene();
        
    }
    public async void StartLoadScene()
    {
        StartCoroutine(LoadLevelAsync());
    }
    IEnumerator LoadLevelAsync()
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(2);
        while (!loadOperation.isDone)
        {
            float prgressValue = Mathf.Clamp01(loadOperation.progress / 0.9f);
            loadBar.GetComponent<Slider>().value = prgressValue;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
