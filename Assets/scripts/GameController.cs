using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GuessAlgorithm))]
public class GameController : MonoBehaviour
{
    [Header("Canvas")]
    public GameObject Canvas;
    public GameObject Old_Canvas;

    [Header("Elements")]
    public GameObject Welcome;
    public GameObject Options;
    public GameObject Game;
    public GameObject Intro;
    public GameObject Guessing;
    public GameObject Finish;

    public TextMeshProUGUI Number;
    public int _pickedNumber
    {
        get { return System.Convert.ToInt32(Number.text); }
        set { Number.text = value.ToString(); }
    }

    GuessAlgorithm _guessAlgorithm;
    Difficulty _difficulty;
    int _attempts;

    bool old_mode;

    AudioManager _audioManager;

    void Start()
    {
        _guessAlgorithm = GetComponent<GuessAlgorithm>();
        _difficulty = GetComponent<Difficulty>();

        _audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();

        RefreshCanvas();
    }
    void RefreshGame()
    {
        _attempts = 0;
        _guessAlgorithm.RefreshGame();
        GuessNumber();
    }

    //Welcome
    public void Play()
    {
        Debug.Log("Play?");
        _audioManager.PlaySound();
        Welcome.SetActive(false);
        Game.SetActive(true);
        Intro.SetActive(true);

        RefreshGame();
        _difficulty.DisableDifficultyUI();
    }

    //Options
    public void Options_()
    {
        _audioManager.PlaySound();
        Welcome.SetActive(false);
        Options.SetActive(true);
    }

    //Exit
    public void Exit()
    {
        _audioManager.PlaySound();
        Application.Quit();
    }

    //Back
    public void Back()
    {
        _audioManager.PlaySound();
        Options.SetActive(false);
        Game.SetActive(false);
        Intro.SetActive(false);
        Guessing.SetActive(false);
        Finish.SetActive(false);
        Finish.transform.Find("CongratSpeechBalloon").gameObject.SetActive(false);
        Finish.transform.Find("GriefSpeechBalloon").gameObject.SetActive(false);
        Welcome.SetActive(true);
    }

    //Welcome
    public void GuessTheNumber()
    {
        _audioManager.PlaySound();
        Debug.Log("Entrou no Guess");
        Intro.SetActive(false);
        Guessing.SetActive(true);

        RefreshGame();
        _difficulty.DisableDifficultyUI();
    }

    //Guessing
    public void GuessNumber()
    {
        if (_attempts < _difficulty.GetAttempts())
        {
            _pickedNumber = _guessAlgorithm.Guess();
            _attempts++;
        }
        else
        {
            Lose();
            return;
        }
    }
    public void IsLesser()
    {
        _audioManager.PlaySound();
        _guessAlgorithm.SetTemporaryMaxValue(_pickedNumber);
        GuessNumber();
    }
    public void IsGreater()
    {
        _audioManager.PlaySound();
        _guessAlgorithm.SetTemporaryMinValue(_pickedNumber);
        GuessNumber();
    }

    //Finish	
    public void Victory()
    {
        FinishGame("Congrat"); //Enable Congrat Speech Balloon
    }
    public void Lose()
    {
        FinishGame("Grief"); //Enable Grief Speech Balloon
    }
    void FinishGame(string result)
    {
        _audioManager.PlaySound();
        Guessing.SetActive(false);
        Finish.SetActive(true);

        Finish.transform.Find(result + "SpeechBalloon").gameObject.SetActive(true);
        _difficulty.EnableDifficultyUI();
    }

    public void PlayAgain()
    {
        _audioManager.PlaySound();
        Finish.transform.Find("CongratSpeechBalloon").gameObject.SetActive(false);
        Finish.transform.Find("GriefSpeechBalloon").gameObject.SetActive(false);

        Finish.SetActive(false);
        Guessing.SetActive(true);

        RefreshGame();
        _difficulty.DisableDifficultyUI();
    }

    public void HiddenButton()
    {
        StartCoroutine(ChangeGameMode());
    }
    IEnumerator ChangeGameMode()
    {
        var transition = GetComponent<Transition>();

        old_mode = !old_mode;
        _audioManager.ChangeSoundPitch(old_mode);

        yield return transition.PixelatedTransitionIn();
        RefreshCanvas();
        yield return transition.PixelatedTransitionOut();
    }

    public void RefreshCanvas()
    {
        GameObject canvas = !old_mode ? Canvas : Old_Canvas;
        GameObject oldCanvas = old_mode ? Old_Canvas : Canvas;

        if (!old_mode)
        {            
            Old_Canvas.SetActive(false);
            Canvas.SetActive(true);

            Welcome = Canvas.transform.Find("GamePanel/Welcome").gameObject;
            Options = Canvas.transform.Find("GamePanel/Options").gameObject; 
            Game = Canvas.transform.Find("GamePanel/Game").gameObject;
            Intro = Canvas.transform.Find("GamePanel/Game/Intro").gameObject;
            Guessing = Canvas.transform.Find("GamePanel/Game/Guessing").gameObject;
            Finish = Canvas.transform.Find("GamePanel/Game/Finish").gameObject;
            Number = Canvas.transform.Find("GamePanel/Game/Guessing/BackgroundPanel/PickedNumber").GetComponent<TextMeshProUGUI>();
        }
        else
        {
            Canvas.SetActive(false);
            Old_Canvas.SetActive(true);

            Welcome = Old_Canvas.transform.Find("GamePanel/Welcome").gameObject;
            Options = null;
            Game = Old_Canvas.transform.Find("GamePanel/Game").gameObject;
            Intro = Old_Canvas.transform.Find("GamePanel/Game/Intro").gameObject;
            Guessing = Old_Canvas.transform.Find("GamePanel/Game/Guessing").gameObject;
            Finish = Old_Canvas.transform.Find("GamePanel/Game/Finish").gameObject;
            Number = Old_Canvas.transform.Find("GamePanel/Game/Guessing/PickedNumber").GetComponent<TextMeshProUGUI>();
        }        
    }
    
}