using UnityEngine;

public class ShakeTheBottle : Minigame
{
    [SerializeField] private Transform Bottle;
    [SerializeField] private float ShakeNeeded = 3f, currentShake = 0f;

    private void OnEnable()
    {
        Debug.Log("Shake the soda bottle and make it pop before time runs out!");
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Update()
    {
        if(!isGameActive) { return; }

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        float totalMovement = Mathf.Sqrt(mouseX * mouseX + mouseY * mouseY); // Sqrt of (MouseX^2 + MouseY^2) = Mouse input value on movement for both axis
        if (totalMovement > 0)
        {
            currentShake += totalMovement * 2f * Time.deltaTime;
            Bottle.localScale = Vector3.Max(Vector3.one * currentShake, Vector3.one);
        }

        if (currentShake >= ShakeNeeded)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            EndMinigame(true);
        }
    }
}