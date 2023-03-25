using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpOff : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    public void CallPopUp()
    {
        canvas.enabled = false;
    }
}
