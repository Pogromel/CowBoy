
using UnityEngine;
public class Aim : MonoBehaviour
{

    [SerializeField] Transform _arm;
    float _offset = -90;
    Vector3 _startingSize;
    Vector3 _armStartingSize;
    #region Start
    // Start is called before the first frame update
    void Start()
    {
        _startingSize = transform.localScale;
        _armStartingSize = _arm.localScale;
    }
    #endregion

    // Update is called once per frame
    void Update()
    {
        Vector3 vector3 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 mousePos = vector3;
        Vector3 perendicualar = _arm.position - mousePos;
        Quaternion val = Quaternion.LookRotation(Vector3.forward, perendicualar);
        val *= Quaternion.Euler(0, 0, _offset);
        _arm.rotation = val;


    }
}
