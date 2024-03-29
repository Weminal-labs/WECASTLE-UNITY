using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MBT
{
    [AddComponentMenu("")]
    [MBTNode("Services/Calculate Angle Service")]
    public class CalculateAngleService : Service
    {
        [Space]
        public TransformReference transform1;
        public TransformReference transform2;
        public FloatReference angleVariable = new FloatReference(VarRefMode.DisableConstant); // Variable to store the calculated angle

        public override void Task()
        {
            Transform t1 = transform1.Value;
            Transform t2 = transform2.Value;
            if (t1 == null || t2 == null)
            {
                return;
            }

            // Calculate the direction vector from this object to the target
            Vector3 direction = t1.position - t2.position;

            // Calculate the angle in degrees (0 to 360)
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Handle negative angles (optional)
            if (angle < 0)
            {
                angle += 360f;
            }

            // Store the angle in the variable
            angleVariable.Value = angle;
        }
    }
}
