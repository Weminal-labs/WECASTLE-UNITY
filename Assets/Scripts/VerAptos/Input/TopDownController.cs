using UnityEngine;

public class TopDownController : MonoBehaviour
{
    public Rigidbody rb;
    private HeroStats Stats;

    private bool isFacingRight;
    private Vector2 movement;

    private bool isAttack;


    [SerializeField]
    private JoyStickComponent joystick;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Stats = this.GetComponent<HeroStats>();
        isFacingRight = true;
        isAttack = false;
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
        if (joystick.joyStickVec != Vector2.zero && !isAttack)
        {
            movement = joystick.joyStickVec;
        }
        if (!isAttack && Input.GetKeyDown(KeyCode.K)){
            isAttack = !isAttack;
            this.transform.GetChild(0).GetComponent<AnimationController>().setBeginAttackAnimation();
        }

        Flip();
    }

    public void SetPos()
    {
        if (movement != Vector2.zero && !isAttack)
        {
            Vector3 movement3D = new Vector3(movement.x, movement.y, 0); // Convert to Vector3
            rb.MovePosition(rb.position + movement3D * Stats.getSpeed() * Time.fixedDeltaTime);
            this.transform.GetChild(0).GetComponent<AnimationController>().setRunAnimation(1);
        }else{
            rb.velocity = Vector2.zero;
            this.transform.GetChild(0).GetComponent<AnimationController>().setRunAnimation(0);
        }
    }

    public void Flip()
    {
        if(!isAttack)
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

    public void setAttack(bool isAttack)
    {
        this.isAttack = isAttack;
    }
}
