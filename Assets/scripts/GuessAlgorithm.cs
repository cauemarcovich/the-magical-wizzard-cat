using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof (Difficulty))]
public class GuessAlgorithm : MonoBehaviour {
    public int MinGuess;
    public int MaxGuess;

    int _min;
    int _max;

    Func<int, int, int> _guessNumber;

    public void RefreshGame () {
        _guessNumber = GetGuessAction ();
        _min = MinGuess;
        _max = MaxGuess;
    }

    public int Guess () { return _guessNumber (_min, _max); }
    public void SetTemporaryMinValue (int min) { _min = min; }
    public void SetTemporaryMaxValue (int max) { _max = max; }

    //GuessActions
    public Func<int, int, int> GetGuessAction () {
        var difficulty = GetComponent<Difficulty> ().GetCurrentDifficulty ();
        
        switch (difficulty) {
            case Difficulty.DifficultyType.Easy:
                return GuessNumber_Easy;
            case Difficulty.DifficultyType.Normal:
                return GuessNumber_Normal;
            case Difficulty.DifficultyType.Hard:
                return GuessNumber_Hard;
            default:
                return null;
        }
    }
    private int GuessNumber_Easy (int _min, int _max) {
        return Utils.GetRandomNumber (_min, _max);
    }
    private int GuessNumber_Normal (int _min, int _max) {
        var middle = Utils.GetNumberAverage (_min, _max);

        var minMiddle = Utils.GetNumberAverage (_min, middle);
        var maxMiddle = Utils.GetNumberAverage (middle, _max);

        return Utils.GetRandomNumber (minMiddle, maxMiddle);
    }
    private int GuessNumber_Hard (int _min, int _max) {
        var third = (_max - _min) / 3;

        var firstPart = _min + third;
        var secondPart = _min + (third * 2);
        var thirdPart = _min + (third * 3);

        var minMiddle = Utils.GetNumberAverage (firstPart, secondPart);
        var maxMiddle = Utils.GetNumberAverage (secondPart, thirdPart);

        return Utils.GetRandomNumber (minMiddle, maxMiddle);
    }
}