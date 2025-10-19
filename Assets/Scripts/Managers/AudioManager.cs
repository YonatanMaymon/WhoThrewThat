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
    private bool inGame = true;
    void Start()
    {
        PlayerController.onOrigamiCatch += OnOrigamiCatch;
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
    IEnumerator PlayAndWait()
    {
        while (inGame)
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
}
