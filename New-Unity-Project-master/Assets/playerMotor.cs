using UnityEngine;


[RequireComponent(typeof(Rigidbody))]



public class playerMotor : MonoBehaviour {


    [SerializeField]
    private Camera cam;

    private Vector3 velocity = Vector3.zero;
    private Vector3 rotation = Vector3.zero;
    private Vector3 cameraRotation = Vector3.zero;


    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    //liikevektori x
    public void Move(Vector3 _velocity) //HUOM! _velocity ei pelkkä velocit!
    {
        velocity = _velocity;
    }
    //rotaatio vektori
    public void Rotate(Vector3 _rotation) //HUOM! _rotation!!
    {
        rotation = _rotation;
    }

    //rotaatio vektori y
    public void RotateCamera(Vector3 _cameraRotation) //HUOM! _
    {
        cameraRotation = _cameraRotation;
    }

    //suorittaa
    private void FixedUpdate()
    {
        Liikuta();
        Rotaatioi();
    }

    //Liikuttaa pelaajaa tms
    void Liikuta()
    {
        if(velocity != Vector3.zero)
        {
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime); // liikuttaa nyk sijainti + velocity vektori
        }
    }

    //rotaatioi :D
    void Rotaatioi()
    {
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation)); //Laskee ton rotaation määrän ton vektorin perusteella (x suunnassa)
        cam.transform.Rotate(-cameraRotation);
    }

}
