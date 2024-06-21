using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BirdMover), typeof(ScoreCounter), typeof(CollisionHandler))]
[RequireComponent(typeof(Shooter),typeof(InputReader))]
public class Bird : MonoBehaviour
{
    private InputReader _inputReader;   
    private BirdMover _birdMover;
    private ScoreCounter _scoreCounter;
    private CollisionHandler _handler;
    private Shooter _shooter;

    public event Action GameOver;

    private void Awake()
    {
        _scoreCounter = GetComponent<ScoreCounter>();
        _handler = GetComponent<CollisionHandler>();
        _birdMover = GetComponent<BirdMover>();
        _shooter = GetComponent<Shooter>();
        _inputReader = GetComponent<InputReader>();
    }

    private void OnEnable()
    {
        _handler.CollisionDetected += ProcessCollision;
    }

    private void OnDisable()
    {
        _handler.CollisionDetected -= ProcessCollision;
    }

    private void Update()
    {
        if (_inputReader.IsFire)
            _shooter.Shoot();
    }

    private void ProcessCollision(IInteractable interactable)
    {
        if (interactable is Bullet || interactable is Ground)
            GameOver?.Invoke();
    }

    public void Reset()
    {
        _scoreCounter.Reset();
        _birdMover.Reset();
    }
}
