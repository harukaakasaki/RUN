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
    Animator m_animator;

    //private Vector3 m_PlaerVelocity;
    private Vector3 m_PlayerPos;

    //スムージング用
    private float m_SmoothedSpeed;

    private static readonly int IsMovingHash = Animator.StringToHash("IsMoving");
    private static readonly int SpeedHash = Animator.StringToHash("Speed");
    // Start is called before the first frame update
    void Start()
    {
        m_State = PlayerState.Idle;
        
        // Animatorコンポーネントを取得
         m_animator = GetComponent<Animator>();
        if (m_animator == null)
        {
            Debug.LogError("Animator が見つかりません.");
        }
        //初期位置を保存
        m_PlayerPos = transform.position;

    }

    // Update is called once per frame
    protected override void Update()
    {
        Tick();
        ApplyGravity();
        this.transform.position += m_Velocity;
    }
    private void LateUpdate()
    {

        // 現在位置
        Vector3 now = transform.position;

        // 位置差分（XZ の水平成分のみ）
        Vector3 delta = now - m_PlayerPos;
        delta.y = 0f;

        // 速度（m/s）
        float rawSpeed = (Time.deltaTime > 0f) ? (delta.magnitude / Time.deltaTime) : 0f;

        // 少しならした方が自然（0.2 は好みで調整）
        m_SmoothedSpeed = Mathf.Lerp(m_SmoothedSpeed, rawSpeed, 0.2f);

        // 閾値（停止ゆらぎ対策）。0.03〜0.1 の間で調整してみてください
        bool isMoving = m_SmoothedSpeed > 0.05f;

        // 状態を更新（もし状態パターンを使うなら）
        m_State = isMoving ? PlayerState.Move : PlayerState.Idle;

        // Animator に反映
        if (m_animator)
        {
            m_animator.SetBool(IsMovingHash, isMoving);
        }

        // 前フレーム位置を更新
        m_PlayerPos = now;

    }

    protected override void Tick()
    {
        //m_Stateが０の時はIdle、１の時はMoveのアニメーションを再生する
        switch (m_State)
        {
            case PlayerState.Idle:
                break;

            case PlayerState.Move:
                break;
        }

    }
}
