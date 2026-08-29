# Overview

## Purpose

Create a concise, implementation-grounded overview of a system or feature.

This skill is **read-only**. It must not modify source code, specifications, plans, tasks, or configuration.

The goal is to answer:

> **What is implemented, how is it structured, and how does data flow through it?**

The implementation is the primary source of truth. Specifications are used only to understand context and intent.

---

## Workflow

### 1. Understand the User's Scope

Determine what the user wants an overview of.

Examples:

```text
/overview

/overview bundle ingestion

/overview the feature I just implemented

/overview how this Flink pipeline works
```

If the user provides a specific feature or component, restrict the investigation to that scope.

If no scope is provided, use the most recent relevant implementation changes.

---

### 2. Inspect Changes First

Before reading specifications, inspect the current repository state.

Start with:

```bash
git status --short
git diff --stat
```

If necessary:

```bash
git diff
git log --oneline -n 10
```

Do not read the entire diff if it is large.

First identify:

* changed files
* new files
* affected modules/packages
* relevant classes
* relevant tests

Then inspect only the implementation files related to the requested scope.

---

### 3. Read Relevant Specifications

After identifying the implementation, locate the related Spec Kit files.

Typical locations:

```text
specs/
spec/
memory/
```

Look for relevant:

```text
spec.md
plan.md
tasks.md
```

Do **not** read all specifications.

Only read the specification files related to the implementation being described.

Use the specification to understand:

* terminology
* intended boundaries
* relevant requirements
* relationships between components

Do not treat the specification as proof that something is implemented.

---

### 4. Build the Overview

Produce only these three sections.

# Overview

Briefly explain:

* what the system/feature does
* its purpose
* the main responsibility of the implementation

Keep this section short.

# Architecture

Describe the major components and how they relate to each other.

Use an ASCII diagram when it improves understanding.

Example:

```text
Kafka
  │
  ▼
Bundle Source
  │
  ▼
Validation
  │
  ▼
Bundle Processor
  │
  ├──► Flink State
  │
  ▼
Kafka Sink
```

Then briefly explain the important components.

Focus on actual implementation.

For example:

```text
BundleSource
  Reads bundle definition events from Kafka.

BundleValidator
  Validates incoming definitions before processing.

BundleProcessor
  Updates keyed Flink state and produces the resulting event.

BundleSink
  Publishes processed bundle events.
```

Do not document every class. Only include components necessary to understand the architecture.

# Data Flow

Explain the lifecycle of data through the system.

For example:

```text
Event
  ↓
Kafka Topic
  ↓
Flink Source
  ↓
Validation
  ↓
Keying / Partitioning
  ↓
Stateful Processing
  ↓
Output
  ↓
Kafka Topic
```

Explain the important behavior at each stage.

For Flink systems, pay particular attention to:

* Kafka source
* event key
* partitioning
* operators
* validation
* keyed state
* state updates
* side outputs
* timers
* sinks
* checkpoint/recovery behavior

Only mention behavior that can be established from the implementation.

---

## Implementation vs Specification

When the implementation differs materially from the specification, describe the **implemented behavior**.

Do not turn the overview into a code review.

Do not:

* suggest refactoring
* create tasks
* criticize implementation
* modify code
* modify specs
* invent design rationale

The purpose is simply to accurately describe the current design.

---

## Token Efficiency

Minimize context consumption.

Use this investigation order:

```text
User request
    ↓
Git changes
    ↓
Relevant implementation files
    ↓
Relevant specification
    ↓
Overview
```

Never:

* scan the entire repository
* read every spec
* read unrelated tasks
* read unrelated source files
* dump large files into context unnecessarily

Prefer targeted searches such as:

```bash
rg "FeatureName|ClassName" .
```

and inspect only the relevant sections.

If the implementation is clear enough to explain the architecture, stop investigating.

---

## Output Rules

The final response must contain only:

```text
# Overview

...

# Architecture

...

# Data Flow

...
```

Keep it concise, technical, and implementation-focused.

Use diagrams where useful.

Do not add sections such as:

* Components
* State & Persistence
* External Dependencies
* Design Decisions
* Specification Alignment
* Files
* Recommendations
* Risks
* Review
