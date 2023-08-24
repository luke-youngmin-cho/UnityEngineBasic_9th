using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopUpTextManager : MonoBehaviour
{
    [SerializeField] private PopUpText _cool;
    [SerializeField] private PopUpText _great;
    [SerializeField] private PopUpText _good;
    [SerializeField] private PopUpText _miss;
    [SerializeField] private PopUpText _bad;
    [SerializeField] private PopUpText _comboTitle;
    [SerializeField] private PopUpText _comboStack;

    public void PopUpHitJudgeText(HitJudge hitJudge)
    {
        if (_cool.gameObject.activeSelf) _cool.transform.position += Vector3.forward;
        if (_great.gameObject.activeSelf) _great.transform.position += Vector3.forward;
        if (_good.gameObject.activeSelf) _good.transform.position += Vector3.forward;
        if (_miss.gameObject.activeSelf) _miss.transform.position += Vector3.forward;
        if (_bad.gameObject.activeSelf) _bad.transform.position += Vector3.forward;

        switch (hitJudge)
        {
            case HitJudge.Bad:
                _bad.PopUp();
                break;
            case HitJudge.Miss:
                _miss.PopUp();
                break;
            case HitJudge.Good:
                _good.PopUp();
                break;
            case HitJudge.Great:
                _great.PopUp();
                break;
            case HitJudge.Cool:
                _cool.PopUp();
                break;
            default:
                break;
        }
    }

    public void PopUpComboText(int comboStack)
    {
        if (comboStack < 2)
            return;

        _comboTitle.PopUp();
        _comboStack.PopUp(comboStack.ToString());
    }

    private void Awake()
    {
        _cool.gameObject.SetActive(true);
        _great.gameObject.SetActive(true);
        _good.gameObject.SetActive(true);
        _miss.gameObject.SetActive(true);
        _bad.gameObject.SetActive(true);
        _comboTitle.gameObject.SetActive(true);
        _comboStack.gameObject.SetActive(true);

        _cool.gameObject.SetActive(false);
        _great.gameObject.SetActive(false);
        _good.gameObject.SetActive(false);
        _miss.gameObject.SetActive(false);
        _bad.gameObject.SetActive(false);
        _comboTitle.gameObject.SetActive(false);
        _comboStack.gameObject.SetActive(false);
    }
}
