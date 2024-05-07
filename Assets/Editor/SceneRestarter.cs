using UnityEditor;
using UnityEngine;
public class ScenceRestarter : MonoBehaviour
{
    [MenuItem("Helpers/Restart Scene #R")]
    private static void RestartScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}
