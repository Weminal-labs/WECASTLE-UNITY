using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MBT
{
    [AddComponentMenu("")]
    [MBTNode(name = "Conditions/InRange Condition")]
    public class InRangeCondition : Condition
    {
        public Abort abort;
        public Type type = Type.Float;
        public FloatReference floatValue = new FloatReference(VarRefMode.DisableConstant);
        public IntReference intValue = new IntReference(VarRefMode.DisableConstant);
        public FloatReference minRange = new FloatReference(0f);
        public FloatReference maxRange = new FloatReference(1f);

        public override bool Check()
        {
            if (type == Type.Float)
            {
                float value = floatValue.Value;
                float minValue = minRange.Value;
                float maxValue = maxRange.Value;
                return value >= minValue && value <= maxValue;
            }
            else
            {
                int value = intValue.Value;
                int minValue = Mathf.RoundToInt(minRange.Value);
                int maxValue = Mathf.RoundToInt(maxRange.Value);
                return value >= minValue && value <= maxValue;
            }
        }

        public override bool IsValid()
        {
            switch (type)
            {
                case Type.Float: return !(floatValue.isInvalid || minRange.isInvalid || maxRange.isInvalid);
                case Type.Int: return !(intValue.isInvalid || minRange.isInvalid || maxRange.isInvalid);
                default: return true;
            }
        }

        public enum Type
        {
            Float, Int
        }
    }
}
