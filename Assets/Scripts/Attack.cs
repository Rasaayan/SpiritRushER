using UnityEngine;
using UnityEngine.UI;

public class Attack : MonoBehaviour
{
    public float damage = 1f;
    public float range = 1000f;
    public Camera attackCam;

    public float totalLight;

    public Text lightCountText;

    public PauseMenu pauseMenuObj;

    public GameManager gameManagerObj;

    private void Update()
    {
        lightCountText.text = "Light : " + totalLight;

        if (Input.GetKeyDown(KeyCode.M) == true )
        {
            totalLight++;
        }
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
        if (Input.GetButtonDown("Fire2") && totalLight > 0)
        {
            ShootLight();
            totalLight--;
        }
    }

    void Shoot()
    {
        if (pauseMenuObj.gamePaused == true || gameManagerObj.newGameBeingMade == true || gameManagerObj.gameOver == true)
        {
            AudioManager.Instance.PlayClip("Button_Click");
        }
        else
        {
            RaycastHit hitinfo;

            Ray hit = attackCam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(hit, out hitinfo, range))
            {
                Debug.Log("We hit " + hitinfo.transform.name);
                Target enemy = hitinfo.transform.GetComponent<Target>();
                if (enemy != null)
                {
                    enemy.TakeDamage(damage);
                }
            }
            AudioManager.Instance.PlayClip("Shoot");
        }      
    }

    void ShootLight()
    {
        RaycastHit hitinfo;

        Ray hit = attackCam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(hit, out hitinfo, range))
        {
            Boss levelBoss = hitinfo.transform.GetComponent<Boss>();
            if (levelBoss != null)
            {
                levelBoss.TakeDamage(25f);
            }
        }
    }
}
