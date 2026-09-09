using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplayRecorder : MonoBehaviour
{
    private List<MoveCommand> _moveCommands = new();
    private bool _isReplaying = false;

    public bool IsReplaying => _isReplaying;

    private void Update()
    {
        if (!_isReplaying && Input.GetKeyDown(KeyCode.R))
        {
            _isReplaying = true;
            StartCoroutine(ReplayMovement());
        }
    }

    public void AddMoveCommands(GameObject player, Vector3 startPosition, Vector2 endPosition, float moveTime)
    {
        _moveCommands.Add(new MoveCommand(player, startPosition, endPosition, moveTime));
    }

    public IEnumerator ReplayMovement()
    {
        foreach (MoveCommand command in _moveCommands)
        {
            yield return StartCoroutine(command.Execute());
        }

        _isReplaying = false;
    }
}