using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AttackData), typeof(Animator))]
public class EnemyBase : MonoBehaviour {

    #region References
    public Enemy.Enemy_Class.Enemy stats;
    private Animator anim;

    #region State References

    public string walkStateName;
    public string deathStateName;

    #endregion

    #endregion

    public virtual void Walk(bool right)
    {
        int modifier = 1;
        Quaternion rotation = transform.rotation;
        if (right)
        {
            rotation.y = 90;
            modifier = -1;
        }
        else
            rotation.y = -90;
        transform.rotation = rotation;
        transform.Translate(Vector3.left * modifier * Time.deltaTime);
        anim.SetBool(walkStateName, true);
    }

    protected string currentAttack;

    public virtual void Attack(int combo)
    {
        currentAttack = stats.attacks[combo].name;
        anim.SetTrigger(currentAttack);
    }

    public virtual void Death()
    {
        anim.SetBool(deathStateName, true);

        //renzo have fun
    }
}
