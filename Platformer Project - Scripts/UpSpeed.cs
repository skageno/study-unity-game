using UnityEngine;

public class UpSpeed : MonoBehaviour
{
    [SerializeField] private float _upgradeTime;
    [SerializeField] private float _upgradePower;
    public float UpgradeTime => _upgradeTime;
    public float UpgradePower => _upgradePower;
}
