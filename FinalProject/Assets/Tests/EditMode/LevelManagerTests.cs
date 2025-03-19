using NUnit.Framework;
using UnityEngine;

[TestFixture]
public class LevelManagerTests
{
    private LevelManager _levelManager;

    [SetUp]
    public void SetUp()
    {
        // Create a GameObject to hold the LevelManager
        var gameObject = new GameObject();
        _levelManager = gameObject.AddComponent<LevelManager>();
    }

    [Test]
    public void ChangeToPlayingState()
    {
        // Transition to Playing state
        _levelManager.SetGameState(LevelState.Playing);

        Assert.AreEqual(LevelState.Playing, _levelManager.CurrentState); // Check the state was correctly changed
        Assert.AreEqual(1, Time.timeScale);  // Check gameplay is enabled (time scale = 1)
    }

    [Test]
    public void ChangeToPausedState()
    {
        // Transition to Paused state
        _levelManager.SetGameState(LevelState.Paused);

        Assert.AreEqual(LevelState.Paused, _levelManager.CurrentState); // Check the state was correctly changed
        Assert.AreEqual(0, Time.timeScale);  // Check gameplay is paused (time scale = 0)
    }

    [Test]
    public void ChangeToLevelClearedState()
    {
        // Transition to LevelCleared state
        _levelManager.SetGameState(LevelState.LevelCleared);

        Assert.AreEqual(LevelState.LevelCleared, _levelManager.CurrentState); // Check the state was correctly changed
        Assert.AreEqual(0, Time.timeScale);  // Check gameplay is stopped (time scale = 0)
    }

    [Test]
    public void ChangeToGameOverState()
    {
        // Transition to GameOver state
        _levelManager.SetGameState(LevelState.GameOver);

        Assert.AreEqual(LevelState.GameOver, _levelManager.CurrentState); // Check the state was correctly changed
        Assert.AreEqual(0, Time.timeScale);  // Check gameplay is stopped (time scale = 0)
    }
}
