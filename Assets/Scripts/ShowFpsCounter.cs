using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShowFpsCounter : MonoBehaviour
{
    private float timer, refresh, avgFramerate;
    private string display = "{0} FPS";
    [SerializeField] private TMPro.TextMeshProUGUI m_Text;

    void Update()
    {
        float timelapse = Time.smoothDeltaTime;
        timer = timer <= 0 ? refresh : timer -= timelapse;

        if (timer <= 0) avgFramerate = (int)(1f / timelapse);
        m_Text.text = string.Format(display, avgFramerate.ToString());
    }
}
