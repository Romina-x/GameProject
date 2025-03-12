using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Class to load scenes from buttons across the game
public class SceneLoader : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private float _transitionTime;
    [SerializeField] private AudioClip _buttonClip;
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
        

        bool useRealTime = LevelManager.Instance != null && 
                   (LevelManager.Instance.CurrentState == LevelState.Paused || 
                    LevelManager.Instance.CurrentState == LevelState.LevelCleared || 
                    LevelManager.Instance.CurrentState == LevelState.GameOver);

        yield return useRealTime ? new WaitForSecondsRealtime(_transitionTime) : new WaitForSeconds(_transitionTime);

        SceneManager.LoadScene(sceneName);
    }
}
