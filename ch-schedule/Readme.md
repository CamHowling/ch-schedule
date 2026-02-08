# Initial thoughts
My inital reaction was that this brief speaks to tradesoffs between performance vs maintainability, readability, and speed of development.
The way that I would typically approach a problem like this is to focus on maintainability, readability, and speed of development unless there was an existing business need to prioritise performance.
Later on, if performance was a problem I'd update my solution to be more performant.

# Approach 1 - Combined Schedule
### Core Idea
Take N attendee schedules and convert them to a overall schedule that represents a collection of times that one or more attendees is unavailable.
Use this new combined schedule to find the first available interval if any.

### Bottlenecks
Sorting the combined schedule is the main bottle neck. The sorting algorithm would have complexity: O(n log n)
This is CPU bound.

### Strength
The key strength is that this approach is intuitive, and lends itself to a readable LINQ base solution.
This results in future work being more accessible and maintainable.

### Downside
As this approach combines all schedules, we'll be evaluating every single schedule busy interval until the first available time slot.

# Approach 2 - Parallel stepping
### Core Idea
Take N attendee schedules, and normalize them. (do not combine them)
Traverse the attendee schedules together stepping to the next available opening when an attendee is busy, to check if everyone else is available.

### Bottlenecks
The main bottleneck with this solution is the complexity required by the developers implementing, reviewing, and maintaining this solution.
While some may not consider this a bottleneck, this type of bottleneck for complex code is usually the most significant one in my experience, blowing trivial work out over longer periods.

### Strength
As we're stepping across the schedules of all attendees, unnecessary evaluations are avoided.

### Downside
The major downside to this approach is that it is harder to work with. 
Its less readable and its more difficult to build

# Selected solution
Approach 1

# Assumptions
1. The typical number of attendees ranges from 1-100.
2. The hardware running this software is appropriately modern.
3. Given 1 and 2 we can expect reasonable compute times.

Given the nature of how these endpoints would likely be used by a typical another application or service, 
I have implemented the Get method as a HttpPost/Post method to meet http standards when using FromBody

# Exclusions
- Persistance - I wont be attaching anything to a real database.
- Feature flag
- Unit testing

# Revisiting and scaling
The main opportunity to revisit and scale this is to move to solution #2.
In order to support this, I'd take advantage of unit testing practices and feature flags.
Using a feature flag, I'd enable the new performant parallel stepping approach.
This enables quick rollback in production without a code change, and progressive rollout.
Additionally I'd like to add unit testing, I ran out of time to get it added/working, so scrapped it to ensure this could be completed.

# Postman testing body

Post: https://localhost:44359/availability

```
{
  "attendeeIds": [
    "11111111-1111-1111-1111-111111111111",
    "22222222-2222-2222-2222-222222222222"
  ],
  "start": "2026-01-01T08:30:00+00:00",
  "end": "2026-01-01T10:30:00+00:00",
  "duration": "00:30:00"
}
```

Put: https://localhost:44359/calendars/11111111-1111-1111-1111-111111111111/busy

```
{
  "attendeeId": "11111111-1111-1111-1111-111111111111",
  "start": "2026-01-01T08:30:00+00:00",
  "end": "2026-01-01T10:00:00+00:00"
}
```