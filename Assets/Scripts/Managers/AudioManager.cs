using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip menuMusic;
    public AudioClip[] gameMusic;
    public AudioClip gameOverAudio;
    public AudioClip OrigamiCatchAudio;
    public AudioClip ScissorsSpawnAudio;
    public VolumeSettings volumeSettings;
    private AudioSource musicSource;
    private AudioSource collectAudioSource;
    private AudioSource scissorsAudioSource;
    private bool gamePlaying = true;
    void Start()
    {
        SpawnManager.onScissorsSpawn += OnScissorsSpawn;
        PlayerController.onOrigamiCatch += OnOrigamiCatch;
        GameManager.onGameOver += OnGameOver;

        musicSource = AddAudioSource(volumeSettings.musicVolume);
        collectAudioSource = AddAudioSource(volumeSettings.collectingVolume);
        scissorsAudioSource = AddAudioSource(volumeSettings.scissorsIndicatorVolume);
        StartCoroutine(PlayAndWait());
    }

    private AudioSource AddAudioSource(float volumeModerator)
    {
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.volume = volumeSettings.masterVolume * volumeModerator;
        return audioSource;
    }

    private void OnGameOver()
    {
        gamePlaying = false;
        musicSource.resource = gameOverAudio;
        musicSource.Play();
    }

    IEnumerator PlayAndWait()
    {
        while (gamePlaying)
        {
            PlayRandom();
            yield return new WaitWhile(() => musicSource.isPlaying);
        }
    }
    private void PlayRandom()
    {
        musicSource.resource = gameMusic[Random.Range(0, gameMusic.Length)];
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
        SpawnManager.onScissorsSpawn -= OnScissorsSpawn;
        PlayerController.onOrigamiCatch -= OnOrigamiCatch;
        GameManager.onGameOver -= OnGameOver;
    }
}
