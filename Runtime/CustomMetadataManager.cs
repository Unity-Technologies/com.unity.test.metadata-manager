using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
#if NUNIT_AVAILABLE
using NUnit.Framework;
#endif
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace com.unity.test.metadatamanager
{
    public class CustomMetadataManager
    {
        public static string MetadataFileName = "metadata.txt";

        private readonly StringBuilder metadata = new StringBuilder();
        private readonly List<string> dependencies;
        private static CurrentSettings instance;
#if UNITY_EDITOR
        private static readonly string ResourceDir = "Assets/Resources";
        private static readonly string SettingsAssetName = "/settings.asset";
#endif
        // A static singleton is use here so that we can update metadata values 
        // after test execution has begun
        public static CurrentSettings Instance
        {
            get
            {
                if (instance == null)
                {
                    EnsureSettingsAsset();
                }

                return instance;
            }
            set => instance = value;
        }

#if UNITY_EDITOR
        public static void SaveSettingsAssetInEditor()
        {
            EditorUtility.SetDirty(Instance);
            AssetDatabase.SaveAssets();
        }
#endif
        public static void EnsureSettingsAsset()
        {
            var settings = Resources.Load<CurrentSettings>("settings");

            if (settings == null)
            {
#if UNITY_EDITOR
                settings = ScriptableObject.CreateInstance<CurrentSettings>();

                if (!Directory.Exists(ResourceDir))
                {
                    Directory.CreateDirectory(ResourceDir);
                }

                AssetDatabase.CreateAsset(settings, ResourceDir + SettingsAssetName);
                AssetDatabase.SaveAssets();
#endif
            }

            Instance = settings;
        }

        public CustomMetadataManager(List<string> dependencies)
        {
            this.dependencies = dependencies;
        }

        public string GetCustomMetadata()
        {
            var metaDatastring = string.Empty;

            if (Instance != null)
            {
                var customMetaData = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("projectname", Instance.ProjectName),
                    new KeyValuePair<string, string>("username", Instance.Username),
                    new KeyValuePair<string, string>("burstenabled", Instance.EnableBurst.ToString()),
                    new KeyValuePair<string, string>("packageundertestname", Instance.PackageUnderTestName),
                    new KeyValuePair<string, string>("PackageUnderTestVersion", Instance.PackageUnderTestVersion),
                    new KeyValuePair<string, string>("PackageUnderTestRevision", Instance.PackageUnderTestRevision),
                    new KeyValuePair<string, string>("PackageUnderTestRevisionDate",
                        Instance.PackageUnderTestRevisionDate),
                    new KeyValuePair<string, string>("PackageUnderTestBranch", Instance.PackageUnderTestBranch),
                    new KeyValuePair<string, string>("renderpipeline", Instance.RenderPipeline),
                    new KeyValuePair<string, string>("testsbranch", Instance.TestsBranch),
                    new KeyValuePair<string, string>("testsrev", Instance.TestsRevision),
                    new KeyValuePair<string, string>("testsrevdate", Instance.TestsRevisionDate),
                    new KeyValuePair<string, string>("dependencies", string.Join(",", dependencies)),
                    new KeyValuePair<string, string>("MtRendering", string.Join(",", Instance.MtRendering.ToString())),
                    new KeyValuePair<string, string>("GraphicsJobs",
                        string.Join(",", Instance.GraphicsJobs.ToString())),
                    new KeyValuePair<string, string>("PlayerGraphicsApi",
                        string.Join(",", Instance.PlayerGraphicsApi)),
                    new KeyValuePair<string, string>("joblink", string.Join(",", Instance.JobLink)),
                    new KeyValuePair<string, string>("jobworkercount", string.Join(",", Instance.JobWorkerCount)),
                    new KeyValuePair<string, string>("apicompatibilitylevel",
                        string.Join(",", Instance.ApiCompatibilityLevel)),
                    new KeyValuePair<string, string>("stripenginecode", string.Join(",", Instance.StripEngineCode)),
                    new KeyValuePair<string, string>("managedstrippinglevel",
                        string.Join(",", Instance.ManagedStrippingLevel)),
                    new KeyValuePair<string, string>("scriptdebugging", string.Join(",", Instance.ScriptDebugging)),
                    new KeyValuePair<string, string>("testprojectname", Instance.TestProjectName),
                    new KeyValuePair<string, string>("testprojectrevision", Instance.TestProjectRevision),
                    new KeyValuePair<string, string>("testprojectrevdate", Instance.TestProjectRevisionDate),
                    new KeyValuePair<string, string>("testprojectbranch", Instance.TestProjectBranch),
                    new KeyValuePair<string, string>("enabledxrtarget", Instance.EnabledXrTarget),
                    new KeyValuePair<string, string>("stereorenderingmode", Instance.StereoRenderingMode),
                    new KeyValuePair<string, string>("StereoRenderingModeDesktop", Instance.StereoRenderingModeDesktop),
                    new KeyValuePair<string, string>("StereoRenderingModeAndroid", Instance.StereoRenderingModeAndroid),
                    new KeyValuePair<string, string>("SimulationMode", Instance.SimulationMode),
                    new KeyValuePair<string, string>("PluginVersion", Instance.PluginVersion),
                    new KeyValuePair<string, string>("DeviceRuntimeVersion", Instance.DeviceRuntimeVersion),
                    new KeyValuePair<string, string>("FfrLevel", Instance.FfrLevel),
                    new KeyValuePair<string, string>("androidtargetarchitecture", Instance.AndroidTargetArchitecture),

                    new KeyValuePair<string, string>("QualityLevel", Instance.QualityLevel),
                    new KeyValuePair<string, string>("LodBias",
                        Instance.LodBias.ToString(CultureInfo.InvariantCulture)),
                    new KeyValuePair<string, string>("MaximumLodLevel", Instance.MaximumLodLevel.ToString()),
                    new KeyValuePair<string, string>("TextureQuality", Instance.TextureQuality.ToString()),

                    new KeyValuePair<string, string>("UrpPaMsaa", Instance.UrpPaMsaa),
                    new KeyValuePair<string, string>("UrpPaRendererList", Instance.UrpPaRendererList),
                    new KeyValuePair<string, string>("UrpPaDefaultRenderer", Instance.UrpPaDefaultRenderer),
                    new KeyValuePair<string, string>("UrpPaRequireDepthTexture",
                        Instance.UrpPaRequireDepthTexture.ToString()),
                    new KeyValuePair<string, string>("UrpPaRequireOpaqueTexture",
                        Instance.UrpPaRequireOpaqueTexture.ToString()),
                    new KeyValuePair<string, string>("UrpPaOpaqueDownsampling", Instance.UrpPaOpaqueDownsampling),
                    new KeyValuePair<string, string>("UrpPaSupportsTerrainHoles",
                        Instance.UrpPaSupportsTerrainHoles.ToString()),
                    new KeyValuePair<string, string>("UrpPaSupportsHDR", Instance.UrpPaSupportsHDR.ToString()),
                    new KeyValuePair<string, string>("UrpPaRenderScale", Instance.UrpPaRenderScale),
                    new KeyValuePair<string, string>("UrpPaMainLightRenderingMode",
                        Instance.UrpPaMainLightRenderingMode),
                    new KeyValuePair<string, string>("UrpPaSupportsMainLightShadows",
                        Instance.UrpPaSupportsMainLightShadows.ToString()),
                    new KeyValuePair<string, string>("UrpPaMainLightShadowmapResolution",
                        Instance.UrpPaMainLightShadowmapResolution.ToString()),
                    new KeyValuePair<string, string>("UrpPaAdditionalLightsRenderingMode",
                        Instance.UrpPaAdditionalLightsRenderingMode),
                    new KeyValuePair<string, string>("UrpPaMaxAdditionalLightsCount",
                        Instance.UrpPaMaxAdditionalLightsCount.ToString()),
                    new KeyValuePair<string, string>("UrpPaSupportsAdditionalLightShadows",
                        Instance.UrpPaSupportsAdditionalLightShadows.ToString()),
                    new KeyValuePair<string, string>("UrpPaAdditionalLightsShadowmapResolution",
                        Instance.UrpPaAdditionalLightsShadowmapResolution.ToString()),
                    new KeyValuePair<string, string>("UrpPaShadowDistance",
                        Instance.UrpPaShadowDistance.ToString(CultureInfo.InvariantCulture)),
                    new KeyValuePair<string, string>("UrpPaShadowCascadeCount",
                        Instance.UrpPaShadowCascadeCount.ToString()),
                    new KeyValuePair<string, string>("UrpPaShadowDepthBias",
                        Instance.UrpPaShadowDepthBias.ToString(CultureInfo.InvariantCulture)),
                    new KeyValuePair<string, string>("UrpPaSupportsSoftShadows",
                        Instance.UrpPaSupportsSoftShadows.ToString()),
                    new KeyValuePair<string, string>("UrpPaColorGradingMode", Instance.UrpPaColorGradingMode),
                    new KeyValuePair<string, string>("UrpPaColorGradingLutSize",
                        Instance.UrpPaColorGradingLutSize.ToString()),
                    new KeyValuePair<string, string>("UrpPaUseSRPBatcher", Instance.UrpPaUseSRPBatcher.ToString()),
                    new KeyValuePair<string, string>("UrpPaSupportsDynamicBatching",
                        Instance.UrpPaSupportsDynamicBatching.ToString()),
                    new KeyValuePair<string, string>("UrpPaSupportsMixedLighting",
                        Instance.UrpPaSupportsMixedLighting.ToString()),
                    new KeyValuePair<string, string>("UrpPaShaderVariantLogLevel", Instance.UrpPaShaderVariantLogLevel),
                    new KeyValuePair<string, string>("UrpExtAntialiasingQuality", Instance.UrpExtAntialiasingQuality),
                    new KeyValuePair<string, string>("CameraAntialiasing", Instance.CameraAntialiasing),
                    new KeyValuePair<string, string>("UrpPaCascade2Split",
                        Instance.UrpPaCascade2Split.ToString(CultureInfo.InvariantCulture)),
                    new KeyValuePair<string, string>("UrpPaCascade3Split", Instance.UrpPaCascade3Split),
                    new KeyValuePair<string, string>("UrpPaCascade4Split", Instance.UrpPaCascade4Split),
                    new KeyValuePair<string, string>("UrpShadowDepthBias",
                        Instance.UrpShadowDepthBias.ToString(CultureInfo.InvariantCulture)),
                    new KeyValuePair<string, string>("UrpShadowNormalBias",
                        Instance.UrpShadowNormalBias.ToString(CultureInfo.InvariantCulture)),
                    new KeyValuePair<string, string>("XrInputActionsFile", Instance.XrInputActionsFile),
                    new KeyValuePair<string, string>("XrInputDeviceInfoFile", Instance.XrInputDeviceInfoFile),
                    new KeyValuePair<string, string>("OpenXRFeatures", Instance.OpenXRFeatures),
                    new KeyValuePair<string, string>("RunDeviceAlias", Instance.RunDeviceAlias)
                };

                UpdateMetadataWithMatchesInTestContext(customMetaData);

                AppendMetadata(customMetaData);

                ReadAndAppendCustomMetadataFromFile();

                metaDatastring = metadata.Remove(0, 1).ToString();
            }

            return metaDatastring;
        }

        private static void UpdateMetadataWithMatchesInTestContext(List<KeyValuePair<string, string>> customMetaData)
        {
            var tempMetaDataList = new List<KeyValuePair<string, string>>();
            tempMetaDataList.AddRange(customMetaData);

            foreach (var metadata in tempMetaDataList)
            {
#if NUNIT_AVAILABLE
                if (TestContext.CurrentContext.Test.Properties.Keys.Any(k => k.Contains(metadata.Key)))
                {
                    var metadataFromPropertyBag = (string) TestContext.CurrentContext.Test.Properties.Get(metadata.Key);
                    customMetaData.Remove(metadata);
                    customMetaData.Add(new KeyValuePair<string, string>(metadata.Key, metadataFromPropertyBag));
                }
#endif
            }
        }

        private void ReadAndAppendCustomMetadataFromFile()
        {
            var tempFilePath = Path.Combine(Directory.GetCurrentDirectory(), MetadataFileName);
            if (File.Exists(tempFilePath))
            {
                using (var sr = new StreamReader(tempFilePath))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        metadata.Append(line);
                    }
                }
            }
        }

        private void AppendMetadata(IEnumerable<KeyValuePair<string, string>> keyValuePairs)
        {
            foreach (var keyValuePair in keyValuePairs)
            {
                metadata.Append(string.Format("|{0}={1}", keyValuePair.Key, keyValuePair.Value));
            }
        }
    }
}