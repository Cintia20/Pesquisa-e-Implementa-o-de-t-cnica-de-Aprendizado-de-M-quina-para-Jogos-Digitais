# Pesquisa-e-Implementa-o-de-t-cnica-de-Aprendizado-de-M-quina-para-Jogos-Digitais

DESCRIÇÃO:
Implementação de um agente inteligente que aprende a jogar FlapBird 
usando Aprendizado por Reforço com Unity ML-Agents.

CONFIGURAÇÃO DO AMBIENTE:
1. Instale o ML-Agents no Unity:
   - Abra o Package Manager
   - Adicione o pacote "ML-Agents"

INSTRUÇÕES PARA EXECUÇÃO:

A) MODO TREINAMENTO:
1. Abra o projeto no Unity
2. Execute o comando no terminal:
   mlagents-learn config/flapbird_config.yaml --run-id=FlapBird_v1
3. Pressione Play no Editor Unity

B) MODO INFERÊNCIA (USAR MODELO TREINADO):
1. Coloque o modelo .nn na pasta Assets/Models/
2. Configure o Behavior Parameters do agente para usar o modelo
3. Pressione Play no Editor Unity

ESTRUTURA DE ARQUIVOS:
- /Assets/Scripts/      -> Códigos-fonte
  * BirdController.cs   -> Lógica do agente
  * ObstacleSpawner.cs  -> Gerador de obstáculos
- /config/              
  * flapbird_config.yaml-> Configuração do treinamento
- /Models/              -> Modelos treinados (opcional)

OBSERVAÇÕES IMPORTANTES:
- O treinamento pode levar várias horas dependendo da configuração
- Para visualizar o progresso, execute:
  tensorboard --logdir results
- Aperte Ctrl+C no terminal para interromper o treinamento

CONTROLES MANUAIS (opcional):
- Espaço: Faz o pássaro pular (quando no modo Heuristic)

PROBLEMAS CONHECIDOS:
- Se encontrar erros de compatibilidade, verifique as versões dos pacotes
- Em caso de falha no treinamento, reduza o learning_rate no arquivo de configuração
