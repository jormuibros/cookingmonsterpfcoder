using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    [SerializeField] private float speedPlayer = 5f;
    [SerializeField] private float runPlayer = 10f;
    [SerializeField] private float initialSpeed;
    [SerializeField] private float speedRotation = 100f;
    public float Jumpforce = 8f;
    [SerializeField] private Animator anim;
    [SerializeField] private bool jump;
    [SerializeField] private bool isRunning, Dash;
    [SerializeField] private bool isFalling;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private int Jtimer =1;
    [SerializeField] private float timer =1;
    [SerializeField] private int dashSpeed = 50;
    private float qTimer=0;
    private float ySpeed;
    void Start()
    {
       initialSpeed = speedPlayer;
       Dash = false;
       isFalling = false;
       isRunning = false;
       jump = false;
       anim = GetComponent<Animator>();
       rb = GetComponent<Rigidbody>();
       
    }

    void Update()
    {
      Move();     
      //Jump();
       if(Input.GetKeyDown(KeyCode.Mouse0))
      {
         Atack();
      }
       if(Input.GetKeyDown(KeyCode.Mouse1))
      {
         Atack2();
      }

        
      if(Input.GetKeyDown(KeyCode.Q)&&qTimer <= 0) 
      {
        dash();
        qTimer+=5;
      }
      else
      {
       anim.SetBool("Dash", false);
      }
      if(qTimer>=0)
      {
       qTimer-= Time.deltaTime; 
      }

    }

    public void Move()
    {
       float H = Input.GetAxis("Horizontal");
       float V = Input.GetAxis("Vertical");

       transform.Rotate(0, H * Time.deltaTime * speedRotation, 0);
       transform.Translate(0,0, V * Time.deltaTime * speedPlayer);
       anim.SetFloat("SpeedX", H);
       anim.SetFloat("SpeedY", V);

       if(Input.GetKey(KeyCode.LeftShift))
       {
           speedPlayer = runPlayer;
           anim.SetBool("isRunning", true);
       }
       else
       {
           anim.SetBool("isRunning", false);
           speedPlayer = initialSpeed;
       }
     
    }

      private void dash()
    {
        if(qTimer<=0)
        {
         anim.SetBool("Dash", true); 
          rb.AddForce(transform.forward * dashSpeed, ForceMode.Impulse);
        }
    }


    /*public void Jump()
    {
        if(jump == false && Jtimer <= 0)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                Jtimer= 1;
                anim.SetBool("jump", true);
                rb.AddForce(new Vector3( 0, Jumpforce,0), ForceMode.Impulse);
            }
        }
        else{
            Falling();
        }
       
    }
    public void Falling()
{
    anim.SetBool("isFalling", true);
    anim.SetBool("jump", false);
}
private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Untagged")
        Debug.Log("SALTA GIL");
        Jtimer= 0;
        anim.SetBool("isFalling", true);  
        anim.SetBool("Jump", true);    
    }
private void OnCollisionExit(Collision other)
    {
        if(other.gameObject.tag == "Untagged")
        Debug.Log("no podes puuuuto");
        anim.SetBool("jump", false);
        anim.SetBool("isFalling", false);

    }*/
  
  private void Atack()
  {
     anim.SetTrigger("Atack");
  }
    private void Atack2()
  {
     anim.SetTrigger("Atack2");
  }
}

