using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initial : MonoBehaviour
{
    private void Start()
    {
        Application.targetFrameRate = 90;
        QualitySettings.vSyncCount = 0;
    }
}
