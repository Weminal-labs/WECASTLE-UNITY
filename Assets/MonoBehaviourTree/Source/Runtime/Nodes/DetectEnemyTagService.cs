using MBT;
using System;
using UnityEngine;

namespace MBTExample
{
    [AddComponentMenu("")]
    [MBTNode("Example/Detect Enemy Tag Service")]
    public class DetectEnemyTagService : Service
    {
        public LayerMask mask = -1;
        [Tooltip("Circle radius")]
        public FloatReference range;
        public StringReference variableToSet = new StringReference(VarRefMode.DisableConstant);

        public override void Task()
        {
            // Find target in radius and feed blackboard variable with results
            Collider[] colliders = Physics.OverlapSphere(transform.position, range.Value, mask);
            if (colliders.Length > 0)
            {
                // Initialize variables for storing closest distance and closest GameObject
                float closestDistance = Mathf.Infinity;
                String closestGameObject = null;

                // Iterate through each detected GameObject
                foreach (Collider collider in colliders)
                {
                    // Calculate distance between current GameObject and detected GameObject
                    float distance = Vector3.Distance(transform.position, collider.transform.position);
                    // Check if current detected GameObject is closer than the previous closest GameObject
                    if (distance < closestDistance)
                    {
                        // Update closest distance and closest GameObject
                        closestDistance = distance;
                        closestGameObject = collider.gameObject.tag;
                    }
                }
                // Assign closest GameObject to the variableToSet
                variableToSet.Value = closestGameObject;
            }
            else
            {
                variableToSet.Value = null;
            }
        }
    }
}
