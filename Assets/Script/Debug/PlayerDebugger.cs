using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerDebugger : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text_ugui;

    public void Refreash(float velocity)
    {
        text_ugui.text = "Velocirt: " + velocity.ToString("F2");
    }
}
