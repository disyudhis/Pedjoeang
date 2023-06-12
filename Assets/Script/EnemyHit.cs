using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    [SerializeField] private GameObject enemyDied;

    public void EnemyKilled()
    {
        Instantiate(enemyDied, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }
}
