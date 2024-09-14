using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroStats : MonoBehaviour
{
    private int health;
    private int healthMax;
    [SerializeField] private int attack;
    [SerializeField] private int speed;
    // Start is called before the first frame update
    void Start()
    {
        health = 100;
        healthMax = 100;
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
    }
    public int getAttack(){
        return attack;
    }
    public int getSpeed(){
        return speed;
    }
}
