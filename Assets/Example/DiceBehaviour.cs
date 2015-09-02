using UnityEngine;
using UnityEngine.UI;
using Container.Framework;

namespace Container.Example
{
    public class DiceBehaviour : MonoBehaviour
    {
        [SerializeField] Button rollButton;
        [SerializeField] Text resultLabel;

        IRandom random;

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
            random = FindObjectOfType<CompositionRoot>().container.Resolve<IRandom>();
        }

        void onRollClick()
        {
            resultLabel.text = random.Range(1, 6).ToString();
        }
    }
}