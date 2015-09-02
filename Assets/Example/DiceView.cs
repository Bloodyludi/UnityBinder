using UnityEngine;
using UnityEngine.UI;
using Container.Framework;

namespace Container.Example
{
    public class DiceView : MonoBehaviour
    {
        [SerializeField] Button rollButton;
        [SerializeField] Text resultLabel;

        IDiceRoller diceRoller;

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
            diceRoller = FindObjectOfType<CompositionRoot>().container.Resolve<IDiceRoller>();
        }

        void onRollClick()
        {
            resultLabel.text = diceRoller.Roll().Result();
        }
    }
}