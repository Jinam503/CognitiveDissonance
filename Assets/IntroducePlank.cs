using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IntroducePlank : MonoBehaviour
{
    public TextMeshProUGUI introduceText;

    private void Awake()
    {
        if (transform.GetChild(0).TryGetComponent(out introduceText)) throw new Exception("텍스트가 자식에 없어용용");
    }
}
