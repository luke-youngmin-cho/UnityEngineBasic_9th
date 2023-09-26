using UnityEngine;
using TMPro;

public class DamagePopUp : MonoBehaviour
{
    private TMP_Text _damageAmount;
    private float _fadeSpeed = 0.8f;
    private float _moveSpeedY = 0.3f;
    private Color _color;

    public static DamagePopUp Create(Vector2 pos, int damage, int layer)
    {
        DamagePopUp popUp =
            Instantiate(DamagePopUpRepository.instance.GetDamagePopUp(layer),
                        pos,
                        Quaternion.identity);
        popUp._damageAmount.text = damage.ToString();
        return popUp;
    }

    private void Awake()
    {
        _damageAmount = GetComponent<TMP_Text>();
        _color = _damageAmount.color;
    }

    private void Update()
    {
        transform.position += Vector3.up * _moveSpeedY * Time.deltaTime;
        _color.a -= _fadeSpeed * Time.deltaTime;
        _damageAmount.color = _color;

        if (_color.a <= 0.0f)
            Destroy(gameObject);
    }
}
