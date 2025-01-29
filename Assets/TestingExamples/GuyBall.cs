using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuyBall : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float impulse;
    [SerializeField] private float maxSpeed;
    private float timeForMinigame = 10;

    private void Start()
    {
        rb.AddForce(new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * impulse, ForceMode2D.Impulse);
    }
    private void Update()
    {
        timeForMinigame -= Time.deltaTime;

        if (rb.linearVelocity.magnitude > maxSpeed)
            rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;

        else if (timeForMinigame <= 0)
        {
            MiniGameManager.instance.LoseLife();
            MiniGameManager.instance.NextGame();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Bin")
        {
            MiniGameManager.instance.GainPoint();
            MiniGameManager.instance.NextGame();
        }
    }
}
