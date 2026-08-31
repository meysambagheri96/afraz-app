# SPEC-17 — Create Booking

## Objective

- Implement the complete booking command.

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
Implement only the UI for the Booking → After Payment / Booking Success page.

REFERENCE IMAGE: `docs\design\booking\3-after-bank.png`


The final page must visually match the provided reference image as closely as possible.

IMPORTANT:
- UI only
- No backend integration
- No API calls
- No database changes
- No real payment verification
- No real booking creation
- No persistence
- Use mock/local data only

The previous Booking pages have already been implemented successfully.

Reuse the EXACT same:
- shared components
- Persian font
- typography scale
- spacing system
- colors
- icons
- cards
- buttons
- safe-area handling
- AppHeader
- BackButton
- Design System

Do NOT create duplicated one-off components if equivalent shared components already exist.

## Tech Stack

Use the existing frontend stack:

- Vue 3
- TypeScript
- Tailwind CSS
- Existing shared Design System
- Existing shared UI components
- Existing Persian Sans Serif font
- Existing RTL/mobile conventions

## General Requirements

- Persian / RTL
- Mobile-first
- Optimized for iPhone 17 Pro Max
- Match the reference image closely in:
  - layout
  - spacing
  - typography
  - card sizes
  - icon sizes
  - shadows
  - colors
  - borders
  - section hierarchy
  - bottom button placement

Do not redesign the page.

## Header

Reuse the existing shared `AppHeader`.

Title:

«جزئیات نوبت»

Subtitle:

«اطلاعات رزرو و جزئیات درخواست شما»

Back button:
- top-right
- use the same shared BackButton already used in previous Booking pages

## Success Section

Create the success state exactly like the reference.

Include:

- Large green success/check icon
- Small decorative confetti around it
- Main title:

«رزرو نوبت با موفقیت ثبت شد»

- Subtitle:

«از اعتماد شما سپاسگزاریم.»

Use the existing success colors/tokens from the Design System.

Do not create arbitrary green shades if an existing success token exists.

## Request Code Card

Create the request code card.

Label:

«کد درخواست»

Value:

«84273»

Include the copy icon/button.

Below the card show:

«لطفا این کد را برای پیگیری‌های بعدی نزد خود نگه دارید.»

Use local mock value only.

The copy button may copy the mock code locally if easy, but no backend is required.

## User Information Section

Section title:

«اطلاعات کاربر»

Create a reusable information card containing these rows:

### نام و نام خانوادگی
Value:
«علی احمدی»

Icon:
User

### شماره موبایل
Value:
«0912 123 4567»

Icon:
Phone

### ایمیل
Value:
«ali.ahmadi@email.com»

Icon:
Mail

Use existing shared icons and row/card components wherever possible.

## Request Details Section

Section title:

«جزئیات درخواست»

Create a reusable details card with rows matching the reference.

### خدمات انتخابی
Value:
«آتلیه کودک»

Icon:
Camera

### تاریخ رزرو
Value:
«دوشنبه ۱۲ فروردین ۱۴۰۴»

Icon:
Calendar

### استودیو
Value:
«آتلیه افراز قم - شعبه مرکزی»

Icon:
Location

### برای
Value:
«علی احمدی»

Icon:
User

### مبلغ بیعانه
Value:
«۷۰۰,۰۰۰ تومان»

Icon:
Card / Wallet

Use the warm highlighted style shown in the reference for the deposit row.

## Payment Confirmation Notice

Below the request detail rows, create the confirmation notice:

«بیعانه شما ثبت شده است.»

Secondary text:

«این مبلغ از کل هزینه نهایی کسر خواهد شد.»

Include the shield/security icon.

Use the existing Info/Success/Notice component if available.

Do not create a new arbitrary alert style.

## Bottom Actions

At the bottom create two actions exactly like the reference.

Primary button:

«مشاهده نوبت‌های من»

Include calendar/list icon.

Secondary button:

«بازگشت به خانه»

Include home icon.

IMPORTANT:

Reuse the existing shared button components and variants.

Do not create page-specific button CSS.

Both buttons must:

- use existing Design System typography
- use existing icon scale
- respect safe-area
- use correct RTL icon placement
- match the reference proportions

For this UI-only implementation:

- «مشاهده نوبت‌های من» may navigate to a placeholder/existing bookings route
- «بازگشت به خانه» may navigate to the existing home route

No backend is required.

## Suggested Component Structure

BookingSuccessPage
├── AppHeader
├── SuccessState
├── RequestCodeCard
├── SectionHeader
├── InformationCard
│   ├── InformationRow
│   ├── InformationRow
│   └── InformationRow
├── SectionHeader
├── BookingDetailsCard
│   ├── DetailRow
│   ├── DetailRow
│   ├── DetailRow
│   ├── DetailRow
│   ├── DepositRow
│   └── InfoNotice
└── BottomActions
    ├── PrimaryButton
    └── SecondaryButton

Adapt this structure to the existing project if equivalent reusable components already exist.

## Local Mock Data

Use local/mock data only:

requestCode = "84273"
fullName = "علی احمدی"
mobile = "0912 123 4567"
email = "ali.ahmadi@email.com"
service = "آتلیه کودک"
date = "دوشنبه ۱۲ فروردین ۱۴۰۴"
studio = "آتلیه افراز قم - شعبه مرکزی"
deposit = "۷۰۰,۰۰۰ تومان"

## Scope Restrictions

Do NOT implement:

- Backend
- API calls
- Payment verification
- Booking creation
- Database changes
- Transaction lookup
- Real request code generation
- Payment gateway callback
- Final business state persistence

This story is strictly UI implementation.

## Reuse Requirements

Before coding, inspect the previous Booking pages and reuse:

- AppHeader
- BackButton
- PrimaryButton
- SecondaryButton
- AppIcon
- Card
- SectionHeader
- Information/Detail Row components if available
- Alert/Notice component
- Typography tokens
- Spacing tokens
- Color tokens
- Safe-area utilities

If a new reusable component is needed, create it generically and avoid duplicating page-specific markup.

## Final Validation

After implementation:

- Run frontend build/type-check
- Verify RTL
- Verify iPhone 17 Pro Max layout
- Verify safe-area handling
- Verify all spacing matches the reference
- Verify typography matches previous Booking pages
- Verify icons use the shared Design System
- Verify buttons use shared variants
- Verify no unnecessary duplicate components were added
- Verify navigation actions work locally

At the end, briefly report:
- reused shared components
- new reusable components added
- assumptions made
- 
## Frontend UI Completion

- [x] After bank page implemented like reference image.

## Codex Execution Instruction

Before coding:

1. Read the project reference, constitution, architecture and `AGENTS.md`.
2. Inspect existing code related to this feature.
4. Implement only this story and required prerequisites.
5. Run the relevant validation commands.
6. Mark tasks current file as done with [x]
