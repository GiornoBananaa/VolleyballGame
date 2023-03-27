using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultySwitcher : MonoBehaviour
{
    public void SwitchDifficulty(int difficulty)
    {
        PlayerPrefs.SetInt("Difficulty", difficulty);
    }
}
