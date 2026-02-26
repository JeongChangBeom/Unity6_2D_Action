using UnityEngine;
using System.Collections.Generic;

public class MonsterBTController : MonoBehaviour
{
    private BTNode root;
    private BaseMonsterController monster;
    private MonsterState monsterState;

    private void Awake()
    {
        monster = GetComponent<BaseMonsterController>();
        monsterState = GetComponent<MonsterState>();
        BuildTree();
    }

    private void Update()
    {
        root?.Evaluate();
    }

    private void BuildTree()
    {
        var attackSequence = new Sequence(new List<BTNode>
        {
            new InAttackRangeNode(monster, monsterState),
            new CanAttackNode(monsterState),
            new AttackNode(monster, monsterState)
        });

        var chaseSequence = new Sequence(new List<BTNode>
        {
            new DetectPlayerNode(monster, monsterState),
            new ChaseNode(monster, monsterState)
        });

        root = new Selector(new List<BTNode>
        {
            attackSequence,
            chaseSequence,
            new IdleNode(monster)
        });
    }
}