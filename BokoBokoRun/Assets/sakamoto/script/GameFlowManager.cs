using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public sealed class GameFlowManager : MonoBehaviour
{
    private static GameFlowManager m_instance;

    [SerializeField] private TitleManager m_titleManger;   //タイトルマネージャー
    [SerializeField] private InGameManager m_inGameManger;//インゲームマネージャー
    [SerializeField] private ResultManager m_resultManger;//リザルトマネージャー


    public enum Scene
    {
        Title,  //タイトルシーン
        InGame, //ゲーム中
        Result, //リザルトシーン
    }
    private static Scene m_scene;

    // Start is called before the first frame update
    void Start()
    {
        //念のためマネージャーをすべて非アクティブ化する
        m_titleManger.enabled = false;
        m_inGameManger.enabled = false;
        m_resultManger.enabled = false;

        //初期シーンをタイトルに設定
        ChangeScene(Scene.Title);
    }

    private void Awake()
    {
        if (m_instance == null)
        {
            m_instance = this;
        }
        else
        {
            Destroy(gameObject);//2つ目以降のインスタンスを破棄
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (m_scene)
        {
            case Scene.Title:
                Debug.Log("タイトルシーン");
                if (m_titleManger.GetIsStart())
                {
                    ChangeScene(Scene.InGame);
                }
                    break;
            case Scene.InGame:
                Debug.Log("インゲーム中");

                break;
            case Scene.Result:
                Debug.Log("リザルトシーン");

                break;
        }
    }

    public void ChangeScene(Scene scene)
    {
        m_scene = scene;
        //対応したシーンのマネージャーのアクティブ化を行う
        switch (scene)
        {
            case Scene.Title:
                m_titleManger.enabled = true;
                m_inGameManger.enabled = false;
                m_resultManger.enabled = false;
                break;
            case Scene.InGame:
                m_titleManger.enabled = false;
                m_inGameManger.enabled = true;
                m_resultManger.enabled = false;
                break;
            case Scene.Result:
                m_titleManger.enabled = false;
                m_inGameManger.enabled = false;
                m_resultManger.enabled = true;
                break;
        }
    }
}
