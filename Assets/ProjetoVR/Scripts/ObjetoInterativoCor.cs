using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(MeshRenderer))]
public class ObjetoInterativoCor : MonoBehaviour
{
    [SerializeField] private int indiceSequencia = 1;
    [SerializeField] private float distanciaInteracao = 2.5f;
    [SerializeField] private Color corInicial = new Color(0.22f, 0.72f, 0.9f, 1f);
    [SerializeField] private Color corInteracao = new Color(0.96f, 0.38f, 0.72f, 1f);
    [SerializeField] private bool iniciarAtivo = false;
    [SerializeField] private Renderer chaoRenderer;
    [SerializeField] private Light luzAssociada;
    [SerializeField] private float intensidadeLuzInativa = 0.55f;
    [SerializeField] private float intensidadeLuzAtiva = 1.1f;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private ParticleSystem particulasAssociadas;
    [SerializeField] private int particulasInteracao = 8;

    private Transform cameraTransform;
    private MeshRenderer meshRenderer;
    private Material materialChaoInstanciado;
    private bool ativo;
    private static AudioClip fallbackClip;

    public int IndiceSequencia => Mathf.Max(1, indiceSequencia);

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        ResolverChaoRenderer();
        GarantirFeedbackVisual();
    }

    private void Start()
    {
        ativo = iniciarAtivo;
        ApplyColor();
        CacheCamera();
    }

    private void Update()
    {
        if (cameraTransform == null)
        {
            CacheCamera();
        }

        if (cameraTransform == null)
        {
            return;
        }

        bool estaPerto = Vector3.Distance(cameraTransform.position, transform.position) <= distanciaInteracao;

        if (estaPerto && Input.GetKeyDown(KeyCode.E))
        {
            ativo = !ativo;
            ApplyColor();
            TocarSomInteracao();
            EmitirParticulas(particulasInteracao);
        }
    }

    private void CacheCamera()
    {
        Camera mainCamera = Camera.main;

        if (mainCamera != null)
        {
            cameraTransform = mainCamera.transform;
        }
    }

    private void ApplyColor()
    {
        if (meshRenderer == null)
        {
            return;
        }

        Color corAtual = ativo ? corInteracao : corInicial;
        meshRenderer.material.color = corAtual;
        AtualizarLuz(corAtual);
        AtualizarChao(corAtual);
        AtualizarParticulas(corAtual);
    }

    private void GarantirFeedbackVisual()
    {
        GarantirLuz();
        GarantirAudioSource();
        GarantirParticulas();
    }

    private void GarantirLuz()
    {
        if (luzAssociada != null)
        {
            return;
        }

        GameObject luzObject = new GameObject("Prisma_PointLight");
        luzObject.transform.SetParent(transform, false);
        luzObject.transform.localPosition = Vector3.zero;

        luzAssociada = luzObject.AddComponent<Light>();
        luzAssociada.type = LightType.Point;
        luzAssociada.range = 2.8f;
        luzAssociada.intensity = intensidadeLuzInativa;
        luzAssociada.color = corInicial;
        luzAssociada.shadows = LightShadows.None;
    }

    private void GarantirAudioSource()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.playOnAwake = false;
        audioSource.loop = false;
        audioSource.spatialBlend = 0.75f;
        audioSource.volume = 0.18f;
        audioSource.dopplerLevel = 0f;
        audioSource.rolloffMode = AudioRolloffMode.Linear;
        audioSource.minDistance = 0.6f;
        audioSource.maxDistance = 4f;
    }

    private void GarantirParticulas()
    {
        if (particulasAssociadas != null)
        {
            return;
        }

        GameObject particulasObject = new GameObject("Prisma_Particles");
        particulasObject.transform.SetParent(transform, false);
        particulasObject.transform.localPosition = Vector3.zero;

        particulasAssociadas = particulasObject.AddComponent<ParticleSystem>();
        particulasAssociadas.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);

        ParticleSystemRenderer rendererParticulas = particulasObject.GetComponent<ParticleSystemRenderer>();
        rendererParticulas.renderMode = ParticleSystemRenderMode.Billboard;

        var main = particulasAssociadas.main;
        main.duration = 1.2f;
        main.loop = true;
        main.playOnAwake = false;
        main.startLifetime = new ParticleSystem.MinMaxCurve(0.8f, 1.2f);
        main.startSpeed = new ParticleSystem.MinMaxCurve(0.03f, 0.08f);
        main.startSize = new ParticleSystem.MinMaxCurve(0.04f, 0.07f);
        main.startColor = corInicial;
        main.gravityModifier = 0f;
        main.simulationSpace = ParticleSystemSimulationSpace.Local;
        main.maxParticles = 24;

        var emission = particulasAssociadas.emission;
        emission.rateOverTime = 4f;

        var shape = particulasAssociadas.shape;
        shape.enabled = true;
        shape.shapeType = ParticleSystemShapeType.Sphere;
        shape.radius = 0.12f;
        shape.radiusThickness = 0.4f;

        particulasAssociadas.Play();
    }

    private void AtualizarLuz(Color corAtual)
    {
        if (luzAssociada == null)
        {
            return;
        }

        luzAssociada.color = corAtual;
        luzAssociada.intensity = ativo ? intensidadeLuzAtiva : intensidadeLuzInativa;
    }

    private void AtualizarChao(Color corAtual)
    {
        ResolverChaoRenderer();

        if (chaoRenderer == null)
        {
            return;
        }

        if (materialChaoInstanciado == null)
        {
            materialChaoInstanciado = chaoRenderer.material;
        }

        materialChaoInstanciado.color = ObterCorChaoSuave(corAtual);
    }

    private void AtualizarParticulas(Color corAtual)
    {
        if (particulasAssociadas == null)
        {
            return;
        }

        var main = particulasAssociadas.main;
        corAtual.a = 0.7f;
        main.startColor = corAtual;
    }

    private void TocarSomInteracao()
    {
        if (audioSource == null)
        {
            return;
        }

        AudioClip clip = audioClip != null ? audioClip : audioSource.clip;
        if (clip == null)
        {
            clip = ObterClipPadrao();
        }

        if (clip != null)
        {
            audioSource.PlayOneShot(clip, 0.7f);
        }
    }

    public void EmitirParticulasSequencia(int quantidade)
    {
        EmitirParticulas(quantidade);
    }

    private void EmitirParticulas(int quantidade)
    {
        if (particulasAssociadas == null)
        {
            return;
        }

        particulasAssociadas.Emit(Mathf.Max(1, quantidade));
    }

    private static AudioClip ObterClipPadrao()
    {
        if (fallbackClip != null)
        {
            return fallbackClip;
        }

        const int sampleRate = 22050;
        const float duration = 0.14f;
        const float frequency = 660f;
        int totalSamples = Mathf.CeilToInt(sampleRate * duration);
        float[] samples = new float[totalSamples];

        for (int i = 0; i < totalSamples; i++)
        {
            float time = i / (float)sampleRate;
            float envelope = 1f - (i / (float)totalSamples);
            samples[i] = Mathf.Sin(2f * Mathf.PI * frequency * time) * 0.12f * envelope;
        }

        fallbackClip = AudioClip.Create("PrismaTone", totalSamples, 1, sampleRate, false);
        fallbackClip.SetData(samples, 0);
        return fallbackClip;
    }

    private void ResolverChaoRenderer()
    {
        if (chaoRenderer != null)
        {
            return;
        }

        GameObject chaoObject = GameObject.Find("Floor");
        if (chaoObject != null)
        {
            chaoRenderer = chaoObject.GetComponent<Renderer>();
        }
    }

    private Color ObterCorChaoSuave(Color corBase)
    {
        Color corEscura = new Color(0.12f, 0.14f, 0.18f, 1f);
        Color corSuave = Color.Lerp(corBase, corEscura, 0.65f);
        corSuave.a = 1f;
        return corSuave;
    }
}
