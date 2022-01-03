using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class playerController : MonoBehaviour
{
    [SerializeField] protected HeroData heroData;
    [SerializeField] protected EnemyData enemyData;
    [SerializeField] private int heroHP;
    [SerializeField] private Animator anim;
    [SerializeField] private bool jump;
    [SerializeField] private float heroInitialSpeed;
    [SerializeField] private float heroSpeed = 5f;
    [SerializeField] private bool isRunning, Dash;
    [SerializeField] private bool isFalling;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private int Jtimer =1;
    [SerializeField] private float timer =1;
    private float qTimer=0;
    private float ySpeed;
    private int temporizador =0;
    [SerializeField] private GameObject Door;
    
    [SerializeField] private GameObject Bottle_Health;
    private InventoryManager mgInventory;
    [SerializeField] private Slider lifeBar;
    
    //events
    public static event Action onDeath;
    public static event Action<int> onLivesChange;
    void Start()
    {
       heroInitialSpeed = heroSpeed;
       Dash = false;
       isFalling = false;
       isRunning = false;
       jump = false;
       anim = GetComponent<Animator>();
       rb = GetComponent<Rigidbody>();
       mgInventory = GetComponent<InventoryManager>();    
       onLivesChange?.Invoke(heroHP); 
    }

    void Update()
    {     
      //Move();
      //Jump();
      GameOver();

        if (Input.GetKeyDown(KeyCode.Z) && mgInventory.InventoryOneHas())
        {
            UseItem();
            heroHP += 1;  
        }

        if (Input.GetKeyDown(KeyCode.X) && mgInventory.InventoryTwoHas())
        {
            UseItem();
            heroHP += 1;  
        }

        if (Input.GetKeyDown(KeyCode.C) && mgInventory.InventoryThreeHas())
        {
            UseItem();
            heroHP += 1; 
        }
        
        lifeBar.GetComponent<Slider>().value = heroHP;
    }

    private void FixedUpdate()
    {
        Move();
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

       transform.Rotate(0, H * Time.deltaTime * heroData.HeroSpeedRotation, 0);
       transform.Translate(0,0, V * Time.deltaTime * heroSpeed);
       anim.SetFloat("SpeedX", H);
       anim.SetFloat("SpeedY", V);

       if(Input.GetKey(KeyCode.LeftShift))
       {
           heroSpeed = heroData.HeroRunSpeed;
           anim.SetBool("isRunning", true);
       }
       else
       {
           anim.SetBool("isRunning", false);
           heroSpeed = heroInitialSpeed;
       }
     
    }

      private void dash()
    {
        if(qTimer<=0)
        {
         anim.SetBool("Dash", true); 
          rb.AddForce(transform.forward * heroData.HerodashSpeed, ForceMode.Impulse);
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

     public void OnTriggerEnter(Collider other) 
    {
    
        if (other.gameObject.CompareTag("EnemyWeapon")) 
        {
           heroHP -= enemyData.EnemyAttackDamage;
           Debug.Log("Monstruo Pega");
           onLivesChange?.Invoke(heroHP);
        }
        if (other.gameObject.CompareTag("Lever"))
        {
            temporizador ++;
        }
    
        if(temporizador > 2)
        {
            Debug.Log("PUERTA DESTRUIDA");
            Destroy(Door.gameObject);   
        }  
        if (other.gameObject.CompareTag("Food"))
        {
            Debug.Log("food");
            GameObject food = other.gameObject;
            food.SetActive(false);
            mgInventory.AddInventoryFour(food.name, food);
            mgInventory.SeeInventoryFour();
            mgInventory.CountFood(food);
        }              

        if(other.gameObject.CompareTag("Potion"))
        {
            heroHP = heroHP + 10;
            Destroy(Bottle_Health.gameObject);
            Debug.Log("POCIÓN DESTRUIDA");
            onLivesChange?.Invoke(heroHP);
        }
    }

    private void GameOver()
    {
        if(heroHP <= 0)
        {
            onDeath?.Invoke();
            Debug.Log("Player Died");
        }
    }
   private void UseItem()
    {
        GameObject food = mgInventory.GetInventoryFour("food");
        food.SetActive(true);
        food.transform.position = transform.position + new Vector3(1f,.1f,.1f);
    }

}

