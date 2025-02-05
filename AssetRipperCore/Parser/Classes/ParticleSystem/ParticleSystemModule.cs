﻿using AssetRipper.Converters.Project;
using AssetRipper.IO.Asset;
using AssetRipper.YAML;

namespace AssetRipper.Parser.Classes.ParticleSystem
{
	public abstract class ParticleSystemModule : IAssetReadable, IYAMLExportable
	{
		protected ParticleSystemModule() { }

		protected ParticleSystemModule(bool enabled)
		{
			Enabled = enabled;
		}

		public virtual void Read(AssetReader reader)
		{
			Enabled = reader.ReadBoolean();
			reader.AlignStream();
		}

		public virtual YAMLNode ExportYAML(IExportContainer container)
		{
			YAMLMappingNode node = new YAMLMappingNode();
			node.Add(EnabledName, Enabled);
			return node;
		}

		public bool Enabled { get; protected set; }

		public const string EnabledName = "enabled";
	}
}
