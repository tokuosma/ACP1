using UnityEngine;




public class playerController : MonoBehaviour {

    [SerializeField]
    private float nopeus= 5f;
    [SerializeField]
    private float RotaatioHerkkyys = 3f;

    private playerMotor motor;

    private void Start()
    {
        motor = GetComponent<playerMotor>();
    }

    private void Update()
    {
        //laskee liikkeen vektorina
        float xMov = Input.GetAxisRaw("Horizontal");
        float zMov = Input.GetAxisRaw("Vertical");

        Vector3 movHorizontal = transform.right * xMov; //aina 1välillä -1 - 1
        Vector3 movVertical = transform.forward * zMov;

        //lopullinen vektori
        Vector3 _velocity = (movHorizontal + movVertical).normalized * nopeus; //normalize koska vektori aina 1 => saadaan suunta vektorille suunta ja nopeudesta nopeus.. 
        
        
        // toteuttaa liikeen
        motor.Move(_velocity);

        //laskee  rotaation vektorina (vaakasuunta)
        float yRot = Input.GetAxisRaw("Mouse X"); //x akseli koska pyörittää yn ympäri
        Vector3 _rotation = new Vector3(0f, yRot, 0f) * RotaatioHerkkyys;

        //Toteuttaa rotaation
        motor.Rotate(_rotation);
    
        //laskee "kameran" rotaation vektorina (pystysuunta)

        float xRot = Input.GetAxisRaw("Mouse Y"); // y koska pyörittää xn ympäri
        Vector3 _cameraRotation = new Vector3(xRot, 0f, 0f) * RotaatioHerkkyys;

        //Toteuttaa rotaation
        motor.RotateCamera(_cameraRotation);
    }

}
