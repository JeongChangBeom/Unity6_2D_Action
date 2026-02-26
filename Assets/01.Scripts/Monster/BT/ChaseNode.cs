public class ChaseNode : BTNode
{
    private BaseMonsterController monster;
    private MonsterState monsterState;

    public ChaseNode(BaseMonsterController monster, MonsterState monsterState)
    {
        this.monster = monster;
        this.monsterState = monsterState;
    }

    public override NodeState Evaluate()
    {
        int dir = monsterState.FacingDir(monster.transform);
        monster.Move(dir);
        return NodeState.Running;
    }
}