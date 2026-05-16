using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
[RequireComponent(typeof(Collider))]
public class MensagemProximidade : MonoBehaviour
{
    [SerializeField] private string mensagem = "Pressione E para interagir.";
    [SerializeField] private Text textoInteracao;
    [SerializeField] private TextMesh textoPainelCentral;
    [TextArea(2, 6)]
    [SerializeField] private string textoPainelAoEntrar = string.Empty;
    [TextArea(1, 4)]
    [SerializeField] private string textoPainelPadrao = "Aproxime-se dos prismas e pressione E para mudar a cor.";

    private void Start()
    {
        ResolverPainelCentral();
        ConfigurarTextoPainelPorNomeSeNecessario();
        DefinirTextoPainel(textoPainelPadrao);
        OcultarMensagem();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!EhPlayer(other))
        {
            return;
        }

        ExibirMensagem();
        ExibirTextoPainel();
    }

    private void OnTriggerExit(Collider other)
    {
        if (!EhPlayer(other))
        {
            return;
        }

        OcultarMensagem();
        RestaurarTextoPainel();
    }

    private void OnDisable()
    {
        OcultarMensagem();
        RestaurarTextoPainel();
    }

    private bool EhPlayer(Collider other)
    {
        return other.GetComponent<CharacterController>() != null
            || other.GetComponentInParent<CharacterController>() != null;
    }

    private void ExibirMensagem()
    {
        if (textoInteracao == null)
        {
            Debug.LogWarning("[ProjetoVR] Texto_Interacao nao foi vinculado em MensagemProximidade.", this);
            return;
        }

        textoInteracao.text = mensagem;
        textoInteracao.gameObject.SetActive(true);
    }

    private void OcultarMensagem()
    {
        if (textoInteracao == null)
        {
            return;
        }

        textoInteracao.text = string.Empty;
        textoInteracao.gameObject.SetActive(false);
    }

    private void ExibirTextoPainel()
    {
        DefinirTextoPainel(ObterTextoPainelAoEntrar());
    }

    private void RestaurarTextoPainel()
    {
        DefinirTextoPainel(textoPainelPadrao);
    }

    private void DefinirTextoPainel(string texto)
    {
        ResolverPainelCentral();

        if (textoPainelCentral == null || string.IsNullOrWhiteSpace(texto))
        {
            return;
        }

        textoPainelCentral.text = texto;
    }

    private string ObterTextoPainelAoEntrar()
    {
        return string.IsNullOrWhiteSpace(textoPainelAoEntrar)
            ? textoPainelPadrao
            : textoPainelAoEntrar;
    }

    private void ResolverPainelCentral()
    {
        if (textoPainelCentral != null)
        {
            return;
        }

        GameObject painel = GameObject.Find("Painel_Aviso_Texto");
        if (painel != null)
        {
            textoPainelCentral = painel.GetComponent<TextMesh>();
        }
    }

    private void ConfigurarTextoPainelPorNomeSeNecessario()
    {
        if (!string.IsNullOrWhiteSpace(textoPainelAoEntrar))
        {
            return;
        }

        switch (gameObject.name)
        {
            case "Prisma_01":
                textoPainelAoEntrar = "Descentralização\nNa Web3, os dados e decisões não ficam em um único servidor central.\nEles são distribuídos entre vários participantes da rede.";
                break;
            case "Prisma_02":
                textoPainelAoEntrar = "Imutabilidade\nDepois que uma informação é registrada na blockchain,\nela não pode ser alterada facilmente.\nIsso aumenta a confiança nos registros.";
                break;
            case "Prisma_03":
                textoPainelAoEntrar = "Transparência\nNa Web3, muitas informações podem ser verificadas publicamente.\nIsso permite auditoria, rastreabilidade e confiança.";
                break;
            case "Area_Informacao_Central":
                textoPainelAoEntrar = textoPainelPadrao;
                break;
        }
    }
}
