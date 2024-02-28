using System;
using UnityEngine;



public class CloudMovement : MonoBehaviour
{
    public float speed = 2.0f;
    private SpriteRenderer spriteRenderer;
    public float transparentStartX = -18.0f;
    public float minTransparency = 0.3f; // Set your desired minimum transparency here
    public float transparentEndX = 18.0f;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

    }
    void Update()
    {
        MoveCloud();  
    }

    void MoveCloud()
    {
        // Move the cloud from left to right
        transform.Translate(Vector3.right * speed * Time.deltaTime);

        // Check if the cloud is out of the screen, then reset its position
        if (gameObject.transform.position.x > 30)
        {
            Destroy(this.gameObject);
        }
        // Check if the cloud is between -10 and 0 on the X-axis
        if (transform.position.x > transparentStartX && transform.position.x < -4)
        {
            // Calculate transparency based on the position
            float transparency = Mathf.Max(minTransparency, 1.0f - Mathf.InverseLerp(transparentStartX, -4, transform.position.x));

            // Set the transparency to the sprite renderer's color
            Color cloudColor = spriteRenderer.color;
            cloudColor.a = transparency;
            spriteRenderer.color = cloudColor;
        }
        // Check if the cloud is between 0 and recoveryStartX on the X-axis
        if (transform.position.x > 4 && transform.position.x < transparentEndX)
        {
            // Calculate transparency based on the position
            float transparency = Mathf.Max(minTransparency,Mathf.InverseLerp(4, transparentEndX, transform.position.x));

            // Set the transparency to the sprite renderer's color
            Color cloudColor = spriteRenderer.color;
            cloudColor.a = transparency;
            spriteRenderer.color = cloudColor;
        }
    }
}
