using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileAnimator : MonoBehaviour
{
    static MissileAnimator singleton;

    [SerializeField]
    GameObject UpPrefab;

    [SerializeField]
    GameObject DownPrefab;

    private MissileAnimatorState state = MissileAnimatorState.Open;

    enum MissileAnimatorState { Animating, Open };

    private void Awake()
    {
        singleton = this;
    }

    public static bool IsAnimating()
    {
        return singleton.state == MissileAnimatorState.Animating;
    }

    public static void LaunchMissile(Vector3 location)
    {
        singleton.StartCoroutine(singleton.ManageMissileLaunch(location));
    }

    private IEnumerator ManageMissileLaunch(Vector3 location)
    {
        while (state != MissileAnimatorState.Open)
            yield return null;
        state = MissileAnimatorState.Animating;

        Transform missile = Instantiate(UpPrefab, location, Quaternion.identity, transform).transform;

        float time = 0;

        float goalTime = 2f;

        while(time <= goalTime)
        {
            time += Time.deltaTime;
            missile.position += Vector3.up * Time.deltaTime * goalTime * 4f;
            yield return null;
        }

        Destroy(missile.gameObject);

        state = MissileAnimatorState.Open;
    }

    public static void LandMissile(Vector3 location)
    {
        singleton.StartCoroutine(singleton.ManageMissileLand(location));
    }

    private IEnumerator ManageMissileLand(Vector3 location)
    {
        while (state != MissileAnimatorState.Open)
            yield return null;
        state = MissileAnimatorState.Animating;

        Transform missile = Instantiate(UpPrefab, location + Vector3.up * 8f, Quaternion.identity, transform).transform;

        float time = 0;

        float goalTime = 2f;

        while (time <= goalTime)
        {
            time += Time.deltaTime;
            missile.position += Vector3.down * Time.deltaTime * goalTime * 4f;
            yield return null;
        }

        Destroy(missile.gameObject);

        state = MissileAnimatorState.Open;
    }
}
