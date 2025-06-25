using UnityEngine;

public class BackgroundSpawnerScript : MonoBehaviour
{
    public GameObject[] layerSprites; // sprites dos backgrounds
    public float[] layerSpeeds;      // velocidades dos sprites

    void Start()
    {
        // Verifica se os sprites foram inseridos
        if (layerSprites.Length == 0)
        {
            Debug.LogError("Nenhum sprite foi instanciado como background.");
            return;
        }

        // Verifica se as velocidades foram inseridas
        if (layerSpeeds.Length == 0)
        {
            Debug.LogError("Nenhuma velocidade foi definida para os backgrounds.");
            return;
        }

        // Verifica se o número de sprites e velocidades coincide
        if (layerSprites.Length != layerSpeeds.Length)
        {
            Debug.LogError("O número de sprites e velocidades não coincide.");
            return;
        }

        for (int i = 0; i < layerSprites.Length; i++)
        {
            InstantiateBackground(layerSprites[i], layerSpeeds[i]);
        }
    }

    // Instancia o background baseado no prefab passado como parametro
    void InstantiateBackground(GameObject layerSprite, float moveSpeed)
    {
        // Criando a primeira imagem no centro do jogo, baseado na camera
        Vector3 cameraCenterPosition = new Vector3(transform.position.x, transform.position.y);
        GameObject firstBackgroundInstance = Instantiate(layerSprite, cameraCenterPosition, transform.rotation);

        // Pegando metade do tamanho da imagem e o tamanho da camera para achar o posicionamento
        // correto da segunda imagem e do reposicionamento quando as imagens chegam no limite
        float spriteHalfWidth = layerSprite.GetComponent<SpriteRenderer>().bounds.extents.x;
        float cameraWidth = Camera.main.transform.position.x + Camera.main.orthographicSize * Camera.main.aspect;

        Vector3 backgroundResetPlace = new Vector3(cameraWidth + spriteHalfWidth, transform.position.y);

        setResetPlace(firstBackgroundInstance, backgroundResetPlace.x);
        setBackgroundMovespeed(firstBackgroundInstance, moveSpeed);
        setDeadZone(firstBackgroundInstance, cameraWidth + spriteHalfWidth);

        //Criando a segunda imagem
        GameObject secondBackgroundInstance = Instantiate(layerSprite, backgroundResetPlace, transform.rotation);
        setResetPlace(secondBackgroundInstance, backgroundResetPlace.x);
        setBackgroundMovespeed(secondBackgroundInstance, moveSpeed);
        setDeadZone(secondBackgroundInstance, cameraWidth + spriteHalfWidth);
    }

    // Defini a zona de morte para os sprites
    void setDeadZone(GameObject layerSpriteInstance, float deadZone)
    {
        BackgroundScript bgScript = layerSpriteInstance.GetComponent<BackgroundScript>();
        if (bgScript == null)
        {
            Debug.LogError("O prefab do background não tem o componente BackgroundScript!");
            return;
        }

        bgScript.deadZone = deadZone;
    }

    // Defini a velocidade de movimento do sprites
    void setBackgroundMovespeed(GameObject layerSpriteInstance, float speed)
    {
        BackgroundScript bgScript = layerSpriteInstance.GetComponent<BackgroundScript>();
        if (bgScript == null)
        {
            Debug.LogError("O prefab do background não tem o componente BackgroundScript!");
            return;
        }

        bgScript.moveSpeed = speed;
    }

    // Defini o ponto de nascimento dos sprites
    void setResetPlace(GameObject layerSpriteInstance, float backgroundResetPlace)
    {
        BackgroundScript bgScript = layerSpriteInstance.GetComponent<BackgroundScript>();
        if (bgScript == null)
        {
            Debug.LogError("O prefab do background não tem o componente BackgroundScript!");
            return;
        }

        bgScript.backgroundResetPlace = backgroundResetPlace;
    }
}
