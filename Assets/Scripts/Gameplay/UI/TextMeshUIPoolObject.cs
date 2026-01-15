using TMPro;
using UnityEngine;
using Utilities.Pool;

namespace Gameplay.UI
{
    public class TextMeshUIPoolObject : MonoBehaviour, IPoolable
    {
        [SerializeField] private TextMeshProUGUI textMeshProUGUI;

        [field: SerializeField] public ObjectType Type { get; set; }
        public bool IsInThePool { get; set; }

        public void SetText(string text)
        {
            textMeshProUGUI.text = text;
        }
        public void SetColor(Color color)
        {
            textMeshProUGUI.color = color;
        }
        public void OnSpawn()
        {
        }

        public void OnReset()
        {
        }
    }
}
