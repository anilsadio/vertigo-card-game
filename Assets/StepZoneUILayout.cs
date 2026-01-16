using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StepZoneUILayout : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI indexText;

    public void Initialize(string index)
    {
        indexText.text = index;
    }
}
