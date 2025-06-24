using UnityEngine;


public class BackgroundScript : MonoBehaviour
{
    public float deadZone = 0;
    public float moveSpeed = 2;
    public float backgroundResetPlace;

    void Update()
    {
        transform.position += Vector3.left * Time.deltaTime * moveSpeed;
        ResetBackground();
    }

    private void ResetBackground()
    {
        if (transform.position.x <= deadZone * -1)
        {
            transform.position = new Vector3(backgroundResetPlace, transform.position.y, transform.position.z);
        }
    }
}
