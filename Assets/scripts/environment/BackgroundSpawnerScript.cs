using UnityEngine;

public class BackgroundSpawnerScript : MonoBehaviour
{
    public GameObject firstLayerSprite;
    public GameObject secondLayerSprite;
    public GameObject thirdLayerSprite;


    void Start()
    {
        InstantiateBackground(firstLayerSprite, 0.5f);

        if (secondLayerSprite != null) { InstantiateBackground(secondLayerSprite, 2f); }

        if (thirdLayerSprite != null) { InstantiateBackground(thirdLayerSprite, 0.2f); }
    }

    // Funcao para criar 2 imagens para o sprite (background) que sera passado por parametro
    void InstantiateBackground(GameObject layerSprite, float moveSpeed)
    {
        // Criando a primeira imagem no centro do jogo, baseado na camera
        Vector3 cameraCenterPosition = new Vector3(transform.position.x, transform.position.y);
        GameObject firstSprite = Instantiate(layerSprite, cameraCenterPosition, transform.rotation);

        // Pegando metade do tamanho da imagem e o tamanho da camera para achar o posicionamento
        // correto da segunda imagem e do reposicionamento quando as imagens chegam no limite
        float spriteHalfWidth = layerSprite.GetComponent<SpriteRenderer>().bounds.extents.x;
        float cameraWidth = Camera.main.transform.position.x + Camera.main.orthographicSize * Camera.main.aspect;

        Vector3 backgroundResetPlace = new Vector3(cameraWidth + spriteHalfWidth, transform.position.y);

        setResetPlace(firstSprite, backgroundResetPlace.x);
        setBackgroundMovespeed(firstSprite, moveSpeed);
        setDeadZone(firstSprite, cameraWidth + spriteHalfWidth);

        //Criando a segunda imagem
        GameObject secondSprite = Instantiate(layerSprite, backgroundResetPlace, transform.rotation);
        setResetPlace(secondSprite, backgroundResetPlace.x);
        setBackgroundMovespeed(secondSprite, moveSpeed);
        setDeadZone(secondSprite, cameraWidth + spriteHalfWidth);
        
    }

    void setDeadZone(GameObject layerSprite, float deadZone)
    {
        BackgroundScript bgScript = layerSprite.GetComponent<BackgroundScript>();
        if (bgScript == null)
        {
            Debug.Log("Ops, esqueceu de vincular o fundo!");
        }

        bgScript.deadZone = deadZone;
    }

    void setBackgroundMovespeed(GameObject layerSprite, float speed)
    {
        BackgroundScript bgScript = layerSprite.GetComponent<BackgroundScript>();

        if (bgScript == null)
        {
            Debug.Log("Ops, esqueceu de vincular o fundo!");
        }

        bgScript.moveSpeed = speed;
    }

    void setResetPlace(GameObject layerSprite, float backgroundResetPlace)
    {
        BackgroundScript bgScript = layerSprite.GetComponent<BackgroundScript>();

        if (bgScript == null)
        {
            Debug.Log("Ops, esqueceu de vincular o fundo!");
        }

        bgScript.backgroundResetPlace = backgroundResetPlace;
    }

}
