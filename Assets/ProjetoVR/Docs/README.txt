Galeria Prisma VR

Descricao curta
- Ambiente educativo em Unity com tema Web3, preparado para atividade de introducao a XR/VR.
- A cena principal apresenta uma galeria simples com 5 prismas interativos, painel informativo e movimentacao no PC/Editor.

Como abrir a cena
- Abra o projeto Unity.
- Acesse Assets/ProjetoVR/Scenes/GaleriaPrisma_Main.unity.
- Confirme que esta cena esta incluida em Build Settings.

Controles no PC/Editor
- W/A/S/D: mover o jogador
- Mouse: olhar ao redor
- Left Shift: correr
- E: interagir com o prisma quando estiver proximo
- Esc: liberar o cursor
- Clique esquerdo: travar o cursor novamente

Como interagir com os prismas
- Aproxime-se de um dos 5 prismas.
- Leia a mensagem de interacao exibida na interface.
- Pressione E para alternar a cor do prisma.
- Cada prisma funciona de forma independente, sem ordem obrigatoria.
- A interacao tambem altera a luz associada, aplica uma variacao suave no chao, toca um som curto e emite particulas sutis.

Conceitos dos prismas
- Prisma 01: Descentralizacao
- Prisma 02: Imutabilidade
- Prisma 03: Transparencia
- Prisma 04: Contratos Inteligentes
- Prisma 05: Tokenizacao

Requisitos tecnicos observados no projeto
- Unity 6000.3.13f1
- Cena principal: Assets/ProjetoVR/Scenes/GaleriaPrisma_Main.unity
- Meta XR SDK instalado no projeto
- XR Plugin Management configurado com OpenXR
- Build Settings preparados para Android/Meta Quest
- Active Input Handling em Both
- Minimum API Level em Android 13 / API 33
- Movimentacao funcional no PC/Editor sem depender apenas do headset
- Chao caminhavel, skybox padrao configurado e mais de 5 objetos 3D na cena

Status da entrega
- A cena principal esta configurada.
- A movimentacao no PC/Editor esta implementada.
- Os prismas possuem interacao com a tecla E.
- O chao e os prismas mudam de cor.
- Ha feedback sonoro/visual.
- O relatorio tecnico esta disponivel em Assets/ProjetoVR/Docs/Relatorio_ProjetoVR.txt.

Observacao sobre logs do Meta XR/MRUK
- Durante testes no Unity Editor, podem aparecer logs informativos de pacotes Meta XR ou MRUK.
- Se a cena abre, o movimento funciona e as interacoes acontecem normalmente, esses logs nao sao necessariamente erro critico de entrega.

Entrega e GitHub
- Para entrega, o repositorio deve conter apenas:
- Assets/
- ProjectSettings/
- Packages/
- Nao devem ser enviados:
- Library/
- Temp/
- Logs/
- Obj/
- Build/
- UserSettings/
