using UnityEngine;
using System.Collections;

public class TopDownController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private JoyStickComponent joystick;
    private HeroStats stats;
    private AnimationController animationController;

    [Header("Movement")]
    private Vector2 movement;
    public bool isFacingRight = true;

    [Header("Attack")]
    [SerializeField] private bool isAttack;
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private float attackAngle = 60f;
    [SerializeField] private float attackCooldown = 2f;
    [SerializeField] private LayerMask enemyLayer;

    private void Start()
    {
        isAttack = false;
        rb = GetComponent<Rigidbody>();
        stats = GetComponent<HeroStats>();
        
    }

    private void Update()
    {
        if (animationController == null)
        {
            animationController = transform.GetChild(0).GetComponent<AnimationController>();
        }
        if (PauseGameManager.instance.IsPaused())
        {
            return;
        }
        GetInput();
        // if (!isAttack)
        // {
        //     GetInput();
        // }
        // else
        // {
        //     // Reset movement when attacking
        //     movement = Vector2.zero;
        // }
    }

    private void FixedUpdate()
    {
        if (PauseGameManager.instance.IsPaused())
        {
            return;
        }
        Move();
    }

    private void GetInput()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (joystick.joyStickVec != Vector2.zero) //&& !isAttack)
        {
            movement = joystick.joyStickVec;
        }

        Flip();
    }

    private void Move()
    {
        if (movement != Vector2.zero) //&& !isAttack)
        {
            Vector3 movement3D = new Vector3(movement.x, movement.y, 0);
            rb.MovePosition(rb.position + movement3D * stats.GetSpeed() * Time.fixedDeltaTime);
            animationController.setRunAnimation(1);
        }
        else
        {
            rb.velocity = Vector2.zero;
            animationController.setRunAnimation(0);
        }
    }

    private void Flip()
    {
        if (!isAttack && ((movement.x < 0 && isFacingRight) || (movement.x > 0 && !isFacingRight)))
        {
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
            isFacingRight = !isFacingRight;
        }
    }

    public void SetAttack(bool attack)
    {
        isAttack = attack;
    }
    public Vector3 GetDirection(){
        return new Vector3(movement.x, movement.y, 0);
    }
}