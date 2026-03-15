using UnityEngine;

public class GripModule : Module
{
    [Tooltip("What's the minimum weight the arm IK should have over the whole " +
        "animation to activate the grip")]
    public float leftArmWeightThreshold = 0.5f, rightArmWeightThreshold = 0.5f;
    public JointMotionsConfig defaultMotionsConfig;

    [Tooltip("Whether to only use Colliders marked as triggers to detect grip collisions.")]
    public bool onlyUseTriggers = false;
    public bool canGripYourself = false;

    private Gripper _leftGrip, _rightGrip;


    private void Start()
    {
        var leftHand = _activeRagdoll.GetPhysicalBone(HumanBodyBones.LeftHand).gameObject;
        var rightHand = _activeRagdoll.GetPhysicalBone(HumanBodyBones.RightHand).gameObject;

        (_leftGrip = leftHand.AddComponent<Gripper>()).GripMod = this;
        (_rightGrip = rightHand.AddComponent<Gripper>()).GripMod = this;
    }

    //Using this to test that the hands can grip stuff
    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            _leftGrip.enabled = 1f > leftArmWeightThreshold;
        }
        if (Input.GetMouseButtonUp(1))
        {
            _leftGrip.enabled = 0f > leftArmWeightThreshold;
        }
        if (Input.GetMouseButtonDown(0))
        {
            _rightGrip.enabled = 1f > rightArmWeightThreshold;
        }
        if (Input.GetMouseButtonUp(0))
        {
            _rightGrip.enabled = 0f > rightArmWeightThreshold;
        }
    }

    public void UseLeftGrip(float weight)
    {
        _leftGrip.enabled = weight > leftArmWeightThreshold;
    }

    public void UseRightGrip(float weight)
    {
        _rightGrip.enabled = weight > rightArmWeightThreshold;
    }
}
