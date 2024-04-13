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
        hp = 700;
        maxHp = 700;
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
        Instantiate(nomalHouse, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
    public int getHp()
    {
        return hp;
    }
}
