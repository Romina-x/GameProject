using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles scene transitions and game exits, including animations and sound effects.
/// </summary>
public class SceneLoader : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private float _transitionTime;
    [SerializeField] private AudioClip _buttonClip;

    /// <summary>
    /// Loads a new scene with a transition animation and sound effect.
    /// </summary>
    /// <param name="sceneName">The name of the scene to load.</param>
    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneTransition(sceneName));
        SoundFXManager.instance.PlaySoundFX(_buttonClip, transform, 1f);
    }

    public void ExitGame()
    {
        SoundFXManager.instance.PlaySoundFX(_buttonClip, transform, 1f);
        Application.Quit();
    }

    IEnumerator LoadSceneTransition(string sceneName)
    {
        _animator.SetTrigger("Start");
        
        // Wait with real time instead if the game is in a paused state, which stops animations
        bool useRealTime = LevelManager.Instance != null && 
                   (LevelManager.Instance.CurrentState == LevelState.Paused || 
                    LevelManager.Instance.CurrentState == LevelState.LevelCleared || 
                    LevelManager.Instance.CurrentState == LevelState.GameOver);

        yield return useRealTime ? new WaitForSecondsRealtime(_transitionTime) : new WaitForSeconds(_transitionTime);

        SceneManager.LoadScene(sceneName);
    }
}
