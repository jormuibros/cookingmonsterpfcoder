using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="EnemyData", menuName="Enemy Data")]
public class EnemyData : ScriptableObject
{
    [SerializeField] private string enemyName;
    [SerializeField] private int enemyAttackDamage;
    [SerializeField] private float enemySpeed = 1f;
    [SerializeField] private float enemySpeedWaypoint  = 1f;
    [SerializeField] private float enemyRangeOfView =10f;
    [SerializeField] private float enemyMinimunDistance = 1f;
    [SerializeField] private float enemyRotationSpeed = 1f;
    [SerializeField] private float enemyAttackRange = 3f;

    [SerializeField] private int enemyLifePoints;

 public string EnemyName { 
        get
        {
            return enemyName;
        } 
    }
 
 public int EnemyAttackDamage
    {
        get
        {
            return enemyAttackDamage;
        }
    }

 public float EnemySpeed
    {
        get
        {
            return enemySpeed;
        }
    }

     public float EnemySpeedWaypoint
    {
        get
        {
            return enemySpeedWaypoint;
        }
    }
     public float EnemyRangeOfView
    {
        get
        {
            return enemyRangeOfView;
        }
    }
     public float EnemyMinimunDistance
    {
        get
        {
            return enemyMinimunDistance;
        }
    }
     public float EnemyRotationSpeed
    {
        get
        {
            return enemyRotationSpeed;
        }
    }

    public float EnemyAttackRange
    {
        get
        {
            return enemyAttackRange;
        }
    }

}