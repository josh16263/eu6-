# Geographical Adventures Upgrade Guide

This document outlines the steps required to open the project in **Unity 6.1 LTS**, add new settlements or regions and load additional regional heightmaps (e.g. detailed Europe data).

## Importing into Unity 6.1

1. Install Unity **6.1 LTS** using the Unity Hub.
2. In Unity Hub select **Add project from disk** and choose the root of this repository.
3. When prompted, let Unity upgrade the project. The upgrade process converts materials and scripts to the new runtime.
4. Enable the **Universal Render Pipeline (URP)** via `Project Settings > Graphics` and assign the URP asset included with Unity 6.1.
5. Go to `Edit > Project Settings > Player > Active Input Handling` and select **Both** or **Input System Package (New)** to ensure the new input system works with the `SettlementSelector`.

## Adding Settlements or Regions

1. Create a new `GameObject` in the scene and add the **`Settlement`** component found under `Assets/Scripts/Map`.
2. Assign a collider and optional mesh renderer. The renderer is used for highlight tinting when the settlement is selected.
3. Make sure the object is on the `Settlement` layer (or whichever layer is assigned to the `SettlementSelector`).
4. The `SettlementSelector` automatically highlights the clicked settlement and opens the `SettlementPanel` UI.

## Loading High Resolution Heightmaps (e.g. Europe)

1. Place the heightmap file (PNG or JPG) in `Assets/StreamingAssets/Heightmaps/`.
2. Add a `HeightmapLoader` component to a manager object.
3. Call `await LoadHeightmapAsync("Heightmaps/Europe.png")` to load the texture at runtime. The method returns a `Texture2D` that can be applied to your terrain or globe mesh.
4. Because the loader runs asynchronously it will not block the main thread while the image is read from disk.

## Folder Structure

```
Assets/
  Scripts/
    Map/            // Globe and heightmap utilities
    UI/             // User interface scripts
    Interaction/    // Input and selection logic
    GameLogic/      // Gameplay systems such as object pools
```

This structure helps separate responsibilities and prepares the project for additional grand‑strategy features.

## Removing Flight Simulator Elements

The original project included a controllable airplane and several aviation UI components.  
For a grand‑strategy style map these features are no longer required.

1. Delete the **Player** folder under `Assets/Scripts/Game/` along with any `PlayerAudio`, `Compass`, `Speedometer` or `GlobePlayerTrail` scripts.
2. Add the new `TopDownCameraController` component (found in `Assets/Scripts/Interaction/`) to the main camera.
3. The controller pans with **WASD/arrow keys** and zooms with the mouse wheel, giving a fixed top‑down perspective similar to CK3 or EU4.
