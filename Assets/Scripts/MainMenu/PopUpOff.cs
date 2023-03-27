using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpOff : MonoBehaviour
{
    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject[] mainUI;

    public void CallPopUp()
    {
        canvas.SetActive(false);
        foreach (GameObject i in mainUI)
        {
            i.SetActive(true);
        }
        PlayerPrefs.Save();
    }
}
