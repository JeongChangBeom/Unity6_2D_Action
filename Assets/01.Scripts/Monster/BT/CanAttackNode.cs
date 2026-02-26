using UnityEngine;

public class CanAttackNode : BTNode
{
    private MonsterState monsterState;

    public CanAttackNode(MonsterState monsterState)
    {
        this.monsterState = monsterState;
    }

    public override NodeState Evaluate()
    {
        return Time.time - monsterState.lastAttackTime >= monsterState.attackCooldown
            ? NodeState.Success
            : NodeState.Failure;
    }
}