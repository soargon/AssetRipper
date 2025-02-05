using AssetRipper.Converters.Project;
using AssetRipper.Parser.Classes.AnimatorController.Constants;
using AssetRipper.IO.Asset;
using AssetRipper.YAML;
using System.Collections.Generic;

namespace AssetRipper.Parser.Classes.AnimatorTransition
{
	public sealed class AnimatorCondition : IYAMLExportable
	{
		public AnimatorCondition(ConditionConstant condition, IReadOnlyDictionary<uint, string> tos)
		{
			ConditionMode = condition.ConditionMode;
			ConditionEvent = tos[condition.EventID];
			EventTreshold = condition.EventThreshold;
		}

		public YAMLNode ExportYAML(IExportContainer container)
		{
			YAMLMappingNode node = new YAMLMappingNode();
			node.Add(ConditionModeName, (int)ConditionMode);
			node.Add(ConditionEventName, ConditionEvent);
			node.Add(EventTresholdName, EventTreshold);
			return node;
		}

		public AnimatorConditionMode ConditionMode { get; set; }
		public string ConditionEvent { get; set; }
		public float EventTreshold { get; set; }

		public const string ConditionModeName = "m_ConditionMode";
		public const string ConditionEventName = "m_ConditionEvent";
		public const string EventTresholdName = "m_EventTreshold";
	}
}
