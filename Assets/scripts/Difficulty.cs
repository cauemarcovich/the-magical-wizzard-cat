using System;
using System.Linq;
using TMPro;
using UnityEngine;

public class Difficulty : MonoBehaviour {
    public TMP_Dropdown DifficultyUI;
    public TMP_Dropdown DifficultyUI_Old;
    public void EnableDifficultyUI () { DifficultyUI.interactable = true; }
    public void DisableDifficultyUI () { DifficultyUI.interactable = false; }

    public enum DifficultyType { Easy = 0, Normal = 1, Hard = 2 }
    private DifficultyType _difficulty;
    public DifficultyType GetCurrentDifficulty () { return _difficulty; }
    public void SetCurrentDifficulty () { _difficulty = (DifficultyType) DifficultyUI.value; }

    void Start () {
        DifficultyUI.AddOptions (Enum.GetNames (typeof (DifficultyType)).ToList ());
        DifficultyUI.value = (int) DifficultyType.Normal;

        DifficultyUI_Old.AddOptions(Enum.GetNames(typeof(DifficultyType)).ToList());
        DifficultyUI_Old.value = (int)DifficultyType.Normal;

        SetCurrentDifficulty ();
    }

    public int GetAttempts () {
        switch (_difficulty) {
            case DifficultyType.Easy:
                return 4;
            case DifficultyType.Normal:
                return 5;
            case DifficultyType.Hard:
                return 6;
            default:
                return -1;
        }
    }
}