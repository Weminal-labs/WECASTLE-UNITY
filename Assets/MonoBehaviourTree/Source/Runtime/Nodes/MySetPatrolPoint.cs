using MBT;
using UnityEngine;

namespace MBTExample
{
    [MBTNode("Example/My Set Patrol Point")]
    [AddComponentMenu("")]
    public class MySetPatrolPoint : Leaf
    {
        public TransformReference variableToSet = new TransformReference(VarRefMode.DisableConstant);
        public Transform[] waypoints;
        private System.Random random = new System.Random();

        public override NodeResult Execute()
        {
            if (waypoints.Length == 0)
            {
                return NodeResult.failure;
            }
            // Randomly select a waypoint index
            int randomIndex = random.Next(waypoints.Length);

            // Set blackboard variable with the selected waypoint (position)
            variableToSet.Value = waypoints[randomIndex];
            return NodeResult.success;
        }
    }
}
