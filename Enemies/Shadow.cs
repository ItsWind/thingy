using UnityEngine;

public class Shadow : Enemy
{
    private void targetPlayer()
    {
        PlayerStats.Sanity -= 0.01666f;

        Vector3 playerPos = XRObjectsGet.XRPlayer.Camera.transform.position;
        Vector3 flooredPlayerPos = new Vector3(playerPos.x, 0, playerPos.z);

        if (!NavAgent.destination.Equals(flooredPlayerPos))
        {
            NavAgent.speed = Speed / 2;
            NavAgent.SetDestination(flooredPlayerPos);
        }
    }

    public override void DoThink()
    {
        if (IsVisibleToPlayer)
        {
            targetPlayer();
            Idle = false;
        }
        else
        {
            if (!NavAgent.destination.Equals(CurrentTargetPos))
            {
                NavAgent.SetDestination(CurrentTargetPos);
                NavAgent.speed = Speed;
            }
            Idle = true;
        }
    }
}
