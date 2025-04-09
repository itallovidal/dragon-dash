using UnityEngine;


public class PowerUpScript : MonoBehaviour
{
    public float deadZone = 0;
    public float moveSpeed = 2;
    public float powerUpResetPlace;

    void Update()
    {
        transform.position += Vector3.left * Time.deltaTime * moveSpeed;
        ResetPowerUp();
    }

    private void ResetPowerUp()
    {
        if (transform.position.x <= (deadZone) * -1)
        {
            transform.position = new Vector3(powerUpResetPlace, transform.position.y, transform.position.z);
        }
    }
}
