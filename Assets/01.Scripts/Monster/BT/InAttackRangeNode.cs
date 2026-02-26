public class InAttackRangeNode : BTNode
{
    private BaseMonsterController monster;
    private MonsterState monsterState;

    public InAttackRangeNode(BaseMonsterController monster, MonsterState monsterState)
    {
        this.monster = monster;
        this.monsterState = monsterState;
    }

    public override NodeState Evaluate()
    {
        float dist = monsterState.DistanceX(monster.transform);

        return dist <= monsterState.attackRange
            ? NodeState.Success
            : NodeState.Failure;
    }
}