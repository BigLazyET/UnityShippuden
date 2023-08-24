using Cinemachine;
using UnityEngine;

namespace Assets.Scripts.Manager
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameObject player;
        [SerializeField] private float respawnTime;
        [SerializeField] private Transform respawnPoint;

        private bool respawn;
        private float respawnStartTime;
        private CinemachineVirtualCamera cvCamera;

        private void Start()
        {
            cvCamera = GameObject.Find("Player Camera").GetComponent<CinemachineVirtualCamera>();
        }

        private void Update()
        {
            CheckRespawn();
        }

        private void CheckRespawn()
        {
            if(Time.time > respawnStartTime + respawnTime && respawn)
            {
                Instantiate(player, respawnPoint);
                cvCamera.Follow = player.transform;
                respawn = false;
            }
        }
    }
}
