using System.Collections;
using UnityEngine;

public class Retroceso : MonoBehaviour
{
    public float recoilStrength = 0.25f; // Strength of the recoil
    public float recoilTime = 0.02f; // Duration of the recoil
    public float returnTime = 0.05f; // Duration of the return to the original position

    private Vector3 originalPosition; // Store the original position of the object
    private bool isRecoiling = false; // Track if the object is currently recoiling

    // Optional: Define a custom direction for recoil (e.g., backward along the Z-axis)
    public Vector3 recoilDirection = -Vector3.forward;

    public void AplicarRetroceso(GameObject objeto)
    {
        if (isRecoiling) return; // Prevent multiple recoils at the same time
        
        // Store the original position of the object
        originalPosition = objeto.transform.localPosition;

        // Start the recoil coroutine
        StartCoroutine(RecoilAndReturn(objeto));
    }

    private IEnumerator RecoilAndReturn(GameObject objeto)
    {
        isRecoiling = true; // Set the recoiling flag to true

        // Calculate the recoil target position (slightly back in the specified direction)
        Vector3 recoilPosition = originalPosition + recoilDirection.normalized * recoilStrength;

        // Smoothly move the object to the recoil position
        float elapsedTime = 0f;
        while (elapsedTime < recoilTime)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / recoilTime);
            objeto.transform.localPosition = Vector3.Lerp(originalPosition, recoilPosition, t);
            yield return null;
        }

        // Smoothly move the object back to the original position
        elapsedTime = 0f;
        while (elapsedTime < returnTime)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / returnTime);
            objeto.transform.localPosition = Vector3.Lerp(recoilPosition, originalPosition, t);
            yield return null;
        }

        // Ensure the object is exactly at the original position
        objeto.transform.localPosition = originalPosition;

        isRecoiling = false; // Reset the recoiling flag
    }
}