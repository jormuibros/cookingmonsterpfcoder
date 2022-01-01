using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControler : MonoBehaviour
{   [SerializeField] private float speedEnemy  = 1f;
    [SerializeField] Transform[] waypoints;
    [SerializeField] float RangeOfView =10f;
    private bool goBack = false;
    private int currentIndex;
    public bool IseeYou ;
    [SerializeField] float minmunDistance;
    [SerializeField] float rotationSpeed;
    [SerializeField] private float attackRange = 3f;
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
     if(Vector3.Distance(transform.position, player.transform.position) <= RangeOfView)
        {
            IseeYou = true;
        }
        else
        {
             IseeYou = false;
        }
    }

    private void Attack()
    {
        Vector3 playerDirection = GetPlayerDirection();
        if(playerDirection.magnitude > attackRange)
        {
            animEnemy.SetBool("isAttack", false);
            rbEnemy.rotation = Quaternion.LookRotation(new Vector3(playerDirection.x, 0, playerDirection.z));
            rbEnemy.AddForce(playerDirection.normalized * speedEnemy, ForceMode.Impulse);
        }
        else
        {
            animEnemy.SetBool("isAttack", true);
        }
        
    }

    private Vector3 GetPlayerDirection()
    {
        return player.transform.position - transform.position;
    }
        private void OnDrawGizmos()
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

        Gizmos.DrawWireSphere(transform.position, RangeOfView);
    }
        void MovementPatrol()
    {

        Vector3 deltaVector = waypoints[currentIndex].position - transform.position;
        Vector3 direction = deltaVector.normalized;
        
        transform.forward = Vector3.Lerp(transform.forward, direction, rotationSpeed * Time.deltaTime);
        transform.position += transform.forward * speedEnemy * Time.deltaTime;
        
        float distance = deltaVector.magnitude;
        
        if(distance < minmunDistance )
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

}
