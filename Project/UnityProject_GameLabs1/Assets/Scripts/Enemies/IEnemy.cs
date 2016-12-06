using System.Collections;
using UnityEngine;

public interface IEnemy
{
    EnemyBase.State GetState();
    int GetAttackDamage();
}
