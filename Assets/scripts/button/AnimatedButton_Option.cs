using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class AnimatedButton_Option : MonoBehaviour, IPointerEnterHandler//, IPointerExitHandler
{
    public UnityEvent Action;

    AnimatedButton _button;
    GameController _gameController;
    TextMeshProUGUI _text;
    TextMeshProUGUI[] _othersTexts;

    void Start()
    {        
        _button = transform.parent.Find("MenuButton").GetComponent<AnimatedButton>();
        _gameController = GameObject.Find("GameController").GetComponent<GameController>();

        _text = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        _othersTexts = new TextMeshProUGUI[2];

        _othersTexts[0] = gameObject.name == "Play"
                            ? transform.parent.Find("Options").GetChild(0).GetComponent<TextMeshProUGUI>()
                            : transform.parent.Find("Play").GetChild(0).GetComponent<TextMeshProUGUI>();

        _othersTexts[1] = gameObject.name == "Exit"
                            ? transform.parent.Find("Options").GetChild(0).GetComponent<TextMeshProUGUI>()
                            : transform.parent.Find("Exit").GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        UpdateRaycasts();
        _button.MoveButton(transform, Action);
    }

    void UpdateRaycasts()
    {
        _text.raycastTarget = false;
        foreach (var t in _othersTexts)
        {
            t.raycastTarget = true;
        }
    }
}