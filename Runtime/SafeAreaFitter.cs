// Copyright (c) 2026 UltraKuneho.
// SPDX-License-Identifier: MIT


using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace UltraKuneho.SafeArea {
  /// <summary>
  /// Automatically fits the attached <see cref="RectTransform"/> to the current
  /// <see cref="Screen.safeArea"/>.
  /// </summary>
  /// <remarks>
  /// Intended to be placed as a direct child of the root
  /// <see cref="RenderMode.ScreenSpaceOverlay"/> <see cref="Canvas"/>.
  /// It may also work under child canvases whose rect matches the root canvas.
  /// </remarks>
  [ExecuteAlways]
  [RequireComponent(typeof(RectTransform))]
  [DisallowMultipleComponent]
  [AddComponentMenu("Layout/" + nameof(SafeAreaFitter))]
  public class SafeAreaFitter : UIBehaviour, ILayoutSelfController {
    private static readonly Vector2 k_Pivot = new(0.5f, 0.5f);

    private const DrivenTransformProperties k_DrivenProperties
      = DrivenTransformProperties.Anchors
      | DrivenTransformProperties.AnchoredPosition
      | DrivenTransformProperties.SizeDelta
      | DrivenTransformProperties.Pivot
      | DrivenTransformProperties.Rotation
      | DrivenTransformProperties.Scale;


    [SerializeField] private RectTransform m_RectTransform;


    private DrivenRectTransformTracker m_Tracker;


    protected override void OnEnable() {
      EnsureReferences();
      m_Tracker.Add(this, m_RectTransform, k_DrivenProperties);
      StartCoroutine(DelayedLayout());
    }

    protected override void OnDisable() {
      m_Tracker.Clear();
    }

    protected override void OnRectTransformDimensionsChange() {
      if (!isActiveAndEnabled) return;
      LayoutRebuilder.MarkLayoutForRebuild(m_RectTransform);
    }

    private IEnumerator DelayedLayout() {
      yield return null;
      OnRectTransformDimensionsChange();
    }

#if UNITY_EDITOR
    protected override void Reset() {
      EnsureReferences();
    }

    protected override void OnValidate() {
      EnsureReferences();

      var parent = m_RectTransform.parent;
      var canvas = parent ? parent.GetComponent<Canvas>() : null;

      if (!canvas || canvas.renderMode != RenderMode.ScreenSpaceOverlay) {
        Debug.LogWarning($"{nameof(SafeAreaFitter)} must be a direct child of a Screen Space Overlay Canvas.", this);
        return;
      }

      if (!canvas.isRootCanvas) {
        Debug.LogWarning($"{nameof(SafeAreaFitter)} parent is not a root canvas and may not work as expected.", this);
      }
    }
#endif

    public void SetLayoutHorizontal() {
      var width = Screen.width;
      if (width <= 0) return;

      var height = Screen.height;
      if (height <= 0) return;

      var area = Screen.safeArea;

      m_RectTransform.localRotation = Quaternion.identity;
      m_RectTransform.localScale = Vector3.one;
      m_RectTransform.anchorMin = new Vector2(area.xMin / width, area.yMin / height);
      m_RectTransform.anchorMax = new Vector2(area.xMax / width, area.yMax / height);
      m_RectTransform.anchoredPosition = Vector2.zero;
      m_RectTransform.sizeDelta = Vector2.zero;
      m_RectTransform.pivot = k_Pivot;
    }

    public void SetLayoutVertical() {
      // Layout is fully handled by SetLayoutHorizontal().
    }


    private void EnsureReferences() {
      if (!m_RectTransform) m_RectTransform = GetComponent<RectTransform>();
    }
  }
}