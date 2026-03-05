using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameUIManager : MonoBehaviour
{
    public GameObject inGameUI;

    // インゲームUIを表示する
    public void Show()
    {
        // UIオブジェクトをアクティブにして表示する
        inGameUI.SetActive(true);
    }

    // インゲームUIを非表示する
    public void Hide()
    {
        // UIオブジェクトを非アクティブにして非表示する
        inGameUI.SetActive(false);
    }

    // ボタンが押されたときに呼ばれる
    public void OnStartButton()
    {
        Debug.Log("ゲームシーン");
        // ここでUI切り替えなどを行う
    }
}
