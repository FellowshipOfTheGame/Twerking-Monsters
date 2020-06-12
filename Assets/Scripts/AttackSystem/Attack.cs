using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Ataques são objetos simples que descrevem simplesmente ações que podem ser executadas.

// O objetivo de fazê-los como ScriptableObjects é para que sejam criados "tipos" de ataques
// que podem ser personalizados.

// Os parametros levados em consideração são o transform, que dá acesso às informações do
// GameObject que executa a ação, o target, um Vector2 que descreve a direção/localização
// do ataque, e um contactFilter que filtra quais objetos/camadas colidirão com o ataque.
public abstract class Attack : ScriptableObject {

    public abstract void Trigger(Transform parent, Vector2 target, ContactFilter2D contactFilter);

}
