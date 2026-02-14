using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    Idle,
    Move,
}
public class Player : C
{
    PlayerState m_State;
    Animator animator;
    
    // Start is called before the first frame update
    void Start()
    {
        m_State = PlayerState.Move;
        
    // Animatorコンポーネントを取得
    animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        Tick();
        ApplyGravity();
        this.transform.position += m_Velocity;

    }
    protected override void Tick()
    {
        //m_Stateが０の時はIdle、１の時はMoveのアニメーションを再生する
        switch (m_State)
        {
            case PlayerState.Idle:
                animator.SetInteger("State", 0); // Idle
                break;

            case PlayerState.Move:
                animator.SetInteger("State", 1); // Move
                break;
        }

    }
}
