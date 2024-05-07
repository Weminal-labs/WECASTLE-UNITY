using UnityEngine;
namespace MBT
{
    [AddComponentMenu("")]
    [MBTNode("Services/Calculate Distance Service Vector3")]
    public class CalculateDistanceServiceVector3 : Service
    {
        [Space]
        public TransformReference transform1; // Changed from TransformReference to Vector3Reference
        public Vector3Reference vector2; // Changed from TransformReference to Vector3Reference
        public FloatReference variable = new FloatReference(VarRefMode.DisableConstant);

        public override void Task()
        {
            Transform v1 = transform1.Value; // Changed from Transform to Vector3
            Vector3 v2 = vector2.Value; // Changed from Transform to Vector3
            variable.Value = Vector3.Distance(v1.position, v2);
        }
    }
}
