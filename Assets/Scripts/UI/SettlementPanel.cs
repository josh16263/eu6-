using UnityEngine;
using UnityEngine.UI;
using Map;

namespace UI
{
    /// <summary>
    /// Simple UI panel that shows the name of the currently selected
    /// settlement. This serves as a placeholder for future city or
    /// province management interfaces.
    /// </summary>
    public class SettlementPanel : MonoBehaviour
    {
        [Tooltip("Root object of the panel")]
        [SerializeField] private GameObject root;

        [Tooltip("Text element used to display the settlement name.")]
        [SerializeField] private Text nameLabel;

        private void Awake()
        {
            if (root != null)
            {
                root.SetActive(false);
            }
        }

        /// <summary>
        /// Populates the panel and makes it visible.
        /// </summary>
        public void Show(Settlement settlement)
        {
            if (root == null || nameLabel == null || settlement == null) return;
            nameLabel.text = settlement.DisplayName;
            root.SetActive(true);
        }

        /// <summary>
        /// Hides the panel.
        /// </summary>
        public void Hide()
        {
            if (root != null)
            {
                root.SetActive(false);
            }
        }
    }
}
