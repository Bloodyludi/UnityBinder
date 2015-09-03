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
        public IDiceRoller DiceRoller { get; set; }

        void OnEnable()
        {
            rollButton.onClick.AddListener(onRollClick);
        }

        void OnDisable()
        {
            rollButton.onClick.RemoveListener(onRollClick);
        }

        void Start()
        {
            this.Inject();
        }

        void onRollClick()
        {
            resultLabel.text = DiceRoller.Roll().Result();
        }
    }
}