using UnityEngine;
using UnityEngine.SceneManagement;

public class enemyController : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;
    public float moveSpeed = 2f;
    public float tolerance = 0.1f;

    private Vector3 targetPoint;
    private bool movingToEnd = true;

    void Start()
    {
        if (startPoint != null && endPoint != null)
            targetPoint = endPoint.position;
    }

    void Update()
    {
        Patrol();
    }

    void Patrol()
    {
        if (startPoint == null || endPoint == null)
            return;

        transform.position = Vector2.MoveTowards(transform.position, targetPoint, moveSpeed * Time.deltaTime);

        float distance = Vector2.Distance(transform.position, targetPoint);
        if (distance <= tolerance)
        {
            movingToEnd = !movingToEnd;
            targetPoint = movingToEnd ? endPoint.position : startPoint.position;
            Flip();
        }
    }

    void Flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerPrefs.SetString("LastLevel", SceneManager.GetActiveScene().name);
            SceneManager.LoadScene("GameOver");
        }
    }

    private void OnDrawGizmos()
    {
        if (startPoint != null && endPoint != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(startPoint.position, endPoint.position);
        }
    }
}
