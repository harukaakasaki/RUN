using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 m_pos;
    //速度
    public float Speed = 0.01f;
    //カメラ
    [SerializeField] Camera m_camera;
    //幅を取得するためのゲームオブジェクト
    [SerializeField] GameObject m_gameObject;

    //幅の半分
    float m_halfWidth;

    void Start()
    {
       m_pos = transform.position;
        //幅を取得
        if (m_gameObject != null)
        {
            var rend = m_gameObject.GetComponent<Renderer>();
            if (rend != null)
            {
                m_halfWidth = rend.bounds.size.x * 0.5f;
            }
            else
            {
                // ローカルスケールから概算
                m_halfWidth = m_gameObject.transform.localScale.x * 0.5f;
            }
        }
        else
        {
            // 幅を取得できない場合は、デフォルト値を使用
            m_halfWidth = 0.5f;
        }
        //カメラが指定されていない場合は、メインカメラを使用
        if (m_camera == null)
        {
            m_camera = Camera.main;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //フレームレートに依存しない移動左に移動
        m_pos.x -= Speed * Time.deltaTime;
        //位置を反映
        transform.position = m_pos;

        // 画面端を計算して、左端を越えたら右側へ戻す
        if (m_camera == null)
            return;

        // カメラとオブジェクトの距離
        float distance = Mathf.Abs(m_camera.transform.position.z - transform.position.z);
        //左端のワールド座標と右のワールド座標を取得
        float leftWorldX = m_camera.ViewportToWorldPoint(new Vector3(0f, 0.5f, distance)).x;
        float rightWorldX = m_camera.ViewportToWorldPoint(new Vector3(1f, 0.5f, distance)).x;

        // オブジェクトの左端がカメラの左端より左に出たら、右端の外側に移動させる
        if (transform.position.x + m_halfWidth < leftWorldX)
        {
            // オブジェクトの右端をカメラの右端の外側に配置
            m_pos.x = rightWorldX + m_halfWidth;
            transform.position = m_pos;
        }
    }
}
