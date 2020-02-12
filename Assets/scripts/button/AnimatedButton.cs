using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AnimatedButton : MonoBehaviour
{
    public float AnimationSpeed;
    Coroutine _moving;

    public void MoveButton(Transform target, UnityEvent _action)
    {
        if (_moving != null)
            StopCoroutine(_moving);

        _moving = StartCoroutine(MoveButton_(target, _action));
    }

    public IEnumerator MoveButton_(Transform target, UnityEvent _action)
    {
        var button = transform.GetComponent<Button>();
        button.onClick.RemoveAllListeners();

        button.onClick.AddListener(_action.Invoke);

        while (Vector2.Distance(transform.position, target.position) > 0.001f)
        {
            transform.position = Vector2.Lerp(transform.position, target.position, AnimationSpeed);
            yield return null;
        }
    }


}