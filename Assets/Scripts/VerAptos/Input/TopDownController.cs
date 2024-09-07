using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownController : MonoBehaviour
{
    public float moveSpeed = 1.0f;

    public Rigidbody2D rb;

    private bool isFacingRight;

    private Vector2 movement;

    [SerializeField]
    private JoyStickComponent joystick;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        isFacingRight = true;
    }
    // Update is called once per frame
    void Update()
    {
        GetInput();
        /*if(Input.GetMouseButtonDown(0))
        {
            this.gameObject.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Attack");
        }*/
    }
    private void FixedUpdate()
    {
        SetPos();
    }
    public void GetInput()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        Flip();
    }
    public void SetPos()
    {
        if (movement.x != 0 || movement.y != 0)
        {
            this.gameObject.transform.GetChild(0).GetComponent<Animator>().SetBool("Running", true);
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        }
        else
        {
            this.gameObject.transform.GetChild(0).GetComponent<Animator>().SetBool("Running", false);
        }
        if(joystick.joyStickVec.y != 0)
        {
            rb.velocity = new Vector2(joystick.joyStickVec.x*moveSpeed, joystick.joyStickVec.y*moveSpeed);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    public void Flip()
    {
        if ((movement.x < 0 && isFacingRight) || (movement.x > 0 && !isFacingRight))
        {
            Vector2 scale = new Vector2(this.transform.localScale.x, this.transform.localScale.y);
            scale.x *= -1;
            this.transform.localScale = scale;
            isFacingRight = !isFacingRight;
        }
        if((joystick.joyStickVec.x < 0 && isFacingRight) ||(joystick.joyStickVec.x > 0 && !isFacingRight))
        {
            Vector2 scale = new Vector2(this.transform.localScale.x, this.transform.localScale.y);
            scale.x *= -1;
            this.transform.localScale = scale;
            isFacingRight = !isFacingRight;
        }
    }
}
