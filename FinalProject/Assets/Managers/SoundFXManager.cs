using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Singleton to manage sound effects in the game, including one-time and looping audio.
/// </summary>
public class SoundFXManager : MonoBehaviour
{
    public static SoundFXManager instance; // Singleton instance of SoundFXManager

    // Prefabs for creating regular or looped sound effects
    [SerializeField] private AudioSource _soundFXObject;
    [SerializeField] private AudioSource _loopingSoundFXObject;

    // Dictionary to store currently playing looping sound effects
    private Dictionary<string, AudioSource> activeLoopingSounds = new Dictionary<string, AudioSource>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    /// <summary>
    /// Plays a single sound effect at a specified position.
    /// </summary>
    /// <param name="audio">The audio clip to play.</param>
    /// <param name="spawnPosition">The position where the sound should be played.</param>
    /// <param name="volume">The volume of the sound effect.</param>
    public void PlaySoundFX(AudioClip audio, Transform spawnPosition, float volume)
    {
        AudioSource audioSource = Instantiate(_soundFXObject, spawnPosition.position, Quaternion.identity);
        audioSource.clip = audio;
        audioSource.volume = volume;
        audioSource.Play();
        float clipLength = audioSource.clip.length;
        Destroy(audioSource.gameObject, clipLength);
    }

    /// <summary>
    /// Starts playing a looping sound effect, ensuring that multiple instances of the same sound are not created.
    /// </summary>
    /// <param name="key">An identifier for the sound.</param>
    /// <param name="audio">The audio clip to loop.</param>
    /// <param name="spawnPosition">The position where the sound should be played.</param>
    /// <param name="volume">The volume of the sound effect.</param>
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

    /// <summary>
    /// Stops a looping sound effect if it is currently playing.
    /// </summary>
    /// <param name="key">The identifier for the sound to stop.</param>
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
