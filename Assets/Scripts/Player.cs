using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;

public delegate void DeadEventHandler();

public class Player : Character
{

    private static Player instance;

    public event DeadEventHandler Dead;

    public static Player Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<Player>();
            }
            return instance;
        }
    }

    //private Animator myAnimator;

    //[SerializeField]
    //private float movementSpeed;

    //private bool facingRight;

    private Iuseable useable;

    [SerializeField]
    private Transform[] groundPoints;

    [SerializeField]
    private float groundRadius;

    [SerializeField]
    private LayerMask whatIsGround;

    private bool immortal = false;

    [SerializeField]
    private float immortalTime;

    [SerializeField]
    private bool airControl;

    [SerializeField]
    private float jumpForce;

    [SerializeField]
    private float climbSpeed;

    [SerializeField]
    private RectTransform HealthBar;

    private float HealthLevel = 1f;

    [SerializeField]
    private Text healthLev;

    public static int totalCoins;

    public static AudioSource audioSource;
    public static AudioClip playerJumpSound,coinSound, knifeThrowSound,meleeAttackSound,slideSound, walkSound,enemyDamageSound, enemyDeathSound;
    public static Sprite ladderIcon,doorIcon;

    private string ladderNdoor;
    private bool isdoor;

    private string doorType;

    //[SerializeField]
    //private GameObject KnifePrefab;

    //[SerializeField]
    //private Transform KnifePoint;

    public Rigidbody2D MyRigidBody { get; set; }

    public GameObject ClimbBtn;
    public Button SlideBtn;

    //public bool Attack { get; set; }
    public bool Slide { get; set; }
    public bool Jump { get; set; }
    public bool OnGround { get; set; }
    public bool OnLadder { get; set; }

    private bool isWater;

    public static int deadCount;

    public static bool lifeOver;

    public bool isFalling
    {
        get
        {
            return MyRigidBody.velocity.y < 0;
        }
    }

    private Vector3 StartPos;

    private SpriteRenderer spriteRenderer;

    public override void Start()
    {
        isWater = false;
        lifeOver = false;
        deadCount = 0;
        totalCoins = 0;
        HealthBar.localScale = new Vector3(HealthLevel, 1f); 
        SlideBtn.interactable = false;
        ClimbBtn.GetComponent<Button>().interactable = false;
        base.Start();
        OnLadder = false;
        //facingRight = true;
        StartPos = transform.position;
        MyRigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        //myAnimator = GetComponent<Animator>();

        coinSound = Resources.Load<AudioClip>("coinSound");
        knifeThrowSound = Resources.Load<AudioClip>("knifeThrow");
        meleeAttackSound = Resources.Load<AudioClip>("meleeAttackSound");
        slideSound = Resources.Load<AudioClip>("slideSound");
        walkSound = Resources.Load<AudioClip>("walk1");
        enemyDamageSound = Resources.Load<AudioClip>("enemyDamage2");
        enemyDeathSound = Resources.Load<AudioClip>("enemyDeath");
        playerJumpSound = Resources.Load<AudioClip>("playerJump");

        audioSource = GetComponent<AudioSource>();

        ladderIcon = Resources.Load<Sprite>("LadderButton");
        doorIcon = Resources.Load<Sprite>("DoorButton");

        isdoor = false;
    }
    void Update()
    {
        if (!TakingDamage && !IsDead)
        {
            if( isWater == true || transform.position.y <= -14f )
            {
                deadCount += 1;
                if (deadCount < 3)
                {
                    Death();
                }
                else
                {
                    StartCoroutine(LifeOver());
                }
            }
            //if (transform.position.y <= -14f)
            //{

            //    deadCount += 1;
            //    if(deadCount < 3)
            //    {
            //        Death();
            //    }
            //    else
            //    {
            //        StartCoroutine(LifeOver());
            //    }
            //}
            HandleInput();
        }
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        HealthBar.localScale = new Vector3(health / 100f, 1f);
        healthLev.text = health.ToString();
        if (MyRigidBody.velocity == Vector2.zero)
        {
            SlideBtn.interactable = false;
        }
        else
        {
            SlideBtn.interactable = true;
        }
        
        if (!TakingDamage && !IsDead)
        {
            float horizontal = CrossPlatformInputManager.GetAxis("Horizontal");
            float vertical = CrossPlatformInputManager.GetAxis("Vertical");

            OnGround = IsGrounded();
            HandleMovements(horizontal,vertical);

            Flip(horizontal);
            //HandleAttacks();

            HandleLayers();
        }

        //ResetValues(); 
    }

    public void OnDead()
    {
        if (Dead != null)
        {
            Dead();
        }
    }

    private void HandleMovements(float horizontal, float vertical) 
    {
        if (isFalling)
        {
            gameObject.layer = 10;
            MyAnimator.SetBool("land", true);
        }
        if(!Attack && !Slide && (OnGround || airControl) && !OnLadder)
        {
            MyRigidBody.velocity = new Vector2(horizontal * movementSpeed, MyRigidBody.velocity.y);
        }
        if(Jump && MyRigidBody.velocity.y == 00 && !OnLadder)
        {
            PlaySound("playerJumpSound");
            MyRigidBody.AddForce(new Vector2(0, jumpForce));
        }
        MyAnimator.SetFloat("speed", Mathf.Abs(horizontal));

        if (OnLadder)
        {
            MyAnimator.speed = Mathf.Abs(vertical); 
            MyRigidBody.velocity = new Vector2(horizontal * 0, vertical * climbSpeed);
        }

        //if (MyRigidBody.velocity.y <0)
        //{
        //    myAnimator.SetBool("land", true);
        //}

        //if (!myAnimator.GetBool("slide") && !this.myAnimator.GetCurrentAnimatorStateInfo(0).IsTag("AttackAnimation") && (isGrounded || airControl))
        //{
        //    MyRigidBody.velocity = new Vector2(horizontal * movementSpeed, MyRigidBody.velocity.y);

        //}
        //if (isGrounded && jump)
        //{  
        //    isGrounded = false;
        //    MyRigidBody.AddForce(new Vector2(0, jumpForce));
        //    myAnimator.SetTrigger("jump"); 
        //}

        //if(slide && !this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("SlideAnimation"))
        //{
        //    myAnimator.SetBool("slide", true);
        //}
        //else if (!this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("SlideAnimation"))
        //{
        //    myAnimator.SetBool("slide", false);
        //}

        //myAnimator.SetFloat("speed", Mathf.Abs(horizontal));


    }

    private void Flip(float horizontal)
    {
        if(horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
        {
            ChangeDirection();
            //facingRight = !facingRight;

            //Vector3 theScale = transform.localScale;
            //theScale.x *= -1;

            //transform.localScale = theScale;
        }
    }

    //private void HandleAttacks()
    //{
    //    if (attack && isGrounded && !this.myAnimator.GetCurrentAnimatorStateInfo(0).IsTag("AttackAnimation"))
    //    {
    //        myAnimator.SetTrigger("attack");
    //        MyRigidBody.velocity = Vector2.zero;
    //    }
    //    if(jumpAttack && !isGrounded && !this.myAnimator.GetCurrentAnimatorStateInfo(1).IsName("JumpAttackAnimation"))
    //    {
    //        myAnimator.SetBool("jumpAttack", true);
    //    }
    //    if (!jumpAttack && !this.myAnimator.GetCurrentAnimatorStateInfo(1).IsName("JumpAttackAnimation"))
    //    {
    //        myAnimator.SetBool("jumpAttack", false);
    //    }
    //}

    private void HandleInput()
    {
        if ((CrossPlatformInputManager.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.Space)) && !OnLadder && !isFalling)
        {
            MyAnimator.SetTrigger("jump");
            //jump = true;
        }
        if (CrossPlatformInputManager.GetButtonDown("Attack") || Input.GetKeyDown(KeyCode.F))
        {
            MyAnimator.SetTrigger("attack");
            PlaySound("meleeAttackSound");
            //attack = true;    
            //jumpAttack = true;
        }
        if (CrossPlatformInputManager.GetButtonDown("Slide") || Input.GetKeyDown(KeyCode.LeftShift))
        {
            if(MyRigidBody.velocity != Vector2.zero && !OnLadder)
            {
                PlaySound("slideSound");
                MyAnimator.SetTrigger("slide");
            }
            
            //slide = true;
        }
        if (CrossPlatformInputManager.GetButtonDown("Throw") || Input.GetKeyDown(KeyCode.LeftControl))
        {
            MyAnimator.SetTrigger("throw");
        }
        if (Input.GetKeyDown(KeyCode.E) || CrossPlatformInputManager.GetButtonDown("Climb"))
        {
            if(ladderNdoor == "ladder")
            {
                Use();
            }
            else if(ladderNdoor == "door")
            {
                if(doorType == "normal")
                DoorControlScript.DoorControl(isdoor);

                if(doorType == "stoneDoor")
                {
                    
                    StoneDoorScript.speed = 3;
                }
            }
        }
    }

    //private void ResetValues()
    //{
    //    attack = false;
    //    slide = false;
    //    jump = false;
    //    jumpAttack = false;
    //}

    private bool IsGrounded()
    {
        if (MyRigidBody.velocity.y <= 0)
        {
            foreach(Transform point in groundPoints)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, whatIsGround);

                for(int i = 0; i < colliders.Length; i++)
                {
                    if (colliders[i].gameObject != gameObject)
                    {
                        //myAnimator.ResetTrigger("jump");
                        //myAnimator.SetBool("land", false);
                        return true;
                    }
                }
            }
        }
        return false;
    }

    private void HandleLayers()
    {
        if (!OnGround)
        {
            MyAnimator.SetLayerWeight(1, 1);
        }
        else
        {
            MyAnimator.SetLayerWeight(1, 0);
        }
        
    }

    public override void ThrowKnife(int value)
    {
        if (!OnGround && value == 1 || OnGround && value == 0)
        {
            PlaySound("knifeThrow");
            base.ThrowKnife(value);
        }
    }

    private IEnumerator IndicateImmortal()
    {
        while (immortal)
        {
            spriteRenderer.enabled = false;

            yield return new WaitForSeconds(0.1f);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(0.1f);
        }
    }

    public override IEnumerator TakeDamage()
    {
        if (!immortal)
        {
            if(health > 0)
            {
                health -= 10;
            }
            
            if (!IsDead)
            {
                MyAnimator.SetTrigger("damage");
                immortal = true;
                StartCoroutine(IndicateImmortal());

                yield return new WaitForSeconds(immortalTime);

                immortal = false;
            }
            else
            {
                MyAnimator.SetLayerWeight(1, 0);
                MyAnimator.SetTrigger("die");
                deadCount += 1;
                if(deadCount >= 3)
                {
                    StartCoroutine(LifeOver());
                    
                   
                }
            }
        }
    }

    IEnumerator LifeOver()
    {
        yield return new WaitForSeconds(1.5f);
        lifeOver = true;
    }

    private void Use()
    {
        if(useable != null)
        {
            useable.Use();
        }
    }

    public override bool IsDead
    {
        get
        {
            if (health <= 0)
            {
                OnDead();
            }
            return health <= 0;
        }
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Useable")
        {
            ClimbBtn.GetComponent<Button>().interactable = true;
            ClimbBtn.GetComponent<Image>().sprite = ladderIcon;
            useable = other.GetComponent<Iuseable>();
            ladderNdoor = "ladder";
        }
        if (other.CompareTag("coin"))
        {
            PlaySound("coinSound");
            totalCoins += 1;
            Debug.Log(totalCoins);
            Destroy(other.gameObject);
        }
        if(other.tag == "water")
        {
            isWater = true;
        }
        if(other.tag == "door")
        {
            ClimbBtn.GetComponent<Button>().interactable = true;
            ClimbBtn.GetComponent<Image>().sprite = doorIcon;
            ladderNdoor = "door";
            isdoor = true;
            doorType = "normal";
        }
        if (other.tag == "stonedoor")
        {
            ClimbBtn.GetComponent<Button>().interactable = true;
            ClimbBtn.GetComponent<Image>().sprite = doorIcon;
            ladderNdoor = "door";
            isdoor = true;
            doorType = "stoneDoor";
        }
        if (other.tag == "coffindoor")
        {
            coffinBoxScript.isDoormove = true;          
        }

        if (other.tag == "enemyDeath")
        {
            health = 0;
        }
        

        base.OnTriggerEnter2D(other);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Useable")
        {
            ClimbBtn.GetComponent<Button>().interactable = false;
            useable = null;
        }

        if(other.tag == "water")
        {
            isWater = false;
        }

        if (other.tag == "door")
        {
            ClimbBtn.GetComponent<Button>().interactable = false;
            isdoor = false;
        }
        if (other.tag == "stonedoor")
        {
            ClimbBtn.GetComponent<Button>().interactable = false;
            doorType = null;
        }
    }

    public override void Death()
    {
        MyRigidBody.velocity = Vector2.zero;
        
        Debug.Log("dead count "+deadCount);
            MyAnimator.SetTrigger("idle");
            health = 100;
            transform.position = StartPos;
    }

    //public void OnTriggerEnter(Collider other)
    //{
    //    if(other.CompareTag("coin"))
    //    {
    //        totalCoins += 1;
    //        Debug.Log(totalCoins);
    //        Destroy(other.gameObject);
    //    }
    //}

    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "coinSound":
                audioSource.PlayOneShot(coinSound);
                break;
            case "knifeThrow":
                audioSource.PlayOneShot(knifeThrowSound);
                break;
            case "meleeAttackSound":
                audioSource.PlayOneShot(meleeAttackSound);
                break;
            case "slideSound":
                audioSource.PlayOneShot(slideSound);
                break;
            case "enemyDamageSound":
                audioSource.PlayOneShot(enemyDamageSound);
                break;
            case "enemyDeathSound":
                audioSource.PlayOneShot(enemyDeathSound);
                break;
            case "playerJumpSound":
                audioSource.PlayOneShot(playerJumpSound);
                break;
        }
    }

    public void WalkSound()
    {
        if(!Attack && !Slide && (OnGround || airControl) && MyRigidBody.velocity.magnitude > 0)
        {
            audioSource.PlayOneShot(walkSound);
        }
    }
}
