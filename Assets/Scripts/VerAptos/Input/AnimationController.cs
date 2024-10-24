using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator anim;
    private TopDownController topDownController;
    // Start is called before the first frame update
    void Start()
    {
        anim = this.gameObject.GetComponent<Animator>();
        topDownController = this.gameObject.transform.parent.GetComponent<TopDownController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setBeginAttackAnimation()
    {
        anim.SetTrigger("Attack");
    }
    public void setEndAttackAnimation()
    {

    }
    public void setRunAnimation(float speed)
    {
        anim.SetFloat("Speed", speed);
    }
    public void SetLoseScreen()
    {
        Destroy(topDownController.gameObject);
    }
}
