using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    [SerializeField] Transform[] waypoints;
    [SerializeField] private float attackRange = 1f;
    [SerializeField] float Speed;
    [SerializeField] private Rigidbody rbEnemy; 
    [SerializeField] float RangeOfView =10f;
    [SerializeField] float minmunDistance;
    [SerializeField] float rotationSpeed;
    [SerializeField] GameObject Hero;
    [SerializeField] private Animator animEnemy;
    public bool IseeYou ;
    public bool isAttack;
    private bool goBack = false;
    private int currentIndex;
        // Start is called before the first frame update
    void Start()
    {
        Hero = GameObject.Find("Hero");
        rbEnemy = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
       if(Vector3.Distance(transform.position, Hero.transform.position) <= RangeOfView)
        {
            IseeYou = true;
            animEnemy.SetBool("isRun", true);                              
        }
        else
        {
             IseeYou = false;
             MovementPatrol(); 
             animEnemy.SetBool("isRun", false);  
        }

        if(IseeYou)
        {
            Attack();
        }
        
    }
    void MovementPatrol()
    {

        Vector3 deltaVector = waypoints[currentIndex].position - transform.position;
        Vector3 direction = deltaVector.normalized;
        
        transform.forward = Vector3.Lerp(transform.forward, direction, rotationSpeed * Time.deltaTime);
        transform.position += transform.forward * Speed * Time.deltaTime;
        
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

 
  private void ChaseCharacter()
    {
       // Debug.Log("ENTRO EN AREA");
        Vector3 direction =(Hero.transform.position - transform.position).normalized;
        transform.forward = Vector3.Lerp(transform.forward, direction, rotationSpeed * Time.deltaTime);
        transform.position += transform.forward * Speed * Time.deltaTime;
        
    }
    
       private void Attack()
    {
        Vector3 playerDirection = GetPlayerDirection();
        if(playerDirection.magnitude <= attackRange)
        {
            animEnemy.SetBool("isAttack", true);
            rbEnemy.rotation = Quaternion.LookRotation(new Vector3(playerDirection.x, 0, playerDirection.z));
            rbEnemy.AddForce(playerDirection.normalized * Speed, ForceMode.Impulse);
        }
        else
        {
            animEnemy.SetBool("isAttack", false);
        }
        
    }

    private Vector3 GetPlayerDirection()
    {
        return (Hero.transform.position - transform.position).normalized;
    }
    private void OnDrawGizmos()
    {
        if(IseeYou == true)
        {
        Gizmos.color = Color.magenta;
        }
        else
        {
            Gizmos.color = Color.cyan;
        }

        Gizmos.DrawWireSphere(transform.position, RangeOfView);
    }
}
