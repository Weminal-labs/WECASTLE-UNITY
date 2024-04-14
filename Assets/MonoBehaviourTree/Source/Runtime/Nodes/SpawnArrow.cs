using UnityEngine;

namespace MBT
{
    [AddComponentMenu("")]
    [MBTNode(name = "Tasks/Spawn Arrow")]
    public class SpawnArrow : Leaf
    {
        public TransformReference target;
        public TransformReference self;

        public FloatReference angleToTarget;
        public GameObjectReference arrowPrefap;
        private float arrowSpeed = 10;
        private float arrowLifetime = 5;
        public IntReference Damage;
        public override NodeResult Execute()
        {
            if (arrowPrefap == null)
            {
                Debug.LogError("Prefap reference is not set in Set Prefap node.");
                return NodeResult.failure;
            }

            Transform targetTransform = target.Value;
            Vector3 direction = target.Value.position - self.Value.position;
            // Spawn arrow at archer's position
            GameObject arrow = GameObject.Instantiate(arrowPrefap.Value, transform.position, Quaternion.identity);
            arrow.GetComponent<ProjectileController>().SetDamage(Damage.Value);
            // Rotate the arrow to face the enemy
            arrow.transform.rotation = Quaternion.Euler(0f, 0f, angleToTarget.Value);

            // Move the arrow in the direction of the enemy (optional)
            arrow.GetComponent<Rigidbody>().velocity = direction.normalized * arrowSpeed;
            GameObject.Destroy(arrow, arrowLifetime);



            return NodeResult.success;
        }

        public override bool IsValid()
        {
            return arrowPrefap != null;
        }
    }
}
