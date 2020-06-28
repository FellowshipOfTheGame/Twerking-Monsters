using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classe abstrata que descreve o comportamento mais básico de uma habilidade, com uma
/// função para ativação e custo de energia.
/// </summary>
public abstract class Skill : ScriptableObject {

    [Tooltip("Quantidade de energia utilizada para a ativação da habilidade")]
    public float energyCost = 0f;
    
    protected abstract void OnTrigger(Entity parent, Vector2 target, ContactFilter2D filter);

    /// <summary>
    /// Executa a habilidade.
    /// </summary>
    /// <param name="parent">Descreve o gameObject a partir do qual a habilidade será executada.</param>
    /// <param name="target">Descreve a direção ou alvo para direcionar a habilidade.</param>
    /// <param name="filter">Filtro de contato, caso a habilidade envolva algum tipo de colisão.</param>
    /// <returns>True caso a ativação seja um sucesso, false caso a quantidade de energia disponível seja menor que o custo da habilidade.</returns>
    public bool Trigger(Entity parent, Vector2 target, ContactFilter2D filter) {
        if (parent.stats.energyPoints < energyCost)
            return false;

        parent.stats.energyPoints -= energyCost;
        
        OnTrigger(parent, target, filter);
        
        return true;
    }

}
