using TMPro;
using UnityEngine;

namespace UI.WheelGame
{
    public class StepZoneUILayout : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI indexText;

        public void Initialize(string index)
        {
            indexText.text = index;
        }
    }
}
