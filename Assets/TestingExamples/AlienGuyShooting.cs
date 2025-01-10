using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienGuyShooting : MonoBehaviour
{
    Camera cam;
    [SerializeField] private LineRenderer[] eyeLaserLines;
    private float lastShotTime;
    [SerializeField] private float laserLineRemainTime;
    [Header("boom booms")]
    [SerializeField] private float checkRadius;
    [SerializeField] private int pointsScored;//should not be done here
    private void Start()
    {
        cam = Camera.main;
    }
    private void Update()
    {
        lastShotTime += Time.deltaTime;
        if (lastShotTime >= laserLineRemainTime)
            foreach (LineRenderer lineRenderer in eyeLaserLines)
            {
                lineRenderer.enabled = false;
            }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            foreach (LineRenderer lineRenderer in eyeLaserLines)
            {
                lineRenderer.enabled = true;
                lastShotTime = 0;
                lineRenderer.SetPosition(0, lineRenderer.transform.position);
                lineRenderer.SetPosition(1, mousePos);
            }
            Collider2D asteroid = Physics2D.OverlapCircle(mousePos, checkRadius);
            if (asteroid != null)
            {
                Destroy(asteroid.gameObject);
                pointsScored++;
            }
        }
        if (pointsScored >= 5)
        {
            MiniGameSwitcher.instnace.NextGame();
        }
    }
}
