using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuyHandControl : MonoBehaviour
{
    [SerializeField] private KeyCode handMoveKey;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float readyRotation;
    [SerializeField] private float readySpeed;
    [SerializeField] private float swingRotation;
    [SerializeField] private float swingSpeed;
    private bool isReady;
    void Update()
    {
        if (Input.GetKey(handMoveKey))
        {
            rb.MoveRotation(Mathf.MoveTowards(rb.rotation, readyRotation, readySpeed));
            isReady = true;
        }
        if (isReady && Input.GetKeyUp(handMoveKey))
        {
            rb.MoveRotation(swingRotation);
            isReady = false;
        }
    }
}
