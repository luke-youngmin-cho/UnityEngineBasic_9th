using System;
using System.Collections.Generic;
using UnityEngine;

public class DamagePopUpRepository : MonoBehaviour
{
    public static DamagePopUpRepository instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = Instantiate(Resources.Load<DamagePopUpRepository>("DamagePopUpRepository"));
            }
            return _instance;
        }
    }
    private static DamagePopUpRepository _instance;

    [Serializable]
    public struct AssetPair
    {
        public LayerMask layerMask;
        public DamagePopUp damagePopUp;
    }
    [SerializeField] private List<AssetPair> _assets;

    public DamagePopUp GetDamagePopUp(int layer)
    {
        return _assets.Find(x => (x.layerMask & 1 << layer) > 0).damagePopUp;
    }
}