using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace MBT
{
    [AddComponentMenu("")]
    [MBTNode(name = "Tasks/Make Damage")]
    public class MakeDamage : Leaf
    {
        public GameObjectReference target;
        public GameObjectReference self;


        public override NodeResult Execute()
        {
            if (target == null || self == null)
            {
                Debug.LogError("Prefap reference is not set in Set Prefap node.");
                return NodeResult.failure;
            }

            if(target.Value.GetComponent<TreeStat>().getHp() <= self.Value.GetComponent<MobStatus>().getDamage())
            {
                self.Value.GetComponent<MobStatus>().LvUp(3);
            }
            target.Value.GetComponent<TreeStat>().takeDame(self.Value.GetComponent<MobStatus>().getDamage());

            /*            NewBehaviourScript newBehaviourScript = target.Value.GetComponent<NewBehaviourScript>();

                        newBehaviourScript.TakeDmg(mobStatus.getDamage());*/
            // 



            return NodeResult.success;
        }

        public override bool IsValid()
        {
            return target != null && self != null;
        }
    }
}