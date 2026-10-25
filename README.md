# TP_2_pvj2026_Ramos_Luca
📋 Descripción del Juego
**Metal Gear Solid: Ghost Infiltration** es un prototipo táctico de sigilo e infiltración desarrollado en Unity 6. El jugador controla a un agente infiltrado en un complejo fortificado, cuyo objetivo es esquivar patrullas automatizadas enemigas (drones con IA de navegación), recolectar información clasificada (discos de datos) y alcanzar la salida abriendo puertas de acceso restringido mediante la resolución de puntos y la interacción a distancia.

---

## 🕹️ Guía de Controles
| Movimiento | W, A, S, D (Relativo a la cámara) |
| Salto | Barra Espaciadora (Espacio) |
| Interactuar | Tecla E (Abrir puertas / terminales) |
| Aturdir Enemigo | Tecla F o Clic Izquierdo (Arma por Raycast) |
| Pausa / Reanudar | Tecla Escape |

---

## ⚙️ Detalle de Opcionales Elegidos
Para el desarrollo de este prototipo se implementaron las siguientes opciones avanzadas requeridas por la cátedra:

1. Opción A (Arma de Aturdimiento por Raycast): 
   - Descripción técnica: Se programó mediante el script `StunGun.cs`. Al disparar, proyecta un Raycast frontal desde la cámara del jugador filtrando por una LayerMask de enemigos. Al impactar al `EnemyAI`, detiene de forma temporal el componente `NavMeshAgent` y desactiva el script de patrullaje durante unos segundos mediante una corrutina (`Coroutine`), permitiendo evadir al centinela.
2. Opción C (Trampa de Rayo Láser / Fuego Continuo): 
   - *Descripción técnica:* Implementada en el script `LaserTrap.cs`. Utiliza un `BoxCollider` configurado como `IsTrigger` para detectar la entrada del jugador. Al cruzar la línea, un bucle de actualización (`Update`) aplica daño continuo por segundo restando salud mediante el sistema `PlayerHealth`.
3. Opción D (Zona de Desaceleración / Barro): 
   - *Descripción técnica:* Desarrollada en el script `MudZone.cs`. Detecta mediante `OnTriggerEnter` y `OnTriggerExit` la presencia del jugador para alterar temporalmente su velocidad de movimiento (`moveSpeed`), reduciéndola al entrar a la zona y restaurándola a su estado original al salir.

---

## 🖼️ Capturas del Nivel en Ejecución

<img width="1366" height="768" alt="programando" src="https://github.com/user-attachments/assets/3372ea9c-8174-487a-b817-1920f3facd20" />

<img width="648" height="326" alt="2 captura" src="https://github.com/user-attachments/assets/376fb621-c08f-430f-bbce-51e3d8724ffd" />
