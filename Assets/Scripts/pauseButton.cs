using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pauseButton : MonoBehaviour
{
    private Toggle m_Toogle;
    private void Start()
    {
        m_Toogle = GetComponent<Toggle>();
        m_Toogle.onValueChanged.AddListener(delegate { ToggleValueChanged(m_Toogle); });

    }
    private void ToggleValueChanged(Toggle change)
    {
        if(m_Toogle.isOn) PauseSystem.Instance.PauseOnOff(true);
        else PauseSystem.Instance.PauseOnOff(false);
    }
}
