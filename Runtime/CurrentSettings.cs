using UnityEngine;

public class CurrentSettings : ScriptableObject
{
    public string ProjectName;
    public string PlayerGraphicsApi;
    public bool MtRendering;
    public bool GraphicsJobs;
    public bool EnableBurst;
    public string ColorSpace;
    public string Username;
    public string RenderPipeline;
    public int AntiAliasing;
    public string TestsRevision;
    public string TestsRevisionDate;
    public string TestsBranch;
    public string PackageUnderTestName;
    public string PackageUnderTestVersion;
    public string PackageUnderTestRevision;
    public string PackageUnderTestRevisionDate;
    public string PackageUnderTestBranch;
    public string ScriptingBackend;
    public string JobLink;
    public int JobWorkerCount;
    public string ApiCompatibilityLevel;
    public bool StripEngineCode;
    public string ManagedStrippingLevel;
    public bool ScriptDebugging;
    public string TestProjectName;
    public string TestProjectRevision;
    public string TestProjectRevisionDate;
    public string TestProjectBranch;
    public string EnabledXrTarget;
    public string StereoRenderingMode;
    public string StereoRenderingModeDesktop;
    public string StereoRenderingModeAndroid;
    public string SimulationMode;
    public string PluginVersion;
    public string DeviceRuntimeVersion;
    public string FfrLevel;
    public string AndroidTargetArchitecture;
    public string QualityLevel;
    public float LodBias;
    public int MaximumLodLevel;
    public int TextureQuality;
    // URP Pipeline Asset metadata
    public string UrpPaMsaa;
    public string UrpPaRendererList;
    public string UrpPaDefaultRenderer;
    public bool UrpPaRequireDepthTexture;
    public bool UrpPaRequireOpaqueTexture;
    public string UrpPaOpaqueDownsampling;
    public bool UrpPaSupportsTerrainHoles;
    public bool UrpPaSupportsHDR;
    public string UrpPaRenderScale;
    public string UrpPaMainLightRenderingMode;
    public bool UrpPaSupportsMainLightShadows;
    public int UrpPaMainLightShadowmapResolution;
    public string UrpPaAdditionalLightsRenderingMode;
    public int UrpPaMaxAdditionalLightsCount;
    public bool UrpPaSupportsAdditionalLightShadows;
    public int UrpPaAdditionalLightsShadowmapResolution;
    public float UrpPaShadowDistance;
    public int UrpPaShadowCascadeCount;
    public float UrpPaShadowDepthBias;
    public bool UrpPaSupportsSoftShadows;
    public string UrpPaColorGradingMode;
    public int UrpPaColorGradingLutSize;
    public bool UrpPaUseSRPBatcher;
    public bool UrpPaSupportsDynamicBatching;
    public bool UrpPaSupportsMixedLighting;
    public string UrpPaShaderVariantLogLevel;
    public string UrpExtAntialiasingQuality;
    public string CameraAntialiasing;
    public float UrpPaCascade2Split;
    public string UrpPaCascade3Split;
    public string UrpPaCascade4Split;
    public float UrpShadowDepthBias;
    public float UrpShadowNormalBias;
}