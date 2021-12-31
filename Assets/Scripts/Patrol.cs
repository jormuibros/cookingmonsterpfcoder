using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    [SerializeField] Transform[] waypoints;
    [SerializeField] float Speed;
    [SerializeField] float RangeOfView =10f;
    [SerializeField] float minmunDistance;
    [SerializeField] float rotationSpeed;
    [SerializeField] GameObject Hero;
    [SerializeField] private Animator animEnemy;
    public bool IseeYou = false;
    private bool goBack = false;
    private int currentIndex;
        // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if(Vector3.Distance(transform.position, Hero.transform.position) <= RangeOfView)
        {
            IseeYou = true;                              
        }
        else
        {
            IseeYou = false;
        }

        if(IseeYou)
        {
            ChaseCharacter();
            animEnemy.SetBool("isRun", true);

        }
        else
        {
            MovementPatrol();   
            animEnemy.SetBool("isRun", false);

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
    
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
        Debug.Log("Jugador Herido");
        //GameManager.instance.addScore();
        //Debug.Log(GameManager.instance.getScore());
        //Destroy(gameObject);
        }

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
