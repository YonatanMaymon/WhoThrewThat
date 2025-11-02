using ArgumentException = System.ArgumentException;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] gameMusic;
    public AudioClip menuMusic, gameOverAudio, OrigamiCatchAudio, ScissorsSpawnAudio, buttonClick;
    public AudioSettings audioSettings;
    private AudioSource musicSource, collectAudioSource, scissorsAudioSource, UIAudioSource;
    private Coroutine musicCoroutine;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoad;
        MenuUI.onButtonClick += OnButtonClicked;
        SpawnManager.onScissorsSpawn += OnScissorsSpawn;
        PlayerController.onOrigamiCatch += OnOrigamiCatch;
        GameManager.onGameOver += OnGameOver;
    }

    void Start()
    {
        musicSource = AddAudioSource(audioSettings.musicVolume);
        UIAudioSource = AddAudioSource(audioSettings.SFXVolume);
        collectAudioSource = AddAudioSource(audioSettings.SFXVolume);
        scissorsAudioSource = AddAudioSource(audioSettings.SFXVolume);
    }
    private AudioSource AddAudioSource(float volumeModerator)
    {
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.volume = audioSettings.masterVolume * volumeModerator;
        return audioSource;
    }
    private void OnButtonClicked()
    {
        UIAudioSource.resource = buttonClick;
        UIAudioSource.Play();
    }

    private void OnSceneLoad(Scene scene, LoadSceneMode loadSceneMode)
    {
        StopMusic();
        switch (scene.buildIndex)
        {
            case (int)Enums.SCENES.MENU:
                musicCoroutine = StartCoroutine(PlayMenuMusic());
                break;
            case (int)Enums.SCENES.GAME:
                musicCoroutine = StartCoroutine(PlayGameMusic());
                break;
            default:
                throw new ArgumentException($"Scene '{scene.rootCount} is not defined in Enums.SCENES");
        }
        ;
    }

    private void StopMusic()
    {
        if (musicCoroutine != null)
            StopCoroutine(musicCoroutine);
        musicSource?.Stop();
    }
    IEnumerator PlayMenuMusic()
    {
        yield return new WaitForSeconds(audioSettings.delayMusicBeforePlayTime);
        musicSource.resource = menuMusic;
        musicSource.Play();
        yield return new WaitWhile(() => musicSource.isPlaying);
        musicCoroutine = StartCoroutine(PlayGameMusic());
    }
    IEnumerator PlayGameMusic()
    {
        while (true)
        {
            yield return new WaitForSeconds(audioSettings.delayMusicBeforePlayTime);
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
        StopMusic();
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
        MenuUI.onButtonClick -= OnButtonClicked;
        SpawnManager.onScissorsSpawn -= OnScissorsSpawn;
        PlayerController.onOrigamiCatch -= OnOrigamiCatch;
        GameManager.onGameOver -= OnGameOver;
    }
}
