using System.Collections.Generic;
using UnityEngine;

public class ContactMonitor : MonoBehaviour
{
    private Rigidbody _rb;

    private List<Collider> _contacts = new List<Collider>();

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!_contacts.Contains(collision.collider))
        {
            _contacts.Add(collision.collider);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        _contacts.Remove(collision.collider);
    }

    public bool IsOnBlock()
    {
        // ѕровер€ем, есть ли контакты с другими блоками
        foreach (var contact in _contacts)
        {
            if (contact.gameObject.CompareTag("Block"))
            {
                return true;
            }
        }

        return false;
    }

    public void ClearContacts()
    {
        _contacts.Clear();
    }
}
