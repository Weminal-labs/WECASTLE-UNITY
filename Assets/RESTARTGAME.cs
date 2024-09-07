using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RESTARTGAME : MonoBehaviour
{
    [SerializeField]
    private Button Restart;

    // Start is called before the first frame update
    void Start()
    {
        if (Restart != null)
        {
            Restart.onClick.AddListener(LoadScene);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void LoadScene()
    {
        SceneManager.LoadScene(1);
    }
}
