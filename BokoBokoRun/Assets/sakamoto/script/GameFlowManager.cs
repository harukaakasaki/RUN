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
    [SerializeField] private FadeManager m_fadeManager;//フェードマネージャー

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

        //タイトルを出す前に黒→透明とする
        StartCoroutine(BootTitle());
    }

    private IEnumerator BootTitle()
    {
        //シーンをタイトルに切り替える
        ChangeScene(Scene.Title);
        //フェードを行う
        yield return m_fadeManager.FadeIn(1.0f, 0.0f);
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
            case Scene.Title://タイトル

                Debug.Log("タイトルシーンです");

                //タイトルマネージャーからゲームスタートされてるかを取得して
                //スタートされている&フェード中じゃなければゲームシーンに遷移する
                if (m_titleManger.GetIsStart() && !m_fadeManager.m_isFading)
                {
                    StartCoroutine(ChangeInGame());
                }
                break;
            case Scene.InGame:
                Debug.Log("インゲームシーンです");
                //TODO:Result遷移も同様の手順で行う
                break;
            case Scene.Result:
                Debug.Log("リザルトシーンです");
                break;
        }
    }

    private IEnumerator ChangeInGame()
    {
        //まずフェードアウトを行う
        yield return m_fadeManager.FadeOut(0.0f, 1.0f);

        //暗転中にマネージャーを切り替え
        ChangeScene(Scene.InGame);

        //フェードインを行う
        yield return m_fadeManager.FadeIn(1.0f, 0.0f);
    }

    public void ChangeScene(Scene scene)
    {
        m_scene = scene;
        switch (scene)
        {
            case Scene.Title:
                m_titleManger.enabled = true;
                break;
            case Scene.InGame:
                m_inGameManger.enabled = true;
                break;
            case Scene.Result:
                m_resultManger.enabled = true;
                break;
        }
    }
}
