using UnityEngine;

public class MoveRCommand : PCommand
{
    public override void Execute(PlayerCMovement player)
    {
        Rigidbody rb = player.GetRB();

        player.moveDir = new Vector3(player.maxSpeed, 0, 0);

        player.playerVelocity = rb.linearVelocity;

        if (player.moveDir.sqrMagnitude < 0.01f) return;

        // transform the world direction to be relative to the "boats" rotation. makes the player movement more accurately track the motion of the boat as it rocks.
        Vector3 finalMoveDir = player.isOnBoat ? player.boatTransform.TransformDirection(player.moveDir.normalized) : player.moveDir;

        rb.AddForce(finalMoveDir * player.acceleration, ForceMode.Acceleration);

        rb.linearVelocity = Vector3.ClampMagnitude(rb.linearVelocity, player.maxSpeed);
    }
}
