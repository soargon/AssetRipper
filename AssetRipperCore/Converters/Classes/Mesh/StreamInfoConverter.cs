﻿using AssetRipper.Converters.Project;
using AssetRipper.Parser.Classes.Mesh;
using AssetRipper.Parser.Classes.Shader.Enums.ShaderChannel;
using AssetRipper.Parser.Classes.Shader.Enums.VertexFormat;
using AssetRipper.Parser.Classes.Utils.Extensions;
using AssetRipper.Parser.Files;

namespace AssetRipper.Converters.Classes.Mesh
{
	public static class StreamInfoConverter
	{
		public static ChannelInfo GenerateChannelInfo(IExportContainer container, StreamInfo[] origin, ShaderChannel channelType)
		{
			return GenerateChannelInfo(container.ExportVersion, origin, channelType);
		}

		public static ChannelInfo GenerateChannelInfo(Version instanceVersion, StreamInfo[] origin, ShaderChannel channelType)
		{
			ChannelInfo instance = new ChannelInfo();
			ShaderChannel4 channelv4 = channelType.ToShaderChannel4();
			int streamIndex = origin.IndexOf(t => t.IsMatch(channelv4));
			if (streamIndex >= 0)
			{
				byte offset = 0;
				ref StreamInfo stream = ref origin[streamIndex];
				for (ShaderChannel4 i = 0; i < channelv4; i++)
				{
					if (stream.IsMatch(i))
					{
						offset += i.ToShaderChannel().GetStride(instanceVersion);
					}
				}

				instance.Stream = (byte)streamIndex;
				instance.Offset = offset;
				instance.Format = channelType.GetVertexFormat(instanceVersion).ToFormat(instanceVersion);
				instance.RawDimension = channelType.GetDimention(instanceVersion);
			}
			return instance;
		}
	}
}
