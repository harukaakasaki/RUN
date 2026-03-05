using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
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
    float m_rotateSpeed = 7.0f;
    
    private Vector3 m_playerPos;
    [SerializeField] private Vector3 m_spawnPos;
    private Vector3 m_prevPos;
    private Vector3 m_targetPos;

    //スムージング用
    private float m_smoothedSpeed;

    // ノックバック用オフセット
    private Vector3 m_knockbackOffset = Vector3.zero;
    private Coroutine m_knockbackCoroutine;

    private static readonly int IsMovingHash = Animator.StringToHash("IsMoving");
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



    protected override void Update()
    {
        //  入力ターゲット位置を取得
        if (m_controller != null)
        {
            m_targetPos = m_controller.transform.position;
        }

        //自身の位置をターゲットに追随 + ノックバックオフセットを適用
        transform.position = m_targetPos + m_knockbackOffset;

        //速度計算
        Vector3 now = transform.position;
        Vector3 delta = now - m_prevPos; // 水平成分のみ見たいなら y=0 に
        delta.y = 0f;

        float rawSpeed = (Time.deltaTime > 0f) ? (delta.magnitude / Time.deltaTime) : 0f;
        m_smoothedSpeed = Mathf.Lerp(m_smoothedSpeed, rawSpeed, 0.2f);
        
        bool isMoving = m_smoothedSpeed > 0.05f;
        m_state = isMoving ? PlayerState.Move : PlayerState.Idle;

        if (m_animator)
        {
            m_animator.SetBool(IsMovingHash, isMoving);
        }

        // 回転移動しているときだけ
        if (delta.sqrMagnitude > 0.0001f)
        {
            Vector3 dir = delta.normalized;
            transform.forward = Vector3.Slerp(transform.forward, dir, Time.deltaTime * m_rotateSpeed);
        }

        // 前フレーム位置を更新
        m_prevPos = now;
        
        Tick();
    }
    // トリガーで敵と接触したときの処理はクラスレベルで定義する

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GameEnemy"))
        {
            Debug.Log("敵に当たりました！");

            // 衝突相手からプレイヤーが離れる向きにベクトルを作る
            Vector3 dir = (transform.position - other.transform.position).normalized;
            // 斜めに飛ばしたいのでY成分を追加
            dir.y = Mathf.Max(dir.y, 0.5f); //必要に応じて上昇量を調整
            dir = dir.normalized;

            // 力の強さ
            float strength = 2.0f;

            Vector3 knockback = dir * strength;

            // 既存のノックバックがあれば止める
            if (m_knockbackCoroutine != null)
            {
                StopCoroutine(m_knockbackCoroutine);
            }
            // 指定時間で減衰させる
            m_knockbackCoroutine = StartCoroutine(KnockbackRoutine(knockback, 0.5f));
            //敵と当たったら死亡判定を入れる

            bool isDead = true; // ここは調整
        }
    }

    // ノックバックの減衰処理
    private IEnumerator KnockbackRoutine(Vector3 force, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            // 線形減衰
            float t = 1f - (elapsed / duration);
            m_knockbackOffset = force * t;
            elapsed += Time.deltaTime;
            yield return null;
        }
        m_knockbackOffset = Vector3.zero;
        m_knockbackCoroutine = null;
    }

    protected override void Tick()
    {
        switch (m_state)
        {
            case PlayerState.Idle:
                break;
            case PlayerState.Move:
                break;
        }
    }
}

