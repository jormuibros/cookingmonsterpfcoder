using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControler : MonoBehaviour
{   
    [SerializeField] private EnemyData enemyData;
    [SerializeField] private HeroData heroData;
    [SerializeField] private int enemyHP;
    [SerializeField] Transform[] waypoints;
    private bool goBack = false;
    private int currentIndex;
    public bool IseeYou ;
    public GameObject player;
    public Rigidbody rbEnemy;
    public Animator  animEnemy;
    private bool isAttack;

    void Start()
    {
        isAttack = false;
        player = GameObject.Find("Hero");
        rbEnemy = GetComponent<Rigidbody>();
        animEnemy = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
     if(Vector3.Distance(transform.position, player.transform.position) <= enemyData.EnemyRangeOfView)
        {
            IseeYou = true;
        }
        else
        {
             IseeYou = false;
        }

    }

    public virtual void Attack()
    {
        Vector3 playerDirection = GetPlayerDirection();
        if(playerDirection.magnitude > enemyData.EnemyAttackRange)
        {
            animEnemy.SetBool("isAttack", false);
            rbEnemy.rotation = Quaternion.LookRotation(new Vector3(playerDirection.x, 0, playerDirection.z));
            rbEnemy.AddForce(playerDirection.normalized * enemyData.EnemySpeed, ForceMode.Impulse);
        }
        else
        {
            animEnemy.SetBool("isAttack", true);
        }
        
    }

    public virtual Vector3 GetPlayerDirection()
    {
        return player.transform.position - transform.position;
    }
        public virtual void OnDrawGizmos()
    {
        if(IseeYou == true)
        {
        Attack();
        animEnemy.SetBool("isRun", true);
        Gizmos.color = Color.magenta;
        }
        else
        {
            animEnemy.SetBool("isRun", false);
            MovementPatrol();
            Gizmos.color = Color.cyan;
        }

        Gizmos.DrawWireSphere(transform.position, enemyData.EnemyRangeOfView);
    }
        void MovementPatrol()
    {

        Vector3 deltaVector = waypoints[currentIndex].position - transform.position;
        Vector3 direction = deltaVector.normalized;
        
        transform.forward = Vector3.Lerp(transform.forward, direction, enemyData.EnemyRotationSpeed * Time.deltaTime);
        transform.position += transform.forward * enemyData.EnemySpeedWaypoint * Time.deltaTime;
        
        float distance = deltaVector.magnitude;
        
        if(distance < enemyData.EnemyMinimunDistance )
        {
          if(currentIndex >= waypoints.Length -1)
            {
                goBack = true;
            }
            else if( currentIndex <= 0)
            {
                goBack = false;
            }
            if(!goBack)
            {
                currentIndex++;
            }
            else currentIndex--;
        }
        
    }

   public virtual void OnTriggerEnter(Collider other) 
    {
     if (other.gameObject.CompareTag("HeroFist")) 
        {
           enemyHP -= heroData.HeroPrimaryDamage;
           Debug.Log("Heroe pega");
        }
       if (other.gameObject.CompareTag("HeroFoot"))
        {
           enemyHP -= heroData.HeroSecondaryDamage;
           Debug.Log("Heroe patea");
        }
        if(enemyHP < 0)
           {
            Destroy(gameObject);
           }


    }

}
