using UnityEngine;

public class RobotController : MonoBehaviour
{
    public ArticulationBody shoulderJoint;

    void Start()
    {
        shoulderJoint = GetComponent<ArticulationBody>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            var drive = shoulderJoint.xDrive;
            drive.target += 1.0f;
            shoulderJoint.xDrive = drive;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            var drive = shoulderJoint.xDrive;
            drive.target -= 1.0f;
            shoulderJoint.xDrive = drive;
        }
    }
}
