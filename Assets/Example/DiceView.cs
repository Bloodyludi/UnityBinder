using UnityEngine;
using UnityEngine.UI;
using Container.Framework;
using Container.Framework.Extensions;

namespace Container.Example
{
    public class DiceView : MonoBehaviour
    {
        [SerializeField] private Button rollButton;
        [SerializeField] private Text resultLabel;

        [Inject]
        public IDiceRoller diceRoller { get; set; }

        private void Start()
        {
            this.Inject();
        }

        private void OnEnable()
        {
            rollButton.onClick.AddListener(onRollClick);
        }

        private void OnDisable()
        {
            rollButton.onClick.RemoveListener(onRollClick);
        }

        private void onRollClick()
        {
            resultLabel.text = diceRoller.Roll().Result();
        }
    }
}