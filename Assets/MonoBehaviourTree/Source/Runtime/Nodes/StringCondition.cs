using UnityEngine;

namespace MBT
{
    [AddComponentMenu("")]
    [MBTNode(name = "Conditions/String Condition")]
    public class StringCondition : Condition
    {
        public Abort abort;
        public StringReference stringReference = new StringReference(VarRefMode.DisableConstant);
        public StringReference stringReference2 = new StringReference();
        public Comparator comparator = Comparator.Equal;

        public override bool Check()
        {
            switch (comparator)
            {
                case Comparator.Equal:
                    return stringReference.Value == stringReference2.Value;
                case Comparator.NotEqual:
                    return stringReference.Value != stringReference2.Value;
            }
            return false;
        }

        public override void OnAllowInterrupt()
        {
            if (abort != Abort.None)
            {
                ObtainTreeSnapshot();
                stringReference.GetVariable().AddListener(OnVariableChange);
                if (!stringReference2.isConstant)
                {
                    stringReference2.GetVariable().AddListener(OnVariableChange);
                }
            }
        }

        public override void OnDisallowInterrupt()
        {
            if (abort != Abort.None)
            {
                stringReference.GetVariable().RemoveListener(OnVariableChange);
                if (!stringReference2.isConstant)
                {
                    stringReference2.GetVariable().RemoveListener(OnVariableChange);
                }
            }
        }

        private void OnVariableChange(string newVal, string oldVal)
        {
            EvaluateConditionAndTryAbort(abort);
        }

        public override bool IsValid()
        {
            return !(stringReference.isInvalid || stringReference2.isInvalid);
        }

        public enum Comparator
        {
            [InspectorName("==")]
            Equal,
            [InspectorName("!=")]
            NotEqual
        }
    }
}
