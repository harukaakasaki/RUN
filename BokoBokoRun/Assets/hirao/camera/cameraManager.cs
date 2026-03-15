using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class cameraManager : MonoBehaviour
{
    //public CinemachineVirtualCamera titleCamera;//タイトル用のカメラ
    //public CinemachineVirtualCamera gameCamera;//タイトル用のカメラ
    //public CinemachineVirtualCamera resultCamera;//タイトル用のカメラ


    //Listでカメラをまとめる
    public List<CinemachineVirtualCamera> cameras = new List<CinemachineVirtualCamera>();
    //[SerializeField] CinemachineVirtualCamera[] cameras;


    //インデックスでアクセスしやすいように定数を定義
    private const int kTitleCamera = 0;
    private const int kSelectCamera = 1;
    private const int kGameCamera = 2;
    private const int kResultCamera = 3;
    private const int kGoalCamera = 4;

    // Start is called before the first frame update
    void Start()
    {
        SetTitle();
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.qKey.wasPressedThisFrame)
        {
            SetTitle();//タイトル用のカメラを優先させる関数を呼び出す
        }

        if (Keyboard.current != null && Keyboard.current.wKey.wasPressedThisFrame)
        {
            SetGame();//ゲーム用のカメラを優先させる関数を呼び出す
        }

        if (Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
        {
            SetResult();//リザルト用のカメラを優先させる関数を呼び出す
        }

        if (Keyboard.current != null && Keyboard.current.rKey.wasPressedThisFrame)
        {
            SetSelect();//リザルト用のカメラを優先させる関数を呼び出す
        }
    }

    public void SetTitle()
    {
        //for文ですべてのカメラの優先度を下げる
        for (int i = 0; i < cameras.Count; i++)
        {
            cameras[i].Priority = 0;
        }
        //タイトル用のカメラの優先度を上げる
        cameras[kTitleCamera].Priority = 10;
    }

    public void SetGame()
    {
        //for文ですべてのカメラの優先度を下げる
        for (int i = 0; i < cameras.Count; i++)
        {
            cameras[i].Priority = 0;
        }
        //インゲーム用のカメラの優先度を上げる
        cameras[kGameCamera].Priority = 10;
    }

    public void SetResult()
    {
        //for文ですべてのカメラの優先度を下げる
        for (int i = 0; i < cameras.Count; i++)
        {
            cameras[i].Priority = 0;
        }
        //リザルト用のカメラの優先度を上げる
        cameras[kResultCamera].Priority = 10;
    }

    public void SetSelect()
    {
        //for文ですべてのカメラの優先度を下げる
        for (int i = 0; i < cameras.Count; i++)
        {
            cameras[i].Priority = 0;
        }
        //セレクト用のカメラの優先度を上げる
        cameras[kSelectCamera].Priority = 10;
    }

    public void SetGoalCamera()
    {
        //for文ですべてのカメラの優先度を下げる
        for (int i = 0; i < cameras.Count; i++)
        {
            cameras[i].Priority = 0;
        }
        //セレクト用のカメラの優先度を上げる
        cameras[kGoalCamera].Priority = 10;
    }
}
