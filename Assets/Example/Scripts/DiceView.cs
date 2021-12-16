using UnityEngine;
using UnityEngine.UI;
using DIContainer.Framework;
using DIContainer.Framework.Extensions;

namespace DIContainer.Example
{
    public class DiceView : MonoBehaviour
    {
        [SerializeField] private Button rollButton;
        [SerializeField] private Text resultLabel;

        [Inject] public IDiceRoller DiceRoller { get; set; }

        private void Start()
        {
            this.Inject();
        }

        private void OnEnable()
        {
            rollButton.onClick.AddListener(OnClickRoll);
        }

        private void OnDisable()
        {
            rollButton.onClick.RemoveListener(OnClickRoll);
        }

        private void OnClickRoll()
        {
            resultLabel.text = DiceRoller.Roll().Result();
        }
    }
}