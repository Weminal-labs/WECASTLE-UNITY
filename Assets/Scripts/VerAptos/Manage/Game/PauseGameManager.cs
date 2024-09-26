using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGameManager : MonoBehaviour
{
    public static PauseGameManager instance {get; private set;}

    private bool isPaused = false;

    private void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(this);
        } else {
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
    public bool IsPaused() {
        return isPaused;
    }
    // Pause game
    public void PauseGame() {
        isPaused = true;
    }
    // Resume game
    public void ResumeGame() {
        isPaused = false;
    }
}
