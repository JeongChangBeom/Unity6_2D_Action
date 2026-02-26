using UnityEngine;

[System.Serializable]
public class MonsterState
{
    public Transform player;

    public float detectRange = 6f;
    public float attackRange = 1.5f;
    public float attackCooldown = 2f;

    public float lastAttackTime;

    public float DistanceX(Transform self)
    {
        if (player == null) return Mathf.Infinity;
        return Mathf.Abs(player.position.x - self.position.x);
    }

    public int FacingDir(Transform self)
    {
        if (player == null) return 1;
        return player.position.x > self.position.x ? 1 : -1;
    }
}