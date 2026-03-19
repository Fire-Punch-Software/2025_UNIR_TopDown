using System;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings.SplashScreen;

public class BaseCharacter : MonoBehaviour
{
    [SerializeField] float linearSpeed = 1f;

    Animator animator;
    Rigidbody2D rb2D;

    protected virtual void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();
    }

    protected virtual void Update()
    {
        if (lastMoveDirection.x > 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (lastMoveDirection.x < 0)
        {
            transform.localScale = Vector3.one;
        }

        animator.SetFloat("HorizontalVelocity", lastMoveDirection.x);
        animator.SetFloat("VerticalVelocity", lastMoveDirection.y);
    }

    public Vector2 lastMoveDirection = Vector2.zero;
    protected void Move(Vector2 direction)
    {
        rb2D.position += lastMoveDirection * linearSpeed * Time.deltaTime;
        lastMoveDirection = direction;
    }

    internal void NotifyPunch()
    {
        Destroy(gameObject);
    }
}
