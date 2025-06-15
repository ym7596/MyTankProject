using UnityEngine;

public class BulletDev : MonoBehaviour
{
    [SerializeField] private GameObject _effect;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
           
            MissileManager.Instance.RemoveMissile(this.gameObject);
            
            var effect = Instantiate(_effect,transform.position, Quaternion.identity);
            Destroy(effect, 2f);
            Destroy(gameObject);
        }
    }
}
