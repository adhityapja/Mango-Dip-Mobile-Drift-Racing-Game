# 🏎️ Mango Dip - Mobile Drift Racing Game

[![Unity](https://img.shields.io/badge/Unity-2022%2B-blue.svg?logo=unity)](https://unity.com/)
[![Platform](https://img.shields.io/badge/Platform-Android-green.svg?logo=android)](https://www.android.com/)
[![License](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE)

> **Mango Dip** is an adrenaline-pumping 3D car-chase and drift survival game built with Unity. Drive a high-speed drifting car, outrun an unrelenting giant villain, and scavenge 10 hidden coins scattered across a sprawling low-poly city without a map or GPS!

---

## 🎮 Game Overview

- **Objective:** Locate and collect all **10 hidden coins** across the city while surviving the chase.
- **The Threat:** A giant villain is constantly hunting you down. Keep moving, drift through corners, and avoid being crushed!
- **Survival:** You only have **3 lives**. One wrong turn or collision with the villain could end your run.
- **No Map, No GPS:** Pure navigation instincts, city landmark recognition, and drift mastery.

---

## ✨ Key Features

- **Drift Physics:** Powered by custom vehicle tuning and the Prometeo Car Controller for tight corners and high-speed slides.
- **Mobile Touch Controls:** On-screen touch buttons designed specifically for responsive Android gameplay (`MobileCarButton`).
- **Villain AI Chase System:** Dynamic pursuit behavior (`VillainChase`) and directional threat indicators (`VillainPointer`).
- **Vibrant Low-Poly World:** Rich 3D city environment featuring urban roads, buildings, water towers, and obstacles (`SimplePoly City`).
- **Health & Life Management:** 3-heart survival system tracking player damage and game over states (`PlayerHealth`, `GameOverManager`).
- **Ready-to-Play Android APK:** Includes pre-compiled `MangoDip.apk` for immediate installation on Android devices.

---

## 🕹️ Controls (Mobile)

| Control | Action |
| :--- | :--- |
| **Accelerate** | Drive forward and build speed |
| **Brake / Reverse** | Slow down or back out of tight spots |
| **Steer Left / Right** | Turn and initiate drifts around street corners |
| **Handbrake** | Trigger high-angle power slides |

---

## 📱 How to Install the APK

1. Download **`MangoDip.apk`** from the root of this repository (or from GitHub Releases).
2. Transfer the APK to your Android device (or download directly on mobile).
3. If prompted, enable **"Install unknown apps"** in your Android device settings.
4. Tap the APK file to install and launch **Mango Dip**!

---

## 🛠️ Project Setup (Unity Editor)

To open and modify the project in Unity:

1. **Clone the repository:**
   ```bash
   git clone https://github.com/adhityapja/Mango-Dip-Mobile-Drift-Racing-Game.git
   ```
2. **Open with Unity Hub:**
   - Launch Unity Hub.
   - Click **Add** -> **Add project from disk**.
   - Select the `Mango-Dip-Mobile-Drift-Racing-Game` root folder.
   - Open using Unity 2022 LTS or newer.
3. **Open the Main Scene:**
   - Navigate to `Assets/Scenes/` in the Project window and open the starting scene (e.g. `Home` or `City`).
4. **Build for Android:**
   - Go to `File` -> `Build Settings...`
   - Switch platform to **Android**.
   - Click **Build** or **Build and Run**.

---

## 📂 Project Structure

```text
Mango Dip/
├── Assets/
│   ├── Character/               # Giant villain 3D models & animations
│   ├── PROMETEO - Car Controller/ # Car dynamics, audio, and physics
│   ├── Scenes/                  # Game levels (Menu, City, Game Over)
│   ├── Scripts/                 # Core gameplay & manager scripts
│   ├── SimplePoly City/         # Low-poly city environment modular assets
│   ├── UI/                      # Touch UI, buttons, and HUD elements
│   └── WhiteTigerArt/           # Textures & props
├── Packages/                    # Unity Package Manager manifest & dependencies
├── ProjectSettings/             # Project input, tag, audio, and quality settings
├── MangoDip.apk                 # Compiled Android build (ready to play)
└── .gitignore                   # Unity-optimized ignore file
```

---

## 👨‍💻 Author

Developed by **[adhityapja](https://github.com/adhityapja)**.
Feel free to open issues or contribute pull requests!
