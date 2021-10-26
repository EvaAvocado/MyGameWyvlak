using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Hero : MonoBehaviour
{
    //скорость персонажа
    [SerializeField] private float speed = 0.5f;

    //количество жизней
    private int lives = 0;
    [SerializeField] private Image[] brains;

    //сила прыжка
    private float jumpForce = 0.95f;

    private float HorizontalMove = 0;
    private bool FactingRight = true;

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;
    private Collider2D bodyCollider;

    private bool isGrounded;
    private Collider2D ground; 
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask whatIsGround;

    private bool isDeadTrigger;
    [SerializeField] private Transform deadTriggerCheck;
    [SerializeField] private LayerMask whatIsDeadTrigger;



    private bool jumpControl;
    private float jumpTime = 0;
    private float disableTime = 0;
    private float jumpControlTime = 0.7f;
    private float disableControlTime = 0.5f;

    public static Hero Instance { get; set; }

    private States State
    {
        get { return (States)anim.GetInteger("State"); }
        set { anim.SetInteger("State", (int)value); }
    }

    //получение компонентов Rigidbody2D и SpriteRender
    private void Awake()
    {
        brains[0].enabled = false;
        brains[1].enabled = false;
        brains[2].enabled = false;

        Instance = this;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        bodyCollider = GetComponent<CapsuleCollider2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }
    
    private void FixedUpdate()
    {
        //print(transform.position.x);
        CheckDeadTrigger();
        CheckGround();
        Jump();
        Run();
    }

    void Update()
    {
        if (isDeadTrigger) Die();
        
        if (isGrounded) State = States.idle;
        if (!isGrounded) State = States.jump;

        if (disableTime <= 0)
        {
            HorizontalMove = Input.GetAxisRaw("Horizontal") * speed;
        } else
        {
            HorizontalMove = 0;
            disableTime -= Time.deltaTime;
        }
        if (isGrounded && HorizontalMove != 0) State = States.run;

        //при нажатии на кнопку запускается метод run
        //if (Input.GetButton("Horizontal"))
        //    Run();
    }

    private void Run()
    {
        Vector2 targetVelocity = new Vector2(HorizontalMove * 10f, rb.velocity.y);
        if (ground != null)
        {
            targetVelocity += ground.attachedRigidbody.velocity;
        }
        rb.velocity = targetVelocity;

        if (HorizontalMove < 0 && FactingRight) Flip();
        else if (HorizontalMove > 0 && !FactingRight) Flip();

    }

    private void Flip()
    {
        FactingRight = !FactingRight;
        Vector3 dir = transform.right * Input.GetAxis("Horizontal");
        sprite.flipX = dir.x < 0.0f;
    }

        private void Jump()
    {
        //if (Input.GetButtonDown("Jump"))
        if (Input.GetKey(KeyCode.Space))
        {
            if (isGrounded) jumpControl = true;
        }
        else jumpControl = false;

        if (jumpControl)
        {
            if ((jumpTime += Time.deltaTime) < jumpControlTime)
            {
                rb.AddForce(transform.up * jumpForce / (jumpTime * 10), ForceMode2D.Impulse);
            }
        }
        else jumpTime = 0;

        //добавляем силы для прыжка
        //rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }

    //проверка земли под ногами
    private void CheckGround()
    {
        Collider2D new_ground = Physics2D.OverlapBox(groundCheck.position, new Vector2(0.4f, 0.025f), 0.0f, whatIsGround);
        isGrounded = new_ground;
        if (!isGrounded)
        {
            State = States.jump;
            this.transform.parent = null;
            ground = null;
        } else
        {
            disableTime = 0;
            if (ground != new_ground)
            {
                print(new_ground.name);
                ground = new_ground;
            }
        }
    }

    //проверка на триггер респавна
    private void CheckDeadTrigger()
    {
        isDeadTrigger = Physics2D.OverlapCircle(groundCheck.position, 0.25f, whatIsDeadTrigger);
    }

    //получение урона
    public void GetDamage()
    {
        lives++;
        Debug.Log(lives);
        if (lives > 3)
        {
            Die();
        }
        if (lives <= 3) brains[lives-1].enabled = true;
    }

    //разрушение объекта
    public virtual void Die()
    {
        //Destroy(this.gameObject);
        if (SceneManager.GetActiveScene().name == "Level2") SceneManager.LoadScene("Level2");
        else if (SceneManager.GetActiveScene().name == "Level1") SceneManager.LoadScene("Level1");

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.otherCollider == bodyCollider && disableTime <= 0 && !isGrounded)
        {
            disableTime = disableControlTime;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //if (collision.gameObject.name.Equals("Tile (5)"))
        //{
        //    this.transform.parent = null;
        //}
    }

}

public enum States
{
    idle,
    jump,
    run
}
