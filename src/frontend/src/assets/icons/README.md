# Afraz icon sprite

`afraz-icons.svg` is the single custom icon source for main navigation, service actions, and photography categories.
Every symbol uses a `24 × 24` view box, rounded outline geometry, `currentColor`, and the shared stroke token.

Use icons through `components/ui/AppIcon.vue`; direct sprite references are reserved for the wrapper. Navigation symbols
provide outline and filled variants. Business and category symbols remain outline icons and use the active brand color
when selected.
