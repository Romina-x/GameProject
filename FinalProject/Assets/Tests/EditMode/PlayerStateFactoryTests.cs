using NUnit.Framework;
using UnityEngine;

[TestFixture]
public class PlayerStateFactoryTests
{
    private GameObject _gameObject;
    private PlayerStateMachine _playerStateMachine;
    private PlayerStateFactory _factory;

    [SetUp]
    public void SetUp()
    {
        // Create a GameObject and add PlayerStateMachine component
        _gameObject = new GameObject("Player");
        _playerStateMachine = _gameObject.AddComponent<PlayerStateMachine>();

        // Create the factory with the PlayerStateMachine context
        _factory = new PlayerStateFactory(_playerStateMachine);
    }

    [TearDown]
    public void TearDown()
    {
        // Clean up after each test
        Object.DestroyImmediate(_gameObject);
    }

    [Test]
    public void CreateIdleState()
    {
        // Get the Idle state from the factory
        var state = _factory.Idle();

        // Assert that the state is of type PlayerIdleState
        Assert.IsInstanceOf<PlayerIdleState>(state);
    }

    [Test]
    public void CreateWalkState()
    {
        // Get the Walk state from the factory
        var state = _factory.Walk();

        // Assert that the state is of type PlayerWalkState
        Assert.IsInstanceOf<PlayerWalkState>(state);
    }

    [Test]
    public void CreateRunState()
    {
        // Get the Run state from the factory
        var state = _factory.Run();

        // Assert that the state is of type PlayerRunState
        Assert.IsInstanceOf<PlayerRunState>(state);
    }

    [Test]
    public void CreateGroundedState()
    {
        // Get the Grounded state from the factory
        var state = _factory.Grounded();

        // Assert that the state is of type PlayerGroundedState
        Assert.IsInstanceOf<PlayerGroundedState>(state);
    }

    [Test]
    public void CreateJumpState()
    {
        // Get the Jump state from the factory
        var state = _factory.Jump();

        // Assert that the state is of type PlayerJumpState
        Assert.IsInstanceOf<PlayerJumpState>(state);
    }
}
