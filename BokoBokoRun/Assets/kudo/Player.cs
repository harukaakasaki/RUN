using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public enum PlayerState
{
    Idle,
    Move,
}

public class Player : C
{
    private Controller m_controller;
    PlayerState m_state;
    Animator m_animator;
    //回転
    float m_rotateSpeed = 5.0f;
    
    private Vector3 m_playerPos;
    [SerializeField] private Vector3 m_spawnPos;

    //スムージング用
    private float m_SmoothedSpeed;

    private static readonly int IsMovingHash = Animator.StringToHash("IsMoving");
    private static readonly int SpeedHash = Animator.StringToHash("Speed");
    // Start is called before the first frame update
    void Start()
    {
        m_state = PlayerState.Idle;
       
        // Animatorコンポーネントを取得
        m_animator = GetComponent<Animator>();
        if (m_animator == null)
        {
            Debug.LogError("Animator が見つかりません.");
        }
        //初期位置を保存
        m_playerPos = m_spawnPos;
        transform.position = m_playerPos;

        // 同じオブジェクトにアタッチされているControllerを取得
        m_controller = GetComponent<Controller>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        Tick();

        //this.transform.position += m_Velocity;
    }

    private void LateUpdate()
    {
        // Controllerの移動を取得
        if (m_controller != null)
        {
            // 現在の位置と前フレームの位置から移動ベクトルを計算
            Vector3 currentPosition = m_controller.transform.position;
            Vector3 moveDir = currentPosition - m_playerPos;

            // 水平方向の移動のみを考慮
            moveDir.y = 0f;

            // 移動ベクトルをm_Velocityに設定
            m_Velocity = moveDir / Time.deltaTime;

            // 進む方向に向く
            if (moveDir.sqrMagnitude > 0.001f) // 移動しているときだけ回転
            {
                transform.forward = Vector3.Slerp(
                    transform.forward,
                    moveDir.normalized,
                    Time.deltaTime * m_rotateSpeed
                );
            }

            // 前フレームの位置を更新
            m_playerPos = currentPosition;
        }

        // 現在位置
        Vector3 now = transform.position;

        // 位置差分（XZ の水平成分のみ）
        Vector3 delta = now - m_playerPos;
        delta.y = 0f;

        // 速度（m/s）
        float rawSpeed = (Time.deltaTime > 0f) ? (delta.magnitude / Time.deltaTime) : 0f;

        // 少しならした方が自然
        m_SmoothedSpeed = Mathf.Lerp(m_SmoothedSpeed, rawSpeed, 0.2f);

        // 閾値
        bool isMoving = m_SmoothedSpeed > 0.05f;

        // 状態を更新
        m_state = isMoving ? PlayerState.Move : PlayerState.Idle;

        // Animator に反映
        if (m_animator)
        {
            m_animator.SetBool(IsMovingHash, isMoving);
        }

        // 前フレーム位置を更新
        m_playerPos = now;
    }

    private void FixedUpdate()
    {
        this.transform.position = m_playerPos;
    }
    protected override void Tick()
    {
        //m_Stateが０の時はIdle、１の時はMoveのアニメーションを再生する
        switch (m_state)
        {
            case PlayerState.Idle:
                break;

            case PlayerState.Move:
                break;
        }
    }
}
