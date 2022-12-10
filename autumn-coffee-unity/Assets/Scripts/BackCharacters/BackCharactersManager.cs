using System.Collections.Generic;
using UnityEngine;
using Ruinum.Core;
using System;


public class BackCharactersManager : BaseSingleton<BackCharactersManager>
{
    [SerializeField] private float _startTime = 5;

    [SerializeField] private float _repeatTime = 30;

    [SerializeField] private List<CharacterSpawnSettings> _settings;

    private void Start()
    { 
        InvokeRepeating(nameof(Spawn), _startTime, _repeatTime);
    }

    private void Spawn()
    {
        var backCharacter = Instantiate(Resources.Load<GameObject>("Prefabs/BackCharacter"), null);

        backCharacter.transform.position = _settings[UnityEngine.Random.Range(0, _settings.Count)].Position;
        backCharacter.transform.rotation = new Quaternion(0,0, _settings[UnityEngine.Random.Range(0, _settings.Count)].YRotation,0);
    }

    [Serializable]
    public class CharacterSpawnSettings
    {
        public Vector2 Position;
        public float YRotation;
    }
}
