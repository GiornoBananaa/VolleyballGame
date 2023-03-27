using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PopUpOn : MonoBehaviour
{
    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject[] mainUI;

    public void CallPopUp()
    {
        foreach(GameObject i in mainUI)
        {
            i.SetActive(false);
        }

        canvas.SetActive(true);
    }
}
