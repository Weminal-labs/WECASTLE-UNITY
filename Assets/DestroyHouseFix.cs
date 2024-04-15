using UnityEngine;

public class DestroyHouseFix : MonoBehaviour
{
    [SerializeField]
    private int hp, maxHp;
    [SerializeField]
    private GameObject nomalHouse;
    // Start is called before the first frame update
    private void Start()
    {
        hp = 400;
        maxHp = 400;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void takeDame(int Damage)
    {
        if (hp - Damage < 0)
        {
            FixBuilding();
        }
        else
        {
            hp -= Damage;
        }

    }
    public void FixBuilding()
    {
        nomalHouse.SetActive(true);
        nomalHouse.GetComponent<HouseStat>().ReBackHouse();
        this.gameObject.SetActive(false);
    }
    public int getHp()
    {
        return hp;
    }
    public void ReBackHouse()
    {
        this.hp = this.maxHp;
    }
}
