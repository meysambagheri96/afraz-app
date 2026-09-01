# SPEC-09 — Authentication Frontend

## Objective

- Implement Persian RTL authentication UX.

## Business Context

This story is part of the Afraz Studio application. It must remain consistent with:

- `docs/afraz-studio-reference.md`
- `docs/afraz-studio-constitution.md`
- `docs/afraz-studio-architecture.md`
- `AGENTS.md`

All customer-facing UI must be Persian, RTL, mobile-first, and optimized for the iPhone 17 Pro Max design target unless this story is backend-only.

## Backend Scope

- Nothing

## Frontend Scope

- Create Welcome, Login, Register, OTP, Forgot/Reset Password if enabled, Session Expired states.
- Persist auth state securely through an abstraction suitable for Capacitor.
- Handle loading/error/resend timer states.
  
# Implement Login + OTP UI with Fullscreen Auth Modal

Implement only the **UI** for the Afraz Studio authentication flow:

1. Login with mobile number
2. OTP verification

Use the provided reference images as the exact visual reference:

- Login reference: `docs\design\login\login.png`
- OTP reference: `docs\design\login\otp.png`

The final implementation should match the screenshots as closely as possible while reusing the existing Afraz Studio design system and shared components.

---

## Authentication Navigation Strategy

Use this architecture:

```text
Primary UX:
Auth Fullscreen Modal ✅

Optional fallback:
 /auth ✅

Dedicated login page as normal navigation:
❌ unnecessary
```

The authentication experience should primarily be implemented as a reusable **Fullscreen Auth Modal** that can open from anywhere in the app when authentication is required.

Examples:

- booking requires authentication
- opening profile while unauthenticated
- checkout requires authentication
- accessing private customer gallery
- accessing customer orders

Preferred flow:

```text
Current App Screen
      │
      ▼
Protected Action
      │
      ▼
Open Auth Fullscreen Modal
      │
      ├── Login
      │      │
      │      ▼
      │     OTP
      │
      ▼
Authentication Success
      │
      ▼
Close Modal
      │
      ▼
Resume Original Action
```

Do not use a normal `/login` page as part of the standard user navigation flow.

---

# Optional `/auth` Fallback Route

Keep or create an optional fallback route:

```text
/auth
```

This route exists only for exceptional/direct navigation scenarios such as:

- direct URL navigation
- browser refresh/recovery
- deep links
- expired session recovery
- debugging
- web fallback behavior

The `/auth` route should render the same authentication UI/components used by the fullscreen modal.

Do NOT maintain two separate implementations.

Use one shared authentication flow and reuse it in both:

```text
Auth Fullscreen Modal
        +
Optional /auth fallback
```

The fullscreen modal remains the primary UX.

---

# Shared Fullscreen Modal

Before implementing authentication, inspect the existing shared component library.

If a reusable modal/fullscreen modal already exists, reuse it.

If it does not exist, create a reusable component such as:

```text
AppFullscreenModal.vue
```

Recommended location:

```text
components/ui/AppFullscreenModal.vue
```

If the existing `AppModal` architecture supports variants, prefer extending it:

```text
AppModal
  variant="default"
  variant="bottom-sheet"
  variant="fullscreen"
```

instead of creating a parallel modal system.

## Fullscreen Modal Requirements

The shared modal should support:

- Full viewport coverage
- iPhone safe areas
- Dynamic Island-safe top spacing
- RTL
- Optional header
- Optional back action
- Optional close action
- Scrollable body
- Sticky/fixed footer support
- Design-system surface/background
- Body scroll locking
- Shared z-index tokens
- Consistent transition/animation
- Accessible dialog semantics where practical
- Escape/back handling
- Capacitor/mobile back compatibility

Do not hard-code auth-specific content inside this shared component.

---

# Auth Flow State

Login and OTP must use local feature state rather than route navigation.

Example:

```ts
type AuthStep = 'login' | 'otp'
```

Flow:

```text
Modal Open
   │
   ▼
login
   │
   │ دریافت کد ورود
   ▼
otp
   │
   │ back
   ▼
login
```

On successful mock OTP confirmation:

```text
otp
 │
 ▼
auth complete callback
 │
 ▼
close modal
 │
 ▼
resume original action
```

No backend call is required in this task.

---

# Reusable Auth Trigger

Design the modal so protected features can later request authentication through a simple reusable interface.

Preferred conceptual API:

```ts
const authenticated = await requireAuth()
```

or:

```ts
authModal.open({
  onSuccess: () => continueOriginalAction()
})
```

Do not over-engineer this in the UI-only story.

If a global modal/store system already exists, reuse it.

---

# Tech Stack

Use the existing project stack:

- Vue 3
- TypeScript
- Tailwind CSS
- Existing Design System
- Existing Persian Sans Serif font
- Existing RTL/mobile conventions
- Existing icon system
- Existing shared UI components

Do not introduce a new UI framework.

---

# Important Scope

This task is primarily **UI-only**.

Do NOT implement:

- real backend API calls
- OTP sending
- OTP verification
- Google OAuth
- JWT
- refresh tokens
- customer persistence
- ASP.NET Core changes
- database changes

Use mock/local frontend behavior only.

---

# Reuse Existing Design System

Before coding, inspect the current frontend.

Reuse existing shared components wherever possible, especially:

- `AppHeader`
- `BackButton`
- `PrimaryButton`
- `SecondaryButton`
- `AppInput`
- `AppIcon`
- `AppDivider`
- Card/surface components
- typography tokens
- color tokens
- spacing tokens
- radius tokens
- safe-area utilities
- icon-size tokens
- existing logo asset

Do NOT create duplicate page-specific UI primitives.

---

# Login Step

The Login UI renders inside the fullscreen modal.

## Header

- Respect Dynamic Island/top safe area
- Back/close action at the top-right
- Reuse existing shared button/icon component
- Closing should dismiss the auth modal and return to the underlying screen
- Do not introduce a new header style

## Branding

Show:

- Afraz Studio logo
- Title:
  `آتلیه افراز`

Use the existing project logo asset.

## Hero Image

Show the baby/child photography hero based on the provided reference.

Requirements:

- prominent
- visually integrated
- warm photography style
- consistent with existing app aesthetic

## Mobile Input

Create the mobile-number field with:

- phone icon
- placeholder:
  `شماره موبایل خود را وارد کنید`
- country prefix:
  `+98`

Reuse the existing shared input component.

Requirements:

- RTL
- mobile/numeric keyboard where supported
- design-system height/radius
- existing typography
- local state only

## Primary Action

Text:

`دریافت کد ورود`

Reuse the existing shared `PrimaryButton`.

On click:

- optionally perform local validation
- transition from `login` to `otp`
- do not call backend

## Divider

Show:

`یا`

Reuse the existing divider component/pattern.

## Google Login

Add Google login UI.

Text:

`ورود با گوگل`

Requirements:

- Google icon
- secondary/outlined button variant
- shared button sizing
- visual-only placeholder handler

Do NOT implement OAuth yet.

## Terms

Show:

`ورود به معنای پذیرش قوانین و حریم خصوصی است.`

Highlight:

- `قوانین`
- `حریم خصوصی`

Use existing brand/accent colors.

---

# OTP Step

OTP must render inside the **same fullscreen modal**.

Do not navigate to a separate route during normal modal flow.

## Header

- Back action returns from `otp` to `login`
- Keep modal open
- Reuse existing shared BackButton
- Respect safe area

## Title

`تایید شماره موبایل`

## Description

Show:

`کد تایید ۶ رقمی به شماره موبایل شما ارسال شد.`

Show mock mobile number:

`۰۹۱۲ ۱۲۳ ۴۵۶۷`

## OTP Input

Use/create a reusable `OtpInput`.

Requirements:

- six boxes
- RTL-aware
- numeric input
- primary active border
- auto-focus next
- backspace previous
- paste support if practical
- local state only

If missing, create it as a reusable component, not inline page-only logic.

## Resend Section

Show:

`کد را دریافت نکردید؟`

Action:

`ارسال مجدد`

Mock timer:

`۰۱:۴۵`

Use local countdown state only.

## Primary Action

Text:

`تایید و ورود`

Reuse `PrimaryButton`.

For this UI-only story:

- call a mock auth success callback
- close modal
- allow original flow to continue conceptually

No real auth/session is required.

## Security Notice

Show:

`اطلاعات شما محفوظ و امن است.`

Reuse existing notice/security component if available.

---

# Optional `/auth` Fallback Implementation

The optional `/auth` route should reuse the exact same auth flow component.

Recommended:

```text
AuthFlow.vue
├── LoginStep
└── OtpStep
```

Then reuse:

```text
AuthFullscreenModal
  └── AuthFlow

/auth
  └── AuthFlow
```

Do not duplicate Login/OTP markup.

The `/auth` route may render `AuthFlow` inside a normal full-screen app surface without modal overlay behavior.

---

# Suggested Component Structure

```text
features/auth/
├── components/
│   ├── AuthFlow.vue
│   ├── AuthFullscreenModal.vue
│   ├── LoginStep.vue
│   └── OtpInput.vue
│
├── pages/
│   └── AuthFallbackPage.vue
│
├── composables/
│   └── useAuthModal.ts
│
├── schemas/
│   └── auth.schema.ts
│
└── types/
    └── auth.types.ts
```

Shared UI:

```text
components/ui/
└── AppFullscreenModal.vue
```

Adapt naming to current project conventions.

---

# Local State

Use local/frontend state for:

- modal open/closed
- auth step
- mobile number
- OTP digits
- resend timer
- optional mock success callback

No persistence is required.

---

# General UI Requirements

- Persian / RTL
- Vue 3
- TypeScript
- Tailwind CSS
- Mobile-first
- Optimized for iPhone 17 Pro Max
- Respect Dynamic Island
- Respect top/bottom safe areas
- Use existing Persian Sans Serif font
- Use existing design tokens
- Use shared component scale
- Match the provided screenshots closely

Match references in:

- typography
- spacing
- icon sizing
- field height
- button height
- radius
- shadows
- hero positioning
- horizontal margins
- safe-area spacing

---

# Scope Restrictions

Do NOT implement:

- dedicated `/login` route
- dedicated `/otp` route
- real API integration
- SMS integration
- Google OAuth
- JWT
- refresh token
- backend authentication
- customer creation
- database changes

Allowed route:

```text
/auth
```

only as an optional fallback/recovery route.

Primary UX remains:

```text
Auth Fullscreen Modal
```

---

# Final Validation

After implementation:

- run frontend build
- run TypeScript type-check
- verify auth opens as fullscreen modal over current screen
- verify closing restores underlying screen
- verify Login → OTP transition without normal route navigation
- verify OTP → Login back behavior
- verify optional `/auth` fallback works using the same components
- verify no separate `/login` or `/otp` normal navigation flow exists
- verify RTL
- verify Dynamic Island safe area
- verify bottom safe area
- verify body scroll locking
- verify OTP interaction
- verify resend timer locally
- verify Google login is visual/placeholder only
- verify all shared components are reused where possible
- verify no duplicate authentication UI implementation exists

At the end report briefly:

- shared components reused
- whether modal was reused or newly implemented
- new reusable components added
- how the auth modal is triggered
- how Login → OTP state is handled
- how `/auth` fallback reuses the same AuthFlow
- assumptions made


## Acceptance Criteria

- [ ] Forms are validated with VeeValidate + Zod.
- [ ] RTL and keyboard behavior are correct.
- [ ] The numeric keyboard (for mobile devices) should open on inputs 
- [ ] Auto fill OTP from received SMS
- [ ] Other pages should not get changes
- [ ] Connect bottom-navigation__icon to login page 
- [ ] Login should be a FullScreen Modal

## Codex Execution Instruction

Before coding:

1. Read the project reference, constitution, architecture and `AGENTS.md`.
2. Inspect existing code related to this feature.
4. Implement only this story and required prerequisites.
5. Run the relevant validation commands.
6. Mark tasks current file as done with [x]
