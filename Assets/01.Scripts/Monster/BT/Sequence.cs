using System.Collections.Generic;

public class Sequence : BTNode
{
    private List<BTNode> nodes;

    public Sequence(List<BTNode> nodes)
    {
        this.nodes = nodes;
    }

    public override NodeState Evaluate()
    {
        foreach (var node in nodes)
        {
            var result = node.Evaluate();

            if (result == NodeState.Failure)
                return NodeState.Failure;

            if (result == NodeState.Running)
                return NodeState.Running;
        }

        return NodeState.Success;
    }
}