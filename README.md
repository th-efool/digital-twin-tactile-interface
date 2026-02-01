
# **TACTIX — Modular Soft-Tactile Grid Interface for Digital Twin Control**

![Unity](https://img.shields.io/badge/Engine-Unity-black?logo=unity)
![Arduino](https://img.shields.io/badge/Hardware-Arduino-00979D?logo=arduino)
![AR Foundation](https://img.shields.io/badge/AR-AR%20Foundation-blueviolet)
![Digital Twin](https://img.shields.io/badge/Architecture-Digital%20Twin-blue)
![Robotics](https://img.shields.io/badge/Domain-Robotics%20Control-darkgreen)
![Sensors](https://img.shields.io/badge/Input-Piezo%20Sensors-orange)
![Serial](https://img.shields.io/badge/Comm-Serial%20USB-lightgrey)
![Platform](https://img.shields.io/badge/Platform-Android%20%7C%20PC-informational)
![Status](https://img.shields.io/badge/Status-Prototype%20%2F%20Research-yellow)

---

## **Overview**

**TACTIX** is a **modular soft-tactile input system** that converts a physical **n×n piezoelectric grid** into a **spatial–temporal command language** for controlling a **Unity-based Digital Twin** in real time.

Each tap on the soft sensor surface is streamed into the runtime as a **deterministic, intensity-aware signal**, where **location, timing, and sequence** jointly encode operator intent. The Digital Twin interprets these signals as structured commands and executes them through a simulated robotic agent.

The system is designed as a **research platform** for:

* Human–machine interaction
* Robotic command abstraction
* Digital Twin validation
* Soft-body electronic interfaces

Rather than replacing traditional controllers, TACTIX explores **pre-UI control**—interaction paradigms that function without screens, speech, or complex mechanical input devices.

---

## **Core Concept**

Conventional input systems collapse intent into isolated triggers (buttons, axes, gestures).
TACTIX instead treats **space + sequence + timing** as the **primitive vocabulary** of control.

* **Spatial position** selects intent
* **Temporal patterns** encode meaning
* **Tap intensity** provides modulation
* **Sequences** form symbolic commands

The result is an input surface that behaves less like a keypad and more like a **programmable tactile language**.

---

## **High-Level Architecture**

```text
Soft Tactile Piezo Grid (n×n)
        │
        ▼
Microcontroller (Arduino)
  - Analog spike detection
  - Thresholding & debouncing
  - Sensor index encoding
        │
        ▼
Serial Transport (USB)
        │
        ▼
Unity Runtime
  - PiezoSerialReceiver (I/O bridge)
  - GridMaster (logic + validation)
  - Digital Twin Robot (execution)
```

The hardware layer remains intentionally minimal and deterministic. All semantic interpretation occurs in software, preserving transparency and debuggability.

---

## **Hardware Integration (Piezo Grid → Runtime)**

The physical input layer consists of an Arduino-driven piezo sensor array, where each piezo element maps directly to a logical grid cell inside the Digital Twin.

### **Hardware Responsibilities**

* Each piezo sensor is connected to a dedicated analog input channel
* Physical taps generate analog voltage spikes proportional to impact intensity
* The microcontroller performs:

  * Noise rejection
  * Thresholding
  * Debouncing
* Valid taps are encoded as **integer grid indices** in the range:

```
0 … (n × n − 1)
```

The microcontroller does **not** infer motion, intent, or behavior.
It functions purely as a **deterministic signal encoder**, ensuring that higher-level interpretation remains explicit and reproducible.

---

## **Software Components**

### **1. GridMaster — Control & Validation Core**

The `GridMaster` acts as the authoritative logic layer for the Digital Twin.

**Responsibilities**

* Dynamic spawning of an **n×n grid**
* Spatial layout computation (padding, scale, offsets)
* Validation of movement constraints (adjacent / diagonal)
* Orchestration of robot movement and orientation
* Visual state feedback for valid, invalid, and active tiles

**Key Properties**

* Parameterized grid size (`Length`)
* Configurable spacing and padding
* Smooth interpolation with rotation alignment
* Material-based visual feedback and pulse animation
* Centralized access for hardware-driven input routing

---

### **2. GridTile — Per-Cell Interaction Node**

Each grid cell is represented by an autonomous `GridTile`.

Each tile:

* Stores its immutable grid index
* Reports activation events to `GridMaster`
* Supports both **software input** (mouse / touch) and **hardware input**

This dual-path design enables:

* Software-only testing
* Hardware-in-the-loop experimentation
* Seamless fallback between interaction modes

---

### **3. PiezoSerialReceiver — Physical–Digital Boundary**

The `PiezoSerialReceiver` forms the interface between physical sensing and digital control.

**Responsibilities**

* Establishes serial communication with the microcontroller
* Parses incoming sensor identifiers
* Accepts multiple serial formats (e.g. `"2"`, `"SENSOR,2"`)
* Validates indices against the active grid size
* Dispatches real-time movement triggers to the Digital Twin

**Design Constraints**

* Non-blocking serial reads
* Robust handling of malformed or noisy input
* Hardware-agnostic beyond index semantics

---

### **4. AR Grid Placement (Optional Module)**

For AR-enabled builds, the grid can be spatially anchored to the real world.

Capabilities include:

* Plane detection–based placement
* Scale and rotation calibration
* Explicit placement confirmation
* Post-calibration plane suppression

This allows **physical input surfaces**, **virtual grids**, and the **real environment** to align spatially.

---

### **5. Calibration & UI Layer**

The calibration layer provides:

* Rotation controls
* Scale adjustment
* Placement confirmation
* Clean transition into a locked interaction state

Calibration is deliberately isolated from interaction logic to preserve system clarity.

---

## **Interaction Model**

1. A physical tap occurs on the piezo grid
2. The tap is encoded as a grid index
3. The index maps to a logical position
4. Movement rules enforce local constraints
5. The Digital Twin animates the transition
6. Visual feedback confirms system state

Because the input is symbolic rather than continuous, the same structure naturally extends to:

* Tap-timing patterns
* Gesture sequences
* Morse-like encodings
* Multi-step command execution

---

## **Why This System Matters**

TACTIX is **not** a UI experiment.

It is:

* A **soft-tactile input language**
* A bridge between **physical sensing** and **virtual embodiment**
* A control abstraction suitable for **robotics and Digital Twins**
* A testbed for interpretable human–machine interaction

The grid scales, the encoding deepens, and the system remains **explicit, inspectable, and extensible**.

---

## **Extension Pathways**

* Tap intensity → velocity or command priority
* Sequence buffers for symbolic macros
* Multi-agent Digital Twin routing
* Tile-triggered inverse kinematics
* Learning-based gesture classification
* Network-synchronized Digital Twin instances

---

## **Intended Applications**

* Robotics research and prototyping
* Human–machine interaction studies
* Digital Twin control validation
* Educational robotics platforms
* AR-assisted physical control interfaces

