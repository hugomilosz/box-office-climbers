# Box Office Climbers

> **Note:** WIP. The core mechanics are functional, but the project is under development.

A 2D game prototype built in Unity where the player must guess if a movie's box office gross was higher or lower than a previous one.

<img src="https://github.com/user-attachments/assets/6e7e17ae-eb9e-4508-b7d6-8d1badfe89fc" alt="Demo" width="700">

---
## Gameplay

The gameplay loop is simple:
- A movie and its box office gross are displayed.
- A second movie is shown, but its gross is hidden.
- The player chooses "Higher" or "Lower".
- A correct guess increases the streak, and the character moves to the next procedurally generated platform. An incorrect guess ends the run.

---
## Current Features

- **Procedural Level Generation**: A single, infinite path is generated piece by piece as the player progresses.
- **Adaptive Timer**: A countdown timer for each choice gets progressively shorter as the player's streak increases.
- **Game Modes**: Choice between "Worldwide" or "Domestic" box office data.

---
## Tech Stack

- **Engine**: Unity
- **Language**: C#
- **Data Sourcing**: The movie box office data used in this game was collected from Box Office Mojo using a custom web scraper, which is also available on my GitHub: **https://github.com/hugomilosz/movie-web-scraper**
