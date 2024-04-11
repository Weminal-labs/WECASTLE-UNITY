using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static CodeMonkey.Utils.UI_TextComplex;

public class TreeStat : MonoBehaviour
{
    [SerializeField]
    private int hp, wood;
    [SerializeField]
    private GameObject w_pref;
    // Start is called before the first frame update
    void Start()
    {
        hp = Random.Range(50, 100);
        wood = Random.Range(1, 5);
    }

    public void takeDame(int damage)
    {
        if (hp-damage <0)
        {
            hp -= damage;
            float x;
            float y;
            for (int i = 0;i<wood; i++)
            {
                x = Random.RandomRange(this.transform.position.x - 5f, this.transform.position.x + 5f);
                y = Random.RandomRange(this.transform.position.y - 5f, this.transform.position.y + 5f);
                Instantiate(w_pref,new Vector3(x,y,transform.position.z),Quaternion.identity);
                this.gameObject.GetComponent<Animator>().SetTrigger("Chopped");
                Destroy(this.gameObject);
            }
        }
        else
        {
            this.gameObject.GetComponent<Animator>().SetTrigger("Hit");
            hp -= damage;
        }
    }
    public int getHp()
    {
        return hp;
    }
}
