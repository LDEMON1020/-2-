using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinning : MonoBehaviour
{
    public GameObject iceRune2;
    public float rotationSpeed = 5f;

    public PlayerShooting playerShooting;

    public float fadeDuration = 2f; // ������ ��Ÿ���� �ð� (��)
    private Material material1;
    private Material material2;
    private Color originalColor1;
    private Color originalColor2;
    public bool isFading = false;
    private bool isVisible = false;
    private float fadeTimer = 0f;

    void Start()
    {
        material1 = GetComponent<Renderer>().material;
        originalColor1 = material1.color;

        Color startColor = originalColor1;
        startColor.a = 0f;
        material1.color = startColor;

        if (iceRune2 != null)
        {
            material2 = iceRune2.GetComponent<Renderer>().material;
            originalColor2 = material2.color;
            Color startColor2 = originalColor2;
            startColor2.a = 0f;
            material2.color = startColor2;
        }
    }
    void Update()
    {
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);

        iceRune2.transform.Rotate(0, 0, -rotationSpeed * Time.deltaTime);

        if (isFading == false && Input.GetKeyDown(KeyCode.Z))
        {
            playerShooting.attackMode = !playerShooting.attackMode;
            isFading = true;
            fadeTimer = 0f;
        }
        

        if (isFading)
        {
            fadeTimer += Time.deltaTime;
            float t = Mathf.Clamp01(fadeTimer / fadeDuration);

            // ���̵� �� or �ƿ�
            float alpha = isVisible ? Mathf.Lerp(1f, 0f, t) : Mathf.Lerp(0f, 1f, t);

            // ù ��° ������Ʈ
            Color newColor1 = originalColor1;
            newColor1.a = alpha;
            material1.color = newColor1;

            // �� ��° ������Ʈ
            if (material2 != null)
            {
                Color newColor2 = originalColor2;
                newColor2.a = alpha;
                material2.color = newColor2;
            }

            // �Ϸ� �� ó��
            if (t >= 1f)
            {
                isFading = false;
                isVisible = !isVisible; // ���� ���� (���̰� �� �� ���̰�)
            }
        }
    }
}