using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] private float controlSpeed = 30f;
    [SerializeField] private float xRange = 10f;
    [SerializeField] private float yRange = 7f;

    [SerializeField] private float positionPitchFactor = -2f;
    [SerializeField] private float controlPitchFactor = -15f;
    [SerializeField] private float positionYawFactor = 2f;
    [SerializeField] private float controlRollFactor = -20f;

    float xThrow, yThrow; // ProcessTranslation metodunda güncellenir, ProcessRotation metodunda kullanabiliriz

    // Update is called once per frame
    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
    }

    void ProcessRotation()
    {
        // konuma/pozisyona bağlı rotasyonun değişmesi
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        // girişe(input) bağlı rotasyonun değişmesi
        float pitchDueToControlThrow = yThrow * controlPitchFactor;

        // hareket ettiği yöne göre rotasyonu değişecek
        // konum ve ınput manager/girdi uçağın eğimini değiştirecek
        float pitch = pitchDueToPosition + pitchDueToControlThrow; // x
        float yaw = transform.localPosition.x * positionYawFactor; // y
        float roll = xThrow * controlRollFactor; // z

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
        // rotasyonda x, yukarı ve aşağı gösterir
        // y, sağ ve solu gösterir
        // z, uçağa manevra verir
    }

    void ProcessTranslation()
    {
        // uçak sağa, sola, yukarı ve aşağı hareket edecek

        // yön tuşlarını kullanarak sağa veya sola hareket ettireceğiz
        xThrow = Input.GetAxis("Horizontal");
        // yukarı veya aşağı
        yThrow = Input.GetAxis("Vertical");

        float xOffset = xThrow * Time.deltaTime * controlSpeed;
        float rawXPos = transform.localPosition.x + xOffset;
        // uçağı ekranın dışına doğru hareket etmemesi için
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);

        float yOffset = yThrow * Time.deltaTime * controlSpeed;
        float rawYPos = transform.localPosition.y + yOffset;
        // uçağı ekranın dışına doğru hareket etmemesi için
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);

        // transform.localPosition(bu nesnenin ebevynine göre konumu): (0, 0, 0)
    }
}
