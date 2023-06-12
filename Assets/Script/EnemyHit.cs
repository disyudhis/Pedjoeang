using TMPro;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    [SerializeField] private GameObject enemyDied;
    [SerializeField] private GameController gameController;
    [SerializeField] private GameObject popupCanvas;

    private void Awake()
    {
        gameController = FindObjectOfType<GameController>();
    }

    public void EnemyKilled()
    {
        Instantiate(enemyDied, transform.position, transform.rotation);
        //calculate the score for hitting this enemy
        float distanceFromPlayer = Vector3.Distance(transform.position, Vector3.zero);
        int bonusPoints = (int)distanceFromPlayer;

        int enemyScore = 10 * bonusPoints;

        //set our text for the popup - instantiate popup canvas
        popupCanvas.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = enemyScore.ToString();
        GameObject enemyPopup = Instantiate(popupCanvas, transform.position, Quaternion.identity);

        //adjust the scale of the popup
        enemyPopup.transform.localScale = new Vector3(transform.localScale.x * (distanceFromPlayer / 2),
            transform.localScale.y * (distanceFromPlayer / 2),
            transform.localScale.z * (distanceFromPlayer / 2));

        //pass score to GameController
        gameController.UpdatePlayerScore(enemyScore);

        Destroy(this.gameObject);
    }
}