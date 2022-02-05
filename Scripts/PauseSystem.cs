using UnityEngine;
using System.Collections;
public class PauseSystem : MonoBehaviour
{
    public static bool IsPause { get; set; }

    private float bufferTimeScale;

    public void Pause()
    {
        IsPause = true;
        bufferTimeScale = Time.timeScale;
        Time.timeScale = 0.001f;
    }

    public void Unpause()
    {
        IsPause = false;
        StartCoroutine(SmoothUnpause());
    }

    IEnumerator SmoothUnpause()
    {
        Time.timeScale = 0.1f;
        for(int i = 0; Time.timeScale < bufferTimeScale; i++)
        {
            yield return new WaitForSecondsRealtime(0.1f);
            Time.timeScale += 0.03f;
        }
    }

    private class CubeParams
    {
        public Vector2 velocity;
        public float rotationSpeed;
    }
}
