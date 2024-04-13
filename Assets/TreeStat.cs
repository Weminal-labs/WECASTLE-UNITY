using UnityEngine;

public class TreeStat : MonoBehaviour
{
    [SerializeField]
    private int hp, wood;
    [SerializeField]
    private GameObject w_pref;
    // Start is called before the first frame update
    [SerializeField]
    private GameObject treeChunk;
    void Start()
    {
        hp = Random.Range(50, 100);
        wood = Random.Range(3, 7);
    }

    public void takeDame(int damage)
    {
        if (hp - damage < 0)
        {
            hp -= damage;
            float x;
            float y;
            for (int i = 0; i < wood; i++)
            {
                x = Random.RandomRange(this.transform.position.x - 2f, this.transform.position.x + 2f);
                y = Random.RandomRange(this.transform.position.y - 2f, this.transform.position.y + 2f);
                Instantiate(w_pref, new Vector3(x, y, transform.position.z), Quaternion.identity);
                this.gameObject.GetComponent<Animator>().SetTrigger("Chopped");

            }
            Instantiate(treeChunk, transform.position, Quaternion.identity);
            Destroy(gameObject);

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
