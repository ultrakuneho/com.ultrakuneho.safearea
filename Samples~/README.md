# Demo

A sample demonstrating how to use `SafeAreaFitter` and visualize the device's safe area.

## Contents

* **Sample.unity** — Demonstrates `SafeAreaFitter` with a visual representation of the current safe area.

## Scene hierarchy

```text
Canvas  (Screen Space - Overlay)
└── SafeArea  (SafeAreaFitter + green overlay image)
    ├── Image  (top-left corner marker)
    ├── Image  (top-center edge marker)
    ├── Image  (top-right corner marker)
    ├── Image  (left-center edge marker)
    ├── Image  (right-center edge marker)
    ├── Image  (bottom-left corner marker)
    ├── Image  (bottom-center edge marker)
    └── Image  (bottom-right corner marker)
```

The `SafeArea` object is driven by `SafeAreaFitter`.

A semi-transparent green image fills the driven `RectTransform`, making the current safe area visible. Eight red markers are anchored to its corners and edge midpoints, allowing you to quickly verify how the safe area changes across devices and orientations.

## Running the sample

1. Open **Sample.unity**.
2. Open **Window > General > Device Simulator**.
3. Switch the **Game** view to **Simulator** mode.
4. Select different devices and rotate between portrait and landscape orientations.

As you switch devices, the green overlay and red markers update automatically to reflect the current `Screen.safeArea`, making it easy to verify that `SafeAreaFitter` responds correctly to different display cutouts, home indicators, and orientations.
