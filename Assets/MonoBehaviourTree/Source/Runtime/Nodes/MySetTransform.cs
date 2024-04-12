using UnityEngine;

namespace MBT
{
    [AddComponentMenu("")]
    [MBTNode(name = "Tasks/My Set Object")]
    public class MySetTransform : Leaf
    {
        [SerializeField]
        private Type type = Type.Transform;
        public TransformReference sourceTransform;
        public GameObjectReference sourceGameObject;
        public TransformReference destinationTransform = new TransformReference(VarRefMode.DisableConstant);
        public GameObjectReference destinationGameObject = new GameObjectReference(VarRefMode.DisableConstant);

        public override NodeResult Execute()
        {
            if (type == Type.Transform)
            {
                Vector3 newPosition = sourceTransform.Value.position;
                newPosition.y += 7f; // Adjust Y-axis by adding 7
                destinationTransform.Value.position = newPosition;
            }
            else
            {
                destinationGameObject.Value.transform.position = sourceGameObject.Value.transform.position + Vector3.up * 7f;
            }
            return NodeResult.success;
        }

        public override bool IsValid()
        {
            // Custom validation to allow nulls in source objects
            switch (type)
            {
                case Type.Transform: return !(sourceTransform == null || IsInvalid(sourceTransform) || destinationTransform.isInvalid);
                case Type.GameObject: return !(sourceGameObject == null || IsInvalid(sourceGameObject) || destinationGameObject.isInvalid);
                default: return true;
            }
        }

        private static bool IsInvalid(BaseVariableReference variable)
        {
            // Custom validation to allow null objects without warnings
            return (variable.isConstant) ? false : variable.blackboard == null || string.IsNullOrEmpty(variable.key);
        }

        private enum Type
        {
            Transform, GameObject
        }
    }
}
