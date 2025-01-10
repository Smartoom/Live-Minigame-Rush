using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuyBall : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float impulse;
    [SerializeField] private float maxSpeed;
    private void Start()
    {
        rb.AddForce(new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * impulse, ForceMode2D.Impulse);
    }
    private void Update()
    {
        if (rb.velocity.magnitude > maxSpeed)
            rb.velocity = rb.velocity.normalized * maxSpeed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Bin")
        {
            //add em points
            MiniGameSwitcher.instnace.NextGame();
        }
    }
}
