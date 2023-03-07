using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnermyWalk : MonoBehaviour
{
    public float speed = 5.0f; // tốc độ di chuyển của quái thú
    public float distanceThreshold = 0.1f; // khoảng cách giữa quái thú và điểm đến được coi là đến nếu nhỏ hơn threshold này
    private Vector3[] waypoints; // mảng chứa tọa độ các điểm đến
    private int currentWaypoint = 0; // chỉ số của điểm đến hiện tại
    private bool isMoving = false; // biến xác định quái thú có đang di chuyển hay không

    // Start is called before the first frame update
    void Start()
    {
        // tìm kiếm và lưu trữ tọa độ của các điểm đến vào mảng
        waypoints = new Vector3[12];
        for (int i = 1; i <= 12; i++)
        {
            GameObject pointWay = GameObject.Find("pointWay" + i);
            waypoints[i - 1] = pointWay.transform.position;
        }

        // di chuyển quái thú đến điểm đến đầu tiên
        //transform.position = waypoints[0];
        isMoving = true;

    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            // tính toán hướng di chuyển tới điểm đến hiện tại
            Vector3 direction = waypoints[currentWaypoint] - transform.position;
            direction.Normalize();

            // di chuyển quái thú theo hướng tính toán được
            transform.position += direction * speed * Time.deltaTime;

            // nếu quái thú đến gần điểm đến hiện tại, chuyển sang điểm đến tiếp theo
            if (Vector3.Distance(transform.position, waypoints[currentWaypoint]) < distanceThreshold)
            {
                currentWaypoint++;
                if (currentWaypoint ==12 )
                {
                    // nếu quái thú đã đi đến điểm đến cuối cùng, hủy đối tượng quái thú và trừ điểm máu của player
                    Destroy(gameObject,3f);
                    Enemy enemy = gameObject.GetComponent<Enemy>();
                    Enemy.RemoveEnemy(enemy);
                    // subtract player health
                    isMoving = false;
                }
            }
        }
    }
}
