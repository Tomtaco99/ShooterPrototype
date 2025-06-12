using UnityEngine;

public class PhysicalBulletMovement : MonoBehaviour
{
    private Rigidbody rb;
    public float ProyectileSpeed;
    public float damage;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * ProyectileSpeed, ForceMode.Impulse);
        Destroy(gameObject, 10f);
    }


    public void OnCollisionEnter(Collision collision)
    {
        rb.constraints = RigidbodyConstraints.FreezeAll;
        rb.velocity = Vector3.zero;
        rb.useGravity = false;
        foreach (var a in Physics.SphereCastAll(transform.position, 5f, transform.forward))
        {
            if (a.collider.TryGetComponent(out IDamagable col))
            {
                col.TakeDamage(damage, transform.position, a.transform.position - transform.position);
            }
        }
        transform.GetComponentInChildren<MeshRenderer>().enabled = false;
        transform.GetComponent<CapsuleCollider>().enabled = false;
    }
    
    Vector4[] MakeUnitSphere(int len)
    {
        Debug.Assert(len > 2);
        var v = new Vector4[len * 3];
        for (int i = 0; i < len; i++)
        {
            var f = i / (float)len;
            float c = Mathf.Cos(f * (float)(Mathf.PI * 2.0));
            float s = Mathf.Sin(f * (float)(Mathf.PI * 2.0));
            v[0 * len + i] = new Vector4(c, s, 0, 1);
            v[1 * len + i] = new Vector4(0, c, s, 1);
            v[2 * len + i] = new Vector4(s, 0, c, 1);
        }
        return v;
    }
}
