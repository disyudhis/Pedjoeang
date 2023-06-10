using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    private Rigidbody enemyRb;
    private Animator enemyAnim;
    public float gravityModifier;

    [Header("Control the speed")]
    public float maxSpeed;
    public float minSpeed;

    public Vector3 movementDirection;
    private float enemySpeed;

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        enemyAnim = GetComponent<Animator>();
        enemyAnim.SetBool("isMoving", false);
        Physics.gravity *= gravityModifier;

        enemySpeed = Random.Range(minSpeed, maxSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(movementDirection * Time.deltaTime * enemySpeed, Space.World);
        enemyAnim.SetBool("isMoving", true);
    }
}
