using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameUIManager : MonoBehaviour
{
    public GameObject inGameUI;

    public void Show()
    {
        inGameUI.SetActive(true);
    }

    public void Hide()
    {
        inGameUI.SetActive(false);
    }

    public void OnStartButton()
    {
        Debug.Log("ÉQÅ[ÉÄÉVÅ[Éì");
    }
}
