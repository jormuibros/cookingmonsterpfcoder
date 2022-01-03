using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBossController : EnemyControler
{
    [SerializeField] private EnemyData enemyData2;
    // Start is called before the first frame update  public virtual void Attack()
     public override void Attack()
    {
        Vector3 playerDirection = GetPlayerDirection();
        if(playerDirection.magnitude > enemyData2.EnemyAttackRange)
        {
            animEnemy.SetBool("isAttack", false);
            rbEnemy.rotation = Quaternion.LookRotation(new Vector3(playerDirection.x, 0, playerDirection.z));
            rbEnemy.AddForce(playerDirection * enemyData2.EnemySpeed, ForceMode.Impulse);
        }
        else
        {
            animEnemy.SetBool("isAttack", true);
        }
        
    }
}
