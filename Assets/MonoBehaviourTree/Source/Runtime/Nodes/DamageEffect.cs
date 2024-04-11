using System.Collections;
using UnityEngine;

public class DamageEffect : MonoBehaviour
{
    public float flashDuration = 0.1f; // Duration of each flash
    public Color flashColor = Color.red; // Color to flash
    private Material material; // Reference to the mob's material
    private Color originalColor; // Original color of the mob

    private void Start()
    {
        // Get the material of the mob
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            material = renderer.material;
            originalColor = material.color;
        }
    }

    public void Flash()
    {
        if (material != null)
        {
            // Start the flashing coroutine
            StartCoroutine(FlashCoroutine());
        }
    }

    private IEnumerator FlashCoroutine()
    {
        // Flash the mob's color to the flash color
        material.color = flashColor;

        // Wait for the specified flash duration
        yield return new WaitForSeconds(flashDuration);

        // Return the mob's color to its original color
        material.color = originalColor;
    }
}
