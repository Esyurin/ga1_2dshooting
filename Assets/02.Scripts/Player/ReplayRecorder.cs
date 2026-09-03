using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplayRecorder : MonoBehaviour
{
    private List<MoveCommand> _moveCommands = new();
    private bool isReplaying = false;

    public bool IsReplaying => isReplaying;

    private void Update()
    {
        if (!isReplaying && Input.GetKeyDown(KeyCode.R))
        {
            isReplaying = true;
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

        isReplaying = false;
    }
}