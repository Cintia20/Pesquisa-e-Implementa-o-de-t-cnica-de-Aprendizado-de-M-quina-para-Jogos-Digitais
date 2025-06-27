using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class BirdController : Agent
{
    public float jumpForce = 5f;
    public float forwardSpeed = 2f;
    public float gravity = 9.8f;
    private float verticalVelocity = 0f;
    private Vector3 initialPosition;
    private ObstacleSpawner spawner;

    public override void Initialize()
    {
        initialPosition = transform.position;
        spawner = FindObjectOfType<ObstacleSpawner>();
    }

    public override void OnEpisodeBegin()
    {
        // Resetar posição e velocidade
        transform.position = initialPosition;
        verticalVelocity = 0f;

        // Destruir todos os obstáculos existentes
        spawner.ClearObstacles();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // 1. Posição vertical do pássaro (1 valor)
        sensor.AddObservation(transform.position.y);

        // 2. Velocidade vertical (1 valor)
        sensor.AddObservation(verticalVelocity);

        // 3. Posição do próximo obstáculo (2 valores: x e y do gap)
        if (spawner.GetNextObstaclePosition(out Vector3 obstaclePos, out float gapY))
        {
            sensor.AddObservation(obstaclePos.x - transform.position.x); // Distância horizontal
            sensor.AddObservation(gapY); // Posição Y do gap
        }
        else
        {
            sensor.AddObservation(10f); // Distância padrão se não houver obstáculo
            sensor.AddObservation(0f); // Posição Y padrão
        }

        // Total de observações: 4
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        // Movimento para frente constante
        transform.Translate(Vector3.right * forwardSpeed * Time.deltaTime);

        // Ação 0: 0 = não pular, 1 = pular
        if (actions.DiscreteActions[0] == 1)
        {
            verticalVelocity = jumpForce;
        }

        // Aplicar gravidade
        verticalVelocity -= gravity * Time.deltaTime;
        transform.Translate(Vector3.up * verticalVelocity * Time.deltaTime);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        // Mapeamento para teste manual (espaço para pular)
        var discreteActionsOut = actionsOut.DiscreteActions;
        discreteActionsOut[0] = Input.GetKey(KeyCode.Space) ? 1 : 0;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            SetReward(-1f); // Penalidade por colidir
            EndEpisode(); // Termina o episódio
        }
    }

    void Update()
    {
        // Recompensa contínua por sobreviver
        AddReward(0.01f);
    }
}