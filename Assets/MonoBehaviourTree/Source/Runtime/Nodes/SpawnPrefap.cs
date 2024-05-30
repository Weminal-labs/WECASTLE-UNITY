using UnityEngine;

namespace MBT
{
    [AddComponentMenu("")]
    [MBTNode(name = "Tasks/Spawn Prefap")]
    public class SpawnPrefap : Leaf
    {


        public GameObjectReference Prefap;
        public override NodeResult Execute()
        {
            if (Prefap == null)
            {
                Debug.LogError("Prefap reference is not set in Set Prefap node.");
                return NodeResult.failure;
            }
            // Spawn arrow at archer's position
            GameObject arrow = GameObject.Instantiate(Prefap.Value, transform.position, Quaternion.identity);
            //arrow.GetComponent<ProjectileController>().SetDamage(Damage.Value);


            return NodeResult.success;
        }

        public override bool IsValid()
        {
            return Prefap != null;
        }
    }
}
