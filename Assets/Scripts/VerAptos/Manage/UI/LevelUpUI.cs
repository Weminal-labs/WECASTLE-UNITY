using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MBT;
using UnityEngine;
using UnityEngine.EventSystems;

public class LevelUpUI : MonoBehaviour
{
    public GameObject levelUpUI;

    public Transform[] spotLevelUpUIElements;

    public List<GameObject> levelUpUIElements;
    // Start is called before the first frame update
    void Start()
    {
        LevelUp();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void SpawnRandomElements()
    {
        // Clear existing elements
        foreach (Transform spot in spotLevelUpUIElements)
        {
            foreach (Transform child in spot)
            {
                Destroy(child.gameObject);
            }
        }

        // Randomly select 3 elements
        List<GameObject> selectedElements = levelUpUIElements.OrderBy(x => Random.value).Take(3).ToList();

        // Spawn selected elements
        for (int i = 0; i < selectedElements.Count; i++)
        {
            if (i < spotLevelUpUIElements.Length)
            {
                GameObject spawnedElement = Instantiate(selectedElements[i], spotLevelUpUIElements[i]);
            }
        }
    }
    public void LevelUp()
    {
        SpawnRandomElements();
        levelUpUI.SetActive(true);
        PauseGameManager.instance.PauseGame();
    }
    public void OnSelectOptionLevelUp(){
        int index = EventSystem.current.currentSelectedGameObject.transform.GetChild(0).GetComponent<Upgrade>().GetIndex();
        GameObject.Find("Ally").GetComponent<HeroStats>().LevelUp(index);
        levelUpUI.SetActive(false);
        PauseGameManager.instance.ResumeGame();
    }
}
