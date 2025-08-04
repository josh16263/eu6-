using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Map;

namespace Interaction
{
    /// <summary>
    /// Handles player input for selecting settlements on the map. Uses the
    /// new Unity Input System and performs a ray cast from the camera to
    /// detect clicked settlements.
    /// </summary>
    public class SettlementSelector : MonoBehaviour
    {
        [Tooltip("Camera used for ray casting into the world.")]
        [SerializeField] private Camera mapCamera;

        [Tooltip("Layer mask that contains settlement colliders.")]
        [SerializeField] private LayerMask settlementMask;

        [Tooltip("UI element that displays information about the selected settlement.")]
        [SerializeField] private UI.SettlementPanel settlementPanel;

        private Settlement currentSelection;

        private void Reset()
        {
            mapCamera = Camera.main;
        }

        private void Update()
        {
            // Respond only when the left mouse button was pressed this frame.
            if (!Mouse.current.leftButton.wasPressedThisFrame) return;
            if (mapCamera == null) return;

            Ray ray = mapCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, settlementMask))
            {
                var settlement = hit.collider.GetComponentInParent<Settlement>();
                if (settlement != null)
                {
                    SelectSettlement(settlement);
                }
            }
        }

        /// <summary>
        /// Applies highlight to the clicked settlement and notifies the UI.
        /// </summary>
        private void SelectSettlement(Settlement settlement)
        {
            if (currentSelection == settlement) return;

            currentSelection?.SetHighlight(false);
            currentSelection = settlement;
            currentSelection.SetHighlight(true);

            if (settlementPanel != null)
            {
                settlementPanel.Show(currentSelection);
            }
        }
    }
}
