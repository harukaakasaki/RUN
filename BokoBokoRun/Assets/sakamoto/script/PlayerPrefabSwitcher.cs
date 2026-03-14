using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPrefabSwitcher : MonoBehaviour
{
    [SerializeField] private PlayerInputManager m_playerInputManager;//PlayerInputManager
    [SerializeField] private GameObject[] m_playerPrefabs;//P1,P2,P3,P4用

    //次のプレイヤーの番号
    private int m_nextIndex = 0;

    // Start is called before the first frame update
    private void Awake()
    {
        //マネージャーが参照されていなければ
        if (m_playerInputManager == null)
        {
            //コンポーネントを取得する
            m_playerInputManager = GetComponent<PlayerInputManager>();
        }

        //Joinが起きる前に0番を必ずセット
        m_nextIndex = 0;
        SetNextPrefab();

        m_playerInputManager.onPlayerJoined += OnPlayerJoined;
    }
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetNextPrefab()
    {
        //次のプレハブを設定する
        var nextPrefab = Mathf.Clamp(m_nextIndex, 0, m_playerPrefabs.Length - 1);
        m_playerInputManager.playerPrefab = m_playerPrefabs[nextPrefab];
    }

    private void OnPlayerJoined(PlayerInput pi)
    {
        //次の参加者用にプレハブを更新
        m_nextIndex++;
        SetNextPrefab();
    }
}
