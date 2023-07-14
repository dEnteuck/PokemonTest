using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class Character : MonoBehaviour
{

    CharacterAnimator animator;
    public bool IsMoving {  get; private set; }
    private void Awake()
    {
        animator = GetComponent<CharacterAnimator>();
    }
    public float moveSpeed;
    public IEnumerator Move(Vector2 moveVec, Action OnMoveOver = null)
    {
        animator.MoveX = Math.Clamp(moveVec.x, -1f,1f);
        animator.MoveY = Math.Clamp(moveVec.y, -1f, 1f);

        var targetPos = transform.position;
        targetPos.x += moveVec.x;
        targetPos.y += moveVec.y;

        if(!IsWalkable(targetPos))
            yield break;

       IsMoving = true;

        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;

       IsMoving = false;

        OnMoveOver?.Invoke();

    }
    public void HandleUpdate()
    {
        animator.IsMoving = IsMoving;
    }

    private bool IsWalkable(Vector3 targetPos)
    {
        if (Physics2D.OverlapCircle(targetPos, 0.2f, GameLayers.i.SolidLayer |GameLayers.i.InteractableLayer) != null)
        {
            return false;
        }

        return true;
    }
    public CharacterAnimator Animator
    {
        get => animator;
    }

}