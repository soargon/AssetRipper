﻿using AssetRipper.Converters.Project;
using AssetRipper.IO.Asset;
using AssetRipper.YAML;

namespace AssetRipper.Parser.Classes.EditorSettings
{
	public struct CollabEditorSettings : IAssetReadable, IYAMLExportable
	{
		public CollabEditorSettings(bool _)
		{
			InProgressEnabled = true;
		}

		public void Read(AssetReader reader)
		{
			InProgressEnabled = reader.ReadBoolean();
			reader.AlignStream();
		}

		public YAMLNode ExportYAML(IExportContainer container)
		{
			YAMLMappingNode node = new YAMLMappingNode();
			node.Add(InProgressEnabledName, InProgressEnabled);
			return node;
		}

		public bool InProgressEnabled { get; set; }

		public const string InProgressEnabledName = "inProgressEnabled";
	}
}
