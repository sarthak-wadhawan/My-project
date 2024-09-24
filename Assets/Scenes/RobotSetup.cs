using UnityEngine;

public class RobotSetup : MonoBehaviour
{
    public float linkMass = 1.0f; // Default mass for each of the links
    public bool addBoxColliderIfNone = true; 

    void Start()
    {
        AddRigidBodyAndColliderRecursively(transform);
    }

    void AddRigidBodyAndColliderRecursively(Transform currentTransform)
    {
        // rigidbody and a collider for all the children
        foreach (Transform child in currentTransform)
        {
            // rigidbody     
            if (child.GetComponent<Rigidbody>() == null)
            {
                Rigidbody rb = child.gameObject.AddComponent<Rigidbody>();
                rb.mass = linkMass;
            }

            if (child.GetComponent<Collider>() == null && addBoxColliderIfNone)
            {
                child.gameObject.AddComponent<BoxCollider>();
            }

            //  apply this to all via recursion
            if (child.childCount > 0)
            {
                AddRigidBodyAndColliderRecursively(child);
            }
        }
    }
}
