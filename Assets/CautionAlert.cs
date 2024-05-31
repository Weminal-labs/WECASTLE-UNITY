using UnityEngine;

public class CautionAlert : MonoBehaviour
{
    public Transform box;
    private void OnEnable()
    {
        box.localPosition = new Vector2(0, -Screen.height);
        box.LeanMoveLocalY(0, 0.5f).setEaseOutExpo().delay = 0.1f;
    }

    private void OnDisable()
    {
        box.LeanMoveLocalY(-Screen.height, 0.5f).setEaseInExpo();

    }
}
