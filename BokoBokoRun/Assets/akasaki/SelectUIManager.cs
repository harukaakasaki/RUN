using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectUIManager : MonoBehaviour
{
    public GameObject selectUI;

    // セレクトUIを表示
    public void Show()
    {
        // UIオブジェクトをアクティブにして表示する
        selectUI.SetActive(true);
    }
    // セレクトUIを非表示
    public void Hide()
    {
        // UIオブジェクトを非アクティブにして非表示にする
        selectUI.SetActive(false);
    }

    // ボタンが押されたときに呼ばれる
    public void OnStartButton()
    {
        Debug.Log("選択");
        // ここでUI切り替えなどを行う
    }
}
