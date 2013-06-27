using UnityEngine;
using System.Collections;

namespace RTS {
    public static class ResourceManager {

        public static float ScrollSpeed { get { return 25; } }
        public static float RotateSpeed { get { return 100; } }
        public static float RotateAmount { get { return 10; } }

        // Width from the edge of screen where we can start scrolling
        public static int ScrollWidth { get { return 100; } }

        // Limit the user going too low or too high with the camera
        public static float MinCameraHeight { get { return 10; } }
        public static float MaxCameraHeight { get { return 40; } }
    }
}
