using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition : MonoBehaviour
{
    public Material material;
    GameController _gameController;

    public SpriteRenderer background;
    public Sprite sp1;
    public Sprite sp2;

    bool started = false;
    float minSize = 0.001f;
    float maxSize = 0.15f;
    float step = 0.002f;
    string pixelSize = "_PixelSize";

    void Start()
    {
        _gameController = GetComponent<GameController>();
        ResetTransition();
    }

    public IEnumerator PixelatedTransitionIn()
    {
        Debug.Log("in");
        float size = minSize;
        while (size < maxSize)
        {
            size += step;
            material.SetFloat(pixelSize, size);
            yield return null;
        }
    }
    public IEnumerator PixelatedTransitionOut()
    {
        Debug.Log("out");
        float size = maxSize;
        while (size > minSize)
        {
            Debug.Log("dentro out");
            size -= step;
            material.SetFloat(pixelSize, size);
            yield return null;
        }
        Debug.Log("cabo out");
    }

    void ResetTransition()
    {
        material.SetFloat(pixelSize, minSize);
        //background.sprite = sp1;
    }
}
