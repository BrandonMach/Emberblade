using UnityEngine;

public class PatrolAI : MonoBehaviour
{
    // Denna Script används för enemies som bee som patrollar runt till spelaren är i range för att attackera
    [Header("general values")]
    [SerializeField] float speed = 10f;
    [SerializeField] float range = 50f;
    [SerializeField] Animator animator;
    float startingX;
    int dir = 1;
    bool idle;
    bool flipped;
    Rigidbody2D rb2d;

    [Header("Attacktimers")]
    [SerializeField] bool isCharging;
    float chargeTime = 0f;
    float startingTime = 0.7f;
    public float aggroTimer;
    [SerializeField] float startAggroTimer = 3f;

    [Header("Attack")]
    bool getAattackPos;
    bool isCurrentlyAttacking;
    [SerializeField] float attackAcc = 2;
    [SerializeField] float attackSpeed = 1;
    Vector2 attackDir;
    Vector2 endPositionForAttack;

    [Header("target")]
    private Vector2 movetowardsPlayer;
    private PlayerInfo playerInfoController;
    [SerializeField] float agroRangeX = 70;
    [SerializeField] float agroRangeY = 30;

    [Header("Misc Timers")]
    int invokeCounter = 0;
    float timer;
    [SerializeField] float timeToGetDelayedPos = 0.4f;
    [SerializeField] float dragEffect = 0.8f;
    [SerializeField] float startDragTimer = 4;
    float dragTimer;



    void Start()
    {
        chargeTime = startingTime;
        idle = true;
        startingX = transform.position.x;
        playerInfoController = GameObject.Find("Player").GetComponent<PlayerInfo>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Idle();
        Charge();
        Attack();
    }
    public void Charge()
    {
        if (isCharging)
        {
            animator.SetBool("IsCharing", true);
            chargeTime -= 1 * Time.deltaTime;
            Debug.Log(chargeTime);
            if (chargeTime <= 0)
            {
                isCharging = false;
                isCurrentlyAttacking = true;
                getAattackPos = true;
                aggroTimer = startAggroTimer;
                dragTimer = startDragTimer;
            }
        }
    }
    public void applyDragDuringAttack()
    {
        if (dragTimer > 0)
            dragTimer -= Time.deltaTime;
    }

    public void Attack()
    {
        if (isCurrentlyAttacking)
        {
            aggroTimer -= Time.deltaTime;
            applyDragDuringAttack();
            animator.SetBool("IsCharing", false);
            animator.SetBool("IsAttacking", true);

            //float angle = Mathf.Atan2(attackDir.x, attackDir.y) * Mathf.Rad2Deg;
            attackDir.Normalize();
            movetowardsPlayer = attackDir;

            Invoke("getDelayedPos", timeToGetDelayedPos);
            rb2d.AddForce((attackDir * attackAcc) * (dragTimer * dragEffect), ForceMode2D.Impulse);

            if (transform.position.x < playerInfoController.transform.position.x)
            {
                transform.localScale = new Vector2(-1, 1);
                flipped = false;
            }
            else
            {
                transform.localScale = new Vector2(1, 1);
                flipped = true;
            }
        }
        if(aggroTimer < 0)
        {
            isCurrentlyAttacking = false;
        }
    }


    private void getDelayedPos()
    {
        if (invokeCounter <= 1)
        {
            attackDir = playerInfoController.transform.position - transform.position;
        }

        invokeCounter++;

        if (invokeCounter > 1)
        {
            timer += Time.deltaTime;
            if (timer > 0.05f)
            {
                attackDir = playerInfoController.transform.position - transform.position;
                attackAcc += 1f;
                timer = 0;
                Debug.Log(timer);
            }
        }
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInfoController.TakeDamage(40);
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("Wall") && isCurrentlyAttacking)
        {
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("Ground") && isCurrentlyAttacking)
        {
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("Roof") && isCurrentlyAttacking)
        {
            Destroy(gameObject);
        }
    }

    public void Idle()
    {
        if (idle)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime * dir);
            if (transform.position.x < startingX || transform.position.x > startingX + range)
            {
                dir *= -1;
                Flip();
            }

            Collider2D[] hitColliders = Physics2D.OverlapBoxAll(transform.position, new Vector2(agroRangeX, agroRangeY), 0);

            foreach (var colliderHit in hitColliders)
            {
                if (colliderHit.gameObject.CompareTag("Player"))
                {
                    idle = false;
                    isCharging = true;
                }
            }
        }

    }

    public void Flip()
    {
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);

        if (transform.localScale.x > 0)
        {
            flipped = true;
        }
        else if (transform.localScale.x < 0)
        {
            flipped = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector2(agroRangeX, agroRangeY));
    }
}
