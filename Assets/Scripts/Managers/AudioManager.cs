using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip menuMusic;
    public AudioClip[] gameMusic;
    public AudioClip OrigamiCatch;
    public VolumeSettings volumeSettings;
    private AudioSource musicSource;
    private AudioSource collectingSource;
    private bool gamePlaying = true;
    void Start()
    {
        // subscribing to events
        PlayerController.onOrigamiCatch += OnOrigamiCatch;
        GameManager.onGameOver += OnGameOver;

        musicSource = AddAudioSource(volumeSettings.musicVolume);
        collectingSource = AddAudioSource(volumeSettings.CollectingVolume);
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
        musicSource.Stop();
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
        collectingSource.resource = OrigamiCatch;
        collectingSource.Play();
    }
    private void OnDisable()
    {
        // unsubscribing to events
        PlayerController.onOrigamiCatch -= OnOrigamiCatch;
        GameManager.onGameOver -= OnGameOver;
    }
}
