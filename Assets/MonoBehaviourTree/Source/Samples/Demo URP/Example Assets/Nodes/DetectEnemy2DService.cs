using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MBT;

namespace MBTExample
{
    [AddComponentMenu("")]
    [MBTNode("Example/Detect Enemy 2D Service")]
    public class DetectEnemy2DService : Service
    {
        public LayerMask mask = -1;
        [Tooltip("Circle radius")]
        public FloatReference range ;
        public TransformReference variableToSet = new TransformReference(VarRefMode.DisableConstant);

        public override void Task()
        {
            // Find target in radius and feed blackboard variable with results
            Collider[] colliders = Physics.OverlapSphere(transform.position, range.Value, mask);
            if (colliders.Length > 0)
            {
                float closestDistance = Mathf.Infinity;
                Transform closestTransform = null;

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
                        closestTransform = collider.transform;
                    }
                }
                // Assign closest GameObject to the variableToSet
                variableToSet.Value = closestTransform;
            }
            else
            {
                variableToSet.Value = null;
            }
        }
    }
}
