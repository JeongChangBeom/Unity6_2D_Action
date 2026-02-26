public class DetectPlayerNode : BTNode
{
    private BaseMonsterController monster;
    private MonsterState monsterState;

    public DetectPlayerNode(BaseMonsterController monster, MonsterState monsterState)
    {
        this.monster = monster;
        this.monsterState = monsterState;
    }

    public override NodeState Evaluate()
    {
        float dist = monsterState.DistanceX(monster.transform);

        return dist <= monsterState.detectRange
            ? NodeState.Success
            : NodeState.Failure;
    }
}