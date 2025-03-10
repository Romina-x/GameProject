using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFXManager : MonoBehaviour
{
    public static SoundFXManager instance;

    [SerializeField] private AudioSource _soundFXObject;

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
}
