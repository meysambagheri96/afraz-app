# SPEC-16 — Booking Calendar & Availability

## Objective

- Implement Persian/Jalali date availability and time-slot calculation.

## Business Context

This story is part of the Afraz Studio application. It must remain consistent with:

- `docs/afraz-studio-reference.md`
- `docs/afraz-studio-constitution.md`
- `docs/afraz-studio-architecture.md`
- `AGENTS.md`

All customer-facing UI must be Persian, RTL, mobile-first, and optimized for the iPhone 17 Pro Max design target unless this story is backend-only.

## Backend Scope
- Nothing yet

## Frontend Scope

حتماً. این نسخه جزئیات کامل‌تری دارد ولی همچنان برای Codex مستقیم و اجرایی است:

Implement the Booking → Date Selection page UI for the Afraz Studio mobile application.

REFERENCE IMAGE: `docs/design/booking/1-calendar.png`

The final implementation must visually match the provided reference image as closely as possible while remaining fully consistent with the existing Afraz Studio design system and shared components.

IMPORTANT:
This task is UI-only.
Do NOT implement backend integration, API calls, database operations, reservation submission, payment, or server-side booking logic.

## Tech Stack

Use the project's existing frontend stack:

- Vue 3
- TypeScript
- Tailwind CSS
- Existing frontend architecture and conventions
- Existing shared Design System
- Existing shared UI components

Do not introduce a new UI framework.

## General Design

- Persian language
- Full RTL layout
- Mobile-first
- Optimize for the existing iPhone 17 Pro Max design/frame
- Follow the existing app typography, spacing, colors, iconography and component sizing
- Use the existing Persian Sans Serif font configured in the project
- Preserve iOS safe areas
- Keep the visual style minimal, modern and consistent with the rest of Afraz Studio

Use the reference image as the primary source for:
- layout
- spacing
- component proportions
- hierarchy
- calendar structure
- typography hierarchy
- status colors
- cards
- CTA placement

Do not redesign the screen unnecessarily.

## Shared Components

Before implementing anything, inspect the existing frontend codebase and Design System.

Reuse existing shared components wherever possible.

Especially reuse:

- App/Page Header
- Back Button
- Primary Button
- Icon components
- Card/container primitives
- Typography tokens
- Color tokens
- Spacing tokens

Do NOT create page-specific copies of components that already exist.

The Back button at the top-right must use exactly the same shared BackButton component used elsewhere in the app.

The bottom «مرحله بعد» button must use the same shared Primary Button component used throughout the application.

If a reusable Calendar component does not exist, create it as a reusable Design System/application component rather than embedding all calendar logic directly inside the page.

## Header

Create the header similar to the reference.

RTL structure:

- Back button on the top-right
- Page title: «رزرو نوبت»
- Subtitle: «روز مورد نظر خود را انتخاب کنید»
- Optional/help icon on the opposite side, matching the reference

Use the existing shared header/back-button styling.

## Persian / Jalali Calendar

Implement a real Persian (Jalali/Shamsi) calendar UI.

Example header:

«فروردین ۱۴۰۴»

The calendar must:

- Display Persian month names
- Display Persian/Jalali dates
- Respect RTL calendar layout
- Show Persian weekday labels
- Support previous/next month navigation
- Correctly display dates belonging to adjacent months as disabled/faded
- Keep calendar state entirely local for now

Use mock/local data for availability.

## Calendar Day States

Support visually distinct states for:

1. Available / قابل رزرو
2. Selected / انتخاب شده
3. Full capacity / تکمیل ظرفیت
4. Disabled / غیر فعال
5. Official holiday / تعطیل رسمی

Match the reference image's visual language.

For example:

- Available → small green indicator
- Selected → prominent filled primary-color circle
- Full capacity → warm/orange indicator/state
- Disabled → muted gray
- Official holiday → red/pink state

Use existing Design System colors where equivalent tokens already exist.

Do not hardcode random colors when suitable tokens already exist.

## Calendar Interaction

UI interaction should work locally.

The user must be able to:

- navigate between months
- tap an available date
- change the selected date

Unavailable, disabled or full-capacity dates should not become selected.

No backend request is required.

Use Vue local/reactive state only.

## Calendar Container

Place the calendar inside a large clean card similar to the reference image.

Maintain:

- generous internal spacing
- clear month header
- navigation controls
- readable date grid
- consistent touch targets
- balanced vertical rhythm

The component must not feel cramped.

## Status Legend

Below the calendar, add the «راهنمای وضعیت روزها» section.

Display all calendar statuses:

- قابل رزرو
- انتخاب شده
- تکمیل ظرفیت
- غیر فعال
- تعطیل رسمی

Each status should have its corresponding visual indicator.

Keep this section visually close to the reference.

## Information Card

Below the legend, create the informational card shown in the reference.

Title:

«توجه»

Text:

«پس از انتخاب روز، در مرحله بعد ساعت مورد نظر خود را انتخاب خواهید کرد.»

Use the appropriate shared icon/design-system icon.

Keep the card subtle and consistent with the application's theme.

## Bottom CTA

Add the primary CTA at the bottom:

«مرحله بعد»

Requirements:

- Reuse the existing shared Primary Button
- Full-width according to the application's standard horizontal margins
- Match the reference proportions
- Respect iPhone bottom safe-area
- Include the appropriate directional icon if supported by the shared button component
- Do not create a custom one-off button style

For this UI-only stage, clicking it may simply trigger a local placeholder action or remain ready for routing in the next story.

It must not call the backend.

## Responsive / Sizing

The design should primarily target the application's iPhone 17 Pro Max layout.

Ensure:

- consistent horizontal margins
- standard section spacing
- appropriate touch targets
- typography scale consistent with the rest of the application
- no horizontal overflow
- proper safe-area handling
- correct RTL behavior

## Component Structure

Prefer a clean structure similar to:

BookingDatePage
├── AppHeader
├── PersianCalendar
│   ├── CalendarHeader
│   ├── WeekDays
│   └── CalendarDay
├── CalendarStatusLegend
├── InfoCard
└── PrimaryButton

Adjust this structure if the existing project architecture already has better equivalents.

## Code Quality

Follow the existing frontend coding standards.

- Keep components small and reusable
- Keep page-specific orchestration in the page
- Keep reusable calendar UI outside the page
- Use typed models/interfaces
- Avoid duplicated Tailwind classes when existing variants/components can be used
- Do not introduce unnecessary dependencies
- Do not modify unrelated pages
- Do not break the existing Design System

## Scope Restrictions

Do NOT implement:

- Backend API
- SQL/EF Core changes
- Booking persistence
- Authentication changes
- Payment
- Reservation capacity backend logic
- Time-slot selection
- Final reservation submission

Those belong to later stories.

For now, mock the calendar availability data locally.

## Final Result

The resulting page should look almost exactly like the supplied reference image, but it must be implemented using the existing Afraz Studio components and Design System rather than as an isolated custom page.

Before writing code:

1. Inspect the current frontend architecture.
2. Identify existing shared components and design tokens.
3. Reuse them.
4. Check whether a Jalali/date utility already exists before adding another dependency.
5. Then implement the page.

After implementation:

- run the frontend build/type-check
- fix any errors introduced by this change
- verify RTL layout
- verify month navigation
- verify local date selection
- verify all five calendar states
- verify iPhone safe-area behavior
- briefly report which existing shared components were reused and which new reusable components were added.


## Acceptance Criteria

- [x] Past dates cannot be booked.
- [x] Persian calendar
- [x] UI reflects mocked availability states; server integration remains out of scope for this UI story.


## Codex Execution Instruction

Before coding:

1. Read the project reference, constitution, architecture and `AGENTS.md`.
2. Inspect existing code related to this feature.
4. Implement only this story and required prerequisites.
5. Run the relevant validation commands.
6. Mark tasks current file as done with [x]
