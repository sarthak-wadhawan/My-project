using UnityEngine;

public class RobotArm : MonoBehaviour
{
    public Transform[] joints;  // Array of joints in the robot arm

    public Vector3 ForwardKinematics(float[] jointAngles)
    {
        Matrix4x4 transformation = Matrix4x4.identity;

        for (int i = 0; i < joints.Length; i++)
        {
            // Apply joint rotation
            transformation *= Matrix4x4.Rotate(Quaternion.Euler(0, 0, jointAngles[i]));
            // Apply translation along the arm link
            transformation *= Matrix4x4.Translate(joints[i].localPosition);
        }

        // Extract the position from the transformation matrix
        return transformation.GetColumn(3);  // The translation vector
    }

    public float[] GradientDescentIK(Vector3 targetPosition, float[] initialJointAngles, int maxIterations, float learningRate)
    {
        float[] jointAngles = (float[])initialJointAngles.Clone();
        for (int iteration = 0; iteration < maxIterations; iteration++)
        {
            Vector3 currentPosition = ForwardKinematics(jointAngles);

            Vector3 error = targetPosition - currentPosition;
            if (error.magnitude < 0.01f) break;

            for (int i = 0; i < jointAngles.Length; i++)
            {
                float[] perturbedAngles = (float[])jointAngles.Clone();
                perturbedAngles[i] += 0.01f;

                Vector3 perturbedPosition = ForwardKinematics(perturbedAngles);
                float gradient = (perturbedPosition - currentPosition).magnitude / 0.01f;

                jointAngles[i] -= learningRate * gradient;
            }
        }
        return jointAngles;
    }
}
