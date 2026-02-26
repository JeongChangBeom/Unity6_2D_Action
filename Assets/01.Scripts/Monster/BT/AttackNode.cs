using UnityEngine;

public class AttackNode : BTNode
{
    private BaseMonsterController monster;
    private MonsterState monsterState;

    public AttackNode(BaseMonsterController monster, MonsterState monsterState)
    {
        this.monster = monster;
        this.monsterState = monsterState;
    }

    public override NodeState Evaluate()
    {
        monster.Attack();
        monsterState.lastAttackTime = Time.time;
        return NodeState.Success;
    }
}