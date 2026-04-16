using UnityEngine;
using System.Collections;

public class Repeater : Peashooter
{
    protected override void Shoot()
    {
        StartCoroutine(DoubleShot());
    }

    IEnumerator DoubleShot()
    {
        FireOnce();

        yield return new WaitForSeconds(0.15f);

        FireOnce();
    }

    void FireOnce()
    {
        GameObject proj = Instantiate(projectilePrefab);
        proj.transform.position = transform.position + Vector3.right * 0.5f;
    }
}