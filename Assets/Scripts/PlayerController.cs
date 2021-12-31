using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //[SerializeField] private float Hinput = 5f;
    [SerializeField] private float speedPlayer = 5f;
    [SerializeField] private float speedRotation = 100f;
    private Rigidbody rb;
  //  [SerializeField] LayerMask groundLayer;
    [SerializeField] private Animator anim;
   // private int temporizador =0;
    public float Jumpforce = 8f;
    public bool Canjump;
   // [SerializeField] private GameObject Door;
    // Start is called before the first frame update
    void Start()
    {
       rb = GetComponent<Rigidbody>();
       Canjump = true;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        //Attack();   
        Jump(); 
        
    }
   /* private void Attack()
    {
     if(Input.GetKeyDown("space"))
        {
         animPlayer.SetBool("Atack", true);
         Debug.Log("ATAQUE");
        }
        else
        {
         animPlayer.SetBool("Atack", false);
        }
    }*/
    void Move()
    {
       float H = Input.GetAxis("Horizontal");
       float V = Input.GetAxis("Vertical");

       transform.Rotate(0, H * Time.deltaTime * speedRotation, 0);
       transform.Translate(0,0, V * Time.deltaTime * speedPlayer);
       
       anim.SetFloat("SpeedX", H);
       anim.SetFloat("SpeedY", V);
       /*if(H !=0 || V !=0 )
       {
       animPlayer.SetBool("isRun", true);
       transform.Rotate(0, H * Time.deltaTime * speedRotation, 0);
       transform.Translate(0,0, V * Time.deltaTime * speedPlayer);
       }*/
       
       /*else
       {
           animPlayer.SetBool("isRun", false);
       }*/
       
    }    
public void Jump()
{
    if(!Canjump)
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetBool("jump", true);
            rb.AddForce( new Vector3(0,Jumpforce,0), ForceMode.Impulse);
        }
        anim.SetBool("Touchground", true);
    }
    Falling();
}
public void Falling()
{
    anim.SetBool("Touchground", false);
    anim.SetBool("jump", false);
}
    
    /*void OnTriggerEnter (Collider collision) 
    {
    if (collision.gameObject.name == "Lever") 
    {
         temporizador ++;
    }
    if(temporizador > 2)
    {
      Debug.Log("PUERTA DESTRUIDA");
      Destroy(Door.gameObject);   
    }
    }*/
    
}
