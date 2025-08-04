using System.IO;
using System.Threading.Tasks;
using UnityEngine;

namespace Map
{
    /// <summary>
    /// Utility for loading heightmap textures asynchronously. This can be
    /// used to stream high resolution regional maps (e.g. Europe) without
    /// blocking the main thread.
    /// </summary>
    public class HeightmapLoader : MonoBehaviour
    {
        /// <summary>
        /// Loads a heightmap texture from disk asynchronously. The path
        /// should be relative to Application.streamingAssetsPath.
        /// </summary>
        public async Task<Texture2D> LoadHeightmapAsync(string relativePath)
        {
            string fullPath = Path.Combine(Application.streamingAssetsPath, relativePath);
            if (!File.Exists(fullPath))
            {
                Debug.LogError($"Heightmap not found: {fullPath}");
                return null;
            }

            byte[] data = await File.ReadAllBytesAsync(fullPath);
            var tex = new Texture2D(2, 2, TextureFormat.R16, false, true);
            tex.LoadImage(data);
            tex.Apply();
            return tex;
        }
    }
}
