using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    private IEnemyState currentState;

    public GameObject Target { get; set; }

    [SerializeField]
    private float meleeRange;

    [SerializeField]
    private float throwRange;

    [SerializeField]
    private Transform EnemyHealth;

    [SerializeField]
    private GameObject healthBar;

    [SerializeField]
    private GameObject coin;

    private float enemyHealthLevel = 1f;

    private SpriteRenderer spriteRenderer;
    

    public bool InMeleeRange
    {
        get
        {
            if (Target != null)
            {
                return Vector2.Distance(transform.position, Target.transform.position) <= meleeRange;
            }
            return false;
        }
    }

    public bool InThrowRange
    {
        get
        {
            if (Target != null)
            {
                return Vector2.Distance(transform.position, Target.transform.position) <= throwRange;
            }
            return false;
        }
    }
    
    public override void Start()
    {
        EnemyHealth.transform.localScale = new Vector3(enemyHealthLevel, 1, 1);

        base.Start();

        Player.Instance.Dead += new DeadEventHandler(RemoveTarget); 

        ChangeState(new IdleState());

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsDead)
        {
            if (!TakingDamage)
            {
                currentState.Execute();
            }
            LookAtTarget();
        }
        
    }

    public void ChangeState(IEnemyState newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }

        currentState = newState;

        currentState.Enter(this);
    }

    public void Move()
    {
        if (!Attack)
        {
            MyAnimator.SetFloat("speed", 1);

            transform.Translate(GetDirection() * (movementSpeed * Time.deltaTime));
        }
    }

    public Vector2 GetDirection()
    {
        return facingRight ? Vector2.right : Vector2.left;
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        currentState.OnTriggerEnter(other);
    }

    private void LookAtTarget()
    {
        if(Target != null)
        {
            float xDir = Target.transform.position.x - transform.position.x;

            if (xDir < 0 && facingRight || xDir > 0 && !facingRight)
            {
                ChangeDirection();
            }
        }
    }

    public void RemoveTarget()
    {
        Target = null;
        ChangeState(new PatrolState());
    }

    public override IEnumerator TakeDamage()
    {
        health -= 10;
        enemyHealthLevel -= 0.2f;
        EnemyHealth.transform.localScale = new Vector3(enemyHealthLevel, 1, 1);
        if (!IsDead)
        {
            Player.PlaySound("enemyDamageSound");
            MyAnimator.SetTrigger("damage");
        }
        else
        {
            Player.PlaySound("enemyDeathSound");
            coin.gameObject.SetActive(true);
            healthBar.gameObject.SetActive(false);
            MyAnimator.SetTrigger("die");

            yield return new WaitForSeconds(3f);

            Death();
            
        }
    }

    public override void Death()
    {
       // ChangeState(new IdleState());
        //Destroy(gameObject);
        //health = 50;
    }

    public override bool IsDead
    {
        get
        {
            return health <= 0;
        }
    }
}
