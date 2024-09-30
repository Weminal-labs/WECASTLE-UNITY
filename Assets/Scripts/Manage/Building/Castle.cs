using UnityEngine;

public class Castle : MonoBehaviour
{
    [SerializeField]
    private Sprite icon;
    [SerializeField]
    private int hp, maxHp;
    [SerializeField]
    private string name;
    [SerializeField]
    private GameObject[] fire;
    private int countFire;

    [SerializeField]
    private GameObject destroyCastle;

    private void Start()
    {
        hp = 1000;
        maxHp = 1000;
    }

    public void TakeDame(int Damage)
    {
        if (hp - Damage < 0)
        {
            DesTroyBuilding();
        }
        else
        {
            hp -= Damage;
        }
        if (hp < 750 && countFire < 6)
        {
            fire[countFire].SetActive(true);
            countFire += 1;
        }
    }
    public void DesTroyBuilding()
    {
        foreach (GameObject mob in this.gameObject.GetComponent<MobInBuilding>().getAllGameObjectsMob())
        {
            mob.SetActive(true);
            mob.GetComponent<MobStatus>().mobOutBuilding();
        }
        Instantiate(destroyCastle, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
