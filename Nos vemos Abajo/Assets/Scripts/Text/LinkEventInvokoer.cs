using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using Object = UnityEngine.Object;


public class LinkEventInvokoer : MonoBehaviour
{
    private TMP_Text _textbox;

    public static event Action<string> LinkFound;

    private void OnEnable()
    {
        TMPro_EventManager.TEXT_CHANGED_EVENT.Add(CheckForLinkEventInTypewriter);
    }

    private void OnDisable()
    {
        TMPro_EventManager.TEXT_CHANGED_EVENT.Remove(CheckForLinkEventInTypewriter);
    }

    private void Awake()
    {
        _textbox = GetComponent<TMP_Text>();
    }

    private void CheckForLinkEventInTypewriter(Object obj)
    {
        if (obj != _textbox)
            return;

        var amountOfLinksInCurrentText = _textbox.textInfo.linkCount;

        if (amountOfLinksInCurrentText == 0)
            return;

        for (int linkIndex = 0; linkIndex < amountOfLinksInCurrentText; linkIndex++)
        {
            var linkInfo = _textbox.textInfo.linkInfo[linkIndex];

            if (_textbox.maxVisibleCharacters == linkInfo.linkTextfirstCharacterIndex)
            {
                LinkFound?.Invoke(linkInfo.GetLinkID());
                break;
            }
        }
    }


}
