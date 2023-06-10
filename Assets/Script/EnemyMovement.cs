using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    //public GameObject Player;
    // public float Distance;
    //public bool isAngered;

    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;
    public float health;
    public float gravityModifier;
    private Rigidbody enemyRb;
    private Animator enemyAnim;
    public NavMeshAgent enemy;

    // Patroling
    public Vector3 walkPoint;

    private bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;

    public GameObject projectile;
    private bool alreadyAttacked;

    //States
    public float sightRange, attackRange;

    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        enemyRb = GetComponent<Rigidbody>();
        enemyAnim = GetComponent<Animator>();
        Physics.gravity *= gravityModifier;
        player = GameObject.Find("PlayerObj").transform;
        enemy = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        //Checl for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange)
        {
            enemyAnim.SetBool("isMoving", true);
            Patroling();
        } else
        {
            enemyAnim.SetBool("isMoving", false);
        }
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
    }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)

            enemy.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
  
    }

    private void ChasePlayer()
    {
        enemy.SetDestination(player.position);
     
    }

    private void AttackPlayer()
    {
        //Make sure enemy doesn't move
        enemy.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            //Attack code here
            Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb.AddForce(transform.up * 8f, ForceMode.Impulse);
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0) Invoke(nameof(DestroyEnemy), 0.5f);
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }

    // Update is called once per frame
    //private void Update()
    //{
    //   Distance = Vector3.Distance(Player.transform.position, this.transform.position);

    //   if (Distance <= 5)
    //   {
    //      isAngered = true;
    //  }
    //   if (Distance >= 5f)
    //  {
    //isAngered = false;
    //    }

    //   if (isAngered)
    //   {
    //       _enemy.isStopped = false;
    //        _enemy.SetDestination(Player.transform.position);
    //   }
    //if (!isAngered)
    //    {
    //         _enemy.isStopped = true;
    //     }
    //  }
}