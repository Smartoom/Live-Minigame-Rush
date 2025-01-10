using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuyMouthMovement : MonoBehaviour
{
    [SerializeField] private float scaleMag;
    [SerializeField] private float scaleMin;
    [SerializeField] private float scaleSpeeddd;
    void Update()
    {
        transform.localScale = new Vector3((Mathf.Sin(Time.time * scaleSpeeddd) + 1) / 2 * scaleMag + scaleMin, 1, 1);
    }
}
