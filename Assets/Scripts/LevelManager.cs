using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private GameObject _playerBall;

    private Transform _ballTransform;

    private List<GameObject> _prefabs;
    private LinkedList<GameObject> _activeLevelSegments = new LinkedList<GameObject>();

    private const string LEVEL_SEGMENTS_PATH = "LevelSegments";
    private const int PRELOADED_SEGMENTS_COUNT = 4;

    private float _nextSegmentY;
    private float _nextSegmentZ;

    // Called before all Start() methods of all MonoBehaviours
    void Awake()
    {
        _playerBall = GameObject.Find("Player Ball");
        _ballTransform = _playerBall.transform;
        LoadPrefabsFromResources();
    }

    private void LoadPrefabsFromResources()
    {
        try
        {
            var loadedObjects = Resources.LoadAll(LEVEL_SEGMENTS_PATH);
            _prefabs = new List<GameObject>();
            foreach (var loadedObject in loadedObjects)
            {
                _prefabs.Add(loadedObject as GameObject);
            }
            foreach (GameObject prefab in _prefabs)
            {
                Debug.Log(prefab.name);
            }
        } catch(Exception e)
        {
            Debug.LogError("Could not load level segments!\n" + e.Message);
        }
        
    }

    void Start()
    {
        ResetLevel();
    }

    void FixedUpdate()
    {
        // Based on the z-position, new level segments are loaded dynamically
        if (_ballTransform.position.z > _nextSegmentZ - ((PRELOADED_SEGMENTS_COUNT-2) * 12)) // 12 = length of segment
        {
            Destroy(_activeLevelSegments.First.Value);
            _activeLevelSegments.RemoveFirst();
            LoadNextLevelSegment();
            
        }
    }

    private void ResetLevel()
    {
        _ballTransform.position = new Vector3(0f, 2f, 0f);
        foreach (GameObject segment in _activeLevelSegments)
        {
            Destroy(segment);
        }
        _activeLevelSegments.Clear();

        _nextSegmentY = 8;
        _nextSegmentZ = -12;
        // It's assumed that a level segment is 8x12 units
        for (int i = 0; i < PRELOADED_SEGMENTS_COUNT; i++)
        {
            LoadNextLevelSegment(); // i-1 to preload one segment for the start before the first segment
        }

    }

    private void LoadNextLevelSegment()
    {
        var random = new System.Random();
        int index = random.Next(_prefabs.Count);

        Vector3 positionOfNextSegment = new Vector3(0f, _nextSegmentY, _nextSegmentZ);
        GameObject levelSegment = Instantiate(_prefabs[index], positionOfNextSegment, Quaternion.identity);
        _nextSegmentY -= 8;
        _nextSegmentZ += 12;

        _activeLevelSegments.AddLast(levelSegment);
    }
}
