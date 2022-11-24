using UnityEngine;

public class Spike : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private float _damageDelay;
    private float _lastDamageTime;
    private Moving _move;
    private void OnCollisionEnter2D(Collision2D other) {
        _move = other.collider.GetComponent<Moving>();
        if(_move != null)
        {
            _move.TakeDamage(_damage);
            _lastDamageTime = Time.time;
        }
    }

    private void Update(){
        if(Time.time - _lastDamageTime > _damageDelay && _move!= null)
        {
            _move.TakeDamage(_damage);
            _lastDamageTime = Time.time;
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        Moving move = other.collider.GetComponent<Moving>();
        if(_move == move)
        {
            _move = null;
        }
    }
}
