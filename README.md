# Galeria Prisma VR

## Descricao curta

Ambiente educativo em Unity com tema Web3, preparado para a atividade de introducao a XR/VR.

## Objetivo do projeto

O projeto apresenta uma galeria virtual simples com 5 prismas interativos, painel informativo e movimentacao no PC/Editor para demonstrar conceitos introdutorios de Web3 em um ambiente navegavel.

## Requisitos tecnicos

- Unity `6000.3.13f1`
- Cena principal em `Assets/ProjetoVR/Scenes/GaleriaPrisma_Main.unity`
- Meta XR SDK instalado no projeto
- XR Plugin Management configurado com OpenXR
- Build preparado para Android/Meta Quest
- Active Input Handling em Both
- Minimum API Level em Android 13 / API 33
- Movimentacao funcional no PC/Editor sem depender apenas do headset

## Como abrir no Unity

1. Abra o projeto no Unity `6000.3.13f1`.
2. Abra a cena `Assets/ProjetoVR/Scenes/GaleriaPrisma_Main.unity`.
3. Confirme no Unity que esta cena esta incluida no Build Settings.

## Cena principal

- `Assets/ProjetoVR/Scenes/GaleriaPrisma_Main.unity`

## Controles no PC/Editor

- `W/A/S/D`: mover o jogador
- `Mouse`: olhar ao redor
- `Left Shift`: correr
- `E`: interagir com o prisma quando estiver proximo
- `Esc`: liberar o cursor
- `Clique esquerdo`: travar o cursor novamente

## Conceitos dos 5 prismas

- Prisma 01: Descentralizacao
- Prisma 02: Imutabilidade
- Prisma 03: Transparencia
- Prisma 04: Contratos Inteligentes
- Prisma 05: Tokenizacao

## Status da entrega

- Cena principal configurada para a atividade
- Movimentacao no PC/Editor implementada
- Prismas com interacao pela tecla `E`
- Chao e prismas mudam de cor durante a interacao
- Ha feedback sonoro e visual
- Relatorio tecnico disponivel em `Assets/ProjetoVR/Docs/Relatorio_ProjetoVR.txt`

## Observacoes finais

- O repositorio foi mantido com as pastas essenciais `Assets/`, `Packages/` e `ProjectSettings/`.
- A documentacao interna original foi preservada em `Assets/ProjetoVR/Docs/README.txt`.
- Durante testes no Unity Editor, logs informativos de pacotes Meta XR/MRUK podem aparecer sem indicar erro critico de entrega.
