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

    Vector3 InitPos = new Vector3(0,0,0);//初期座標

    
    private Vector3 m_playerPos;
    [SerializeField] private Vector3 m_spawnPos;
    private Vector3 m_prevPos;
    private Vector3 m_targetPos;

    // ノックバック用オフセット
    private Vector3 m_knockbackOffset = Vector3.zero;
    private Coroutine m_knockbackCoroutine;

    private static readonly int IsMovingHash = Animator.StringToHash("IsMoving");
    // Start is called before the first frame update
    void Start()
    {
        m_state = PlayerState.Idle;
        //初期座標の更新
        transform.position = InitPos;

       
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
        //TODO
        //プレイヤーの移動
        //プレイヤーの回転
        //プレイヤーのふっとばし

        Vector3 move = new Vector3();

        //  入力ターゲット位置を取得
        if (m_controller != null)//現在はmoveInputをそのまま私
        {
            m_targetPos = m_controller.MoveInput.normalized;//padの入力を更新

            //移動量//そのフレームの
            move = m_controller.MoveInput;
        }

        if(move.sqrMagnitude > 0.001f)
        {
            Quaternion targetRot = Quaternion.LookRotation(move);

            transform.rotation = Quaternion.Slerp(transform.rotation,
                targetRot, 0.5f);
        }


        //targetPosを小さくする
        m_targetPos = m_targetPos * 0.01f;

        //自身の位置を元の座標+ターゲット + ノックバックオフセットを適用
        transform.position = transform.position + m_targetPos + m_knockbackOffset;

        if(move.sqrMagnitude > 0.01f)//そのフレームで移動していたらStateを変える
        {
            m_state = PlayerState.Move;
            Debug.Log("m_stateはmoveです");
        }
        else
        {
            m_state = PlayerState.Idle;
            Debug.Log("m_stateはIdleです");
        }



        if (m_animator)
        {
            m_animator.SetBool(IsMovingHash, m_state ==  PlayerState.Move);//動いているかをboolでanimatorのセット
        }

        //// 回転移動しているときだけ
        //if (delta.sqrMagnitude > 0.0001f)
        //{
        //    Vector3 dir = delta.normalized;
        //    transform.forward = Vector3.Slerp(transform.forward, dir, Time.deltaTime * m_rotateSpeed);
        //}

        // 前フレーム位置を更新
       // m_prevPos = now;//使わない
        //使ってない
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

