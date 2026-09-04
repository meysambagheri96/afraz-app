# SPEC-08 — Authentication Backend

## Objective

- Implement customer authentication foundation.
- Login with jwt 
- Create User AggregateRoot, with :
  - Entities: Roles, Addresses, Otps, Logins, Sessions,  ...
- Add User CRUD Commands and Queries
- Add User Registeration and Login Commands
- Add migration for User
- Ef Fluent config for Users 
- Add all required fields for User/Customer + These fields:
		public int UserId { get; private set; }
		public string FirstName { get; private set; }
		public string LastName { get; private set; }
		public string NationalCode { get; private set; }
		public string ShebaNumber { get; private set; }
		public string CardNumber { get; set; }
		public string AccountNumber { get; set; }
		public string Phone { get; private set; }
		public string DialingCode { get; private set; }
		public string Email { get; private set; }
		public bool IsActive { get; private set; }
		public string Avatar { get; set; } 
		public DateTime? LastLoginDate { get; set; }
		public Gender? Gender { get; set; }
		public DateTime? BirthDate { get; set; }
		public DateTime ModifiedDate { get; set; }
		public int ModifiedBy { get; private set; }
		public int CreatedBy { get; private set; }
- Handle Login with jwt bearer
- Handle Login with google (on google redirect, create user and generate token)

## Business Context

This story is part of the Afraz Studio application. It must remain consistent with:

- `docs/afraz-studio-reference.md`
- `docs/afraz-studio-constitution.md`
- `docs/afraz-studio-architecture.md`
- `AGENTS.md`

All customer-facing UI must be Persian, RTL, mobile-first, and optimized for the iPhone 17 Pro Max design target unless this story is backend-only.

## Backend Scope

- Implement registration, login, OTP request/verify skeleton, refresh token, logout/revocation.
- Use JWT access tokens and rotating refresh tokens.
- Add rate limiting for OTP/login endpoints.
- Persist users securely.

## Frontend Scope

- No customer-facing frontend implementation is required in this story unless needed for verification.

## Acceptance Criteria

- [ ] Authentication endpoints are tested.
- [ ] Google login and OTP login implemented. 
## Codex Execution Instruction

Before coding:

1. Read the project reference, constitution, architecture and `AGENTS.md`.
2. Inspect existing code related to this feature.
3. Produce a concise implementation plan listing affected modules/files, database changes, API changes, frontend changes, tests and risks.
4. Implement only this story and required prerequisites.
5. Run the relevant validation commands.
