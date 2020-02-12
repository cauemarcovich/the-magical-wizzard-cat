using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioClip ClickSound;
    public float OldModePitch;


    Button _button { get { return GetComponent<Button>(); } }
    AudioSource _source { get { return GetComponent<AudioSource>(); } }

    void Start()
    {

        //gameObject.AddComponent<AudioSource>();
        //_source.clip = Sound;
        //_source.playOnAwake = false;

        //_button.onClick.AddListener(() => PlaySound());
    }

    public void PlaySound()
    {
        _source.PlayOneShot(ClickSound);
    }

    public void ChangeSoundPitch(bool oldMode)
    {
        StartCoroutine(ChangePitch(oldMode));
    }

    IEnumerator ChangePitch(bool oldMode)
    {
        if (oldMode)
        {
            while (_source.pitch > OldModePitch)
            {
                Debug.Log("teste 1");
                _source.pitch = Mathf.Clamp(_source.pitch - Time.deltaTime, OldModePitch, 1);
                yield return null;
            }
        }
        else
        {
            while (_source.pitch < 1)
            {
                Debug.Log("teste 2");
                _source.pitch = Mathf.Clamp(_source.pitch + Time.deltaTime, _source.pitch, 1) ;
                yield return null;
            }
        }

        Debug.Log("saí");
    }
}
