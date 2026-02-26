using UnityEngine;

public abstract class BaseMonsterController : MonoBehaviour
{
    protected MonsterState monsterState;
    protected Animator anim;

    [Header("Movement")]
    [SerializeField] protected float moveSpeed = 2f;

    protected virtual void Awake()
    {
        monsterState = GetComponent<MonsterState>();
        anim = GetComponent<Animator>();
    }

    public virtual void Move(int dir)
    {
        transform.position += Vector3.right * dir * moveSpeed * Time.deltaTime;
        Flip(dir);

        if (anim != null)
        {
            anim.SetBool("Move", true);
        }
    }

    protected void Flip(int dir)
    {
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * dir;
        transform.localScale = scale;
    }

    public virtual void Attack()
    {
        if (anim != null)
        {
            anim.SetTrigger("Attack");
        }
    }

    public virtual void Idle()
    {
        if (anim != null)
        {
            anim.SetBool("Move", false);
        }
    }
}