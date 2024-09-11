using GameAudioScriptingEssentials;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginController : MonoBehaviour
{
    [SerializeField]
    private Button loginButton;
    [SerializeField] private AudioClipRandomizer randomizer;
    private void Start()
    {
        randomizer.PlaySFX();
        /*if(loginButton != null)
        {
            loginButton.onClick.AddListener(RequestLogin);
        }*/
    }
    /*[DllImport("__Internal")]
    public static extern void RequestLogin();*/
    private void LoadScene()
    {
        /*randomizer.StopSFX();*/
        SceneManager.LoadScene(1);
    }
}
