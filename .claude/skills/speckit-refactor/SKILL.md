---

name: speckit-refactor
description: Review and refactor Spec Kit artifacts after implementation. Use when the user questions an implementation decision, wants to remove or change implemented behavior, or wants to synchronize spec.md, plan.md, tasks.md, contracts, and related artifacts with an approved change. Always trace questioned implementation decisions back to their source artifacts before proposing changes, and require explicit user approval before modifying artifacts or implementation as a consequence of the review.
-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

# Spec Kit Refactor

## Purpose

This skill provides an interactive post-implementation review workflow for Spec Kit.

It is intended to be used after:

```text
/speckit.implement
```

The purpose is **not** to automatically rewrite the specification or make the implementation conform to assumptions.

The purpose is to help the user challenge implementation decisions and determine:

1. Why something was implemented.
2. Which Spec Kit artifact caused or justified it.
3. Whether it was an explicit requirement or merely an implementation choice.
4. What would be affected if the user rejects it.
5. Which artifacts need to change after the user's decision.

The user remains the decision-maker.

---

# Core Principle

**Never defend an implementation merely because the agent implemented it.**

When the user questions something such as:

```text
"I don't want this method."

"Why did you create this class?"

"Where is this required?"

"Show me what this implementation is based on."

"Why did you implement it this way?"

"I don't think we need this."

"Remove this behavior."
```

Do not immediately modify code or Spec Kit artifacts.

First investigate and explain the source of the implementation decision.

Use this traceability chain:

```text
                    ┌──────────────┐
                    │ Implementation│
                    └──────┬───────┘
                           ↓
                    ┌──────────────┐
                    │    Tasks     │
                    └──────┬───────┘
                           ↓
                    ┌──────────────┐
                    │     Plan     │
                    └──────┬───────┘
                           ↓
                    ┌──────────────┐
                    │     Spec     │
                    └──────┬───────┘
                           ↓
                    ┌──────────────┐
                    │ User Intent  │
                    └──────────────┘
```

Determine where the questioned decision originated.

---

# Required Context

Before analyzing an implementation decision, locate the active Spec Kit feature directory.

Use the repository's existing Spec Kit conventions and active feature state.

Read the relevant artifacts when present:

```text
constitution.md
spec.md
plan.md
tasks.md
research.md
data-model.md
contracts/
quickstart.md
checklists/
```

Also inspect the relevant implementation files.

Do not read the entire repository unnecessarily.

Start with the files directly related to the questioned implementation and expand only when necessary.

---

# Decision Classification

Every questioned implementation decision must be classified as one of:

### 1. Explicit Requirement

The specification directly requires the behavior.

Example:

```text
spec.md

FR-003:
Invalid bundle definitions MUST NOT enter processing state.
```

### 2. Derived Design Decision

The specification requires a behavior, while `plan.md` defines how it should be implemented.

Example:

```text
spec.md:
Invalid definitions must be rejected.

plan.md:
Validation occurs before state mutation.
```

### 3. Task Requirement

The implementation is explicitly requested by `tasks.md`.

Example:

```text
tasks.md:

T014:
Implement bundle definition validation.
```

### 4. Implementation Choice

The implementation detail was chosen by the agent but is not explicitly required by the specification, plan, or task.

Example:

```text
validateBundleDefinition()
```

The behavior may be required, but the method itself is not.

### 5. Unsupported Decision

No requirement, plan item, task, contract, or other artifact can be found that justifies the implementation.

In this case, explicitly state:

> No supporting artifact was found for this implementation decision.

Never invent a justification.

---

# Evidence-First Behavior

When the user asks:

```text
"Where is this required?"
```

respond with evidence before proposing a change.

Use this structure:

```text
Implementation
<file/class/method>

Source
<artifact>

Reference
<section / requirement / task>

Requirement
<what the artifact says>

Relationship
<why the implementation was derived from it>

Conclusion
<required behavior / implementation choice / unsupported>
```

Example:

```text
Implementation:
BundleDefinitionValidationOperator.validate()

Source:
spec.md

Reference:
Functional Requirements → FR-003

Requirement:
Invalid bundle definitions must not enter processing state.

Plan reference:
plan.md → Processing Flow → Validation

Task reference:
tasks.md → T014

Conclusion:
The validation behavior is required.

The specific `validate()` method is NOT explicitly required.
It is an implementation choice.
```

---

# Never Confuse Behavior With Implementation

This is one of the most important rules.

If the user says:

```text
"I don't want this method."
```

Do not conclude:

```text
The validation requirement should be removed.
```

Instead determine:

```text
Method:
    Implementation choice

Behavior:
    Required by specification
```

The correct response is:

```text
The method itself is not required.

The behavior it provides is required by FR-003.

Removing the method is possible, but the required behavior
must still be provided elsewhere.
```

---

# User Approval Gate

**Do not make changes simply because the user questioned something.**

Questioning is not approval.

The following are examples of questions:

```text
"Why is this here?"

"Do we really need this?"

"Where did this come from?"

"I don't like this implementation."
```

These require investigation and explanation.

They do NOT authorize changes.

Explicit approval includes statements such as:

```text
"Remove it."

"Change it."

"Update the plan."

"Yes, do it."

"Go ahead."

"Use the other approach."
```

When approval is unclear, ask for confirmation.

---

# Implementation Review Workflow

When the user challenges an implementation:

## Step 1 — Identify

Identify the exact implementation element being challenged.

Record:

* file
* class
* method
* field
* API
* event
* state
* dependency
* architecture decision

---

## Step 2 — Trace

Search the Spec Kit artifacts for the implementation element and the behavior it provides.

Trace:

```text
Implementation
→ tasks.md
→ plan.md
→ spec.md
```

Also inspect contracts and supporting artifacts where relevant.

---

## Step 3 — Classify

Determine whether the implementation is:

```text
Explicit requirement
Derived design decision
Task requirement
Implementation choice
Unsupported decision
```

---

## Step 4 — Explain

Explain exactly what required it.

Do not use vague statements such as:

```text
"The plan required this."
```

Instead provide:

```text
plan.md
Section: Processing Flow
Reference: Validation stage

The plan specifies that validation occurs before state mutation.
```

---

## Step 5 — Explain Impact

If the user wants it removed or changed, determine what would be affected.

Inspect:

```text
spec.md
plan.md
tasks.md
contracts/
implementation
tests
```

Explain:

```text
What can be removed safely
What requirement must remain
What artifacts become stale
What implementation must change
What tests are affected
```

---

## Step 6 — Ask for Approval

Before making changes that affect artifacts or behavior, obtain explicit approval.

If appropriate, present the available approaches.

Example:

```text
The method itself is not required by the specification.

The required behavior is bundle validation.

Removing this method would require moving the validation behavior
to another component.

I can:

1. Remove the method and move validation into the operator.
2. Keep the validation behavior but use another abstraction.
3. Keep the current implementation.

Which approach should I apply?
```

Do not change anything until the user selects an approach.

---

# Refactoring Spec Kit Artifacts

After explicit approval, update only the artifacts affected by the decision.

## spec.md

Treat `spec.md` as the behavioral contract.

Do NOT change it merely because the implementation changed.

Only update `spec.md` when the user explicitly changes the intended behavior or requirement.

Example:

```text
User:
"We don't need bundle validation anymore."

Before changing spec.md:

Explain:

Current requirement:
FR-003

Affected plan:
Validation stage

Affected tasks:
T014

Affected implementation:
BundleDefinitionValidationOperator

Then request confirmation.
```

If the user confirms, update the specification and its downstream artifacts.

---

## plan.md

`plan.md` describes the technical approach.

Update it when the user approves a different technical approach.

Examples:

* remove a component
* change component responsibility
* change state management
* change event flow
* change validation location
* change persistence approach
* change dependency
* change API design

The plan should describe the approved approach, not an abandoned one.

---

## tasks.md

Update tasks when their implementation approach changes.

Examples:

```text
Old:

T014 Implement BundleDefinitionValidationOperator.

New:

T014 Implement bundle definition validation within the
ingestion operator.
```

Remove tasks that are genuinely obsolete.

Do not remove a task merely because the implementation has already completed it unless that matches the repository's task-management convention.

Preserve incomplete work.

---

## Contracts

Update contracts only when the approved change affects:

* API contracts
* event contracts
* schemas
* input/output behavior
* serialization
* external interfaces

Do not modify contracts simply to make implementation look correct.

---

# Requirement Changes

If the user explicitly changes a requirement:

```text
Old requirement
      ↓
User decision
      ↓
Update spec.md
      ↓
Update plan.md
      ↓
Update tasks.md
      ↓
Identify affected implementation
```

Do not silently rewrite downstream artifacts without checking their dependency on the changed requirement.

---

# Implementation Changes

This skill may help coordinate implementation changes after user approval.

However:

**Do not change production code merely because a user questioned it.**

First establish:

```text
What is wrong?
Why is it wrong?
What requirement should remain?
What implementation should replace it?
```

Then obtain approval.

When implementation changes are approved, ensure the Spec Kit artifacts are updated consistently.

---

# Unsupported Implementation

If no artifact supports the questioned implementation, explicitly say:

```text
I could not find this requirement in:

- spec.md
- plan.md
- tasks.md
- contracts
- supporting artifacts

This appears to be an implementation decision introduced during
implementation rather than a requirement from the Spec Kit artifacts.
```

Do not manufacture a reason.

This is especially important for:

* helper methods
* abstractions
* classes
* interfaces
* design patterns
* additional validation
* retries
* caching
* state
* configuration
* dependencies
* abstractions that were not requested

---

# Detecting Over-Implementation

When reviewing the implementation, look for functionality that goes beyond the artifacts.

Examples:

```text
Specification:
Validate bundle ID.

Implementation:
Validate bundle ID
+ validate product IDs
+ validate store IDs
+ normalize names
+ cache definitions
+ retry validation
```

Determine which behaviors are:

```text
Required
Derived
Implementation choice
Unsupported
```

Do not assume additional behavior is desirable.

Report it to the user.

---

# Detecting Missing Implementation

Also identify behavior required by the artifacts but absent from implementation.

For example:

```text
Required:
Invalid events must be routed to a structured side output.

Implementation:
Invalid events are simply dropped.
```

Report this as a requirement/implementation mismatch.

Do not silently rewrite the specification to match the implementation.

---

# Artifact Consistency

After an approved change, verify:

```text
spec.md
   ↓
plan.md
   ↓
tasks.md
   ↓
implementation
```

Check for:

* stale references
* obsolete classes
* obsolete methods
* obsolete tasks
* outdated architecture
* outdated contracts
* contradictory requirements
* missing tasks
* unsupported implementation decisions
* duplicate requirements
* incorrect component names
* incorrect event names
* incorrect data flow

---

# Minimal Change Principle

Make the smallest artifact changes necessary.

Do not regenerate every artifact simply because one requirement changed.

Do not rewrite large portions of `spec.md`, `plan.md`, or `tasks.md` when a small targeted change is sufficient.

Preserve unrelated content.

---

# No Silent Scope Expansion

Do not introduce additional requirements, features, architecture, or cleanup while performing a refactor.

If you discover something unrelated:

```text
Unrelated finding:
<finding>

Do not modify it automatically.
```

Only address it if the user explicitly asks.

---

# Final Response After Approved Changes

After changes are made, report:

```text
## Changed

- <artifact>
- <artifact>

## Removed

- <obsolete item>

## Preserved

- <requirement that remains unchanged>

## Implementation Impact

- <implementation change>

## Validation

- spec.md ✓
- plan.md ✓
- tasks.md ✓
- contracts ✓
```

Keep the report concise.

---

# Operating Principle

This skill is a **reviewer and traceability tool**, not an autonomous architect.

Its behavior must follow:

```text
User questions implementation
          ↓
Investigate
          ↓
Trace to Spec Kit artifacts
          ↓
Show exact references
          ↓
Distinguish requirement from implementation choice
          ↓
Explain impact
          ↓
Get explicit approval
          ↓
Apply approved change
          ↓
Synchronize artifacts
          ↓
Validate consistency
```

Never follow this flow:

```text
User questions implementation
          ↓
Agent assumes implementation is wrong
          ↓
Agent changes spec
          ↓
Agent changes plan
          ↓
Agent changes tasks
```

The user's intent controls changes.

The existing Spec Kit artifacts provide evidence.

The implementation provides the current reality.

The skill's responsibility is to keep these three aligned **only after the user has decided what the desired behavior should be**.
