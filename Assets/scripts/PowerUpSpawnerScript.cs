using UnityEngine;

public class PowerUpSpawnerScript : MonoBehaviour
{
    public GameObject firstPowerUpSprite;
    public GameObject secondPowerUpSprite;

    void Start()
    {
        InstantiatePowerUp(firstPowerUpSprite, 0.5f);
        InstantiatePowerUp(secondPowerUpSprite, 2f);
    }

    void InstantiatePowerUp(GameObject PowerUpSprite, float moveSpeed)
    {
        // Obtendo o centro da câmera para posicionar o PowerUp
        Vector3 cameraCenterPosition = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
        cameraCenterPosition.z = 0; // Garantir que o objeto esteja no plano correto

        GameObject firstSprite = Instantiate(PowerUpSprite, cameraCenterPosition, Quaternion.identity);

        // Configurando propriedades do PowerUp
        float spriteHalfWidth = PowerUpSprite.GetComponent<SpriteRenderer>().bounds.extents.x;
        float cameraWidth = Camera.main.orthographicSize * Camera.main.aspect;

        Vector3 powerUpResetPlace = new Vector3(cameraWidth + spriteHalfWidth, cameraCenterPosition.y);

        setResetPlace(firstSprite, powerUpResetPlace.x);
        setPowerUpMovespeed(firstSprite, moveSpeed);
        setDeadZone(firstSprite, cameraWidth + spriteHalfWidth);

        //Criando a segunda imagem
        GameObject secondSprite = Instantiate(PowerUpSprite, powerUpResetPlace, transform.rotation);
        setResetPlace(secondSprite, powerUpResetPlace.x);
        setPowerUpMovespeed(secondSprite, moveSpeed);
        setDeadZone(secondSprite, cameraWidth + spriteHalfWidth);

        Debug.Log("PowerUp instanciado na posição: " + cameraCenterPosition);
    }

    void setDeadZone(GameObject PowerUpSprite, float deadZone)
    {
        PowerUpScript powerUpScript = PowerUpSprite.GetComponent<PowerUpScript>();
        if (powerUpScript == null)
        {
            Debug.LogError("O script PowerUpScript não está anexado ao objeto!");
            return;
        }

        powerUpScript.deadZone = deadZone;
    }

    void setPowerUpMovespeed(GameObject PowerUpSprite, float speed)
    {
        PowerUpScript powerUpScript = PowerUpSprite.GetComponent<PowerUpScript>();
        if (powerUpScript == null)
        {
            Debug.LogError("O script PowerUpScript não está anexado ao objeto!");
            return;
        }

        powerUpScript.moveSpeed = speed;
    }

    void setResetPlace(GameObject PowerUpSprite, float powerUpResetPlace)
    {
        PowerUpScript powerUpScript = PowerUpSprite.GetComponent<PowerUpScript>();
        if (powerUpScript == null)
        {
            Debug.LogError("O script PowerUpScript não está anexado ao objeto!");
            return;
        }

        powerUpScript.powerUpResetPlace = powerUpResetPlace;
    }
}
