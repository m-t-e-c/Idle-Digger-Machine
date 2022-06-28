using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lofelt.NiceVibrations;

public class HapticControl : MonoBehaviour
{
    [Header("===Haptics===")]
    public HapticClip[] hit;
    public HapticClip button;
    public HapticClip insufficient;
    public HapticClip grunting;
    public HapticClip done;
    public HapticClip enteredRing;
    public HapticClip alert;
    public HapticClip win;
    public HapticClip subtle;

    //bool hapticsSupported = DeviceCapabilities.meetsAdvancedRequirements;

    public void ButtonPressed() => HapticController.Play(button);
    public void InsufficientMoney()  => HapticController.Play(insufficient);
    public void Grunt() => HapticController.Play(grunting);
    public void PlayerHit() => HapticController.Play(hit[Random.Range(0,hit.Length-1)]);
    public void EnterSomething() => HapticController.Play(enteredRing);
    public void DidSomething() => HapticController.Play(done);
    public void Won() => HapticController.Play(win);
    public void Lose() => HapticController.Play(alert);
    public void ButtonSubtle() => HapticController.Play(subtle);
    public void PlayCont(float x, float y) => HapticPatterns.PlayEmphasis(x, y);
}
