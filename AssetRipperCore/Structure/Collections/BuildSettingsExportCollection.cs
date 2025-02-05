﻿using AssetRipper.Converters.Project;
using AssetRipper.Converters.Project.Exporters;
using AssetRipper.IO;
using AssetRipper.Parser.Asset;
using AssetRipper.Parser.Classes;
using AssetRipper.Parser.Classes.EditorBuildSettings;
using AssetRipper.Parser.Classes.EditorSettings;
using AssetRipper.Parser.Classes.NavMeshProjectSettings;
using AssetRipper.Parser.Classes.Object;
using AssetRipper.Parser.Classes.Physics2DSettings;
using AssetRipper.Parser.Classes.QualitySettings;
using AssetRipper.Parser.Classes.UnityConnectSettings;
using AssetRipper.Parser.Files.SerializedFiles;
using AssetRipper.Utils;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AssetRipper.Structure.Collections
{
	public sealed class BuildSettingsExportCollection : ManagerExportCollection
	{
		public BuildSettingsExportCollection(IAssetExporter assetExporter, VirtualSerializedFile file, Object asset) : this(assetExporter, file, (BuildSettings)asset) { }

		public BuildSettingsExportCollection(IAssetExporter assetExporter, VirtualSerializedFile virtualFile, BuildSettings asset) : base(assetExporter, asset)
		{
			EditorBuildSettings = EditorBuildSettings.CreateVirtualInstance(virtualFile);
			EditorSettings = EditorSettings.CreateVirtualInstance(virtualFile);
			if (!NavMeshProjectSettings.HasNavMeshProjectSettings(asset.File.Version))
			{
				NavMeshProjectSettings = NavMeshProjectSettings.CreateVirtualInstance(virtualFile);
			}
			if (!NetworkManager.HasNetworkManager(asset.File.Version))
			{
				NetworkManager = NetworkManager.CreateVirtualInstance(virtualFile);
			}
			if (!Physics2DSettings.HasPhysics2DSettings(asset.File.Version))
			{
				Physics2DSettings = Physics2DSettings.CreateVirtualInstance(virtualFile);
			}
			if (!UnityConnectSettings.HasUnityConnectSettings(asset.File.Version))
			{
				UnityConnectSettings = UnityConnectSettings.CreateVirtualInstance(virtualFile);
			}
			if (!QualitySettings.HasQualitySettings(asset.File.Version))
			{
				QualitySettings = QualitySettings.CreateVirtualInstance(virtualFile);
			}
		}

		public override bool Export(ProjectAssetContainer container, string dirPath)
		{
			string subPath = Path.Combine(dirPath, ProjectSettingsName);
			string fileName = $"{EditorBuildSettings.ClassID.ToString()}.asset";
			string filePath = Path.Combine(subPath, fileName);

			if (!DirectoryUtils.Exists(subPath))
			{
				DirectoryUtils.CreateVirtualDirectory(subPath);
			}

			BuildSettings asset = (BuildSettings)Asset;
			IEnumerable<Scene> scenes = asset.Scenes.Select(t => new Scene(t, container.SceneNameToGUID(t)));
			EditorBuildSettings.Initialize(scenes);
			AssetExporter.Export(container, EditorBuildSettings, filePath);

			fileName = $"{EditorSettings.ClassID.ToString()}.asset";
			filePath = Path.Combine(subPath, fileName);

			AssetExporter.Export(container, EditorSettings, filePath);

			if (NavMeshProjectSettings != null)
			{
				fileName = $"{NavMeshProjectSettings.ExportPath}.asset";
				filePath = Path.Combine(subPath, fileName);

				AssetExporter.Export(container, NavMeshProjectSettings, filePath);
			}
			if (NetworkManager != null)
			{
				fileName = $"{NetworkManager.ExportPath}.asset";
				filePath = Path.Combine(subPath, fileName);

				AssetExporter.Export(container, NetworkManager, filePath);
			}
			if (Physics2DSettings != null)
			{
				fileName = $"{Physics2DSettings.ExportPath}.asset";
				filePath = Path.Combine(subPath, fileName);

				AssetExporter.Export(container, Physics2DSettings, filePath);
			}
			if (UnityConnectSettings != null)
			{
				fileName = $"{UnityConnectSettings.ExportPath}.asset";
				filePath = Path.Combine(subPath, fileName);

				AssetExporter.Export(container, UnityConnectSettings, filePath);
			}
			if (QualitySettings != null)
			{
				fileName = $"{QualitySettings.ExportPath}.asset";
				filePath = Path.Combine(subPath, fileName);

				AssetExporter.Export(container, QualitySettings, filePath);
			}

			fileName = $"ProjectVersion.txt";
			filePath = Path.Combine(subPath, fileName);

			using (Stream fileStream = FileUtils.CreateVirtualFile(filePath))
			{
				using (StreamWriter writer = new InvariantStreamWriter(fileStream, new UTF8Encoding(false)))
				{
					writer.Write("m_EditorVersion: 2017.3.0f3");
				}
			}
			return true;
		}

		public override bool IsContains(Object asset)
		{
			switch (asset.ClassID)
			{
				case ClassIDType.EditorBuildSettings:
					return asset == EditorBuildSettings;
				case ClassIDType.EditorSettings:
					return asset == EditorSettings;
				case ClassIDType.NavMeshProjectSettings:
					return asset == NavMeshProjectSettings;
				case ClassIDType.NetworkManager:
					return asset == Physics2DSettings;
				case ClassIDType.Physics2DSettings:
					return asset == EditorBuildSettings;
				case ClassIDType.UnityConnectSettings:
					return asset == UnityConnectSettings;
				case ClassIDType.QualitySettings:
					return asset == QualitySettings;

				default:
					return base.IsContains(asset);
			}
		}

		public override long GetExportID(Object asset)
		{
			return 1;
		}

		public override IEnumerable<Object> Assets
		{
			get
			{
				yield return Asset;
				yield return EditorBuildSettings;
				yield return EditorSettings;
				if (NavMeshProjectSettings != null)
				{
					yield return NavMeshProjectSettings;
				}
				if (NetworkManager != null)
				{
					yield return NetworkManager;
				}
				if (Physics2DSettings != null)
				{
					yield return Physics2DSettings;
				}
				if (UnityConnectSettings != null)
				{
					yield return UnityConnectSettings;
				}
				if (QualitySettings != null)
				{
					yield return QualitySettings;
				}
			}
		}

		public EditorBuildSettings EditorBuildSettings { get; }
		public EditorSettings EditorSettings { get; }
		public NavMeshProjectSettings NavMeshProjectSettings { get; }
		public NetworkManager NetworkManager { get; }
		public Physics2DSettings Physics2DSettings { get; }
		public UnityConnectSettings UnityConnectSettings { get; }
		public QualitySettings QualitySettings { get; }
	}
}
