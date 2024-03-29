using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MBT
{
    [AddComponentMenu("")]
    [MBTNode(name = "Tasks/Set Animation")]
    public class SetAnimation : Leaf
    {
        public Animator animator;
        public string animationName;
        public bool playAnimation;

        public override NodeResult Execute()
        {
            if (animator == null)
            {
                Debug.LogError("Animator reference is not set in SetAnimation node.");
                return NodeResult.failure;
            }

            if (playAnimation)
            {
                animator.Play(animationName);
            }
            else
            {
                animator.StopPlayback();
            }

            return NodeResult.success;
        }

        public override bool IsValid()
        {
            return animator != null;
        }
    }
}
