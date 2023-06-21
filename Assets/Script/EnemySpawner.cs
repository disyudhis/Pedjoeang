using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Size of the spawner area")]
    public Vector3 spawnerSize;

    [Header("Rate of spawn")]
    public float spawnRate = 1f;

    [Header("Model to spawn")]
    [SerializeField] private GameObject enemyModel;

    private float spawnTimer = 0f;

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 1, 0, 0.5f);
        Gizmos.DrawCube(transform.position, spawnerSize);
    }

    private void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer > spawnRate)
        {
            spawnTimer = 0;
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        if (GameController.currentGameStatus == GameController.GameState.Playing)
        {
            //get a random position for the enemy

            Vector3 spawnPoint = transform.position + new Vector3(UnityEngine.Random.Range(-spawnerSize.x / 2, spawnerSize.x / 2),
            UnityEngine.Random.Range(-spawnerSize.y / 2, spawnerSize.y / 2),
            UnityEngine.Random.Range(-spawnerSize.z / 2, spawnerSize.z / 2));

            GameObject enemy = Instantiate(enemyModel, spawnPoint, transform.rotation);

            enemy.transform.SetParent(this.transform);
        }
        if (GameController.currentGameStatus == GameController.GameState.GameOver)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in enemies)
            {
                Destroy(enemy);
            }
        }
    }
}