using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectUIManager : MonoBehaviour
{
    public GameObject selectUI;

    public void Show()
    {
        selectUI.SetActive(true);
    }

    public void Hide()
    {
        selectUI.SetActive(false);
    }

    public void OnStartButton()
    {
        Debug.Log("‘I‘ð");
    }
}
