using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFXManager : MonoBehaviour
{
    public static SoundFXManager instance;

    [SerializeField] private AudioSource _soundFXObject;
    [SerializeField] private AudioSource _loopingSoundFXObject;

    private Dictionary<string, AudioSource> activeLoopingSounds = new Dictionary<string, AudioSource>();
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void PlaySoundFX(AudioClip audio, Transform spawnPosition, float volume)
    {
        AudioSource audioSource = Instantiate(_soundFXObject, spawnPosition.position, Quaternion.identity);
        audioSource.clip = audio;
        audioSource.volume = volume;
        audioSource.Play();
        float clipLength = audioSource.clip.length;
        Destroy(audioSource.gameObject, clipLength);
    }

    public void PlayRandomSoundFX(AudioClip[] audio, Transform spawnPosition, float volume)
    {
        int rand = Random.Range(0, audio.Length);

        AudioSource audioSource = Instantiate(_soundFXObject, spawnPosition.position, Quaternion.identity);
        audioSource.clip = audio[rand];
        audioSource.volume = volume;
        audioSource.Play();
        float clipLength = audioSource.clip.length;
        Destroy(audioSource.gameObject, clipLength);
    }

    public void StartLoopingSoundFX(string key, AudioClip audio, Transform spawnPosition, float volume)
    {
        if (activeLoopingSounds.ContainsKey(key)) return; // Prevent multiple instances of the same sound

        AudioSource audioSource = Instantiate(_loopingSoundFXObject, spawnPosition.position, Quaternion.identity);
        audioSource.clip = audio;
        audioSource.volume = volume;
        audioSource.loop = true;
        audioSource.Play();

        activeLoopingSounds[key] = audioSource;
    }

    public void StopLoopingSoundFX(string key)
    {
        if (activeLoopingSounds.TryGetValue(key, out AudioSource audioSource))
        {
            audioSource.Stop();
            Destroy(audioSource.gameObject);
            activeLoopingSounds.Remove(key);
        }
    }
}
