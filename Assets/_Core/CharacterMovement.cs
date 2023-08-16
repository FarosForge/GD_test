using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private MovementState[] states;
    [SerializeField] private Transform character;
    [SerializeField] private Animator animator;

    private int currentState = 0;
    private float minimumDistance = 0.1f;


    private void Start()
    {
        SetCharacterState();
    }

    private void Update()
    {
        if (GetState().GetDistance(character) > minimumDistance)
        {
            Move();
        }
        else
        {
            NextState();
        }
    }

    private void NextState()
    {
        currentState++;

        if (currentState >= states.Length)
        {
            currentState = 0;
        }

        SetCharacterState();
    }

    private void SetCharacterState()
    {
        character.LookAt(GetState().endPoint);

        if (animator == null) return;

        animator.SetInteger("State", (int)GetState().type);
    }

    private void Move()
    {
        character.position = Vector3.MoveTowards(character.position, GetState().endPoint.position,
            GetState().movementSpeed * Time.deltaTime);
    }

    private MovementState GetState()
    {
        return states[currentState];
    }
}

[System.Serializable]
public struct MovementState
{
    public Transform endPoint;
    public float movementSpeed;
    public StateType type;

    public float GetDistance(Transform character)
    {
        return Vector3.Distance(character.position, endPoint.position);
    }
}

public enum StateType
{
    Idle,
    Walk,
    Run
}
