using MBT;
using UnityEngine;
using UnityEngine.AI;

namespace MBTExample
{
    [AddComponentMenu("")]
    [MBTNode("Example/Move Navmesh Agent Vector3")]
    public class MoveNavmeshAgentVector3 : Leaf
    {
        public Vector3Reference destination;
        public NavMeshAgent agent;
        //public float stopDistance = 2f;
        public FloatReference stopDistance = new FloatReference(1f);
        [Tooltip("How often target position should be updated")]
        public float updateInterval = 1f;
        private float time = 0;

        public override void OnEnter()
        {
            if (destination.Value != null)
            {
                time = 0;
                agent.isStopped = false;
                agent.SetDestination(destination.Value);
            }

        }

        public override NodeResult Execute()
        {
            time += Time.deltaTime;
            //fix
            if (destination.Value == null)
            {
                return NodeResult.failure;
            }
            // Update destination every given interval
            if (time > updateInterval)
            {
                // Reset time and update destination
                time = 0;
                agent.SetDestination(destination.Value);
            }
            // Check if path is ready
            if (agent.pathPending)
            {
                return NodeResult.running;
            }
            // Check if agent is very close to destination
            if (agent.remainingDistance < stopDistance.Value)
            {
                return NodeResult.success;
            }


            // Check if there is any path (if not pending, it should be set)
            if (agent.hasPath)
            {
                return NodeResult.running;
            }
            // By default return failure
            return NodeResult.failure;
        }

        public override void OnExit()
        {
            agent.isStopped = true;
            // agent.ResetPath();
        }

        public override bool IsValid()
        {
            return !(destination.isInvalid || agent == null);
        }
    }
}
