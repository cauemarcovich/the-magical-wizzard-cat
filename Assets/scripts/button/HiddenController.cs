using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HiddenController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Sprite Background;
    public Sprite Background_Old;
       
    GameObject _particles;

    void Start()
    {
        _particles = transform.GetChild(0).gameObject;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        _particles.SetActive(true);
        _particles.GetComponent<ParticleSystem>().Play();
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        _particles.SetActive(false);
    }
}
