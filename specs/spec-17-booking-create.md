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
Implement only the UI for the second Booking step: Customer Information / Booking Details.

REFERENCE IMAGE: `docs/design/booking/2-customer-info.png`


The final page must visually match the provided reference image as closely as possible.

IMPORTANT:
- UI only
- No backend integration
- No API calls
- No database changes
- No payment integration
- No actual OTP sending
- No booking submission
- Use mock/local state only

The first Booking step (Jalali calendar/date selection) has already been implemented successfully.

Reuse the EXACT same shared components, typography, spacing, colors, icons, header, buttons, safe-area handling, and Design System from the previous Booking page.

Do NOT create duplicate one-off components if an existing shared component already exists.

## Tech Stack

Use the existing project stack:

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
- Match the reference image in:
  - layout
  - spacing
  - typography scale
  - component dimensions
  - shadows
  - borders
  - colors
  - icon sizes
  - card hierarchy
  - bottom CTA placement

Do not redesign the page.

## Header

Reuse the existing shared `AppHeader` from the previous Booking page.

Header content:

Title:
«رزرو نوبت»

Subtitle:
«مشخصات خود را وارد کنید»

Back button:
- top-right
- exactly the same shared BackButton/component used in the previous Booking screen

Do not create a new header implementation.

## Selected Date Card

At the top of the page, show the currently selected booking date exactly like the reference.

Example:

Label:
«تاریخ انتخاب شده»

Main date:
«دوشنبه ۱۲ فروردین ۱۴۰۴»

Include the large calendar icon on the right.

Add a secondary action button:

«تغییر تاریخ»

Use existing shared button/icon components.

For now, clicking this button may simply navigate back to the previous Booking Date page or trigger a local placeholder action.

## Customer Information Section

Section title:

«مشخصات شما»

Create three form fields:

1. نام
2. نام خانوادگی
3. شماره موبایل

Use the existing shared form/input component from the Design System.

Do not create custom input styling for this page.

Each field must include the corresponding icon on the right.

Suggested icons:
- User
- User
- Mobile

Phone placeholder/example:

«مثال: ۰۹۱۲ ۱۲۳ ۴۵۶۷»

Below the mobile field show:

«کد تایید برای این شماره ارسال خواهد شد.»

For now:
- no OTP request
- no phone verification
- no backend validation

Local form state is enough.

## Booking Details Card

Create the «جزئیات رزرو» card exactly like the reference image.

Rows:

### خدمات انتخابی
Value:
«آتلیه کودک»

Include the small colored status dot.

### تاریخ
Value:
«دوشنبه ۱۲ فروردین ۱۴۰۴»

### ساعت
Value:
«ساعت تعیین می‌شود»

Include the clock icon according to the reference.

Use reusable row/item components if such components already exist.

Do not hardcode inconsistent layout for each row.

## Deposit Section

Inside the booking details card, create the deposit section.

Title:

«مبلغ بیعانه»

Subtitle:

«برای قطعی شدن رزرو نوبت»

Amount:

«۷۰۰,۰۰۰ تومان»

Use the same currency formatting conventions already used by the app.

Include the information icon shown in the reference.

Use existing color/design tokens.

## Cancellation / Refund Notice

Below the deposit amount, add the warning/information area:

«در صورت لغو نوبت تا ۲۴ ساعت قبل از موعد، بیعانه به شما بازگردانده می‌شود.»

Include the shield icon.

Use the same warm/soft warning surface from the existing Design System.

Do not introduce a new arbitrary warning style if an existing Alert/Info component can be reused.

## Bottom CTA

At the bottom, create the primary CTA:

«پرداخت و ادامه»

IMPORTANT:

Reuse the exact same shared Primary Button component used in the previous Booking screen.

Do not create a custom CTA.

Requirements:

- Full width based on existing app margins
- Same height/radius/icon scale as previous page
- Same typography
- Same primary color
- Safe-area aware
- Directional arrow/icon exactly according to existing RTL button behavior

For now, click action may be a local placeholder.

Do NOT start a real payment.

## Security Note

Below the CTA show:

«اطلاعات شما محفوظ و امن است.»

Include the small lock icon.

This is only visual.

## Component Reuse

Before coding, inspect the existing Booking page implementation and reuse:

- AppHeader
- BackButton
- PrimaryButton
- SecondaryButton
- AppInput
- AppIcon
- Card
- Info/Alert component
- Typography tokens
- Spacing tokens
- Color tokens
- Safe-area utilities

If a component from the previous Booking page can support this screen with props/variants, extend/reuse it instead of copying it.

## Suggested Component Structure

BookingCustomerInfoPage
├── AppHeader
├── SelectedDateCard
├── CustomerInfoForm
│   ├── AppInput
│   ├── AppInput
│   └── AppInput
├── BookingDetailsCard
│   ├── BookingDetailRow
│   ├── BookingDetailRow
│   ├── BookingDetailRow
│   └── DepositSummary
├── InfoAlert
├── PrimaryButton
└── SecurityCaption

Adapt this structure to existing project conventions if equivalent components already exist.

## Local State

Use local Vue reactive state only for:

- firstName
- lastName
- mobile
- selectedDate mock/display value

No persistence is required.

## Scope Restrictions

Do NOT implement:

- Backend
- API calls
- EF Core changes
- Actual booking creation
- OTP sending
- OTP verification
- Payment gateway
- Payment state
- Final reservation submission

This task is strictly UI implementation.

## Final Validation

After implementation:

- Run frontend build/type-check
- Verify RTL
- Verify iPhone 17 Pro Max layout
- Verify safe-area behavior
- Verify form controls use shared Design System
- Verify header matches previous Booking page
- Verify bottom CTA matches previous Booking page
- Verify typography and icon scale are consistent
- Verify no unnecessary duplicate components were created

At the end, briefly report:
- Which existing shared components were reused
- Which new reusable components were added
- Any assumptions made

## Acceptance Criteria

- [ ] Concurrent requests cannot double-book the same slot.
- [ ] Frontend cannot alter price.
- [ ] Validation errors are shown clearly.

## Frontend UI Completion

- [x] Customer information step implemented with local state only.
- [x] Selected date, booking summary, deposit and cancellation notice implemented.
- [x] Shared header, input, icon and primary button components reused.
- [x] Local validation errors are displayed without backend or payment integration.

## Codex Execution Instruction

Before coding:

1. Read the project reference, constitution, architecture and `AGENTS.md`.
2. Inspect existing code related to this feature.
4. Implement only this story and required prerequisites.
5. Run the relevant validation commands.
6. Mark tasks current file as done with [x]
