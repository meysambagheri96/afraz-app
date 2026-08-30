# Frontend asset pipeline

Assets are grouped by responsibility so feature code does not depend on files under `docs/`.

```text
assets/
├── icons/                Custom theme-aware SVG sprite
├── images/
│   ├── photography/      Optimized portfolio, gallery, product and banner variants
│   └── placeholders/     Lightweight fallbacks used before or after image loading
├── logos/                Approved Afraz brand marks only
└── patterns/             Reusable non-interactive SVG background patterns
```

## Rules

- Use `AppIcon` with an `AppIconName`; do not import a second icon library into feature components.
- Meaningful standalone icons receive a Persian `label`. Icons inside an already-labelled control remain decorative.
- Photography assets should use WebP or AVIF where practical, include explicit dimensions, and load lazily outside the initial viewport.
- Keep originals outside the frontend bundle. Add only delivery-sized variants required by a screen.
- Logo files must be approved brand assets and must not be recreated from screenshots.
- File names are lowercase kebab-case and describe content rather than screen position.
