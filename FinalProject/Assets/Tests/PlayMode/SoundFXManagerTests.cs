using System.Collections.Generic;
using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class SoundFXManagerTests
{
    private GameObject _managerObject;
    private SoundFXManager _soundFXManager;
    private AudioSource _soundFXPrefab;
    private AudioSource _loopingSoundFXPrefab;
    private AudioClip _testClip;

    [SetUp]
    public void SetUp()
    {
        // Create SoundFXManager GameObject
        _managerObject = new GameObject("SoundFXManager");
        _soundFXManager = _managerObject.AddComponent<SoundFXManager>();

        // Create dummy prefabs
        GameObject soundFXObj = new GameObject("SoundFX");
        _soundFXPrefab = soundFXObj.AddComponent<AudioSource>();

        GameObject loopingSoundFXObj = new GameObject("LoopingSoundFX");
        _loopingSoundFXPrefab = loopingSoundFXObj.AddComponent<AudioSource>();

        // Assign dummy prefabs
        typeof(SoundFXManager).GetField("_soundFXObject", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .SetValue(_soundFXManager, _soundFXPrefab);
        typeof(SoundFXManager).GetField("_loopingSoundFXObject", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .SetValue(_soundFXManager, _loopingSoundFXPrefab);

        // Create dummy audio clip
        _testClip = AudioClip.Create("TestClip", 44100, 1, 44100, false);
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(_managerObject);
        Object.DestroyImmediate(_soundFXPrefab.gameObject);
        Object.DestroyImmediate(_loopingSoundFXPrefab.gameObject);
    }

    [Test]
    public void InstanceCreatedOnAwake()
    {
        _soundFXManager.SendMessage("Awake"); // Manually call Awake
        Assert.AreEqual(SoundFXManager.instance, _soundFXManager);
    }


    [UnityTest]
    public IEnumerator StartLoopingSoundFX()
    {
        GameObject dummySpawn = new GameObject("SpawnPoint");

        _soundFXManager.StartLoopingSoundFX("testLoop", _testClip, dummySpawn.transform, 0.7f);
        yield return null;

        var field = typeof(SoundFXManager).GetField("activeLoopingSounds", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        var dict = field.GetValue(_soundFXManager) as Dictionary<string, AudioSource>;

        Assert.IsTrue(dict.ContainsKey("testLoop"));
        Assert.AreEqual(0.7f, dict["testLoop"].volume);
        Assert.AreEqual(_testClip, dict["testLoop"].clip);
        Assert.IsTrue(dict["testLoop"].loop);

        Object.DestroyImmediate(dummySpawn);
    }

    [UnityTest]
    public IEnumerator StopLoopingSoundFX()
    {
        GameObject dummySpawn = new GameObject("SpawnPoint");
        _soundFXManager.StartLoopingSoundFX("testLoop", _testClip, dummySpawn.transform, 0.7f);
        yield return null;

        _soundFXManager.StopLoopingSoundFX("testLoop");
        yield return null;

        var field = typeof(SoundFXManager).GetField("activeLoopingSounds", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        var dict = field.GetValue(_soundFXManager) as Dictionary<string, AudioSource>;

        Assert.IsFalse(dict.ContainsKey("testLoop"));

        Object.DestroyImmediate(dummySpawn);
    }
}
