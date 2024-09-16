using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroStats : MonoBehaviour
{
    [Header("Stats")]
    private int health;
    private int healthMax;
    [SerializeField] private int attack;
    [SerializeField] private int speed;


    [Header("Component")]
    [SerializeField]
    private GameObject UIHP;
    private Slider sliderHP;
    private Slider sliderHPEffect;

    // Start is called before the first frame update
    void Start()
    {
        health = 100;
        healthMax = 100;
        sliderHP = UIHP.GetComponent<Slider>();
        sliderHP.maxValue = healthMax;
        sliderHP.value = health;
        sliderHPEffect = UIHP.transform.GetChild(1).GetComponent<Slider>();
        sliderHPEffect.maxValue = healthMax;
        sliderHPEffect.value = health;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void setHealth(int health){
        this.health+=health;
        if(this.health > healthMax){
            this.health = healthMax;
        }
        if(this.health <= 0){
            this.health = 0;
            Debug.Log("Hero is dead");
        }
        sliderHP.value = health;
        StartCoroutine(hpSliderEffect());
    }
    public int getAttack(){
        return attack;
    }
    public int getSpeed(){
        return speed;
    }
    IEnumerator hpSliderEffect()
    {
        while(sliderHPEffect.value != sliderHP.value){
            if(sliderHPEffect.value < sliderHP.value){
                sliderHPEffect.value += 1;
            }else{
                sliderHPEffect.value -= 1;
            }
            yield return new WaitForSeconds(0.01f);
        }
    }
}
