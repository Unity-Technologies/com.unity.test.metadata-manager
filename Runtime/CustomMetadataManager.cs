using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using com.unity.test.performance.runtimesettings;
using NUnit.Framework;
using UnityEngine;

namespace com.unity.test.metadatamanager
{
    public class CustomMetadataManager : ICustomMetadataManager
    {
        private readonly StringBuilder metadata = new StringBuilder();
        private readonly List<string> dependencies;
        public static string MetadataFileName = "metadata.txt";

        public CustomMetadataManager(List<string> dependencies)
        {
            this.dependencies = dependencies;
        }

        public string GetCustomMetadata()
        {
            var settings = Resources.Load<CurrentSettings>("settings");

            var customMetaData = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("projectname", settings.ProjectName),
                new KeyValuePair<string, string>("username", settings.Username),
                new KeyValuePair<string, string>("burstenabled", settings.EnableBurst.ToString()),
                new KeyValuePair<string, string>("packageundertestname", settings.PackageUnderTestName),
                new KeyValuePair<string, string>("PackageUnderTestVersion", settings.PackageUnderTestVersion),
                new KeyValuePair<string, string>("PackageUnderTestRevision", settings.PackageUnderTestRevision),
                new KeyValuePair<string, string>("PackageUnderTestRevisionDate", settings.PackageUnderTestRevisionDate),
                new KeyValuePair<string, string>("PackageUnderTestBranch", settings.PackageUnderTestBranch),
                new KeyValuePair<string, string>("renderpipeline", settings.RenderPipeline),
                new KeyValuePair<string, string>("testsbranch", settings.TestsBranch),
                new KeyValuePair<string, string>("testsrev", settings.TestsRevision),
                new KeyValuePair<string, string>("testsrevdate", settings.TestsRevisionDate),
                new KeyValuePair<string, string>("dependencies", string.Join(",", dependencies)),
                new KeyValuePair<string, string>("MtRendering", string.Join(",", settings.MtRendering.ToString())),
                new KeyValuePair<string, string>("GraphicsJobs", string.Join(",", settings.GraphicsJobs.ToString())),
                new KeyValuePair<string, string>("joblink", string.Join(",", settings.JobLink)),
                new KeyValuePair<string, string>("jobworkercount", string.Join(",", settings.JobWorkerCount)),
                new KeyValuePair<string, string>("apicompatibilitylevel", string.Join(",", settings.ApiCompatibilityLevel)),
                new KeyValuePair<string, string>("stripenginecode", string.Join(",", settings.StripEngineCode)),
                new KeyValuePair<string, string>("managedstrippinglevel", string.Join(",", settings.ManagedStrippingLevel)),
                new KeyValuePair<string, string>("scriptdebugging", string.Join(",", settings.ScriptDebugging)),
                new KeyValuePair<string, string>("testprojectname", settings.TestProjectName),
                new KeyValuePair<string, string>("testprojectrevision", settings.TestProjectRevision),
                new KeyValuePair<string, string>("testprojectrevdate", settings.TestProjectRevisionDate),
                new KeyValuePair<string, string>("testprojectbranch", settings.TestProjectBranch),
                new KeyValuePair<string, string>("enabledxrtarget", settings.EnabledXrTarget),
                new KeyValuePair<string, string>("stereorenderingmode", settings.StereoRenderingMode),
                new KeyValuePair<string, string>("StereoRenderingModeDesktop", settings.StereoRenderingModeDesktop),
                new KeyValuePair<string, string>("StereoRenderingModeAndroid", settings.StereoRenderingModeAndroid),
                new KeyValuePair<string, string>("SimulationMode", settings.SimulationMode),
                new KeyValuePair<string, string>("PluginVersion", settings.PluginVersion),
                new KeyValuePair<string, string>("DeviceRuntimeVersion", settings.DeviceRuntimeVersion),
                new KeyValuePair<string, string>("FfrLevel", settings.FfrLevel),
                new KeyValuePair<string, string>("androidtargetarchitecture", settings.AndroidTargetArchitecture),
            };

            UpdateMetadataWithMatchesInTestContext(customMetaData);

            AppendMetadata(customMetaData);

            ReadAndAppendCustomMetadataFromFile();

            return metadata.Remove(0, 1).ToString();
        }

        private static void UpdateMetadataWithMatchesInTestContext(List<KeyValuePair<string, string>> customMetaData)
        {
            var tempMetaDataList = new List<KeyValuePair<string, string>>();
            tempMetaDataList.AddRange(customMetaData);

            foreach (var metadata in tempMetaDataList)
            {
                if (TestContext.CurrentContext.Test.Properties.Keys.Any(k => k.Contains(metadata.Key)))
                {
                    var metadataFromPropertyBag = (string)TestContext.CurrentContext.Test.Properties.Get(metadata.Key);
                    customMetaData.Remove(metadata);
                    customMetaData.Add(new KeyValuePair<string, string>(metadata.Key, metadataFromPropertyBag));
                }
            }
        }

        private void ReadAndAppendCustomMetadataFromFile()
        {
            var tempFilePath = Path.Combine(Directory.GetCurrentDirectory(), MetadataFileName);
            if (File.Exists(tempFilePath))
            {
                using (StreamReader sr = new StreamReader(tempFilePath))
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