using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetMove : MonoBehaviour
{
    [SerializeField] private GameFlowManager m_flowManager;
    [SerializeField] private InGameManager m_inGameManager;//インゲームマネージャー

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     
    }
    public void MoveCamera(float x)
    {
        if (m_flowManager.GetNowScene() == GameFlowManager.Scene.InGame &&
            m_inGameManager.IsCanMove())
        {
            Vector3 move = new Vector3(x, 0.0f, 0.0f);

            transform.position += move;
        }  
    }
}
