using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Hero : MonoBehaviour
{
    //скорость персонажа
    private float realSpeed = 0.55f;
    private float speed;
    private bool onTriggerSlowedCloudExit = false;


    //здоровье
    [SerializeField] private float maxHealth;
    [SerializeField] private float health;
    [SerializeField] private Slider healthSlider;
    //количество жизней
    public int lives = 4;
    [SerializeField] private Image[] brains;

    //сила прыжка
    private float jumpForce = 0.95f;

    private float HorizontalMove = 0;
    private bool FactingRight = true;
    [SerializeField] private bool blockMoveX = false;

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;
    private Collider2D bodyCollider;

    private bool isGrounded;
    private Collider2D ground; 
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask whatIsGround;

    private bool isDeadTrigger;
    [SerializeField] private LayerMask whatIsDeadTrigger;

    private bool jumpControl;
    private float jumpTime = 0;
    private float disableTime = 0;
    private float jumpControlTime = 0.7f;
    private float disableControlTime = 0.5f;

    private float timeLeft = 0;

    public bool haveKey = false;
    [SerializeField] private Image imageKey;

    public static Hero Instance { get; set; }

    private States State
    {
        get { return (States)anim.GetInteger("State"); }
        set { anim.SetInteger("State", (int)value); }
    }

    //получение компонентов Rigidbody2D и SpriteRender
    private void Awake()
    {
        maxHealth = 100;
        health = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth;
        
        //сохранение текущего здоровья
        if (!PlayerPrefs.HasKey("Health"))
        {
            PlayerPrefs.SetFloat("Health", maxHealth);
        }
        
        //сохранение одухотворенности
        if (!PlayerPrefs.HasKey("Lives"))
        {
            PlayerPrefs.SetInt("Lives", lives);
        }

        if (!PlayerPrefs.HasKey("HaveKey"))
        {
            PlayerPrefs.SetInt("HaveKey", (haveKey ? 1 : 0));
        }
        
        Instance = this;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        bodyCollider = GetComponent<CapsuleCollider2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        speed = realSpeed;
    }

    private void Start()
    {
        health = PlayerPrefs.GetFloat("Health");
        lives = PlayerPrefs.GetInt("Lives");
        if(PlayerPrefs.GetInt("HaveKey") == 1) haveKey = true;
        else haveKey = false;
        
    }

    private void LateUpdate()
    {
        Run(); 
    }

    private void FixedUpdate()
    {
        //print(transform.position.x);
        CheckDeadTrigger();
        CheckGround();
        Jump();
        CheckLives();
        CheckKey();

        //замедление от облака
        if (onTriggerSlowedCloudExit)
        {
            timeLeft += Time.deltaTime;
            if (timeLeft >= 1)
            {
                this.speed = this.realSpeed;
                timeLeft = 0;
                onTriggerSlowedCloudExit = false;
            }
        }
    }

   

    void Update()
    {
        if (isDeadTrigger) GetDamage(maxHealth);

        if (!blockMoveX)
        {
            if (isGrounded) State = States.calm;
            if (!isGrounded) State = States.jump;

            if (disableTime <= 0)
            {
                HorizontalMove = Input.GetAxisRaw("Horizontal") * speed;
            }
            else
            {
                HorizontalMove = 0;
                disableTime -= Time.deltaTime;
            }
            if (isGrounded && HorizontalMove != 0) State = States.run;
        }

        //установка слайдера здоровья в соответствии с текущим здоровьем
        healthSlider.value = health;
        //сохранение текущего здоровья
        PlayerPrefs.SetFloat("Health", health);
        
        

        //при нажатии на кнопку запускается метод run
        //if (Input.GetButton("Horizontal"))
        //    Run();
    }
    
    // "Вызывается перед закрытием приложения" (с) Олег
    private void OnApplicationQuit()
    {
        PlayerPrefs.DeleteKey("Health");
        PlayerPrefs.DeleteKey("Lives");
        PlayerPrefs.DeleteKey("HaveKey");
    }

    private void Run()
    {
        if (!blockMoveX)
        {
            transform.Translate(new Vector3(HorizontalMove * 10f * Time.deltaTime, 0, 0));
            //Vector2 targetVelocity = new Vector2(HorizontalMove * 10f, rb.velocity.y);
            //if (ground != null)
            //{
            //    targetVelocity += ground.attachedRigidbody.velocity;
            //}
            //rb.velocity = targetVelocity;

            if (HorizontalMove < 0 && FactingRight) Flip();
            else if (HorizontalMove > 0 && !FactingRight) Flip();
        }
    }
    
    private void Flip()
    {
        if (!blockMoveX)
        {
            FactingRight = !FactingRight;
            /*Vector3 dir = transform.right * Input.GetAxis("Horizontal");
            sprite.flipX = dir.x < 0.0f;*/
            transform.localScale *= new Vector2(-1, 1);
        }
    }

    private void Jump()
    {
        if (!blockMoveX)
        {
            //if (Input.GetButtonDown("Jump"))
            if (Input.GetKey(KeyCode.Space))
            {
                if (isGrounded) jumpControl = true;
            }
            else jumpControl = false;

            if (jumpControl)
            {
                blockMoveX = false;
                if ((jumpTime += Time.deltaTime) < jumpControlTime)
                {
                    rb.AddForce(transform.up * jumpForce / (jumpTime * 10), ForceMode2D.Impulse);
                }
            }
            else jumpTime = 0;
        }

        //добавляем силы для прыжка
        //rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }

    //проверка земли под ногами
    private void CheckGround()
    {
        Collider2D new_ground = Physics2D.OverlapBox(groundCheck.position, new Vector2(0.5f, 0.025f), 0.0f, whatIsGround);
        isGrounded = new_ground;
        if (!isGrounded)
        {
            ground = null;      
        } else
        {
            disableTime = 0;
            if (ground != new_ground)
            {
                ground = new_ground;
            }
        }
    }

    //проверка на триггер респавна
    private void CheckDeadTrigger()
    {
        isDeadTrigger = Physics2D.OverlapCircle(groundCheck.position, 0.25f, whatIsDeadTrigger);
    }

    public void HealthRecovery(float health)
    {
        this.health += health;
    }

    public void CheckLives()
    {
        PlayerPrefs.SetInt("Lives", lives);
        //система с одухотворенностью
        switch (lives)
        {
            case 3:
                brains[2].enabled = false;
                brains[1].enabled = true;
                brains[0].enabled = true;
                break;
            case 2: 
                brains[2].enabled = false;
                brains[1].enabled = false;
                brains[0].enabled = true;
                break;
            case 1: 
                brains[2].enabled = false;
                brains[1].enabled = false;
                brains[0].enabled = false;
                break;
            default:
                lives = 4;
                brains[2].enabled = true;
                brains[1].enabled = true;
                brains[0].enabled = true;
                break;
        }
    }
    //получение урона
    public void GetDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            lives--;
            PlayerPrefs.SetInt("Lives", lives);
            LevelAgain();
            health = maxHealth;
        }
    }

    //разрушение объекта
    protected virtual void LevelAgain()
    {
        //Destroy(this.gameObject);
        if (SceneManager.GetActiveScene().name == "Level2") SceneManager.LoadScene("Level2");
        else if (SceneManager.GetActiveScene().name == "Level1") SceneManager.LoadScene("Level1");

    }
    
    private void CheckKey()
    {
        if (haveKey) imageKey.enabled = true;
        else imageKey.enabled = false;
        PlayerPrefs.SetInt("HaveKey", (haveKey ? 1 : 0));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.otherCollider == bodyCollider && disableTime <= 0 && !isGrounded)
        {
            disableTime = disableControlTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("SlowingCloud") || collider.CompareTag("EvilCloud") && this.speed == realSpeed)
        {
            this.speed *= 0.65f;
        }

        if (collider.CompareTag("HealthCloud") && this.speed == realSpeed)
        {
            this.speed *= 1.1f;
        }

       
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.CompareTag("EvilCloud"))
        {
            GetDamage(0.08f);
        }

        if (collider.CompareTag("HealthCloud"))
        {
            HealthRecovery(0.1f);
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("SlowingCloud") || collider.CompareTag("EvilCloud") || collider.CompareTag("HealthCloud"))
        {
            onTriggerSlowedCloudExit = true;
            
        }
    }


    [SerializeField] private Transform AddPosition;
    
    private void LedgeGo()
    {
        transform.position = new Vector3(AddPosition.position.x, AddPosition.position.y, transform.position.z);
    }
    private void LedgeStop()
    {
        State = States.calm;
        blockMoveX = false;
    }

    public void StartAnimLedgeGo()
    {
        blockMoveX = true;
        rb.velocity = Vector2.zero;
        State = States.getUp;

    }


    public Rigidbody2D getRb()
    {
        return rb;
    }
}

public enum States
{
    calm,
    jump,
    run,
    getUp,
}
