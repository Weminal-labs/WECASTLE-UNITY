using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class HeroStats : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int attack;
    [SerializeField] private int speed;
    private int currentHealth;

    [Header("Level Up")]
    [SerializeField] private List<int> levelUpList = new List<int>{1,1,1,0,0};
    [SerializeField] private int exp = 0;
    [SerializeField] private int expToNextLevel = 10;
    [SerializeField] private int level = 1;

    [Header("UI Components")]
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Slider healthEffectSlider;
    [SerializeField] private Slider expSlider;
    [SerializeField] private LevelUpUI levelUpUI;

    private void Start()
    {
        InitializeHealth();
        SetupHealthSliders();
        SetupExpSlider();
    }

    private void InitializeHealth()
    {
        currentHealth = maxHealth;
    }

    private void SetupExpSlider()
    {
        if (expSlider != null)
        {
            expSlider.maxValue = expToNextLevel;
            expSlider.value = exp;
        }
    }
    private void SetupHealthSliders()
    {
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }

        if (healthEffectSlider != null)
        {
            healthEffectSlider.maxValue = maxHealth;
            healthEffectSlider.value = currentHealth;
        }
    }

    public void setHealth(int amount)
    {
        if(PauseGameManager.instance.IsPaused()){
            return;
        }
        currentHealth += amount;

        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }

        if (currentHealth <= 0)
        {
            Debug.Log("Hero is dead");
        }
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        StartCoroutine(UpdateHealthEffectSlider());
    }

    private void AddExp(int amount)
    {
        exp += amount;
        if (exp >= expToNextLevel)
        {
            exp = 0;
            expToNextLevel *= 2;
            levelUpUI.LevelUp();
        }
        SetupExpSlider();
    }

    private IEnumerator UpdateHealthEffectSlider()
    {
        if (healthEffectSlider == null) yield break;
        yield return new WaitForSeconds(0.5f);
        while (healthEffectSlider.value > currentHealth)
        {
            healthEffectSlider.value -= 1;
            yield return new WaitForSeconds(0.1f);
        }
        healthEffectSlider.value = currentHealth;
    }

    public int GetAttack() => attack;

    public int GetSpeed() => speed;
    public void LevelUp(int index)
    {
        switch(index){
            case 0:
                maxHealth += 50;
                break;
            case 1:
                attack += 10;
                break;
            default:
                break;
        }
        levelUpList[index]++;
    }
    public List<int> getLevelUpList()
    {
        return levelUpList;
    }
}