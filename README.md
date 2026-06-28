# SafeArea

A lightweight Unity UI layout component that automatically fits an attached `RectTransform` to `Screen.safeArea` using Unity's built-in layout system instead of per-frame polling.

> **Highlights**
>
> * Integrates directly with Unity's UI layout system
> * No `Update()` polling
> * Edit Mode, Device Simulator, and Prefab Mode support
> * Uses `DrivenRectTransformTracker` to clearly own driven properties
> * Zero runtime allocations
> * Single component with no managers or companion scripts

---

## Features

* **Native layout integration.** Participates directly in Unity's Canvas layout rebuild process through `ILayoutSelfController`.
* **No per-frame polling.** Updates only when the layout is rebuilt instead of checking `Screen.safeArea` every frame.
* **Editor-friendly.** Supports Edit Mode, Device Simulator, and Prefab Mode via `[ExecuteAlways]`.
* **Clear ownership.** Uses `DrivenRectTransformTracker` so driven properties are locked in the Inspector and attributed to `SafeAreaFitter`.
* **Configuration validation.** Detects common hierarchy mistakes before entering Play Mode.
* **Allocation-free.** Updates the attached `RectTransform` without generating heap allocations.
* **Minimal API.** One component with no configuration required.

---

## Requirements

* Unity 2021.2+
* `com.unity.ugui` 1.0.0+

---

## Installation

### Option 1: Unity Package Manager (recommended)

1. Open **Window > Package Manager**.
2. Click the **+** button in the top-left corner.
3. Select **Add package from Git URL...**
4. Enter one of the following:

**Latest stable version**

```text
https://github.com/ultrakuneho/com.ultrakuneho.safearea.git
```

**Tagged release** (recommended)

```text
https://github.com/ultrakuneho/com.ultrakuneho.safearea.git#v1.0.0
```

### Option 2: Edit `Packages/manifest.json`

Add the package directly to your project's dependencies.

**Latest version**

```json
{
  "dependencies": {
    "com.ultrakuneho.safearea": "https://github.com/ultrakuneho/com.ultrakuneho.safearea.git"
  }
}
```

**Specific release**

```json
{
  "dependencies": {
    "com.ultrakuneho.safearea": "https://github.com/ultrakuneho/com.ultrakuneho.safearea.git#v1.0.0"
  }
}
```

---

## Quick Start

1. Create a **Screen Space - Overlay** `Canvas`.
2. Create an empty child GameObject (with a `RectTransform`) directly beneath the root canvas.
3. Add the `SafeAreaFitter` component.
4. Add your UI content as children of that object.

The attached `RectTransform` automatically matches `Screen.safeArea`.

---

## Why another safe area component?

Most safe area implementations are simple `MonoBehaviour`s that continuously poll `Screen.safeArea` from `Update()` and manually adjust a `RectTransform`.

`SafeAreaFitter` instead implements `UIBehaviour` and `ILayoutSelfController`, allowing safe area updates to occur as part of Unity's normal layout rebuild process.

Compared to polling-based implementations, this approach:

* **Integrates naturally with Unity UI.** Works alongside `LayoutGroup`, `ContentSizeFitter`, and other layout controllers.
* **Avoids unnecessary work.** Updates only when the layout changes instead of every frame.
* **Behaves consistently in the Editor.** Supports Edit Mode, Device Simulator, and Prefab Mode.
* **Clearly owns the driven `RectTransform`.** Inspector-driven properties are locked and attributed to `SafeAreaFitter`.
* **Detects common setup issues early.** Reports invalid hierarchy configurations before entering Play Mode.
* **Keeps runtime overhead minimal.** Performs updates without heap allocations.

---

## How it works

`SafeAreaFitter` implements `ILayoutSelfController`, allowing it to participate directly in Unity's layout system.

Whenever Unity rebuilds the Canvas layout, the component updates the attached `RectTransform` using the current `Screen.safeArea`. This naturally handles orientation changes and other layout rebuilds without requiring an Update() loop.

If an external system changes the screen state without triggering a layout rebuild, queue one manually:

```csharp
LayoutRebuilder.MarkLayoutForRebuild(rectTransform);
```

where `rectTransform` is the `RectTransform` driven by `SafeAreaFitter`.

---

## Limitations

* Only **Screen Space - Overlay** canvases are supported.
* `SafeAreaFitter` should be attached directly beneath the root **Screen Space - Overlay** `Canvas`.
* Child canvases may also work if their rect matches the root canvas, but this cannot be verified automatically and will generate an Editor warning.

---

## License

MIT — see [LICENSE](LICENSE).
