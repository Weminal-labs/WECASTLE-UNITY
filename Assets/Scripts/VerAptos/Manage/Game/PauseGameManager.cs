using UnityEngine;

public class PauseGameManager : MonoBehaviour
{
    public static PauseGameManager instance { get; private set; }

    private bool isPaused = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    // Get pause status
    public bool IsPaused()
    {
        return isPaused;
    }
    // Pause game
    public void PauseGame()
    {
        Time.timeScale = 0f;  // Freeze the game

        isPaused = true;
    }
    // Resume game
    public void ResumeGame()
    {
        Time.timeScale = 1f;  // Unfreeze the game

        isPaused = false;
    }
}
