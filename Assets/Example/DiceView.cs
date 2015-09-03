using UnityEngine;
using UnityEngine.UI;
using Container.Framework;

namespace Container.Example
{
    public class DiceView : MonoBehaviour
    {
        [SerializeField] Button rollButton;
        [SerializeField] Text resultLabel;

        [Inject]
        public IDiceRoller diceRoller { get; set; }

        private void Start()
        {
            this.Inject();
        }

        private  void OnEnable()
        {
            rollButton.onClick.AddListener(onRollClick);
        }

        private  void OnDisable()
        {
            rollButton.onClick.RemoveListener(onRollClick);
        }

        private void onRollClick()
        {
            resultLabel.text = diceRoller.Roll().Result();
        }
    }
}