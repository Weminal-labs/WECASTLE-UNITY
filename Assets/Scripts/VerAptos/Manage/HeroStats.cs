using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HeroStats : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int attack;
    [SerializeField] private int speed;
    private int currentHealth;

    [Header("Level Up")]
    [SerializeField] private List<int> levelUpList = new List<int> { 1, 1, 1, 0, 0 };
    [SerializeField] private int exp = 0;
    [SerializeField] private int expToNextLevel = 10;
    [SerializeField] private int level = 1;

    [Header("UI Components")]
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Slider healthEffectSlider;
    [SerializeField] private Slider expSlider;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private LevelUpUI levelUpUI;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI extText;

    private void Start()
    {
        InitializeHealth();
        SetupHealthSliders();
        SetupExpSlider();
        if (extText != null)
        {
            extText.SetText(exp + " / " + expToNextLevel);
        }
        if (healthText != null)
        {
            healthText.SetText(currentHealth + " / " + maxHealth);
        }
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
        if (healthText != null)
        {
            healthText.SetText(currentHealth + " / " + maxHealth);
        }
    }

    public void setHealth(int amount)
    {
        if (PauseGameManager.instance.IsPaused())
        {
            return;
        }
        currentHealth += amount;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
        }
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        if (healthText != null)
        {
            healthText.SetText(currentHealth + " / " + maxHealth);
        }
        if(currentHealth<=0)
        {
            Die();
            VerAptosController.instance.ShowLoseScreen();
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
            level++;
            levelText.SetText("Lv:\n" + level);
            levelUpUI.LevelUp();
        }
        if(extText != null)
        {
            extText.SetText(exp + " / " + expToNextLevel);
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
        switch (index)
        {
            case 0:
                maxHealth += levelUpList[0] * 20;
                currentHealth = maxHealth;
                healthSlider.maxValue = maxHealth;
                healthEffectSlider.maxValue = maxHealth;
                healthSlider.value = maxHealth;
                healthEffectSlider.value = maxHealth;
                if (healthText != null)
                {
                    healthText.SetText(currentHealth + " / " + maxHealth);
                }
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
    public void Die()
    {
        this.transform.GetChild(0).gameObject.GetComponent<Animator>().SetTrigger("Death");
    }
    public void setHealthSkill(int amount)
    {
        if(currentHealth - amount <= 0)
        {
            currentHealth = 1;
        }
        else
        {
            currentHealth -= amount;
        }
        if (healthText != null)
        {
            healthText.SetText(currentHealth + " / " + maxHealth);
        }
        StartCoroutine(UpdateHealthEffectSlider());
    }
    public int getMaxHealth()
    {
        return maxHealth;
    }
}