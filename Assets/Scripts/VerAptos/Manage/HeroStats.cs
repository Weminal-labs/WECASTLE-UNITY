using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HeroStats : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int attack;
    [SerializeField] private int speed;
    private int currentHealth;

    [Header("UI Components")]
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Slider healthEffectSlider;

    private void Start()
    {
        InitializeHealth();
        SetupHealthSliders();
    }

    private void InitializeHealth()
    {
        currentHealth = maxHealth;
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
}