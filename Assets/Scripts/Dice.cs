using System.Collections;
using UnityEngine;
using System;

public class Dice : MonoBehaviour
{
    public int lastRoll = 1;
    public Animator animator; // optional: set an Animator to play roll animation

    public int Roll()
    {
        int val = UnityEngine.Random.Range(1, 7);
        lastRoll = val;
        if (animator != null) animator.SetTrigger("Roll");
        return val;
    }

    public IEnumerator RollWithDelay(Action<int> onComplete, float delay = 1f)
    {
        if (animator != null) animator.SetTrigger("Roll");
        yield return new WaitForSeconds(delay);
        int val = Roll();
        onComplete?.Invoke(val);
    }
}
