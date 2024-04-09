using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace MBT
{
    [AddComponentMenu("")]
    [MBTNode(name = "Tasks/Mob Make Fight Damage")]
    public class MobFightDamage : Leaf
    {
        public GameObjectReference self;
        public LayerMask mask = -1;
        [Tooltip("Circle radius")]
        public FloatReference range;

        public override NodeResult Execute()
        {
            if (  self == null)
            {
                Debug.LogError("Prefap reference is not set in Set Prefap node.");
                return NodeResult.failure;
            }
            Collider[] colliders = Physics.OverlapSphere(transform.position, range.Value, mask);

            foreach (Collider eachTarget in colliders)
            {
              
                eachTarget.gameObject.GetComponent<EnemyController>().takeDame(self.Value.GetComponent<MobStatus>().getDamage());

            }


            /*            NewBehaviourScript newBehaviourScript = target.Value.GetComponent<NewBehaviourScript>();

                        newBehaviourScript.TakeDmg(mobStatus.getDamage());*/
            // 



            return NodeResult.success;
        }

        public override bool IsValid()
        {
            return  self != null;
        }
    }
}