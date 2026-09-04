# SPEC-10 — Customer Profile Foundation

## Objective

- Create customer profile data model and basic profile APIs/UI.

## Business Context

This story is part of the Afraz Studio application. It must remain consistent with:

- `docs/afraz-studio-reference.md`
- `docs/afraz-studio-constitution.md`
- `docs/afraz-studio-architecture.md`
- `AGENTS.md`

All customer-facing UI must be Persian, RTL, mobile-first, and optimized for the iPhone 17 Pro Max design target unless this story is backend-only.

## Backend Scope
 - Nothing
 - 
## Frontend Scope

# Implement User Profile UI

Implement only the **frontend UI** for the Afraz Studio **User Profile** page.

Use the provided profile reference image as the exact visual reference:

- `docs\design\profile\profile.png`

The final result must visually match the provided screenshot as closely as possible and must use the existing Afraz Studio Design System.

## Scope

UI only.

Do NOT implement:

- Backend/API calls
- Database changes
- Real profile update
- Real logout
- Authentication/session changes
- Real reservations/orders fetching
- File upload for profile avatar
- Real support submission

Use local/mock data and placeholder navigation only.

## Stack

Use the existing project stack:

- Vue 3
- TypeScript
- Tailwind CSS
- Existing shared Design System
- Existing Persian Sans Serif font
- Existing RTL/mobile conventions

## Reuse Existing Components

Before coding, inspect the current frontend and reuse existing shared components wherever possible:

- `AppHeader`
- `BottomNavigation`
- `AppCard`
- `AppIcon`
- `AppButton`
- `AppBadge`
- `AppDivider`
- shared typography, spacing and color tokens
- safe-area / Dynamic Island utilities

Do not create duplicate design-system components.

## Page Design

Create the page in Persian / RTL.

### Header

Reuse the existing shared app header.

Title:

`پروفایل`

Include the same top actions shown in the reference, such as notification/settings icons, using existing shared icon buttons.

### Profile Summary Card

Display:

- user avatar
- name: `سارا محمدی`
- mobile: `0912 123 4567`
- membership badge: `عضو ویژه`
- action: `ویرایش اطلاعات`

Reuse existing card/button/icon styles.

Avatar interaction can be visual only.

## Profile Menu

Create one large clean menu card exactly like the reference.

Each row should reuse a common reusable menu-row component with:

- icon on the right
- title
- short subtitle
- navigation chevron on the left
- divider between rows

Menu items should match the Afraz business:

1. `رزروهای من`
   - `مشاهده و مدیریت رزروهای آتلیه`

2. `سفارش‌های من`
   - `مشاهده سفارش‌ها و وضعیت پرداخت`

3. `عکس‌های من`
   - `مشاهده و انتخاب عکس‌های آماده شده`

4. `آلبوم‌های من`
   - `آلبوم‌های خریداری شده و در حال ساخت`

5. `آدرس‌های من`
   - `مدیریت آدرس‌های ارسال سفارش‌ها`

6. `اطلاعات حساب`
   - `مشاهده و ویرایش اطلاعات کاربری`

7. `پشتیبانی`
   - `تماس با پشتیبانی و ثبت درخواست`

8. `درباره آتلیه افراز`
   - `با ما و خدمات ما بیشتر آشنا شوید`

9. `خروج از حساب کاربری`
   - red/destructive style exactly like the reference

Use local placeholder click handlers only.

## Admin-Only Items

If the mock current user is marked as admin, append an additional admin section using the same menu-row design:

- `مدیریت محتوا`
- `مدیریت سفارشات`
- `مدیریت رزروها`
- `مدیریت بنرها`

Keep this section visually consistent with the rest of the profile page.

Do not implement admin functionality in this story.

## Bottom Navigation

Reuse the exact existing shared floating/liquid-glass bottom navigation.

The `پروفایل` item must be active.

Do not create a new navigation component.

## Responsive Target

- iPhone 16/17 Pro Max
- Persian RTL
- Dynamic Island safe-area
- bottom safe-area
- no horizontal overflow

## Suggested Structure

```text
features/profile/
├── pages/
│   └── ProfilePage.vue
├── components/
│   ├── ProfileSummaryCard.vue
│   ├── ProfileMenu.vue
│   └── ProfileMenuItem.vue
└── data/
    └── profile.mock.ts
```

Adapt to the existing project structure and avoid unnecessary abstractions.

## Final Validation

After implementation:

- run frontend build
- run TypeScript type-check
- verify RTL
- verify iPhone layout
- verify shared header and bottom navigation are reused
- verify typography/icons/cards match the existing Afraz Design System
- verify menu order and business-specific labels
- verify destructive logout row styling
- verify no duplicate Design System components were introduced

At the end briefly report reused components and newly added reusable components.

- Create profile overview and edit profile screens.
- Add profile avatar/basic information UI.

## Acceptance Criteria

- [ ] Authenticated customer can view and update own profile.
- [ ] Another user's profile cannot be accessed.

## Codex Execution Instruction

Before coding:

1. Read the project reference, constitution, architecture and `AGENTS.md`.
2. Inspect existing code related to this feature.
3. Produce a concise implementation plan listing affected modules/files, database changes, API changes, frontend changes, tests and risks.
4. Implement only this story and required prerequisites.
5. Run the relevant validation commands.
6. Report completed work, tests executed and any remaining assumptions.
