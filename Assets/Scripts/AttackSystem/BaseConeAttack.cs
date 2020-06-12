using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// O BaseConeAttack é um tipo de ataque cuja colisão é descrita por um setor circular (cone).
// O ataque cria um colisor em formato de cone em determinada direção, indicada pelo target.
// Esse collider é do tipo trigger, para não interferir nas colisões de física do objeto.
public abstract class BaseConeAttack : Attack {

    [Tooltip("O ângulo em graus do cone a ser gerado")]
    [Range(1, 360)] public float arcAngle;

    [Tooltip("O raio do cone gerado")]
    public float arcRadius;

    // Usado para descrever o quão próximo de um círculo real será o collider. Significa basicamente
    // que, no PolygonCollider2D, será gerado um vértice a cada x graus. A precisão resultante de
    // um grande número de vértices tem um grande custo de performance, e, no geral, valores muito baixos
    // terão o mesmo resultado, especialmente para arcos pequenos.
    private static readonly float Precision = 30f;

    // Função executada quando o Trigger é chamado.
    // (sinceramente nem sei pq eu dividi as funções, a diferença seria somente que o GenerateHitbox() precisaria
    // ser chamado nos scripts que extendem esse)
    public abstract void OnTrigger(PolygonCollider2D hitbox, Vector2 direction, ContactFilter2D contactFilter);

    // Quando é executado, o ataque gera o collider e então executa uma ação descrita no OnTrigger.
    // O collider é gerado como componente do parent, e o target descreve a direção para a qual ele
    // aponta. Nota-se que a rotação do collider depende da rotação local do objeto, e não dos eixos
    // globais.
    public override void Trigger(Transform parent, Vector2 target, ContactFilter2D contactFilter) {
        PolygonCollider2D hitbox;
        hitbox = GenerateConeHitbox(parent, arcAngle, arcRadius, target);

        OnTrigger(hitbox, target, contactFilter);

        Destroy(hitbox, 0.5f);
    }

    // Função auxiliar que retorna um vetor com as colisões do collider.
    // (preguiça de digitar isso em todos os scripts que herdam disso XD.
    //   sim, daria pra simplesmente pegar esse resultado e passar como parâmetro
    //   pro OnTrigger(), porém aí as informações do collider são perdidas, tipo o
    //   transform necessário pra gerar o Knockback para longe do player)
    protected Collider2D[] GetOverlapColliders(Collider2D hitbox, ContactFilter2D contactFilter) {
        Collider2D[] results = new Collider2D[16];
        hitbox.OverlapCollider(contactFilter, results);
        return results;
    }

    // Função que gera o collider. Este se trata de um PolygonCollider2D, cujos vértices são calculados
    // a partir do arcAngle e arcRadius.
    PolygonCollider2D GenerateConeHitbox(Transform parent, float angle, float radius, Vector2 direction) {
        Vector2[] points;
        int numPoints = Mathf.FloorToInt(angle / Precision);
        float angleBetweenPoints = angle / (numPoints + 1);
        
        // A forma que os pontos do collider são calculados é:
        // A partir da direção, gerar o "ponto mínimo" (o ângulo da direção - metade do ângulo do arco)
        // e o máximo (direção + a outra metade do ângulo). E, por fim, colocar os pontos adicionais entre
        // os dois, somando valores ao ângulo ponto mínimo (varia conforme o número de pontos).
        float minPointAngle;
        float maxPointAngle;
        float directionAngle;

        if (direction.y > 0)
            directionAngle = Vector2.Angle(Vector2.right, direction);
        else
            directionAngle = 360 - Vector2.Angle(Vector2.right, direction);

        minPointAngle = SumAngles(directionAngle, -1 * (angle / 2));
        maxPointAngle = SumAngles(directionAngle, (angle / 2));

        // Se o ângulo do arco for 360 então é um círculo, e os pontos mínimo e máximo serão o mesmo,
        // então adiciona apenas um deles, e o centro não precisa ser um ponto do collider.
        if (angle == 360)
            points = new Vector2[numPoints + 1];
        else {
            points = new Vector2[numPoints + 3];
            points[numPoints + 1] = new Vector3(Mathf.Cos(maxPointAngle * Mathf.Deg2Rad), Mathf.Sin(maxPointAngle * Mathf.Deg2Rad)) * radius;
            points[numPoints + 2] = Vector3.zero;
        }

        points[0] = new Vector3(Mathf.Cos(minPointAngle * Mathf.Deg2Rad), Mathf.Sin(minPointAngle * Mathf.Deg2Rad)) * radius;

        for (int i = 1; i <= numPoints; i++) {
            float curr_angle = SumAngles(minPointAngle, i * angleBetweenPoints);
            points[i] = new Vector3(Mathf.Cos(curr_angle * Mathf.Deg2Rad), Mathf.Sin(curr_angle * Mathf.Deg2Rad)) * radius;
        }

        // Adiciona o collider no parent.
        PolygonCollider2D collider = parent.gameObject.AddComponent<PolygonCollider2D>();
        
        collider.isTrigger = true;
        collider.points = points;
        
        return collider;
    }

    // Considerando um círculo, e partindo do ponto 0°, valores positivos são movimentos no sentido anti-horário e negativos no sentido horário.
    // Retorna um valor entre 0 e 359.
    float SumAngles(params float[] angles) {
        float sum = 0;
        float result;

        for (int i = 0; i < angles.Length; i++)
            sum += angles[i];

        result = sum % 360;

        if (result < 0)
            result = 360 + result;

        return result;
    }
}