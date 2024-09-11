using UnityEngine;

public class TopDownController : MonoBehaviour
{
    public float moveSpeed = 1.0f;
    public Rigidbody rb;

    private bool isFacingRight;
    private Vector2 movement;

    [SerializeField]
    private JoyStickComponent joystick;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        isFacingRight = true;
    }

    void Update()
    {
        GetInput();
    }

    private void FixedUpdate()
    {
        SetPos();
    }

    public void GetInput()
    {
        // Get keyboard input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Override with joystick input if present
        if (joystick.joyStickVec != Vector2.zero)
        {
            movement = joystick.joyStickVec;
        }

        Flip();
    }

    public void SetPos()
    {
        if (movement != Vector2.zero)
        {
            transform.GetChild(0).GetComponent<Animator>().SetBool("Running", true);
            Vector3 movement3D = new Vector3(movement.x, movement.y, 0); // Convert to Vector3
            rb.MovePosition(rb.position + movement3D * moveSpeed * Time.fixedDeltaTime);
        }
        else
        {
            transform.GetChild(0).GetComponent<Animator>().SetBool("Running", false);
        }
    }

    public void Flip()
    {
        if ((movement.x < 0 && isFacingRight) || (movement.x > 0 && !isFacingRight))
        {
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
            isFacingRight = !isFacingRight;
        }
    }
}
