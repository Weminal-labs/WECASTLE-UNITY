using UnityEngine;

namespace MBT
{
    [AddComponentMenu("")]
    [MBTNode(name = "Tasks/Enemy Make Fight Damage")]
    public class EnemyFightDamage : Leaf
    {
        public GameObjectReference self;
        public LayerMask mask = -1;
        [Tooltip("Circle radius")]
        public FloatReference range;

        public override NodeResult Execute()
        {
            if (self == null)
            {
                Debug.LogError("Prefap reference is not set in Set Prefap node.");
                return NodeResult.failure;
            }
            Collider[] colliders = Physics.OverlapSphere(transform.position, range.Value, mask);

            foreach (Collider eachTarget in colliders)
            {
                eachTarget.gameObject.GetComponent<MobStatus>().takeDame(self.Value.GetComponent<EnemyController>().getDamage());
            }


            /*            NewBehaviourScript newBehaviourScript = target.Value.GetComponent<NewBehaviourScript>();

                        newBehaviourScript.TakeDmg(mobStatus.getDamage());*/
            // 



            return NodeResult.success;
        }

        public override bool IsValid()
        {
            return self != null;
        }
    }
}