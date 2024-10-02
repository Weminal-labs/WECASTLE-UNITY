using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
                GameObject spawnedElement = Instantiate(selectedElements[i], spotLevelUpUIElements[i].transform);
                int index = spawnedElement.GetComponent<Upgrade>().GetIndex();
                int value = GameObject.FindGameObjectWithTag("Ally").GetComponent<HeroStats>().getLevelUpList()[index];
                spawnedElement.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText("Level:\n" + (value + 1).ToString());
                spotLevelUpUIElements[i].GetComponent<Button>().onClick.RemoveAllListeners();
                spotLevelUpUIElements[i].GetComponent<Button>().onClick.AddListener(() => OnSelectOptionLevelUp(index));
            }
        }
    }
    public void LevelUp()
    {
        PauseGameManager.instance.PauseGame();
        SpawnRandomElements();
        levelUpUI.SetActive(true);
    }
    public void OnSelectOptionLevelUp(int index)
    {
        GameObject.FindGameObjectWithTag("Ally").GetComponent<HeroStats>().LevelUp(index);
        levelUpUI.SetActive(false);
        PauseGameManager.instance.ResumeGame();
    }
}
