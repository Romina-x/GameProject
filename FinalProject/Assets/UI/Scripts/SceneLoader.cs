using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Class to load scenes from buttons across the game
public class SceneLoader : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private float _transitionTime;
    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneTransition(sceneName));
    }

    public void ExitGame()
    {
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
