public class IdleNode : BTNode
{
    private BaseMonsterController monster;

    public IdleNode(BaseMonsterController monster)
    {
        this.monster = monster;
    }

    public override NodeState Evaluate()
    {
        monster.Idle();
        return NodeState.Running;
    }
}