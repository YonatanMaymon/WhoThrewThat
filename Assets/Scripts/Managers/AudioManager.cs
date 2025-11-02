using ArgumentException = System.ArgumentException;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] gameMusic;
    public AudioClip menuMusic, gameOverAudio, OrigamiCatchAudio, ScissorsSpawnAudio;
    public AudioSettings audioSettings;
    private AudioSource musicSource, collectAudioSource, scissorsAudioSource;
    private Coroutine musicCoroutine;
    private bool gamePlaying = true;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoad;
        SpawnManager.onScissorsSpawn += OnScissorsSpawn;
        PlayerController.onOrigamiCatch += OnOrigamiCatch;
        GameManager.onGameOver += OnGameOver;
    }

    void Start()
    {
        musicSource = AddAudioSource(audioSettings.musicVolume);
        collectAudioSource = AddAudioSource(audioSettings.collectingVolume);
        scissorsAudioSource = AddAudioSource(audioSettings.scissorsIndicatorVolume);
    }
    private AudioSource AddAudioSource(float volumeModerator)
    {
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.volume = audioSettings.masterVolume * volumeModerator;
        return audioSource;
    }

    private void OnSceneLoad(Scene scene, LoadSceneMode loadSceneMode)
    {
        StartCoroutine(LoadSceneMusic(scene));
    }

    private IEnumerator LoadSceneMusic(Scene scene)
    {
        yield return new WaitForSeconds(audioSettings.delayMusicOnSceneLoadTime);
        switch (scene.buildIndex)
        {
            case (int)Enums.SCENES.MENU:
                StartMusicCoroutine(PlayMenuMusic());
                break;
            case (int)Enums.SCENES.GAME:
                StartMusicCoroutine(PlayGameMusic());
                break;
            default:
                throw new ArgumentException($"Scene '{scene.rootCount} is not defined in Enums.SCENES");
        }
        ;
    }
    private void StartMusicCoroutine(IEnumerator routine)
    {
        if (musicCoroutine != null)
            StopCoroutine(musicCoroutine);
        musicCoroutine = StartCoroutine(routine);
    }
    IEnumerator PlayMenuMusic()
    {
        musicSource.resource = menuMusic;
        yield return new WaitWhile(() => musicSource.isPlaying);
        StartMusicCoroutine(PlayGameMusic());
    }
    IEnumerator PlayGameMusic()
    {
        while (gamePlaying)
        {
            PlayRandomGameMusic();
            yield return new WaitWhile(() => musicSource.isPlaying);
        }
    }

    private void PlayRandomGameMusic()
    {
        musicSource.resource = gameMusic[Random.Range(0, gameMusic.Length)];
        musicSource.Play();
    }

    private void OnGameOver()
    {
        gamePlaying = false;
        StopCoroutine(musicCoroutine);
        musicSource.resource = gameOverAudio;
        musicSource.Play();
    }

    private void OnOrigamiCatch(int _data)
    {
        collectAudioSource.resource = OrigamiCatchAudio;
        collectAudioSource.Play();
    }

    private void OnScissorsSpawn()
    {
        scissorsAudioSource.resource = ScissorsSpawnAudio;
        scissorsAudioSource.Play();
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoad;
        SpawnManager.onScissorsSpawn -= OnScissorsSpawn;
        PlayerController.onOrigamiCatch -= OnOrigamiCatch;
        GameManager.onGameOver -= OnGameOver;
    }
}
