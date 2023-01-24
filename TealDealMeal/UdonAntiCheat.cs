using UnityEngine;
using UdonSharp;
using VRC.SDKBase;

[UdonBehaviourSyncMode(BehaviourSyncMode.None)]
public class UdonAntiCheat : UdonSharpBehaviour {
    [Tooltip("Use the maximum movement speed multiplied by 2")]
    public float speedLimit = 8;
    
    VRCPlayerApi localPlayer;
    byte limitCounter;

    void Start() {
        localPlayer = Networking.LocalPlayer;
    }

    void FixedUpdate() {
        Vector3 pos = localPlayer.GetVelocity();
        pos.y = 0;
        float speed = pos.magnitude;

        if (speed > speedLimit) {
            if (limitCounter == 3) {
                limitCounter = 0;
                localPlayer.Respawn();
                return;
            }

            limitCounter++;
        } else
            limitCounter = 0;
    }
}