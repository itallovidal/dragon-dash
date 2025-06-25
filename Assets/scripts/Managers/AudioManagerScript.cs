using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using System.Collections.Generic;

public class AudioManagerScript : MonoBehaviour
{
    public static AudioManagerScript instance;

    public AudioSource audioSource;
    
    // Adicione referências para os diferentes clips de música
    [Header("Music Tracks")]
    public AudioClip menuMusic;
    public AudioClip cloudsLevelMusic;
    public AudioClip florestLevelMusic;
    public AudioClip spaceLevelMusic;
    
    // Dicionário para mapear cenas aos seus clips de música
    private Dictionary<string, AudioClip> sceneMusicMap;

    private string[] scenesWithMusic = {"LevelScene", "MenuScene", "clouds_level", "florest_level", "space_level"};

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        if (scenesWithMusic.Contains(SceneManager.GetActiveScene().name))
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Inicializa o dicionário que mapeia cenas às músicas
        InitializeMusicMap();

        audioSource = GetComponent<AudioSource>();
        
        // Configura a música da cena atual
        UpdateMusicForCurrentScene();
    }

    private void InitializeMusicMap()
    {
        sceneMusicMap = new Dictionary<string, AudioClip>
        {
            { "MenuScene", menuMusic },
            { "LevelScene", menuMusic }, // Use a mesma música do menu ou defina outra
            { "clouds_level", cloudsLevelMusic },
            { "florest_level", florestLevelMusic },
            { "space_level", spaceLevelMusic }
        };
    }

    private void UpdateMusicForCurrentScene()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        if (sceneMusicMap.TryGetValue(currentScene, out AudioClip clip) && clip != null)
        {
            if (audioSource.clip != clip)
            {
                audioSource.clip = clip;
                
                // Só toca se WantMusic for true
                if (PlayerPrefs.GetString("WantMusic", "true") == "true")
                {
                    float volume = currentScene == "MenuScene" ? 1f : 0.2f;
                    audioSource.volume = volume;
                    audioSource.Play();
                }
                else
                {
                    audioSource.volume = 0;
                    audioSource.Stop();
                }
            }
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (System.Array.Exists(scenesWithMusic, s => s == scene.name))
        {
            // Verifica se há uma música específica para esta cena
            if (sceneMusicMap.TryGetValue(scene.name, out AudioClip clip) && clip != null)
            {
                // Se a música atual for diferente, troca para a nova
                if (audioSource.clip != clip)
                {
                    audioSource.clip = clip;
                    
                    // Só toca se WantMusic for true
                    if (PlayerPrefs.GetString("WantMusic", "true") == "true")
                    {
                        float volume = scene.name == "MenuScene" ? 1f : 0.2f;
                        audioSource.volume = volume;
                        audioSource.Play();
                    }
                    else
                    {
                        audioSource.volume = 0;
                        audioSource.Stop();
                    }
                }
                // Se for a mesma música mas não estiver tocando, inicia apenas se WantMusic for true
                else if (!audioSource.isPlaying && PlayerPrefs.GetString("WantMusic", "true") == "true")
                {
                    float volume = scene.name == "MenuScene" ? 1f : 0.2f;
                    audioSource.volume = volume;
                    audioSource.Play();
                }
            }
        }
        else
        {
            audioSource.Stop();
        }
    }
}