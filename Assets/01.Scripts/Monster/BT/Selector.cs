using System.Collections.Generic;

public class Selector : BTNode
{
    private List<BTNode> nodes;

    public Selector(List<BTNode> nodes)
    {
        this.nodes = nodes;
    }

    public override NodeState Evaluate()
    {
        foreach (var node in nodes)
        {
            var result = node.Evaluate();

            if (result == NodeState.Success)
                return NodeState.Success;

            if (result == NodeState.Running)
                return NodeState.Running;
        }

        return NodeState.Failure;
    }
}