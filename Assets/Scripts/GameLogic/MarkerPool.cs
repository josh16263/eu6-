using System.Collections.Generic;
using UnityEngine;

namespace GameLogic
{
    /// <summary>
    /// Generic object pool used for map markers and city icons. This
    /// reduces runtime allocations by reusing inactive objects instead of
    /// instantiating and destroying them every time.
    /// </summary>
    public class MarkerPool : MonoBehaviour
    {
        [Tooltip("Prefab used when the pool needs to grow.")]
        [SerializeField] private GameObject markerPrefab;

        [Tooltip("Number of markers to create on start.")]
        [SerializeField] private int initialSize = 32;

        private readonly Queue<GameObject> available = new();

        private void Awake()
        {
            // Pre-build the pool for smoother performance.
            for (int i = 0; i < initialSize; i++)
            {
                var marker = Instantiate(markerPrefab, transform);
                marker.SetActive(false);
                available.Enqueue(marker);
            }
        }

        /// <summary>
        /// Retrieves an available marker from the pool. A new object is
        /// created if the pool is empty.
        /// </summary>
        public GameObject Get()
        {
            if (available.Count == 0)
            {
                var marker = Instantiate(markerPrefab, transform);
                marker.SetActive(false);
                available.Enqueue(marker);
            }

            var obj = available.Dequeue();
            obj.SetActive(true);
            return obj;
        }

        /// <summary>
        /// Returns a marker back to the pool for reuse.
        /// </summary>
        public void Release(GameObject marker)
        {
            if (marker == null) return;
            marker.SetActive(false);
            available.Enqueue(marker);
        }
    }
}
