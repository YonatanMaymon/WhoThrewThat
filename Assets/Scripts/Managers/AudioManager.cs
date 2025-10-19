using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip menuMusic;
    public AudioClip[] gameMusic;
    private AudioSource audioSource;
    private bool inGame = true;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(PlayAndWait());
    }
    // use to play next music clip
    IEnumerator PlayAndWait()
    {
        while (inGame)
        {
            PlayRandom();
            yield return new WaitWhile(() => audioSource.isPlaying);
        }
    }
    private void PlayRandom()
    {
        audioSource.resource = gameMusic[Random.Range(0, gameMusic.Length)];
        audioSource.Play();
    }
}
