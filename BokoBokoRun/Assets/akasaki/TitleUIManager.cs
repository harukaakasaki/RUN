using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleUIManager : MonoBehaviour
{
    public GameObject titleUI;

    // タイトルUIを表示する
    public void Show()
    {
        // UIオブジェクトをアクティブにして表示する
        titleUI.SetActive(true);
    }

    // タイトルUIを非表示にする
    public void Hide()
    {
        // UIオブジェクトを非アクティブにして非表示にする
        titleUI.SetActive(false);
    }

    // ボタンが押されたときに呼ばれる
    public void OnStartButton()
    {
        Debug.Log("ゲームスタート");
        // ここでUI切り替えなどを行う
    }
}
