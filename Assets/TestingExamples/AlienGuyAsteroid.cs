using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienGuyAsteroid : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float impulse;

    private void Start()
    {
        rb.AddForce((new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f)) - transform.position).normalized * impulse, ForceMode2D.Impulse);//towards center
    }
    private void Update()
    {
        if (transform.position.sqrMagnitude > 100)
        {
            Destroy(gameObject);
        }
    }
}
