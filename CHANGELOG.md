# Changelog

## [v1.0.2]

### Fixed
- `SafeAreaFitter` only deferred its initial layout rebuild in the Editor, so builds could skip it entirely and leave the RectTransform in its unlaid-out state (e.g. still collapsed to zero scale) on enable.

## [v1.0.1]

### Fixed
- `SafeAreaFitter` only calculated its layout on first enable, leaving the safe area stale after orientation changes or other UI resize events.

## [v1.0.0]

### Added
- `SafeAreaFitter` component.
- Sample scene.