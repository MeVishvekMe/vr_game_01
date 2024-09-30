using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GameManager : MonoBehaviour {
    public Transform rockSpawnPoint;
    public GameObject rockPrefab;

    private float _spawnTime;

    private void Start() {
        spawnRock();
    }

    private void Update() {
        _spawnTime += Time.deltaTime;
        if(_spawnTime >= 10f) {
            spawnRock();
            _spawnTime = 0;
        }
    }

    private void spawnRock() {
        GameObject rock = Instantiate(rockPrefab, rockSpawnPoint.position, Quaternion.identity);
        rock.GetComponent<Rigidbody>().AddTorque(new Vector3(-50f, 0f, 0f), ForceMode.Impulse);
    }

}
